using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.ServiceModel;
using System.Diagnostics;
using System.Data.SqlClient;

using MSPGenerator.SimulationService;
using MSPGenerator.GeneratingService;
namespace MSPGenerator
{

    /// <summary>
    /// Třída pro logování služeb generátoru. Loguje se simulace a přidělování případů.
    /// </summary>
    /// <remarks>
    /// Třída zapisuje všechny akce do tabulky Action. Případná selhání a informace o jejich příčině do tabulky Fault.
    /// Parametry úspěšné simulace (taková která byla provedena) do tabulky SimulationInfo. Výsledky simulace nezapisuje nikam.
    /// Dále loguje volání přidělování případů. Zde se jedná o zápis do tabulky Action, dále do tabulky GeneratingInfo uloží počet senátů, generované náhodné číslo, výsledek-tj. identifikátor případu.
    /// Dále do tabulky State uloží stavy senátů pro každé volání přidělení. Konkrétně informace o tom, zda byl povolen, počty aktuálně aktivních případů, jejich zatížení a interval pravděpodobnosti.
    /// Zápisy do tabulky State jsou důležité z důvodu kontroly, zda nedochází k podvrhům apod. Údaje představují snapshot, ze kterého je možné zkonstruovat a ověřit správnost přidělení.
    /// </remarks>
    /// <remarks>
    /// Struktura tabulek je součástí dokumentace.
    /// </remarks>
    /// <exception cref="FaultException<GeneratorServiceFault>"> Z  testovacích důvodů všechny veřejné metody logu generují tuto výjimku v případě jakýchkoliv problémů při logování. Tato výjimka je službou předávána klientovi. Pro ostré nasazení tohoto logu by ji bylo nutné vyjmout, aby služba neposílala informace o problémech s logováním klientům.</exception>
    public class MSPGeneratorServicesLog
    {

        /// <summary>
        /// Connection string pro přístup k logu.
        /// </summary>
        /// <remarks>
        /// V konfiguračním souboru se jedná o položku LoggingServicesConnection.
        /// </remarks>
        static readonly string connectionstring = ConfigurationManager.AppSettings["LoggingServicesConnection"];//zjisti connection string
        /// <summary>
        /// Řetězec s SQL příkazem pro zápis do tabulky akcí Action, rodičovská tabulka .
        /// </summary>
        static readonly string insertaction = "INSERT INTO Action (UserName, ActionName, ActionTime, Fault) VALUES (@userid,@action,@time,@fault);";
        /// <summary>
        /// Řetězec s SQL příkazem pro zápis do tabulky akcí Action (rodičovská tabulka), navíc je volán select na výběr id poslední vložené položky.
        /// </summary>
        /// <remarks>
        /// Použití když je třeba získat identifikátor pro zápis do dceřiných tabulek Fault,SimulationInfo,GeneratingInfo a StatisticsInfo.
        /// </remarks>
        static readonly string insertaction_scope_identity = insertaction + "SELECT CAST(SCOPE_IDENTITY() AS INT);";
        /// <summary>
        /// Řetězec pro zápis do tabulky Fault.
        /// </summary>
        static readonly string insertfault = "INSERT INTO Fault (Message, ActionId) VALUES (@message,@action_id);";
      /// <summary>
      /// Řetezec pro zápis simulačních parametrů do tabulky SimulationInfo
      /// </summary>
        static readonly string insertsimparams = "INSERT INTO SimulationInfo (SenatesCount, CasesToDistribution, IterationCount, UsedAlgorithms, ActionId) VALUES (@senates,@cases,@iterations,@algorithms,@action_id);";
        /// <summary>
        /// Řetezec pro zápis informací o voláni generátoru do tabulky GeneratingInfo
        /// </summary>
        static readonly string insertgenparams = "INSERT INTO GeneratingInfo (SenatesCount, UsedAlgorithmId, CaseId, RandomValue,SelectedSenateId,ActionId) VALUES (@senates,@algorithm,@case,@randvalue,@senate,@action_id);";
        /// <summary>
        /// Řetezec pro zápis informací o volání generátoru do tabulky State
        /// </summary>
        static readonly string insertstates = "INSERT INTO State (SenateId, Enabled, SenateLoad, ActiveCasesCount,LeftBound,RightBound,ActionId) VALUES (@senate_id,@enabled,@senate_load,@case_count,@left_bound,@right_bound,@action_id);";
        
