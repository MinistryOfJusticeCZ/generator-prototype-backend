using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;
using ServicesTests.HostedSimulationService;//pro komponentu simulace
using System.IO;//pridano 24.1. pro test exportu xls

namespace ServicesTests.SimulationService
{
    /*Zde se nachází "UNIT" testy pro testování simulací ze strany klienta 
     * Protože každá služba si z důvodů pozdější oddělitelnosti a nezávislosti definuje
     * svoje vlastní definice senátů a uživatelů, tak jsou testy jednotlivých služeb definovány
     * ve vlastním jmenném prostoru.
     */
    class SimulationTester
    {
        /// <summary>
        /// Proxy použitá pro přístup k simulační službě.
        /// </summary>
        static SimulationServiceClient proxy_simulation; 

        /// <summary>
        /// Statický konstruktor pro inicializaci proxy
        /// </summary>
       static SimulationTester()
        {
            try
            {
                proxy_simulation = new SimulationServiceClient("WSHttpBinding_SimulationService");//pokus se vytvorit spojeni se sluzbou
            }
            catch(Exception e)
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


        #region TestyaLaděníKubera
        public static void AlgortihmInfoTest()
        {
            Console.WriteLine("FrontEndAlgorithmInfo TESTS:");
            FrontEndAlgorithmInfoTest("Petr");
            FrontEndAlgorithmInfoTest("Petra");//neznamy uzivatel
            FrontEndAlgorithmInfoTestNullArgument(null);
            FrontEndAlgorithmInfoTestNullID(null);

        }
        public static void FrontEndAlgorithmInfoTest(string name)
        {
            Console.WriteLine("Testing FrontEnd:GetAlgorithmInfo-normal use.\t UserIdentity {0}", name);
            UserID user = new UserID(name);
            try
            {
                foreach (var item in proxy_simulation.GetAlgorithmInfo(user))
                    Console.WriteLine("ID:{0}\tName:{1}\tDescription:{2}", item.AlgorithmID, item.AlgorithmName, item.AlgorithmDescription);
            }
            catch (FaultException<GeneratorServiceFault> fault)//odchytavani vyjimek ze serveru
            {
                ConsoleServerMessage(fault);
            }
            catch (Exception e)//odchytnuti vyjimky z klienta
            {
                Console.WriteLine("Client:{0}", e.Message);
            }
            finally
            {
                Console.WriteLine("-".Repeat(70));
            }
        }
        public static void FrontEndAlgorithmInfoTestNullArgument(string name)
        {
            Console.WriteLine("Testing FrontEnd:GetAlgorithmInfo-null argument");
            try
            {
                foreach (var item in proxy_simulation.GetAlgorithmInfo(null))
                    Console.WriteLine("ID:{0}\tName:{1}\tDescription:{2}", item.AlgorithmID, item.AlgorithmName, item.AlgorithmDescription);
            }
            catch (FaultException<GeneratorServiceFault> fault)//odchytavani vyjimek ze serveru
            {
                ConsoleServerMessage(fault);
            }
            catch (Exception e)//odchytnuti vyjimky z klienta
            {
                Console.WriteLine("Client:{0}", e.Message);
            }
            finally
            {
                Console.WriteLine("-".Repeat(70));
            }
        }
        public static void FrontEndAlgorithmInfoTestNullID(string name)
        {
            Console.WriteLine("Testing FrontEnd:GetAlgorithmInfo-null user identity property");
            UserID user = new UserID(name);
            user.ID = null;//chyba-nulova identita
            try
            {
                foreach (var item in proxy_simulation.GetAlgorithmInfo(user))
                    Console.WriteLine("ID:{0}\tName:{1}\tDescription:{2}", item.AlgorithmID, item.AlgorithmName, item.AlgorithmDescription);
            }
            catch (FaultException<GeneratorServiceFault> fault)//odchytavani vyjimek ze serveru
            {
                ConsoleServerMessage(fault);
            }
            catch (Exception e)//odchytnuti vyjimky z klienta
            {
                Console.WriteLine("Client:{0}", e.Message);
            }
            finally
            {
                Console.WriteLine("-".Repeat(70));
            }
        }

        /// <summary>
        /// Testy vytváření senátů
        /// </summary>
        /// <remarks>
        /// Kontrola validnosti údajů senátů, použití helperů atd.
        /// </remarks>
        /// <param name="name"></param>
        /// <param name="enabled"></param>
        /// <param name="load"></param>
        /// <param name="activecases"></param>
        public static void SimulationCreateSenateTests(string name, bool enabled, double load, int activecases)
        {
            try
            {
                Console.WriteLine("Testing FrontEnd:Creation of senats: via name: Name {0}", name);
                Senate s = new Senate(name);
                Console.WriteLine("Senate created:ID:{0}\tLoad:{1}\tEnabled:{2}\tActiveCases:{3}", s.ID, s.Load, s.Enabled, s.ActiveCases);

                Console.WriteLine("Testing FrontEnd:Creation of senats: via name: Name {0} and Enabled {1}", name, enabled);
                s = new Senate(name, enabled);
                Console.WriteLine("Senate created:ID:{0}\tLoad:{1}\tEnabled:{2}\tActiveCases:{3}", s.ID, s.Load, s.Enabled, s.ActiveCases);

                Console.WriteLine("Testing FrontEnd:Creation of senats: via name: Name {0} and Enabled {1}", name, !enabled);
                s = new Senate(name, !enabled);
                Console.WriteLine("Senate created:ID:{0}\tLoad:{1}\tEnabled:{2}\tActiveCases:{3}", s.ID, s.Load, s.Enabled, s.ActiveCases);

                Console.WriteLine("Testing FrontEnd:Creation of senats: via name: Name {0} and Enabled {1} and Load {2}", name, enabled, load);
                s = new Senate(name, enabled, load);
                Console.WriteLine("Senate created:ID:{0}\tLoad:{1}\tEnabled:{2}\tActiveCases:{3}", s.ID, s.Load, s.Enabled, s.ActiveCases);

                try
                {
                    Console.WriteLine("Testing FrontEnd:Creation of senats: via name: Name {0} and Enabled {1} and Load {2}", name, enabled, 3 * load);
                    s = new Senate(name, enabled, 3 * load);
                    Console.WriteLine("Senate created:ID:{0}\tLoad:{1}\tEnabled:{2}\tActiveCases:{3}", s.ID, s.Load, s.Enabled, s.ActiveCases);
                }
                catch (Exception e)//odchytnuti vyjimky z klienta
                {
                    Console.WriteLine("Client:{0}", e.Message);
                }
                Console.WriteLine("Testing FrontEnd:Creation of senats: via name: Name {0} and Enabled {1} and Load {2} and ActiveCases {3}", name, enabled, load, activecases);
                s = new Senate(name, enabled, load, activecases);
                Console.WriteLine("Senate created:ID:{0}\tLoad:{1}\tEnabled:{2}\tActiveCases:{3}", s.ID, s.Load, s.Enabled, s.ActiveCases);
                try
                {
                    Console.WriteLine("Testing FrontEnd:Creation of senats: via name: Name {0} and Enabled {1} and Load {2} and ActiveCases {3}", name, enabled, load, -activecases);
                    s = new Senate(name, enabled, load, -activecases);
                    Console.WriteLine("Senate created:ID:{0}\tLoad:{1}\tEnabled:{2}\tActiveCases:{3}", s.ID, s.Load, s.Enabled, s.ActiveCases);
                }
                catch (Exception e)//odchytnuti vyjimky z klienta
                {
                    Console.WriteLine("Client:{0}", e.Message);
                }

            }
            catch (FaultException<GeneratorServiceFault> fault)//odchytavani vyjimek ze serveru
            {
                ConsoleServerMessage(fault);
            }
            catch (Exception e)//odchytnuti vyjimky z klienta
            {
                Console.WriteLine("Client:{0}", e.Message);
            }
            finally
            {
                Console.WriteLine("-".Repeat(70));
            }

        }

        #endregion


        /// <summary>
        /// Ukazka pouziti volani simulaci
        /// </summary>
        public static void ExampleUsage()
        {
            string username = "Tester"; //ID uzivatele, melo by byt ziskano na frontendu (FE) prihlasenim
            UserID user = new UserID(username);//uzivatel-je predavan serveru
            try
            {
                //zjisteni informaci o algoritmech
                Console.WriteLine("Zjistuji informace o implementovanych algoritmech:");
                var algoritms = proxy_simulation.GetAlgorithmInfo(user);//seznam informaci o algoritmech, volani pomoci proxy_simulation objektu
                foreach (var item in algoritms)
                    Console.WriteLine("ID:{0}\tJmeno:{1}\tPopis:{2}", item.AlgorithmID, item.AlgorithmName, item.AlgorithmDescription);
                //vytvoreni senatu-informace o senatech musi byt nactene z databaze a zde vytvoreny jejich instance
                //Dochazi k volani konstruktoru na strane klienta, viz DataContractHelpers.cs, kde jsou tyto konstruktory implementovany
                Senate s1 = new Senate("INS-tester-1");//senat, ktery je povoleny,zatizeni 100, pocet aktivnich pripadu 0
                Senate s2 = new Senate("INS-tester-2", false);//senat je zakazany-nebude pouzit v simulaci
                Senate s3 = new Senate("INS-tester-3", true, 100);//senat je povoleny, zatizeni je 100, pocet aktivnich pripadu 0
                Senate s4 = new Senate("INS-tester-4", true, 100,0);//senat je povoleny, zatizeni 100, pocet aktivnich pripadu 0
                Senate s5 = new Senate("INS-tester-5");
                //po odkomentovani vyjimka-volana na strane klienta
                //Senate s6 = new Senate("INS-tester-6",true,120);//zpusobi vyjimku na strane klienta, nepovolena hodnota zatizeni 120
                SimulationParams par = new SimulationParams();//vytvoreni tridy pro simulacni parametry, pred odeslanim na server musi byt vse nastaveno
                par.User = user;//nastav si uzivatele
                par.Senates = new List<Senate>() { s1, s2, s3, s4, s5 };//musi se vytvorit seznam senatu pro simulaci
                //vyber algoritmu pro simulaci, na FE uzivatel vybere
                par.AlgorithmsToSimulate = (from a in algoritms select int.Parse(a.AlgorithmID)).ToList();//zjisti-vem ID vsech implementovanych algoritmu pro pouziti v simulaci
                par.IterationsCount = 10;//nastaveni poctu iteraci
                par.CasesToDistribution = 100;//pocet rozdelovanych pripadu v kazde iteraci
                var results = proxy_simulation.DoSimulation(par);//proved simulaci-bezi na serveru

                //vlozeno 24.1. test exportu do xls
                var report = new SimulationReport(algoritms, par, results);
                report.CreateReportToFile("report.xlsx");//zapise do souboru-pro spousteni v konzoli-pouzije si PLASTIAK    
                //Pro DANA MAJEROVA -vytvorenisouboru v pameti pro posilani-ODKOMENTOVAT nasledujici radek a zakomentovat ReportToFile
                //  var memory = report.CreateReportOnTheFly();//vytvor v pameti--je to mozne prevest na pole bytu a poslat response -viz http://howtodomssqlcsharpexcelaccess.blogspot.cz/2014/05/aspnet-how-to-create-excel-file-in.html


                //test Kubera-jestli to funguje-Vezmu vytvoreny memory stream a ulozim ho do souboru- funguje
                 using (var memory = report.CreateReportOnTheFly())
                 {//nutno pridat nahoru using System.IO; pro praci se soubory
                     using(FileStream fs=new FileStream("reportfly.xlsx",FileMode.OpenOrCreate))
                     {
                         memory.Position = 0;//nastav to na zacatek
                         memory.CopyTo(fs);//kopiruj pamet do file streamu
                         fs.Flush();//zapis
                     }
                 }
                


                //konec testu exportu
                foreach (var data in results)//pro kazdy algoritmus pouzity v simulaci, data jsou kompletni data pro jeden algoritmus
                {
                    Console.WriteLine("+".Repeat(70));
                    var name = (from a in algoritms where a.AlgorithmID == data.UsedAlgorithm.ToString() select a.AlgorithmName).First();
                    Console.WriteLine("Pouzity algoritmus {0}:{1}", data.UsedAlgorithm, name);
                    Console.WriteLine("+".Repeat(70));
                    int iterace=0;
                    foreach (var array in data.Data)//zpracuj data-vypis za jednotlive iterace,array je distribuce pripadu mezi senaty
                    {
                        foreach (var item in array)//vypis hodnot pres senaty
                            Console.Write("{0}\t", item);
                        Console.WriteLine("MAXDIF:{0}",data.MaxDifference[iterace]);
                        iterace++;
                        Console.WriteLine();
                    }
                    Console.WriteLine("-".Repeat(70));
                    Console.WriteLine("\nStatisticke informace");
                    Console.WriteLine("-".Repeat(70));
                    StatisticInfo(data.Data);
                    Console.WriteLine("-".Repeat(70));
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

        /// <summary>
        /// Metoda pro vypis statistickych informaci ze vstupnich dat.
        /// </summary>
        /// <param name="data">Vysledky simulace</param>
        static void StatisticInfo(List<List<int>> data)//kazde pole v seznamu predstavuje distribuci pripadu
        {
            int senatescount = data.First().Count();//vem delku prvniho pole, vsechny pole maji stejnou delku
            Console.WriteLine("MAX:");
            for (int i = 0; i < senatescount; i++)//pres vsechny senaty
            {
                var senatedata = from x in data select x[i];//obsahuje vzdy iterator-neco jako seznam-dat pro i ty senat
                Console.Write("{0}\t", senatedata.Max());//nalezni a tiskni maximum nad danym iteratorem
            }
            Console.WriteLine("\nMIN:");
            for (int i = 0; i < senatescount; i++)//pres vsechny senaty
            {
                var senatedata = from x in data select x[i];//obsahuje vzdy iterator-neco jako seznam-dat pro i ty senat
                Console.Write("{0}\t", senatedata.Min());//nalezni a tiskni minimum
            }

            /* HEADFUCKHACK-pouziti LINQ, podobne SQL
             * Pouziju trochu abstraktnejsi pristup.
             * Data jsou neprakticky List<List<int>>, ale potrebuji urcit "pole" hodnot prumeru pres sloupce.
             * Podobne jako v predchozim udelam si iterator pres jednotlive sloupce a v nem iteruji ve sloupci:(from i in Enumerable.Range(0, senatescount) select from x in data select x[i])
             * Tento "iterator itaratoru" pak prochazim a pro kazdy jeho prvek pocitam prumer.
             */
            Console.WriteLine("\nAVG:");
            var avglist = (from iter in (from i in Enumerable.Range(0, senatescount) select from x in data select x[i]) select iter.Average()).ToList();//LINQ vyraz pro vytvoreni seznamu pres prumery
            foreach (var avg in avglist)
                Console.Write("{0}\t", avg);


            Console.WriteLine("\nSTDEV:");
            for (int i = 0; i < senatescount; i++)//pres vsechny senaty
            {
                var squared = from y in (from x in data select x[i]) select Math.Pow(y - avglist[i], 2);//iterator pres druhou mocninu 
                double stdev = Math.Sqrt(squared.Sum() / (squared.Count() - 1));
                Console.Write("{0:F2}\t", stdev);
            }
            Console.WriteLine();
        }

    }


    class SimulationParallelTester
    {
        SimulationServiceClient proxy_simulation; 
        /// <summary>
        /// Konstruktor pro inicializaci proxy
        /// </summary>
       public SimulationParallelTester()
        {
            try
            {
                proxy_simulation = new SimulationServiceClient("WSHttpBinding_SimulationService");//pokus se vytvorit spojeni se sluzbou
            }
            catch(Exception e)
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
        /// Ukazka pouziti volani simulaci. Kazde volani simuluje jednoho klienta.
        /// </summary>
        public void ClientRun(string username)
        {
            try
            {
                    UserID user = new UserID(username);//uzivatel-je predavan serveru
           
                    //zjisteni informaci o algoritmech
                    Console.WriteLine("Zjistuji informace o implementovanych algoritmech:");
                    var algoritms = proxy_simulation.GetAlgorithmInfo(user);//seznam informaci o algoritmech, volani pomoci proxy_simulation objektu
                    foreach (var item in algoritms)
                        Console.WriteLine("ID:{0}\tJmeno:{1}\tPopis:{2}", item.AlgorithmID, item.AlgorithmName, item.AlgorithmDescription);
                    //vytvoreni senatu-informace o senatech musi byt nactene z databaze a zde vytvoreny jejich instance
                    //Dochazi k volani konstruktoru na strane klienta, viz DataContractHelpers.cs, kde jsou tyto konstruktory implementovany
                    Senate s1 = new Senate("INS-tester-1");//senat, ktery je povoleny,zatizeni 100, pocet aktivnich pripadu 0
                    Senate s2 = new Senate("INS-tester-2", false);//senat je zakazany-nebude pouzit v simulaci
                    Senate s3 = new Senate("INS-tester-3", true, 50);//senat je povoleny, zatizeni je 50, pocet aktivnich pripadu 0
                    Senate s4 = new Senate("INS-tester-4", true, 100, 20);//senat je povoleny, zatizeni 100, pocet aktivnich pripadu 20
                    Senate s5 = new Senate("INS-tester-5");
                    //po odkomentovani vyjimka-volana na strane klienta
                    //Senate s6 = new Senate("INS-tester-6",true,120);//zpusobi vyjimku na strane klienta, nepovolena hodnota zatizeni 120
                    SimulationParams par = new SimulationParams();//vytvoreni tridy pro simulacni parametry, pred odeslanim na server musi byt vse nastaveno
                    par.User = user;//nastav si uzivatele
                    par.Senates = new List<Senate>() { s1, s2, s3, s4, s5 };//musi se vytvorit seznam senatu pro simulaci
                    //vyber algoritmu pro simulaci, na FE uzivatel vybere
                    par.AlgorithmsToSimulate = (from a in algoritms select int.Parse(a.AlgorithmID)).ToList();//zjisti-vem ID vsech implementovanych algoritmu pro pouziti v simulaci
                    par.IterationsCount = 100;//nastaveni poctu iteraci
                    par.CasesToDistribution = 1000;//pocet rozdelovanych pripadu v kazde iteraci
                    var results = proxy_simulation.DoSimulation(par);//proved simulaci-bezi na serveru
              
                    if (results.Count != par.AlgorithmsToSimulate.Count)//test zda ma spravny pocet vysledku
                        throw new Exception("WRONG RESULTS: Algorithm to simulate inconsistence.");
                    else//kdyz ok
                        foreach (var result in results)
                        {
                            if (result.Data.Count != par.IterationsCount)
                                throw new Exception("WRONG RESULTS: Iteration counts.");
                            StatisticInfo(result.Data,username);
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
                Console.WriteLine("DONE: {0}",username);
            }
        }


        static object locker3 = new object();
        /// <summary>
        /// Metoda pro vypis statistickych informaci ze vstupnich dat.
        /// </summary>
        /// <param name="data">Vysledky simulace</param>
        static void StatisticInfo(List<List<int>> data,string username)//kazde pole v seznamu predstavuje distribuci pripadu
        {
            lock (locker3)//zamkni si 
            {
                Console.WriteLine("-".Repeat(70));
                Console.WriteLine(username);
                int senatescount = data.First().Count();//vem delku prvniho pole, vsechny pole maji stejnou delku
                Console.WriteLine("MAX:");
                for (int i = 0; i < senatescount; i++)//pres vsechny senaty
                {
                    var senatedata = from x in data select x[i];//obsahuje vzdy iterator-neco jako seznam-dat pro i ty senat
                    Console.Write("{0}\t", senatedata.Max());//nalezni a tiskni maximum nad danym iteratorem
                }
                Console.WriteLine("\nMIN:");
                for (int i = 0; i < senatescount; i++)//pres vsechny senaty
                {
                    var senatedata = from x in data select x[i];//obsahuje vzdy iterator-neco jako seznam-dat pro i ty senat
                    Console.Write("{0}\t", senatedata.Min());//nalezni a tiskni minimum
                }

                /* HEADFUCKHACK-pouziti LINQ, podobne SQL
                 * Pouziju trochu abstraktnejsi pristup.
                 * Data jsou neprakticky List<List<int>>, ale potrebuji urcit "pole" hodnot prumeru pres sloupce.
                 * Podobne jako v predchozim udelam si iterator pres jednotlive sloupce a v nem iteruji ve sloupci:(from i in Enumerable.Range(0, senatescount) select from x in data select x[i])
                 * Tento "iterator itaratoru" pak prochazim a pro kazdy jeho prvek pocitam prumer.
                 */
                Console.WriteLine("\nAVG:");
                var avglist = (from iter in (from i in Enumerable.Range(0, senatescount) select from x in data select x[i]) select iter.Average()).ToList();//LINQ vyraz pro vytvoreni seznamu pres prumery
                foreach (var avg in avglist)
                    Console.Write("{0}\t", avg);


                Console.WriteLine("\nSTDEV:");
                for (int i = 0; i < senatescount; i++)//pres vsechny senaty
                {
                    var squared = from y in (from x in data select x[i]) select Math.Pow(y - avglist[i], 2);//iterator pres druhou mocninu 
                    double stdev = Math.Sqrt(squared.Sum() / (squared.Count() - 1));
                    Console.Write("{0:F2}\t", stdev);
                }
                Console.WriteLine();
                Console.WriteLine("-".Repeat(70));
             
            }
        }
    }

    /// <summary>
    /// Pomocná třída, pouze opakuje řetězce
    /// </summary>
    static class Extensions
    {
        static object locker = new object();
        public static string Repeat(this string what, int count)
        {
            lock (locker)
            {
                StringBuilder sb = new StringBuilder(what);
                for (int i = 1; i < count; i++)
                    sb.Append(what);
                return sb.ToString();
            }
        }
    }

}
