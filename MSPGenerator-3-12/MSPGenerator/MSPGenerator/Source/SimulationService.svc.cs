using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Diagnostics;
using System.Configuration;

using SelectionsAlgorithms;

namespace MSPGenerator.SimulationService
{
    /// <summary>
    /// Implementace rozhraní služeb generátoru pro simulaci.
    /// </summary>
    /// <remarks>Třída slouží jako fasáda na generátorem přidělování případů <see cref="SelectionAlgorithm"/>. Třída poskytuje frontendu základní služby potřebné pro simulaci. Konkrétně metodu <see cref=" GetImplementedAlgorithmInfo"/>, která vrací seznam implementovaných algoritmů přidělování případů. Dále je zde poskytována metoda <see cref="DoSimulation"/>, která umožňuje provádět simulace. Veškeré operace jsou logovány na straně serveru.</remarks>
    /// <exception cref="GeneratorServiceFault"> V případě neplatných parametrů metod, či jinak nevalidních údajů, dochází k vyvolání výjimky.</exception>
    public class SimulationService : ISimulationService
    {
        AlgorithmFactory factory = new AlgorithmFactory();//vytvor factory

        /// <summary>
        /// Metoda vrací informace o implementovaných algoritmech rozdělování případů na straně serveru.
        /// </summary>
        /// <param name="id">Identifikátor uživatele.</param>
        /// <returns></returns>
        public List<AlgorithmInfo> GetImplementedAlgorithmInfo(UserIdentity id)
        {
            bool fault = false;//predpokladej ze chyba neni
            string faultmessage="UNKNOWN";
            try
            {
                if (UserIdentity.CheckUserIdentity(id))//over parametry, pokud selze nastava vyjimka
                {
                    Debug.WriteLine("Server:GetImplementedAlgorithmInfo:UserIdentity " + id.ID);
                    return (from x in factory.Descriptions select new AlgorithmInfo(x.ID.ToString(), x.Name, x.Description)).ToList<AlgorithmInfo>();
                }
            }
            catch (FaultException<GeneratorServiceFault> ex)//nastane pri vnitrnich problem s validovanim uzivatele
            {
                fault = true;//nastala chyba pri validovani
                faultmessage = ex.Reason.ToString();//uloz si spravu
                throw;//vyhod vyjimku znovu
            }
            catch(Exception e)//nemelo by nikdy nastat
            {
                faultmessage = e.Message;//nejaka neocekavana chyba
                fault = true;
                throw new FaultException<GeneratorServiceFault>(new GeneratorServiceFault("Unknown reason."), new FaultReason("Unknown reason."));//vyhod vyjimku
            }
            finally
            {
                if (ConfigurationManager.AppSettings["LoggingSimulationService"].ToUpper() == "TRUE")//kdyz je logovani zapnuto
                {
                    Debug.WriteLine("LOGING:");
                    if (fault)//kdyz byla chyba
                    {
                        string user = (id != null && id.ID != null) ? id.ID : "UNKNOWN";//nemusel byt zadan uzivatel
                        Debug.WriteLine("Fault "+user+" : "+faultmessage);
                        MSPGeneratorServicesLog.WriteActionFault(user, "GetAlgorithmInfo", faultmessage);//loguj akci a chybu
                    }
                    else
                    {
                        Debug.WriteLine("Succes");
                        MSPGeneratorServicesLog.WriteActionSuccess(id.ID, "GetAlgorithmInfo");//loguj akci
                    }
                }
                
            }
            return null;

        }
            