       /// <summary>
       /// Metoda pro zápis do tabulky akcí Action
       /// </summary>
       /// <param name="connection">Connection-připojení</param>
       /// <param name="user_id">Řetězec s identifikátorem uživatele</param>
       /// <param name="action_name">Typ akce (simulace, zjišťování info o algoritmech, generovani prideleni pripadu)</param>
       /// <param name="action_time">Čas události</param>
       /// <param name="fault">Zda při akci došlo k selhání</param>
       /// <remarks>
        /// Zápis do tabulky (logu) je dán SQL příkazem v  řetězci <see cref="insertaction"/>.
       /// </remarks>
        private static void WriteAction(SqlConnection connection, string user_id, string action_name, DateTime action_time, bool fault)
        {
            using (SqlCommand command = new SqlCommand(insertaction, connection))
            {
                command.Parameters.AddWithValue("@userid", user_id);//id uzivatele
                command.Parameters.AddWithValue("@action", action_name);//typ akce
                command.Parameters.AddWithValue("@time", action_time);//cas akce
                command.Parameters.AddWithValue("@fault", fault);//selhani-neni/je
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Metoda pro zápis do tabulky akcí Action, která navíc vrací Id poslední zapsané položky.
        /// </summary>
        /// <param name="connection">Connection-připojení</param>
        /// <param name="user_id">Řetězec s identifikátorem uživatele</param>
        /// <param name="action_name">Typ akce (simulace, zjišťování info o algoritmech)</param>
        /// <param name="action_time">Čas události</param>
        /// <param name="fault">Zda při akci došlo k selhání</param>
        /// <returns>Identifikátor poslední zapsané položky (primární klíč). </returns>
        /// <remarks>
        /// Předpokládá se využití primárního klíče jako cizího klíče v dceřiných tabulkách.
        /// </remarks>
        private static int WriteActionGetLastId(SqlConnection connection, string user_id, string action_name, DateTime action_time, bool fault)
        {
            using (SqlCommand command = new SqlCommand(insertaction_scope_identity, connection))
            {
                command.Parameters.AddWithValue("@userid", user_id);//id uzivatele
                command.Parameters.AddWithValue("@action", action_name);//typ akce
                command.Parameters.AddWithValue("@time", action_time);//cas akce
                command.Parameters.AddWithValue("@fault", fault);//selhani-nastalo/ano-ne
                return (int)command.ExecuteScalar();//vrat vysledek
            }
        }

        /// <summary>
        /// Metoda zapisuje úspěšně provedenou akci do tabulky akcí Action.
        /// </summary>
        /// <param name="user_id">Identifikátor uživatele</param>
        /// <param name="action_name">Jméno/typ akce</param>
        /// <remarks>
        /// Předpokládá se užití v případě logování úspěšné akce, kde se neukládají žádné parametry akce (simulace).
        /// Protože by měla být použita při úspěšné akci, není proveden test na existenci parametrů.
        /// </remarks>
        public static void WriteActionSuccess(string user_id,string action_name)
        {
            try
            {
                DateTime action_time = DateTime.Now;//zjisti si cas
                Debug.WriteLine("LOGING: " + connectionstring);
                using(SqlConnection connection=new SqlConnection(connectionstring))//udelej spojeni
                {
                    connection.Open();
                    WriteAction(connection,user_id, action_name, action_time,false );//nenastala chyba
                }
            }
            catch (Exception e)//LADENI:pokud se cokoliv pri logovani nepovede, je poslana informace klientovi
            {
                throw new FaultException<GeneratorServiceFault>(new GeneratorServiceFault("SimulationServiceLog: "+e.Message), new FaultReason(e.Message));
            }
        }

        /// <summary>
        /// Metoda zapisuje úspěšně provedenou akci do tabulky akcí Action, navíc ukládá i parametry simulace.
        /// </summary>
        /// <param name="user_id">Identifikátor uživatele</param>
        /// <param name="action_name">Jméno/typ akce</param>
        /// <param name="sparams">Parametry simulace k uložení</param>
        ///  /// <remarks>
        /// Předpokládá se užití v případě logování úspěšné akce, kde se ukládají parametry akce (simulace).
        /// Protože by měla být použita při úspěšné akci, není proveden test na existenci parametrů.
        /// </remarks>
        /// <remarks>
        /// Dochází k logování simulačních parametrů do tabulky SimulationInfo
        /// Počtu senátů, počtu případů, počtu iterací, identifikátory použitých algoritmů výběru.
        /// </remarks>
        public static void WriteSimulationActionSuccess(string user_id,string action_name,SimulationParams sparams)
        {
            try
            {
                DateTime action_time = DateTime.Now;//zjisti cas
                Debug.WriteLine("LOGING: " + connectionstring);
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();//otevri spojeni
                    //zapis do tabulky akci uspech
                    int action_id = WriteActionGetLastId(connection, user_id, action_name, action_time, false);//nenastala chyba, zjisti id
                    using (SqlCommand command = new SqlCommand(insertsimparams, connection))
                    {
                        //(@senates,@cases,@iterations,@algorithms,@action_id
                        //preved seznam algoritmu na retezec-muze pro velke seznamy byt pomale, ale cekam malou delku seznamu 1,2,3
                        string algorithms = sparams.AlgorithmsToSimulate.Select(item => item.ToString()).Aggregate((x, y) => string.Format("{0} {1}", x, y));//predpokladam maly pocet algoritmu cca 1-2
                        command.Parameters.AddWithValue("@senates", sparams.Senates.Count);//pocet senatu
                        command.Parameters.AddWithValue("@cases", sparams.CasesToDistribution);//pocet pripadu k rozdeleni
                        command.Parameters.AddWithValue("@iterations", sparams.IterationsCount);//pocet iteraci
                        command.Parameters.AddWithValue("@algorithms", algorithms);//algoritmy-prevedeno na retezec
                        command.Parameters.AddWithValue("@action_id", action_id);//id akce
                        command.ExecuteNonQuery();//proved prikay
                    }
                }
            }
            catch (Exception e)//LADENI:pokud se cokoliv pri logovani nepovede, je poslana informace klientovi
            {
                throw new FaultException<GeneratorServiceFault>(new GeneratorServiceFault("SimulationServiceLog: " + e.Message), new FaultReason(e.Message));
            }
        }


        /// <summary>
        ///  Metoda zapisuje úspěšně provedenou akci do tabulky akcí Action, navíc ukládá i parametry generování.
        /// </summary>
        /// <param name="user_id">Identifikátor uživatele</param>
        /// <param name="action_name">Jméno akce</param>
        /// <param name="gparams">Parametry generování k uložení</param>
        /// <param name="random_value">Generovaná náhodná hodnota</param>
        /// <param name="parray">Spočtené pole pravděpodobností</param>
        /// <param name="result">Vybraný senát</param>
        /// <remarks>
        /// Předpokládá se užití v případě logování úspěšné akce, kde se ukládají parametry akce (generování).
        /// Protože by měla být použita při úspěšné akci, není proveden test na existenci parametrů.
        /// </remarks>
        /// <remarks>
        /// Dochází k logování parametrů generování do tabulky GeneratingInfo a stavů (aktuální konfigurace) do tabulky State
        /// Do tabulky GeneratingInfo se ukládá: počet senátů, identifikátor použitého algoritmu,identifikátor případu, generovaná náhodná hodnota,vybraný senát a id k odkazu na rodičovskou tabulku Action.
        /// Do tabulky State se ukládá:identifikátor každého senátu, zda je povolený, jeho zatížení, počet aktivních případů, levá a pravá mez intervalu pravděpodobnosti a id k odkazu na rodičovskou tabulku Action.
        /// Pokud je senát nepovolen, tak je levá a pravá mez intervalu pravděpodobnosti nastavena na hodnotu 0.
        /// </remarks>
        public static void WriteGeneratingActionSuccess(string user_id, string action_name, AssignCaseParams gparams,double random_value,double [] parray,string result)
        {
            try
            {
                DateTime action_time = DateTime.Now;//zjisti cas
                Debug.WriteLine("LOGING: " + connectionstring);
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();//otevri spojeni
                    //zapis do tabulky akci uspech
                    int action_id = WriteActionGetLastId(connection, user_id, action_name, action_time, false);//nenastala chyba, zjisti id
                    using (SqlCommand command = new SqlCommand(insertgenparams, connection))
                    {
                       // (@senates,@algorithm,@case,@randvalue,@senate,@action_id)
                        command.Parameters.AddWithValue("@senates", gparams.Senates.Count);//pocet senatu
                        command.Parameters.AddWithValue("@algorithm", gparams.AlgorithmToUse);//pouzity algoritmus
                        command.Parameters.AddWithValue("@case", gparams.CaseIdentificator);//identifikator pripadu
                        command.Parameters.AddWithValue("@randvalue", random_value);//vylosovna hodnota
                        command.Parameters.AddWithValue("@senate", result);//vybrany senat
                        command.Parameters.AddWithValue("@action_id", action_id);//id akce
                        command.ExecuteNonQuery();//proved prikaz
                    }
                   //zapis informaci o jednotlivych senatech
                    int counter = 0;
                    double left = 0;
                    double right = parray[0];//prava mez
                    foreach (var senate in gparams.Senates)//pro kazdy senat pis stav
                    {
                        

                        using (SqlCommand command = new SqlCommand(insertstates, connection))
                        {
                            // (@senate_id,@enabled,@senate_load,@case_count,@left_bound,@right_bound,@action_id)
                            command.Parameters.AddWithValue("@senate_id", senate.ID);//id senatu
                            command.Parameters.AddWithValue("@enabled", senate.Enabled);//zda byl povoleny
                            command.Parameters.AddWithValue("@senate_load", senate.Load);//zatizeni senatu
                            command.Parameters.AddWithValue("@case_count", senate.ActiveCases);//pocet aktivnich pripadu

                            if (senate.Enabled)
                            {
                                command.Parameters.AddWithValue("@left_bound", left);//leva mez intrvalu pravdepodobnosti senatu
                                command.Parameters.AddWithValue("@right_bound", right);//prava mez intrvalu pravdepodobnosti senatu
                            }
                            else//kdyz je senat zakazan, tak leva i prava hranice je 0
                            {
                                command.Parameters.AddWithValue("@left_bound", 0);//leva mez intrvalu pravdepodobnosti senatu
                                command.Parameters.AddWithValue("@right_bound", 0);//prava mez intrvalu pravdepodobnosti senatu
                            }
                            command.Parameters.AddWithValue("@action_id", action_id);//id akce
                            command.ExecuteNonQuery();//proved prikaz
                        }
                        if (senate.Enabled )//pro povolene senaty posouvej hranici
                        {
                            counter++;
                            if (counter < parray.Length)
                            {
                                left = right;
                                right = parray[counter];
                            }
                        }
                    }

                }
            }
            catch (Exception e)//LADENI:pokud se cokoliv pri logovani nepovede, je poslana informace klientovi
            {
                throw new FaultException<GeneratorServiceFault>(new GeneratorServiceFault("SimulationServiceLog: " + e.Message), new FaultReason(e.Message));
            }
        }


