using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Diagnostics;
namespace SelectionsAlgorithms
{


    /// <summary>
    /// Třída poskytuje služby související nutné pro přidělování případů. Všechny algoritmy přidělování jsou jejími potomky.
    /// </summary>
    /// <remarks>
    /// Konkrétně třída poskytuje svým potomkům generování náhodného čísla v intervalu (0,1) za pomoci bezpečného generátoru náhodných čísel, viz metoda <see cref="NextSecureDouble()"/>.
    /// Dále poskytuje náhodný výběr prvku z pole, kde každý prvek v poli má danou pravděpodobnost, což tedy odpovídá výběru jednotlivých senátů na základě pravděpodobnosti jejich výběru.
    /// Konkrétní nastavení pravděpodobností pro výběr jednotlivých prvků závisí na daném algoritmu, tj. toto je realizováno na úrovni potomků, kteří  musí implementovat metody pro výpočet intervalů pravděpodobností a pro provádění simulaci.
    /// </remarks>
    /// <remarks>
    ///Protože třída lsouží jako výpočetní jádro, nepředpokládá se kontrola validity jakýkoliv údajů, mimo správné délky použitých polí. Případné kontroly je nutné realizovat na úrovni API/fasády k této třídě.  U všech vstupních polí se předpokládá jejich délka odpovídající počtu povolených senátů. 
    /// </remarks>
    /// <remarks>Generátor bezpečných náhodných čísel byl otestován na splnění základních statistických veličin.</remarks>
    public abstract class SelectionAlgorithm
    {
        /// <summary>
        /// Pole pro uložení intervalů pravděpodobnosti jednotlivých zúčastněných senátů.
        /// Předpokládají se intervaly (0,p0> pro první senát, (p0,p1> pro druhý senát atd.
        /// </summary>
        protected double[] parray;//pole pravdepodobnosti

       
        /// <summary>
        /// Vlastnost s poslední generovanou náhodnou hodnotou.
        /// </summary>
        /// <remarks>Pouze pro čtení.</remarks>
        public double LastGeneratedValue { get; private set; }//je zde ulozena posledni generovana hodnota


        /// <summary>
        /// Metoda vrací kopii pole pravděpodobnosti.
        /// </summary>
        /// <param name="length">Délka-počet prvků pole ke skopírování</param>
        /// <returns>Kopie pole pravděpodobnosti</returns>
        /// <remarks>
        /// Délka požadované kopie nesmí být větší než délka pole. Typické použití předpokládá vstupní délku pole odpovídající počtu povolených senátů k výběru.
        /// </remarks>
        public double[] GetProbabilityArray(int length)
        {
            double[] copy_parray = new double[length];//alokuj-pro vytvoreni kopie vstupniho pole
            Array.Copy(parray, copy_parray, length);//vytvor kopii vstupnich dat
            return copy_parray;
        }

        /// <summary>
        /// Provádí inicializaci algoritmu pro losování. Je provedena alokace pole pravděpodobností. Je nezbytné, aby před použitím alogritmu losování byla tato metoda volána.
        /// </summary>
        /// <remarks>V případě, že pole je již alokováno a má dostatečnou velikost, tak se neděje nic.</remarks>
        /// <param name="senatescount">Počet senátů, kterým budou případy rozdělovány.</param>
        public virtual void InitAlgorithm(int senatescount)
        {
            if (parray == null || parray.Length < senatescount)//kdyz neni alokovano, nebo ma malou velikost
                parray = new double[senatescount]; //alokuj pole pro pravdepodobnosti
        }

        /// <summary>
        /// Výběr senátu na základě pravděpodobnosti jeho výběru. Metoda pro správnou funkčnost požaduje inicializaci pravděpodobností výběru pomocí metody <see cref=" ComputeProbabilities"/>.
        /// </summary>
        /// <returns>Vrací index v poli, který odpovídá pořadí senátu.</returns>
        /// <remarks>Pokud dojde k výjimce v této metodě, musí být špatně inicializována použitá pole. Nemělo by v žádném případě nastávat. Typická chyba je špatně alokované pole s pravděpodobnostmi, nebo špatně spočtené parvděpodobnosti.</remarks>
        protected int SelectSenateIndex()
        {
            double p = NextSecureDouble();//generuj nahodne cislo v intervalu <0,1)
            int index = 0;
            while (p > parray[index])//pokud vyjede z pole, muselo byt neco spatne inicializovano-vyjimka je odchytavana o uroven vyse pri pouziti teto tridy
            {
                index++;
            }
            return index;//vrat index
        }

