using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Diagnostics;



namespace MSPGenerator
{
    /// <summary>
    /// Třída pro zapouzdření informací nutných pro implementování stabdardů JSB_IS
    /// </summary>
    class BackEndBaseNames
    {
        public const string Name = "GeneratorBase";
        public const string DomainName = "GENERATOR";
        public const string ServiceNS = "urn:cz:justice:jsb:services:" + DomainName + ":" + Name;
    }


    #region FaultContract
    /// <summary>
    /// Třída pro předávání informací o selháních a vyjímkách na straně generátoru.
    /// </summary>
    [DataContract(
         Name = "GeneratorServiceFault",
         Namespace = BackEndBaseNames.ServiceNS//jmenný prostor služby
        )]
    public class GeneratorServiceFault
    {
        /// <summary>
        /// Inicializařní konstruktor. Inicializace je provedena předáním zprávy o výjimce.
        /// </summary>
        /// <param name="message">Text zprávy o výjimce.</param>
        public GeneratorServiceFault(string message)
        {
            Message = message;
        }
        /// <summary>
        /// Informace o výjimce.
        /// </summary>
        [DataMember]
        public string Message { get; set;  }
    }
    #endregion

    #region UserContract
    /// <summary>
    /// Třída pro předávání uživatelské identity. 
    /// </summary>
    /// <remarks>
    /// Všechny kontroly se dějí pouze na straně služby. Při nesplnění kontrol na straně služby dochází k výjimce. Pokud je potřeba kontrolovat validnost na straně klienta, tak je nutné si tyto kontroly a testy implementovat sám. 
    /// </remarks>
    /// <exception cref="GeneratorServiceFault">Nastává při nesplnění testů validity dat na straně služby. </exception>
    [DataContract(
         Name = "UserID",
         Namespace = BackEndBaseNames.ServiceNS//jmenný prostor služby
        )]
    public class UserIdentity
    {
        [DataMember(IsRequired=true)]
        public string ID {get;set;}
       
        /// <summary>
        /// Konstruktor pro vytvoření identity uživatele. Může být volán jen na straně služby.
        /// </summary>
        /// <param name="id"></param>
        public UserIdentity(string id) { 
           ID = id; 
           Debug.WriteLine("Server:UserIdentity created: " + ID);
        }
        /// <summary>
        /// Konstruktor pro vytvoření identity uživatele. Nemělo by vůbec docházet k jeho volání, pro testovací účely. Může být volán jen na straně služby.
        /// </summary>
        public UserIdentity()
        {
            ID = "UNKNOWN"; Debug.WriteLine("Created UNKNOWN user!!!!!");
            throw new FaultException<GeneratorServiceFault>(new GeneratorServiceFault("UserIdentity ID property is UNKNOWN."), new FaultReason("Created UNKNOWN user!!!!!"));
        }


        /// <summary>
        /// Metoda pro kontrolu validity instance uživatelské identity. Předpokládá se užití na straně služby.
        /// </summary>
        /// <remarks>
        /// Metoda nekontroluje a neověřuje uživatele vůči databázi uživatelů. Kontroluje zde instance není nulová a všechny atributy uživatele jsou nastavené. Je to z důvodu použití automatických vlastností.
        /// </remarks>
        /// <remarks>
        /// V případě rozšíření třídy o další atributy, je nutné napsat jejich kontroly.
        /// </remarks>
        /// <param name="id">Instance uživatelské identity.</param>
        /// <returns>Výsledek validace entity.</returns>
        public static bool CheckUserIdentity(UserIdentity id)
        {
            if (id == null)
            {
                throw new FaultException<GeneratorServiceFault>(new GeneratorServiceFault("Non initialiazed user."), new FaultReason("UserIdentity is null."));
            }
            if (id.ID == null)
            {
                throw new FaultException<GeneratorServiceFault>(new GeneratorServiceFault("UserIdentity ID property is null."), new FaultReason("UserIdentity ID property is null."));
            }
           
            return true;
        }
    }
    #endregion

    #region SenateContract
    /// <summary>
    /// Obalová třída nesoucí informace o jednotlivých senátech.
    /// </summary>
    /// <remarks>
    /// Všechny kontroly se dějí pouze na straně služby. Při nesplnění kontrol na straně služby dochází k výjimce. Pokud je potřeba kontrolovat validnost na straně klienta, tak je nutné si tyto kontroly a testy implementovat sám. 
    /// </remarks>
    /// <exception cref="GeneratorServiceFault">Nastává při nesplnění testů validity dat na straně služby. </exception>
    [DataContract(
        Name = "Senate",
        Namespace = BackEndBaseNames.ServiceNS//jmenný prostor služby
        )]
    public  class Senate
    {
        /// <summary>
        /// Vlastnost představuje zatížení daného senátu. Platná je hodnota v intervalu (0,100) %.
        /// </summary>
        /// <remarks>
        /// Při použití jiné hodnoty dochází při kontrole k výjimce <see cref=" FaultException<GeneratorServiceFault>"/>.
        /// </remarks>
        [DataMember(IsRequired = true)]
        public double Load   { get; set; } 
        