        /// <summary>
        /// Metoda pro zápis selhání do tabulky selhání Fault a primární tabulky Action.
        /// </summary>
        /// <param name="user_id">Identifikátor uživatele</param>
        /// <param name="action_name">Název/typ akce</param>
        /// <param name="fault_message">Chybová zpráva</param>
        /// <remarks>
        /// Metoda zapíše do tabulky akcí Action danou akci s příznakem selhání.
        /// Navíc do tabulky selhání Fault zapíše zprávu o příčině neúspěchu. 
        /// </remarks>
        public static void WriteActionFault(string user_id,string action_name,string fault_message)
        {
            try
            {
                DateTime action_time = DateTime.Now;
                Debug.WriteLine("LOGING: " + connectionstring);
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    Debug.WriteLine("LOGING: opened");
                    int action_id = 0;
                    //zapis akci a zjisti id posledniho zapisu
                    action_id = WriteActionGetLastId(connection,user_id, action_name, action_time, true);//selhani nastalo
                    using (SqlCommand command = new SqlCommand(insertfault, connection))
                    {
                        command.Parameters.AddWithValue("@message", fault_message);//text zpravy
                        command.Parameters.AddWithValue("@action_id", action_id);//id akce
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)//LADENI:pokud se cokoliv pri logovani nepovede, je poslana informace klientovi
            {
                throw new FaultException<GeneratorServiceFault>(new GeneratorServiceFault("SimulationServiceLog: " + e.Message), new FaultReason(e.Message));
            }
        }

     
    }
}