        /// <summary>
        /// Metoda spočte pravděpodobnosti výběru jednotlivých senátů a vrací index (dle pořadí na vstupu) vybraného senátu.
        /// </summary>
        /// <param name="loads">Pole zatížení jednotlivých povolených senátů.</param>
        /// <param name="activecases">Pole s počty případů jednotlivých povolených senátů.</param>
        /// <returns>Vrací index odpovídající pořadí vybraného senátu.</returns>
        /// <remarks>
        /// Tato metoda slouží k předělování senátů. Sama si spočte pravděpodobnosti přidělení. Nedochází k modifikaci počtu přidělených případů.
        /// </remarks>
        /// <remarks>
        /// Předpokládá se užití této metody jako nejvnitřnější API pro generování přidělení. Veškerá pole na vstupu musí délkou odpovídat  pouze povoleným senátům.
        /// </remarks>
        /// <remarks>Pokud dojde k výjimce v této metodě, musí být špatně inicializována použitá pole. Nemělo by v žádném případě nastávat. Typická chyba je špatně alokované pole s pravděpodobnostmi, nebo špatně spočtené parvděpodobnosti.</remarks>
        public int SelectSenate(double [] loads, int [] activecases)
        {
            ComputeProbabilities(loads,activecases);//spocti pravdepodobnosti
            return SelectSenateIndex();//vyber senat na zaklade spoctenych pravdepodobnosti
        }

        /// <summary>
        /// Metoda pro výpočet praděpodobností výběru senátů. Při vlastním výběru je pak uvažováno stejné pořadí senátů jako na vstupu této metody.
        /// </summary>
        /// <param name="loads">Pole zatížení jednotlivých povolených senátů.</param>
        /// <param name="activecases">Pole s počty případů jednotlivých povolených senátů.</param>
        /// <remarks>
        /// Konkrétní výpočet pravděpodobností si provádí každá implementace losovacího algoritmu vlastní. Předpokládá se, že k tomu budou použity informace o zatížení senátů a o počtech případů, které mají k řešení.
        /// </remarks>
        protected abstract void ComputeProbabilities(double[] loads, int[] activecases);

        /// <summary>
        /// Metoda provádí simulaci rozdělování daného počtu případů zvoleným senátům.
        /// Dochází ke spočtení intervalu pravděpodobností a pak k přidělení daného počtu případů mezi senáty.
        /// </summary>
        /// <param name="loads">Pole, kde je uvedeno zatížení jednotlivých povolených senátů.</param>
        /// <param name="activecases">Pole, kde je uveden počet aktivních případů jednotlivých  povolených senátů.</param>
        /// <param name="casecount">Počet případů k rozdělení</param>
        /// <param name="result">Pole s četnostmi přidělených případů jednotlivým  povoleným senátům. Délka pole odpovídá počtu povolených senátů. </param>
        /// <returns>Diference</returns>
        /// Metoda zapouzdřuje opakované přidělování senátů. Při implementaci je nutné zabezpečit aby docházelo jak ke spočtení pravděpodobností výběru jednotlivých senátů metodou <see cref="ComputeProbabilities(double[] loads, int[] activecases)"/>, tak i ke jejich výběru pomocí metody <see cref="SelectSenateIndex()"/>.
        /// Metoda vrací maximální diferenci během jedné iterace.
        /// </remarks>
        /// <remarks>
        /// Z implementačních důvodů je výsledek zapisován do vstupního pole <c>int [] results</c>"/>, odpadá tak nutnost toto pole vytvářet při každém volání metody. Pro získání správných výsledků musí být toto pole vynulováno. 
        /// </remarks>
        public abstract int Simulate(double[] loads, int[] activecases, int casecount, int[] result);