        /// <summary>
        /// Privátní kontrola validity zatížení
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool CheckLoad(double value)
        {
            if (value < 0 || value > 100)//musi byt v intervalu 0-100
            {
                var reason=new FaultReason("Load must be in interval <0,100> %.");
                throw new FaultException<GeneratorServiceFault>(new GeneratorServiceFault(reason.ToString()+" Wrong Value: " + value),reason);
            }
            return true;
        }

        /// <summary>
        /// Identifikátor senátu.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string ID { get; set; }

        /// <summary>
        /// Hodnota <value>true</value> indikuje, že senátu může být přidělen případ.
        /// </summary>
        [DataMember(IsRequired = true)]
        public bool Enabled { get; set; }
        /// <summary>
        /// Čitač aktivních případů daného senátu. Hodnota určuje, kolik případů má senát aktuálně v řešení.
        /// Platná je hodnota v intervalu (0,infinity)
        /// </summary>
        /// <remarks>
        /// Při použití jiné hodnoty dochází při kontrole k výjimce <see cref=" FaultException<GeneratorServiceFault>"/>.
        /// </remarks>
        [DataMember(IsRequired = true)]
         public int ActiveCases { get;set; }
           
        
        /// <summary>
        /// Privátní kontrola počtu případů:(0,infinity)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool CheckActiveCases(int value)
        {
            if (value < 0)
            {
                var reason=new FaultReason("ActiveCases must be in interval <0,infinity>.");
                throw new FaultException<GeneratorServiceFault>(new GeneratorServiceFault(reason.ToString()+" Wrong Value: " + value),reason );
            }
            return true;
        }


        /// <summary>
        /// Kontrola konzistentnosti instance.
        /// </summary>
        /// <param name="instance">Instance pro kontrolu.</param>
        /// <returns>Úspěch/neúspěch kontroly</returns>
        /// <remarks>Akce může skončit pouze úspěchem, v opačném případě vrací výjimku<see cref="FaultException<GeneratorServiceFault>"/></remarks>
        /// <exception cref="FaultException<GeneratorServiceFault>"
        public static bool CheckSenate(Senate s)
        {
            try
            {
                if (s == null)
                    throw new FaultException<GeneratorServiceFault>(new GeneratorServiceFault("Non initialiazed"), new FaultReason("Senate  is null."));
                if (s.ID == null)
                    throw new FaultException<GeneratorServiceFault>(new GeneratorServiceFault("Non initialiazed"), new FaultReason("SenateID is null."));
                s.CheckActiveCases(s.ActiveCases);
                s.CheckLoad(s.Load);
            }
            catch(FaultException<GeneratorServiceFault> e)//provadi zretezeni retezcu zprav
            {
                var result=new FaultReason("Senat is not valid.");
                  throw new FaultException<GeneratorServiceFault>(new GeneratorServiceFault(string.Format("{0}:{1}",result,e.Detail.Message)), result );
            }
            return true;
        }

        /// <summary>
        /// Konstruktor pro vytváření senátu. Při použití na straně služby dochází ke kontrole validnosti údajů, při použití na straně klienta nikoliv.
        /// </summary>
        /// <param name="id">Identifikátor senátu</param>
        /// <param name="enabled">Senát je povolen k přidělení případů.</param>
        /// <param name="load">Zatížení senátu.</param>
        /// <param name="acases">Počet aktivních případů.</param>
        public Senate(string id,bool enabled,double load,int acases)
        {
            this.Load = load;
            this.ID = id;
            this.Enabled = enabled;
            this.ActiveCases = acases;
        }
        /// <summary>
        /// Konstruktor pro vytváření senátu. Automaticky má 0 aktivních případů.
        /// Při použití na straně služby dochází ke kontrole validnosti údajů, při použití na straně klienta nikoliv.
        /// </summary>
        /// <param name="id">Identifikátor senátu</param>
        /// <param name="enabled">Senát je povolen k přidělení případů.</param>
        /// <param name="load">Zatížení senátu.</param>
        /// <param name="acases">Počet aktivních případů.</param>
        public Senate(string id,bool enabled,double load):this(id,enabled,load,0) {} //0 pripadu v reseni
       
        /// <summary>
        /// Konstruktor pro vytváření senátu. Automaticky má 0 aktivních případů a 100% zatížení.
        /// Při použití na straně služby dochází ke kontrole validnosti údajů, při použití na straně klienta nikoliv.
        /// </summary>
        /// <param name="id">Identifikátor senátu</param>
        /// <param name="enabled">Senát je povolen k přidělení případů.</param>
        public Senate(string id,bool enabled):this(id,enabled,100.0)    { }//pouzije se 100% zatizeni, 0 pripadu v reseni
        /// <summary>
        /// Konstruktor pro vytváření senátu. Automaticky má 0 aktivních případů, 100% zatížení a je povolen k přidělení případů.
        /// Při použití na straně služby dochází ke kontrole validnosti údajů, při použití na straně klienta nikoliv.
        /// </summary>
        /// <param name="id">Identifikátor senátu</param>
        public Senate(string id) : this(id, true, 100.0, 0) { }// pouzije se povereny senat,100% zatizeni a 0 pripadu v reseni
    }
    #endregion





}