using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Diagnostics;
using System.Configuration;

using SelectionsAlgorithms;

namespace MSPGenerator.GeneratingService
{
    /// <summary>
    /// Implementace rozhraní služeb generátoru pro přidělování případů.
    /// </summary>
    /// <remarks>Třída slouží jako fasáda na generátorem přidělování případů <see cref="SelectionAlgorithm"/>.
    /// Třída poskytuje službu náhodného výběru senátu z dané množiny senátů. Konkrétně metodu <see cref=" AssignCase"/>, která vrací identifikátor přiděleného senátu. Veškeré operace jsou logovány na straně serveru.
    /// </remarks>
    /// <exception cref="GeneratorServiceFault"> V případě neplatných parametrů metod, či jinak nevalidních údajů, dochází k vyvolání výjimky.</exception>

    public class GeneratingService : IGeneratingService
    {
        /// <summary>
        /// Factory objekt pro vytváření instancí implementovaných algoritmů přidělování.
        /// </summary>
        AlgorithmFactory factory = new AlgorithmFactory();//vytvor si factory
        /// <summary>
        /// Metoda z dané množiny senátů na vstupu vybere pomocí specifikovaného algoritmu jeden. Jedná se pouze o fasádu nad vnitřním přidělováním. 
        /// </summary>
        /// <param name="gparams">Vstupní parametry obsahující kdo operaci volal, seznam senátů k přidělení, identifikátor případu a algoritmus pro výběr.</param>
        /// <returns>Metoda vrací identifikátor senátu.</returns>
        /// <remarks> Metoda se chová bezstavově, tj. nedochází ke změně vstupních údajů. 
        /// Všechny parametry přidělení jako jsou senáty, jejich zatížení, identifikátor případu k rozdělení je nutné zadat na vstupu. Metoda nepoužívá žádnou databázi pro čtení těchto informací. Co se jí předá, to použije. Pokud je některý ze senátů použitý pro simulaci zakázaný, tak si je automaticky vyjmut ze simulace.
        /// Předávané informace o uživateli jsou použité pro logování.
        /// </remarks>
        /// <remarks>
        /// Identifikátor algoritmu musí odpovídat vnitřnímu seznamu použitelných algoritmů, jinak dochází k výjimce. Předpokládá se úprava před nasazením, tak aby algoritmus byl pevně daný.
        /// </remarks>
        /// <remarks>
        /// Použijí se pouze povolené senáty, nepovolené senáty se dále v simulaci nepoužijí. Pokud není žádný senát povolený, je volána výjimka. 
        /// </remarks>
        /// <exception cref="GeneratorServiceFault">V případě neplatných parametrů dochází k vyvolání výjimky.</exception>
        public string AssignCase(AssignCaseParams gparams)
        {
            bool fault = false;//predpokladej ze chyba neni
            string faultmessage = "UNKNOWN";
            string result = "UNKNOWN";
            SelectionAlgorithm algorithm=null;
            try
            {
                Debug.WriteLine("ASSIGNCASE");
                if (AssignCaseParams.CheckAssignCaseParams(gparams))//kontroluj parametry
                {
                    algorithm = factory.GetSelectionAlgorithm(gparams.AlgorithmToUse);//vem algoritmus
                    result= Assign(algorithm, gparams);
                    return result;
                }
            }
            catch (FaultException<GeneratorServiceFault> ex)//nastane pri vnitrnich problem s validovanim parametru
            {
                 fault = true;//nastala chyba pri validovani
                 faultmessage = ex.Reason.ToString() + ((ex.Detail != null) ? ex.Detail.Message : " ");//uloz si zpravu i sp pripadnym detailem
                 throw;//vyhod vyjimku znovu
            }
            catch (ArgumentException e)//spatne parametry vstupu
            {
                faultmessage = e.Message;
                fault = true;
                throw new FaultException<GeneratorServiceFault>(new GeneratorServiceFault("Wrong generating parameters: " + e.Message), new FaultReason("Wrong generating parameters."));
            }
            catch (KeyNotFoundException e) //nastava v pripade pouziti neexistujiciho algoritmu-identifikatoru
            {
                throw new FaultException<GeneratorServiceFault>(new GeneratorServiceFault("Invalid algorithm ID in AlgorithmToUse."), new FaultReason("Generation algorithm  invalid ID."));
            }
            catch (Exception e)//nemelo by nastavat
            {
                throw new FaultException<GeneratorServiceFault>(new GeneratorServiceFault("Unknown problem: " + e.Message), new FaultReason("Generation algorithm unknown problem."));
            }
            finally
            {
                if (ConfigurationManager.AppSettings["LoggingGeneratingService"].ToUpper() == "TRUE")//kdyz je logovani zapnuto
                {
                    Debug.WriteLine("LOGING:");
                    if (fault)//kdyz byla chyba
                    {
                        //osetreni situace kdy neni ani zadan uzivatel
                        string user = (gparams != null && gparams.User != null && gparams.User.ID != null) ? gparams.User.ID : "UNKNOWN";//nemusel byt zadan uzivatel
                        Debug.WriteLine("Fault " + user + " : " + faultmessage);
                        MSPGeneratorServicesLog.WriteActionFault(user, "AssignCase", faultmessage);//loguj akci a chybu
                    }
                    else//kdyz chyba nebyla
                    {
                        Debug.WriteLine("Succes");
                        double p = algorithm.LastGeneratedValue;//jaka byla posledni losovana hodnota
                        int activesenates_count = (from s in gparams.Senates where s.Enabled select s).Count();
                        double[] parray = algorithm.GetProbabilityArray(activesenates_count);//kopiruj pravdepodobnosti pro aktivni senaty
                        MSPGeneratorServicesLog.WriteGeneratingActionSuccess(gparams.User.ID, "AssignCase", gparams,p,parray,result);//loguj akci
                    }
                }

            }
            return null;//sem by se kod vubec nemel dostat, bud vse OK, nebo vyjimka
           
        }