        /// <summary>
        /// Metoda poskytující rozhraní pro provádění simulací.
        /// </summary>
        /// <param name="sparams">Parametry simulace.</param>
        /// <returns>Výsledky simulace</returns>
        /// <remarks> Metoda provádí bezstavovou simulaci, tj. nedochází ke změně vstupních údajů. Přidělené případy během simulace ovlivňují pouze aktuální simulaci.
        /// Všechny parametry simulace jako jsou senáty, jejich zatížení, počet případů k rozdělení je nutné zadat na vstupu. Metoda nepoužívá žádnou databázi pro čtení těchto informací. Co se jí předá, to použije. Pokud je některý ze senátů použitý pro simulaci zakázaný, tak si je automaticky vyjmut ze simulace.
        /// Předávané informace o uživateli jsou použité pro logování.
        /// </remarks>
        /// <remarks>
        /// Použijí se pouze povolené senáty, pokud není žádný povolený, je volána výjimka.
        /// </remarks>
        /// <remarks>
        /// Identifikátor algoritmu musí odpovídat vnitřnímu seznamu použitelných algoritmů, jinak dochází k výjimce.
        /// </remarks>
        /// <exception cref="GeneratorServiceFault">V případě neplatných parametrů dochází k vyvolání výjimky.</exception>
        public List<SimulationResult> DoSimulation(SimulationParams sparams)
        {
            bool fault = false;//predpokladej ze chyba neni
            string faultmessage = "UNKNOWN";
            try 
            {
                Debug.WriteLine("DOSIMULATION");
                List<SimulationResult> results=null; //pro ulozeni vysledku simulace
                if (SimulationParams.CheckSimulationParams(sparams))//pokud by bylo nesplneno, dochazi na volani vyjimky
                {
                    results = new List<SimulationResult>(); //seznam s vysledky simulace pro kazdou iteraci a kazdy algoritmus
                    foreach (var algorithm_id in sparams.AlgorithmsToSimulate) //pro kazdy algoritmus k simulaci
                    {
                        Debug.WriteLine("Simulate: " + algorithm_id.ToString());
                        var algorithm = factory.GetSelectionAlgorithm(algorithm_id);//vem algoritmus
                        var simresult=Simulate(algorithm, sparams);//volej simulaci danym algoritmem, mam jak data, tak diference
                        results.Add(new SimulationResult(simresult.Item1,simresult.Item2,algorithm_id));//pridej vysledky do celkovych vysledku
                    }
                    return results;//vrat vysledky
                }
            }
           catch (FaultException<GeneratorServiceFault> ex)//nastane pri vnitrnich problem s validovanim parametru
            {
                fault = true;//nastala chyba pri validovani
                faultmessage = ex.Reason.ToString()+((ex.Detail!=null)? ex.Detail.Message:" ");//uloz si zpravu i sp pripadnym detailem
                throw;//vyhod vyjimku znovu
            }
            catch (ArgumentException e)//nejaka chyba v parametrech
            {
                faultmessage = e.Message;
                fault = true;
                throw new FaultException<GeneratorServiceFault>(new GeneratorServiceFault("Wrong simulation parameters: "+e.Message), new FaultReason("Wrong simulation parameters."));
            }
            catch (KeyNotFoundException e) //nastava v pripade pouziti neexistujiciho algoritmu-identifikatoru
            {
                faultmessage = "Simulation algorithm  invalid ID "+e.Message;
                fault = true;
               throw new FaultException<GeneratorServiceFault>(new GeneratorServiceFault("Invalid algorithm ID in AlgorithmsToSimulate list."), new FaultReason("Simulation algorithm  invalid ID."));
            }
            catch(Exception e)//nemelo by nastavat
            {
                faultmessage = e.Message;
                fault = true;
               throw new FaultException<GeneratorServiceFault>(new GeneratorServiceFault("Unknown problem: "+e.Message), new FaultReason("Simulation algorithm unknown problem."));
            }
            finally
            {
                if (ConfigurationManager.AppSettings["LoggingSimulationService"].ToUpper() == "TRUE")//kdyz je logovani zapnuto
                {
                    Debug.WriteLine("LOGING:");
                    if (fault)//kdyz byla chyba
                    {   
                        //osetreni situace kdy neni ani zadan uzivatel
                        string user = (sparams != null && sparams.User != null && sparams.User.ID != null) ? sparams.User.ID : "UNKNOWN";//nemusel byt zadan uzivatel
                        Debug.WriteLine("Fault " + user + " : " + faultmessage);
                        MSPGeneratorServicesLog.WriteActionFault(user, "DoSimulation", faultmessage);//loguj akci a chybu
                    }
                    else//kdyz chyba nebyla
                    {
                        Debug.WriteLine("Succes");
                        MSPGeneratorServicesLog.WriteSimulationActionSuccess(sparams.User.ID, "DoSimulation", sparams);//loguj akci
                    }
                }
             }
            return null;
        }

