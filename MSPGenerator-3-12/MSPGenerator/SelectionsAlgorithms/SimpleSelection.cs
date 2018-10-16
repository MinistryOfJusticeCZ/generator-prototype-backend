using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace SelectionsAlgorithms
{
    /// <summary>
    /// Třída realizující triviální rozdělování případů senátům tak, že bere zřetel pouze na jejich zatížení. Není brán v potaz počet aktuálně přidělených případů.
    /// Detailní popis algoritmu viz matematický model algoritmu.
    /// </summary>
    [AlgorithmDescription("Algoritmus:bez paměti", Enabled = true, Description = "Triviální přidělování případů bez přihlédnutí k aktuální vytíženosti.",ID=2)]
    public class SimpleSelection : SelectionAlgorithm
    {
        /// <summary>
        /// Metoda nastaví intervaly pravděpodobnosti jednotlivých senátů přímoúměrně jejich zatížení.
        /// Nebere zřetel na aktuální počet aktivních případů. Předpokládá se, že existuje alespoň jeden senát s nenulovým zatížením.
        /// </summary>
        /// <remarks>Velikost intervalu pravděpodobnosti odpovídá zatížení senátu. Je to normováno celkovým zatížením všech zúčastněných senátů.</remarks>
        /// <param name="loads">Zatížení jednotlivých senátů.</param>
        /// <param name="activecases">Počty aktivních případů jednotlivých senátů.</param>
        /// <exception cref="System.Exception"> Pokud nedošlo k inicializaci voláním metod <see cref="InitAlgorithm(int senatescount)"/> pro inicializaci algoritmu a metody <see cref="ComputeProbabilities(double[] loads, int[] activecases)"/>.</exception>
        protected override void ComputeProbabilities(double[] loads, int[] activecases)
        {
            if (parray!=null && loads.Length <= parray.Length)//test, zda je pole pravdepodobnosti alokovano a ma spravnou delku
            {
                double totalload = loads.Sum();//celkovy naklad vsech senatu-neni nulovy, je osetreno pred volanim metody
                parray[0] = loads[0] / totalload;                                              
                for (int i = 1; i < loads.Length; i++)
                    parray[i] =parray[i-1]+loads[i] / totalload;
            }
            else
                throw new Exception("Unitialized probability array.");//nemelo by v zadnem pripade nastat-pri spravnem pouziti

        }
        /// <summary>
        /// Metoda provádí vlastní přidělení (simulaci) daného počtu případů senátům. Není zde brán zřetel na již přidělené případy.
        /// </summary>
        /// <param name="loads">Zatížení senátů.</param>
        /// <param name="activecases">Počty případů v řešení pro jednotlivé senáty, nevyužito.  V průběhu výpočtu není toto pole modifikováno.</param>
        /// <param name="casecount">Počet případů k rozdělení.</param>
        /// <param name="result">Pole s výsledky simulace, tedy distribuce případů jednotlivým senátům.</param>
        public override int Simulate(double[] loads, int[] activecases, int casecount,int [] result )
        {
            ComputeProbabilities(loads,activecases); //inicializuj pole pravdepodobnosti, staci jen na pocatku simulace
            int selected;

            int maxdif = 0;//pouzito pro vypocet maximalni diference
            int tmpdif = 0;
            for(int item=0;item<casecount;item++)//delej pro vsechny pripady k rozdeleni
            {
                        selected=SelectSenateIndex();//vyber =je pridelen senat, tedy jeho index
                        result[selected]++;//zvys pocet pridelenych pripadu
                        tmpdif = FindMaxMinDiference(result);//urci max min diferenci po danem prideleni
                        if (tmpdif > maxdif)//hledej maximum diference
                            maxdif = tmpdif;
            }
            return maxdif;//navrat maximalni diference
        }
    }
 }
