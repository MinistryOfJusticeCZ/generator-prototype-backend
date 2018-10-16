using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace MSPGenerator.GeneratingService
{

    /// <summary>
    /// Obalová třída pro přenos parametrů přidělování případů do služby.
    /// </summary>
    /// <remarks>
    /// Pro ostré nasazení se předpokládá úprava předávaných parametrů dle požadavků na integraci služby.
    /// </remarks>
      [DataContract(
       Name = "AssignCaseParams",
       Namespace = GeneratingServiceNames.GeneratorServiceNS//jmenný prostor služby
       )]
    public class AssignCaseParams
    {
        /// <summary>
        /// Uživatel který přidělení případů volá provádí. Předpokládá se použití validního uživatele.
        /// </summary>
        [DataMember(IsRequired = true)]
        public UserIdentity User { get; private set; }
        /// <summary>
        /// Neprázdný seznam senátů, ze kterých je vybíráno.
        /// <remarks>
        /// Použití prázdného seznamu senátů, nebo použití nevalidních senátů (<see cref="Senate"/>) vede při kontrole k vyvolání výjimky <see cref="FaultException<GeneratorServiceFault>"/>.
        /// </remarks>
        /// </summary>
        [DataMember(IsRequired = true)]
        public List<Senate> Senates { get; private set; }

        /// <summary>
        /// Identifikátor přidělovaného případu.
        /// </summary>
        /// <remarks>
        /// Tento identifikátor je zde z důvodu začlenění do systému a pro logování informací o přidělování. Nesmí být null, jinak dochází k volání výjimky <see cref="FaultException<GeneratorServiceFault>"/>.
        /// </remarks>
        [DataMember(IsRequired = true)]
        public string CaseIdentificator { get; private set; }

        /// <summary>
        /// Identifikátor algoritmu použitého pro simulaci.
        /// </summary>
        /// <remarks> 
        /// Při implementaci ostré verze služby, se předpokládá použití vybraného konkrétního algoritmu. Dojde tedy k odstranění tohoto parametru, nebo se skryje a bude použit s pevnou hodnotou odpovídající konkrétnímu preferovanému algoritmu.
        /// </remarks>
        [DataMember(IsRequired = true)]
        public int AlgorithmToUse { get; private set; }

        /// <summary>
        /// Metoda pro kontrolu validnosti zadaných údajů pro přidělování případů. Předpokládá se použití na straně služby.
        /// </summary>
        /// <param name="instance">Instance parametrů přidělení ke kontrole.</param>
        /// <returns>Úspěch kontroly.</returns>
        /// <remarks>Při neplatných údajích dochází k vyhození výjimky<see cref="FaultException<GeneratorServiceFault>"/></remarks>
        /// <exception cref="FaultException<GeneratorServiceFault>"
        public static bool CheckAssignCaseParams(AssignCaseParams instance)
        {
            if (instance == null)
                 throw new FaultException<GeneratorServiceFault>(new GeneratorServiceFault("Non initialiazed simulation parameters."), new FaultReason("SimulationsParams is null."));
            UserIdentity.CheckUserIdentity(instance.User);//kontrola uzivatelske identity
            if (instance.Senates == null)//test zda jsou zadany nejake senaty
                throw new FaultException<GeneratorServiceFault>(new GeneratorServiceFault("Non initialiazed list of senates."), new FaultReason("List of Senates is null."));
            instance.Senates.ForEach((x) => Senate.CheckSenate(x));//kontrola konzistence senatu v seznamu
            if (instance.CaseIdentificator == null)
                throw new FaultException<GeneratorServiceFault>(new GeneratorServiceFault("Non initialiazed case identificator."), new FaultReason("Case Identificator is null."));
            return true;
        }


          /// <summary>
          /// Metoda pro převod identity na řetězec.
          /// </summary>
          /// <remarks>Předpokládá se užití hlavně pro lazení.</remarks>
          /// <returns>Řetězec odpovídající vstupním parametrům.</returns>
        public override string ToString()//predpokladam validni data
        {
            return string.Format("USER:{0}\nSenates:{1}\nAlgorithms:{2}\nCaseID:{3}", User.ID, Senates.Count, AlgorithmToUse);
        }
    }

}