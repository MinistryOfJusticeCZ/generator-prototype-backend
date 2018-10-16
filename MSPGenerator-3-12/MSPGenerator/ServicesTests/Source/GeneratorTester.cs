using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;
using ServicesTests.HostedGeneratingService;//pro komponentu pridelovani pripadu


namespace ServicesTests.GeneratingService
{
    /*
     * Zde se nachází "UNIT" testy pro testování pridelovani pripadu ze strany klienta
     * Protože každá služba si z důvodů pozdější oddělitelnosti a nezávislosti definuje
     * svoje vlastní definice senátů a uživatelů, tak jsou testy jednotlivých služeb definovány
     * ve vlastním jmenném prostoru.
     */

    class GeneratorTester
    {
        /// <summary>
        /// Proxy objekt pro pristup ke službám generátoru
        /// </summary>
        static GeneratingServiceClient proxy_generation;
        static GeneratorTester()
        {
            try
            {
                proxy_generation = new GeneratingServiceClient("WSHttpBinding_GeneratingService");;//pokus se vytvorit spojeni se sluzbou
            }
            catch (Exception e)
            {
                ConsoleClientMessage(e);
            }

        }

        /// <summary>
        /// Metoda pro vypis chyb na serveru.
        /// </summary>
        /// <param name="fault"></param>
        public static void ConsoleServerMessage(FaultException<GeneratorServiceFault> fault)
        {
            var tmpc = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("!!!Server:{0}", fault.Message);
            Console.WriteLine("\t>Details:{0}", fault.Detail.Message);
            Console.ForegroundColor = tmpc;
        }
        /// <summary>
        /// Metoda pro výpis chyb na strane klienta
        /// </summary>
        /// <param name="fault"></param>
        public static void ConsoleClientMessage(Exception fault)
        {
            var tmpc = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("!!!Client:{0}", fault.Message);
            Console.ForegroundColor = tmpc;
        }


        /// <summary>
        /// Ukázka použití volání služeb generátoru přidělení
        /// </summary>
        public static void ExampleUsage()
        {
            string username = "Tester"; //ID uzivatele, melo by byt ziskano na frontendu (FE) prihlasenim
            UserID user = new UserID(username);//uzivatel-je predavan serveru
            try
            {
                //vytvoreni senatu-informace o senatech musi byt nactene z databaze a zde vytvoreny jejich instance
                //Dochazi k volani konstruktoru na strane klienta, viz DataContractHelpers.cs, kde jsou tyto konstruktory implementovany
                Senate s1 = new Senate("INS-tester-1",true);//senat, ktery je povoleny,zatizeni 100, pocet aktivnich pripadu 0
                Senate s2 = new Senate("INS-tester-2",false);//senat je zakazany-nebude pouzit v simulaci
                Senate s3 = new Senate("INS-tester-3",true);//senat je povoleny, 
                Senate s4 = new Senate("INS-tester-4",true);//senat je povoleny,
                Senate s5 = new Senate("INS-tester-5",true);
                //po odkomentovani vyjimka-volana na strane klienta
                //Senate s6 = new Senate("INS-tester-6",true,120);//zpusobi vyjimku na strane klienta, nepovolena hodnota zatizeni 120
                AssignCaseParams par = new AssignCaseParams();
                par.User = user;//nastav si uzivatele ktery provede akci
                par.Senates = new List<Senate>() { s1, s2, s3, s4, s5 };//musi se vytvorit seznam senatu pro ze kterých se bude vybírat
                  //vyber algoritmu pro simulaci, na FE uzivatel vybere
                 par.AlgorithmToUse=0;//vyber algoritmu pro vyber-platne hodnoty k 10.1.2018 jsou 0,1

                 for (int i = 0; i < 1; i++)
                 {
                     par.CaseIdentificator = string.Format("CASE {0}", i);
                     var results = proxy_generation.AssignCase(par);//proved vyber
                     Console.WriteLine("Byl vybran senat: {0}", results);
                 }

            }
            catch (FaultException<GeneratorServiceFault> fault)//odchytavani vyjimek ze serveru
            {
                ConsoleServerMessage(fault);
            }
            catch (Exception e)//odchytnuti vyjimky z klienta
            {
                ConsoleClientMessage(e);
            }
            finally
            {
                Console.WriteLine("Hotovo");
            }
       

        }
    }