       /// <summary>
       /// Metoda pro vlastní simulaci přidělování případů.
       /// </summary>
       /// <param name="algorithm">Algoritmus použitý pro simulaci.</param>
       /// <param name="sparams">Vstupní parametry simulace.</param>
        /// <returns>Rozdělení případů mezi senáty a seznam maximálních diferencí v jednotlivých iteracích . Každé pole ve vraceném seznamu List<int[]>  odpovídá jedné iteraci. V poli je uložena distribuce zadaného počtu případů mezi senáty. Pořadí prvků v poli odpovídá pořadí senátů na vstupu.</returns>
       /// <remarks>
       ///  Vstupem je vybraný algoritmus a parametry simulace. Metoda provádí jednotlivé iterace simulace. V každé iteraci dojde k rozdělení všech zadaných případů mezi povolené senáty.
       /// Z důvodů flexibility metoda přijme v simulačních parametrech všechny senáty a vybere si pouze aktivní-povolené senáty k simulaci. V případě, že všechny senáty mají nulové zatížení <see cref="Senate"/>, je vyvolána výjimka.
       /// </remarks>
       /// <exception cref="ArgumentException">Nastává v případě nulového zatížení všech senátů, nebo v případě, že všechny senáty jsou zakázány.</exception>
        Tuple<List<int[]>,List<int>> Simulate(SelectionAlgorithm algorithm, SimulationParams sparams)
        {
            if ((from s in sparams.Senates where s.Enabled select s).Count() == 0)//vsechny senaty jsou zakazany
                throw new ArgumentException("No Enabled senate in list of senates.");//vyhod vyjimku
           
            var loads = (from s in sparams.Senates where s.Enabled select s.Load).ToArray<double>();//zatizeni jednotlivych aktivnich senatu, bere v potaz pouze povolene senaty
            if (loads.Sum() == 0)//test,zda existuje alespon jeden senat s nenulovym zatizenim.
                throw new ArgumentException("No nonzero Load for senates.");

            List<int[]> simulationresult = new List<int[]>();//seznam s distribucemi v jednotlivych iteracich
         
            var activecases = (from s in sparams.Senates where s.Enabled select s.ActiveCases).ToArray<int>();//pocty aktivnich pripadu, bere v potaz pouze povolene senaty
            int[] tmpresult = new int[loads.Length];//pomocne pole pro vysledky
            List<int> maxdifs = new List<int>();//prazdny seznam pro ulozeni diferenci
            algorithm.InitAlgorithm(loads.Length);//inicializuj algoritmus-alokace poli
            for(int iteration=0;iteration<sparams.IterationsCount;iteration++)//pres vsechny iterace
            {
              int maxdif=algorithm.Simulate(loads,activecases,sparams.CasesToDistribution,tmpresult);//zavolej jednu iteraci simulace=prideleni vsech pripadu senatum
             ///V result jsou vysledky, ale pouze pro aktivni/povolene senaty, je nutne to prendat do pole vysledku, kde jsou i vysledky pro nepovolene senaty, pro ktere to je nula.
              maxdifs.Add(maxdif);//pridej maximalni diferenci do seznamu
             int[] result = new int[sparams.Senates.Count];//udelej pole pro ulozeni vysledku pro vsechny senaty (i zakazane pro simulaci)
             int tmpindex = 0;
             for(int i=0;i<sparams.Senates.Count;i++)
             {
                 if (sparams.Senates[i].Enabled)//kdyz je povolen, tak pro nej existuje vysledek v simulaci
                 {
                     result[i] = tmpresult[tmpindex];
                     tmpresult[tmpindex] = 0;//nuluj pole, pri dalsi iteraci musi byt prazdne=0 hodnota
                     tmpindex++;
                 }
                 else
                     result[i] = 0;//kdyz neni povoleny, tak musel obdrzet nula pripadu
             }
            simulationresult.Add(result);//pridej distribuci
            }

            return new   Tuple<List<int[]>,List<int>> (simulationresult,maxdifs);//vrat data simulace a diference
        }

     }
}