        /// <summary>
        /// Vlastní implementace přidělení případu.
        /// </summary>
        /// <param name="algorithm">Algoritmus který se použije pro výběr.</param>
        /// <param name="gparams">Vstupní parametry obsahující kdo operaci volal, seznam senátů k přidělení, identifikátor případu</param>
        /// <returns>Identifikátor vybraného senátu</returns>
        /// <remarks>
        ///  V případě, že všechny senáty mají nulové zatížení <see cref="Senate"/>, nebo není žádný povolen je vyvolána výjimka.
        /// </remarks>
        /// <exception cref="ArgumentException">Nastává v případě nulového zatížení všech senátů, nebo v případě, že všechny senáty jsou zakázány.</exception>
         private string Assign(SelectionAlgorithm algorithm, AssignCaseParams gparams)
        { 
            if((from s in gparams.Senates where s.Enabled select s).Count()==0)//vsechny senaty jsou zakazany
             throw new ArgumentException("No Enabled senate in list of senates.");

            var loads = (from s in gparams.Senates where s.Enabled select s.Load).ToArray<double>();//zatizeni jednotlivych aktivnich senatu, bere v potaz pouze povolene senaty
            if (loads.Sum() == 0)//test,zda existuje alespon jeden senat s nenulovym zatizenim.
                throw new ArgumentException("No nonzero Load for senates.");
            var activecases = (from s in gparams.Senates where s.Enabled select s.ActiveCases).ToArray<int>();//pocty aktivnich pripadu, bere v potaz pouze povolene senaty
            algorithm.InitAlgorithm(loads.Length);//inicializuj algoritmus, alokuj vnitrni  pole algoritmu
            var index = algorithm.SelectSenate(loads, activecases);//vyber index povoleneho senatu
            //nutno prevest na identifikator senatu a uvazovat i senaty, ktere byly zakazany
            Debug.WriteLine("Selected index: " + index);
           /*Udelam si iterator nad vsemy povolenymi senaty=poradi senatu pak odpovida poradi pod kterym byly vybrany
            * Pak preskocim dany pocet, kde pocet odpovida indexu vzbraneho senatu a vezmu prvni.
            * Napr kdyz. byl vybran index 0, preskocim nula prvku a vezmu prvni.
            * Napr. kdyz byl vybran index 1, preskocim jeden a vezmu prvni
            */
            return (from s in gparams.Senates where s.Enabled select s.ID).Skip(index).First();
        }
    }
}