    class GeneratorParallelTester
    {
        /// <summary>
        /// Proxy objekt pro pristup ke službám generátoru
        /// </summary>
        GeneratingServiceClient proxy_generation;
        public GeneratorParallelTester()
        {
            try
            {
                proxy_generation = new GeneratingServiceClient("WSHttpBinding_GeneratingService"); ;//pokus se vytvorit spojeni se sluzbou
            }
            catch (Exception e)
            {
                ConsoleClientMessage(e);
            }

        }

        static object locker1 = new object();
        /// <summary>
        /// Metoda pro vypis chyb na serveru.
        /// </summary>
        /// <param name="fault"></param>
        public static void ConsoleServerMessage(FaultException<GeneratorServiceFault> fault)
        {
            lock (locker1)
            {
                var tmpc = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("!!!Server:{0}", fault.Message);
                Console.WriteLine("\t>Details:{0}", fault.Detail.Message);
                Console.ForegroundColor = tmpc;
            }
        }


        static object locker2 = new object();
        /// <summary>
        /// Metoda pro výpis chyb na strane klienta
        /// </summary>
        /// <param name="fault"></param>
        public void ConsoleClientMessage(Exception fault)
        {
            lock (locker2)
            {
                var tmpc = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("!!!Client:{0}", fault.Message);
                Console.ForegroundColor = tmpc;
            }
        }
        /// <summary>
        /// Ukazka pouziti volani sluzeb generovani. Kazde volani simuluje jednoho klienta.
        /// </summary>
        public void ClientRun(string username)
        {
            UserID user = new UserID(username);//uzivatel-je predavan serveru
            try
            {
                //vytvoreni senatu-informace o senatech musi byt nactene z databaze a zde vytvoreny jejich instance
                //Dochazi k volani konstruktoru na strane klienta, viz DataContractHelpers.cs, kde jsou tyto konstruktory implementovany
                Senate s1 = new Senate("INS-tester-1", true);//senat, ktery je povoleny,zatizeni 100, pocet aktivnich pripadu 0
                Senate s2 = new Senate("INS-tester-2", false);//senat je zakazany-nebude pouzit v simulaci
                Senate s3 = new Senate("INS-tester-3", true);//senat je povoleny, 
                Senate s4 = new Senate("INS-tester-4", true);//senat je povoleny,
                Senate s5 = new Senate("INS-tester-5", true);
                //po odkomentovani vyjimka-volana na strane klienta
                //Senate s6 = new Senate("INS-tester-6",true,120);//zpusobi vyjimku na strane klienta, nepovolena hodnota zatizeni 120
                AssignCaseParams par = new AssignCaseParams();
                par.User = user;//nastav si uzivatele ktery provede akci
                par.Senates = new List<Senate>() { s1, s2, s3, s4, s5 };//musi se vytvorit seznam senatu pro ze kterých se bude vybírat
                //vyber algoritmu pro simulaci, na FE uzivatel vybere
                par.AlgorithmToUse = 0;//vyber algoritmu pro vyber-platne hodnoty k 10.1.2018 jsou 0,1

                for (int i = 0; i < 10; i++)
                {   par.CaseIdentificator = string.Format("CASE {0}", i);
                    var results = proxy_generation.AssignCase(par);//proved vyber
                    Console.WriteLine("{1}:Byl vybran senat: {0}", results,username);
                }

            }
            catch (FaultException<GeneratorServiceFault> fault)//odchytavani vyjimek ze serveru
            {
                ConsoleServerMessage(fault);
            }
            catch (Exception e)//odchytnuti vyjimky z klienta
            {
                ConsoleClientMessage(e);
            }
            finally
            {
                Console.WriteLine("Hotovo");
            }


        }
    }


}
