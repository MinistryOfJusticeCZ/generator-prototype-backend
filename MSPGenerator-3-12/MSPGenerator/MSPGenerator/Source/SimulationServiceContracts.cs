using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace MSPGenerator.SimulationService
{
    #region AlgorithmInfoContract
    /// <summary>
    /// Třída zapouzdřující informace o implementovaných algoritmech přidělování případů pro frontend.
    /// <remarks>
    /// Všechny implementované a nabízení algoritmy přidělování případů poskytují pomocí atributu <see cref="AlgorithmDescription"/> informace o sobě. 
    /// Vytváření těchto informací je automatizováno pomocí reflexe. Daná třída je tedy pouze projekcí původního atributu pro účely přenosu informací o implementovaných algoritmech z backendu aplikace na frontend, např. za účelem automatického vytváření GUI.
    /// </remarks>
    /// </summary>
    [DataContract(
        Name = "AlgorithmInfo",
        Namespace = SimulationServiceNames.ServiceNS//jmenný prostor služby
        )]
    public class AlgorithmInfo
    {

        /// <summary>
        /// Vlastnost obsahující identifikátor losovacího algoritmu.
        /// </summary>
        ///  
        [DataMember(IsRequired = true)]
        public string AlgorithmID { get; private set; }
        /// <summary>
        /// Vlastnost obsahující jméno losovacího algoritmu.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string AlgorithmName { get; private set; }
        /// <summary>
        /// Vlastnost obsahující krátký popis losovacího algoritmu.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string AlgorithmDescription { get; private set; }

        /// <summary>
        /// Inicializační konstruktor pro vytvoření instance třídy.
        /// </summary>
        /// <param name="id">Identifikátor algoritmu</param>
        /// <param name="name">Interní jméno algoritmu</param>
        /// <param name="description">Stručný popis algoritmu</param>
        public AlgorithmInfo(string id, string name, string description)
        {
            AlgorithmID = id;
            AlgorithmName = name;
            AlgorithmDescription = description;
            Debug.WriteLine("AlgorithmInfo created.");
        }


    }

    #endregion

    #region SimulationParamsContract
    /// <summary>
    /// Obalová třída pro přenos parametrů simulace do služby.
    /// </summary>
    /// <remarks>
    /// Všechny kontroly se dějí pouze na straně služby. Při nesplnění kontrol na straně serveru dochází k výjimce. Pokud je potřeba kontrolovat validnost na straně klienta, tak je nutné si tyto kontroly a testy implementovat sám. 
    /// </remarks>
    /// <exception cref="GeneratorServiceFault">Nastává při nesplnění testů validity dat na straně serveru. </exception>
    [DataContract(
         Name = "SimulationParams",
         Namespace = SimulationServiceNames.ServiceNS//jmenný prostor služby
        )]
    public class SimulationParams
    {
        /// <summary>
        /// Uživatel který simulaci provádí. Předpokládá se použití validního uživatele.
        /// </summary>
        [DataMember(IsRequired = true)]
        public UserIdentity User { get; private set; }
        /// <summary>
        /// Neprázdný seznam senátů pro které je simulace prováděna.
        /// <remarks>Použití prázdného seznamu senátů, nebo použití nevalidních senátů (<see cref="Senate"/>) vede při kontrole k vyvolání výjimky <see cref="FaultException<GeneratorServiceFault>"/>.</remarks>
        /// </summary>
        [DataMember(IsRequired = true)]
        public List<Senate> Senates { get; private set; }
        /// <summary>
        /// Počet případů k distribuci v každé iteraci. Musí být nejméně jedna.
        /// </summary>
        /// <remarks>Použití hodnoty menší než jedna vede při kontrole k výjimce <see cref="FaultException<GeneratorServiceFault>"/>.</remarks>
        [DataMember(IsRequired = true)]
        public int CasesToDistribution { get; private set; }

        /// <summary>
        /// Počet iterací simulace. Musí být nejméně jedna.
        /// </summary> 
        /// <remarks>Použití hodnoty menší než jedna vede při kontrole k výjimce <see cref="FaultException<GeneratorServiceFault>"/>.</remarks>
        [DataMember(IsRequired = true)]
        public int IterationsCount { get; private set; }
        /// <summary>
        /// Seznam algoritmů použitých pro simulaci. Seznam nesmí být prázdný a nesmí obsahovat prázdné řetězce.
        /// </summary>
        /// <remarks> Existence vybraných algoritmů je ověřována až při jejich použití.</remarks>
        [DataMember(IsRequired = true)]
        public List<int> AlgorithmsToSimulate { get; private set; }

        /// <summary>
        /// Metoda pro kontrolu validnosti zadaných údajů pro simulaci. Předpokládá se použití na straně serveru.
        /// </summary>
        /// <param name="instance">Instance parametrů simulace ke kontrole.</param>
        /// <returns>Úspěch kontroly.</returns>
        /// <remarks>Při neplatných údajích dochází k vyhození výjimky<see cref="FaultException<GeneratorServiceFault>"/></remarks>
        /// <exception cref="FaultException<GeneratorServiceFault>"
        public static bool CheckSimulationParams(SimulationParams instance)
        {
            if (instance == null)
                throw new FaultException<GeneratorServiceFault>(new GeneratorServiceFault("Non initialiazed simulation parameters."), new FaultReason("SimulationsParams is null."));
            UserIdentity.CheckUserIdentity(instance.User);//kontrola uzivatelske identity
            if (instance.Senates == null)//test zda jsou zadany nejake senaty
                throw new FaultException<GeneratorServiceFault>(new GeneratorServiceFault("Non initialiazed list of senates."), new FaultReason("List of Senates is null."));
            instance.Senates.ForEach((x) => Senate.CheckSenate(x));//kontrola konzistence senatu v seznamu
            if (instance.AlgorithmsToSimulate == null)
                throw new FaultException<GeneratorServiceFault>(new GeneratorServiceFault("Non initialiazed list of algorithms."), new FaultReason("List of AlgorithmsToSimulate is null."));
            if (instance.IterationsCount < 1)//pocet simulaci musi byt alespon jedna
                throw new FaultException<GeneratorServiceFault>(new GeneratorServiceFault(String.Format("Number of iteration of simulation: {0} is less then 1", instance.IterationsCount)), new FaultReason("No iteration."));
            if (instance.CasesToDistribution < 1)//pocet pripadu k rozdeleni musi byt alespon jedna
                    throw new FaultException<GeneratorServiceFault>(new GeneratorServiceFault(String.Format("Cases to distribution: {0} is less then 1", instance.CasesToDistribution)), new FaultReason("No cases to distribution."));
            return true;
        }


        public override string ToString()//predpokladam validni data
        {
            return string.Format("USER:{0}\nSenates:{1}\nCases to distribution:{2}\nIterations:{3}\nAlgorithms:{4}", User.ID, Senates.Count, CasesToDistribution, IterationsCount, AlgorithmsToSimulate.Count);
        }
    }
    #endregion

    #region SimulationResultContract
    /// <summary>
    /// Obalová třída nesoucí informace o výsledcích simulace. Obsahuje jaký algoritmus byl použit a kolik který senát dostal případů z celkového počtu případů v každé iteraci simulace. Iterací se rozumí rozdělení celého balíku případů.
    /// Předpokládá se její vytváření pouze na straně serveru.
    /// </summary>
    /// <remarks>Všechny kontroly se dějí pouze na straně serveru. Při nesplnění kontrol na straně serveru dochází k výjimce. Pokud je potřeba kontrolovat validnost na straně klienta, tak je nutné si tyto kontroly a testy implementovat sám. </remarks>
    /// <exception cref="GeneratorServiceFault">Nastává při nesplnění testů validity dat na straně serveru. </exception>
    [DataContract(
         Name = "SimulationResult",
         Namespace = SimulationServiceNames.ServiceNS//jmenný prostor služby
        )]
    public class SimulationResult
    {
        /// <summary>
        /// Informace o použitém algoritmu .
        /// </summary>
        [DataMember(IsRequired = true)]
        public int UsedAlgorithm { get; private set; }

        /// <summary>
        /// Seznam obsahující rozdělení případů přidělených senátům během jedné iterace. Každý prvek seznamu odpovídá jedné iteraci rozdělení případů.
        /// Pořadí prvků v poli odpovídá pořadí senátů vstupujících do simulace.
        /// </summary>
        [DataMember(IsRequired = true)]
        public List<int[]> Data { get; private set; }

        /// <summary>
        /// Seznam obsahující maximální diference během iterace algoritmu. Každý prvek seznamu odpovídá jedné iteraci rozdělení případů.
        /// </summary>
        [DataMember(IsRequired = true)]
        public List<int> MaxDifference { get; private set; }

        public SimulationResult() { }
        /// <summary>
        /// Inicializační konstruktor pro předání informací o výsledcích simulace.
        /// </summary>
        /// <param name="data">Seznam s výsledky jednotlivých iterací.</param>
        /// <param name="maxdiference">Seznam s diferencemi v jednotlivých iteracích.</param>
        /// <param name="algorithmid">Identifikátor algoritmu.</param>
        public SimulationResult(List<int[]> data,List<int> maxdiference, int algorithmid)
        {
            this.Data = data;
            this.UsedAlgorithm = algorithmid;
            this.MaxDifference = maxdiference;
        }
    }
    #endregion
   
}