        /// <summary>
        /// Spočte diferenci v počtu přidělených případů mezi senátem, který má maximální a minimální počet přidělených případů.
        /// </summary>
        /// <param name="result">Pole přidělených případů jeenotlivým senátům.</param>
        /// <returns>Diference</returns>
        /// <remarks>Pokud senáty nemají stejné zatížení, nebo stejný počet případů na vstupu (aktuální počet případů k řešení), tak je tento údaj irelevantní.</remarks>
        protected int FindMaxMinDiference(int[] result)
        {
            return result.Max()-result.Min();
        }


        /// <summary>
        /// Slouží pro uložení bezpečného generátoru náhodných čísel.
        /// </summary>
        /// <remarks>
        /// Dle dokumentace Microsoftu by měla být volání generátoru vláknově bezpečné.
        /// </remarks>
        private static  RNGCryptoServiceProvider _secureRng;

        static SelectionAlgorithm()
        {
            Debug.WriteLine("Initializing generator");
            _secureRng = new RNGCryptoServiceProvider();
        }
       
        /// <summary>
        /// Metoda vracející hodnotu typu double v intervalu (0,1).Pro generování je použit bezpečný generátor <see cref="RNGCryptoServiceProvider"/>.
        /// </summary>
        /// <returns>Náhodné číslo v intervalu (0,1)</returns>
        /// <remarks>Generátor samotný vrací náhodné byty, tyto je nutné převést na double. Byl použit postup:
        /// <see ref="https://stackoverflow.com/questions/403572/how-to-get-random-double-value-out-of-random-byte-array-values"/>
        /// Funkčnost ověřena pomocí vzorců pro výpočet základních statistických veličin bez uložení v paměti:
        /// <see ref="https://www.johndcook.com/blog/standard_deviation/"/>
        /// Obdrženo za použití metody <see cref="StatisticTest(int n)"/> :
        ///     Počet použitých čísel: 10000000
        ///     0.500101491036701: Mean: Teorie:0.5
        ///     0.0833417443987784: Variance: Teorie:1/12=0.08333333333
        ///     0.288689702619921: STDEV: Teorie" SQRT(1/12)=0.28867513459
        ///     8.3748019630292E-10: Minimum: 
        ///     0.999999444626804: Maximum: 
        /// </remarks>
        protected double NextSecureDouble()
        {
            var bytes = new byte[8];
            _secureRng.GetBytes(bytes);
            var v = BitConverter.ToUInt64(bytes, 0);
            // We only use the 53-bits of integer precision available in a IEEE 754 64-bit double.
            // The result is a fraction, 
            // r = (0, 9007199254740991) / 9007199254740992 where 0 <= r && r < 1.
            v &= ((1UL << 53) - 1);
            var r = (double)v / (double)(1UL << 53);
            LastGeneratedValue = r;//zapamatuj si poslední losovanou hodnotu
            return r;
        }

        /// <summary>
        /// Metoda pro otestování náhodnosti generátoru náhodných čísel.
        /// </summary>
        /// <param name="n">Počet testovacích pokusů.</param>
        private void StatisticTest(int n)
        {
            double m = NextSecureDouble();//inicializuj
            double mold = 0;
            double s = 0;
            double x = 0;
            double min = m;
            double max = m;
            Debug.WriteLine("Testing statistical properties of generator.");
            for (int k = 2; k <= n; k++)
            {
                x = NextSecureDouble();
                mold = m;//uchovej stare
                m = m + (x - m) / k;//spocti nove m
                s = s + (x - mold) * (x - m);//spocti 
                if (x < min)
                    min = x;
                if (x > max)
                    max = x;
            }
            double variance = (n > 1) ? s / (n - 1) : s;
            double stdev = Math.Sqrt(variance);
            Debug.WriteLine("Used numbers: " + n.ToString());
            Debug.WriteLine("Mean: ", m.ToString());
            Debug.WriteLine("Variance: ", variance.ToString());
            Debug.WriteLine("STDEV: ", stdev.ToString());
            Debug.WriteLine("Minimum: ", min.ToString());
            Debug.WriteLine("Maximum: ", max.ToString());
            Debug.WriteLine("Testing done");

        }


    }
}
