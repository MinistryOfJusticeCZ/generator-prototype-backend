using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace SelectionsAlgorithms
{
    /// <summary>
    /// Třída realizující rozdělování případů senátům tak, že bere zřetel na jejich zatížení a počet aktuálně přidělených případů.
    /// Detailní popis algoritmu viz matematický model algoritmu.
    /// </summary>
    [AlgorithmDescription("Algoritmus:s pamětí", Enabled = true, Description = "Přidělování s využitím aktuální vytíženosti  (alpha=0.5, beta=2).", ID = 0)]
    public class SelectionWithMemory : SelectionAlgorithm
    {
        /// <summary>
        /// V případě, že aktuální zatížení senátu je největší ze všech zúčastněných senátů, je jeho defaultní pravděpodobnost (která je dána zatížením senátu) snížena násobením touto hodnotou.
        /// </summary>
        const double alpha = 0.5;
        /// <summary>
        ///  V případě, že aktuální zatížení senátu je nejmenší ze všech zúčastněných senátů, je jeho defaultní pravděpodobnost (která je dána zatížením senátu) zvýšena násobením touto hodnotou.
        /// </summary>
        const double beta = 2;
        /// <summary>
        /// Pole pro uložení aktuální vytíženosti senátu. Aktuální vytíženost a_i í-tého senátu je dána jako počet jeho aktivních případů děleno zatížením senátu.
        /// Čím větší hodnota, tím je senát vytíženější.
        /// </summary>
        double[] aarray;
        /// <summary>
        /// Metoda pro výpočet intervalů pravděpodobnosti s použitím aktuálního počtu případů v řešení. Metoda počítá aktuální vytíženost a_i í-tého senátu jako počet jeho aktivních případů děleno zatížením senátu.
        /// Hodnota a_i je normalizována a škálována, více viz matematický model algoritmu. Pokud je aktuální zatížení senátu větší, dochází ke snížení pravděpodobnosti. 
        /// Naopak, pokud je aktuální zatížení menší, dochází ke zvýšení pravděpodobnosti výběru.
        /// </summary>
        /// <param name="loads">Pole se zatížením jednotlivých senátů.</param>
        /// <param name="activecases">Pole s počty případů jednotlivých senátů v řešení.</param>
        protected override void ComputeProbabilities(double[] loads, int[] activecases)
        {
            //Debug.WriteLine("Compute probability");
            if (parray != null && loads.Length <= parray.Length)//test, zda je pole pravdepodobnosti alokovano a ma spravnou delku
            {
                if (aarray != null && loads.Length <= aarray.Length)//test, zda je pole aktualni vytizenosti alokovano a ma spravnou delku
                {
                //    Debug.WriteLine("Actual load:");
                    //Pokud je mozne zatizeni senatu nula, a jsou zde aktualni pripady, tak aktualni zatizeni je velmi vysoke->nekonecno.
                    //V pripade nuloveho poctu pripadu, je aktualni zatizeni vzdy nula
                    for(int i=0;i<loads.Length;i++)//spocti aktualni zatizenost
                    {
                        aarray[i] = activecases[i] / (loads[i]+1e-16);//pokud je mozne zatizeni senatu 0
                  //      Debug.Write(aarray[i] + ":");
                    }

                    double amin = double.MaxValue;
                    double amax = double.MinValue;
                    for (int i = 0; i < loads.Length; i++)
                     {
                         if (aarray[i] < amin)
                             amin = aarray[i];
                         if (aarray[i] > amax)
                             amax = aarray[i];
                     }
                    double delta = amax - amin;
                    double ascaled;
                    //normovani-skalovani
                    if (delta > 0)//pokud jsou senaty rozdilne vytizeny
                    {
                        for (int i = 0; i < loads.Length; i++)//skaluj aktualni vytizenost
                        {
                            ascaled = (aarray[i] - amin) / delta;
                            aarray[i] = alpha * ascaled + (1 - ascaled) * beta;//normavane a skalovane a_i je v intervalu <alpha,beta>
                        }
                    }
                    else//v pripade, ze je aktualni  vysenaty aktualne vytizeny stejne, je naskalovano na hodnotu 1-nema to vliv na vyber senatu
                    {
                        for (int i = 0; i < loads.Length; i++)//normuj aktualni vytizenost
                        {
                            aarray[i] = 1;
                        }
                    }
                            //Debug.WriteLine("\nNormed ai:");
                    //spocti pravdepodobnosti
                    for (int i = 0; i < loads.Length; i++)
                    {
                        parray[i] = loads[i] * aarray[i];
                 //      Debug.Write(aarray[i] + ":");
                        
                    }
                            //Debug.WriteLine("\nProbability:");
                    //spocti intervaly pravdepodobnosti v <0,1>
                    double totalp = parray.Sum();
                    parray[0] = parray[0] / totalp;
                //            Debug.Write(parray[0]);
                    for (int i = 1; i < loads.Length; i++)
                    {
                        parray[i] = parray[i - 1] + parray[i] / totalp;
                        //    Debug.Write(":");
                        //    Debug.Write(parray[i]-parray[i-1]);
                    }
                 //  Debug.WriteLine("\n----------------------------------");
                }
                else
                    throw new Exception("Unitialized actual load array.");//nemelo by v zadnem pripade nastat-pri spravnem pouziti

            }
            else
                throw new Exception("Unitialized probability array.");//nemelo by v zadnem pripade nastat-pri spravnem pouziti

    
        }

        /// <summary>
        /// Metoda provádí vlastní přidělení (simulaci) daného počtu případů senátům. Jsou brány v potaz již přidělené případy. V průběhu simulace dochází k navyšování přidělených případů a toto dále ovlivňuje přidělování případů.
        /// </summary>
        /// <param name="loads">Zatížení senátů.</param>
        /// <param name="activecases">Počty případů v řešení pro jednotlivé senáty.</param>
        /// <param name="casecount">Počet případů k rozdělení, v průběhu výpočtu není modifikováno.</param>
        /// <param name="result">Pole s výsledky simulace, tedy distribuce případů jednotlivým senátům.</param>
        /// <remarks>Metoda nemodifikuje vstupní údaje o počtu přidělených případů jednotlivým senátům. Pracuje se s lokální kopiíí pole.</remarks>
        public override int Simulate(double[] loads, int[] activecases, int casecount, int [] result)
        {
            ComputeProbabilities(loads, activecases); //inicializuj pole pravdepodobnosti
            int selected;
            int[] copycases = new int[activecases.Length];//alokuj-pro vytvoreni kopie vstupniho pole
            Array.Copy(activecases, copycases, activecases.Length);//vytvor kopii vstupnich dat

            int maxdif = 0;//pouzito pro vypocet maximalni diference
            int tmpdif = 0;
            for (int item = 0; item < casecount; item++)
            {
                selected = SelectSenateIndex();//vyber=pridel pripad, je vracen index senatu
               // Debug.WriteLine("Selected: " + selected);
                copycases[selected]++;//zvys vybranemu senatu pocet aktivnich pripadu
                result[selected]++;//zvys pocet pridelenych pripadu
                tmpdif = FindMaxMinDiference(result);//urci max min diferenci po danem prideleni
                if (tmpdif > maxdif)//hledej maximum diference
                    maxdif = tmpdif;
                ComputeProbabilities(loads, copycases); //reinicializuj pole pravdepodobnosti
            }
            return maxdif;//navrat maximalni diference
        }

        /// <summary>
        /// Provádí inicializaci algoritmu pro losování. Je provedena alokace polí pravděpodobnosti a aktuálního zatížení senátu. Je nezbytné, aby před použitím alogritmu losování byla tato metoda volána.
        /// </summary>
        /// <remarks>V případě, že pole jsou již alokována a mají dostatečnou velikost, tak se neděje nic.</remarks>
        /// <param name="senatescount">Počet senátů, kterým budou případy rozdělovány.</param>
        public override void InitAlgorithm(int senatescount)
        {
            base.InitAlgorithm(senatescount); //proved alokaci pole pro pravdepodobnost
            if (aarray == null || aarray.Length < senatescount)//kdyz neni alokovano, nebo ma malou velikost
               aarray = new double[senatescount]; //alokuj pole pro aktualni vytizeni
        }
    }

   
}
