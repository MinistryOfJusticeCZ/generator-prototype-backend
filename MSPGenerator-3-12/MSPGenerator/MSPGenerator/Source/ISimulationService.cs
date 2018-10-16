using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Diagnostics;


namespace MSPGenerator.SimulationService
{
    /// <summary>
    /// Rozhraní popisující služby poskytované službou frontendu za účelem provádění simulací rozdělování případů.
    /// </summary>
    /// <remarks>
    /// Rozhraní nabízí metody pro zjištění implementovaných algoritmů a metodu pro provádění simulace. Slouží jako fasáda nad algoritmy generátoru přidělení.
    /// </remarks>
    [ServiceContract(
        Name=SimulationServiceNames.ServiceName,//jméno služby
        Namespace=SimulationServiceNames.ServiceNS//jmenný prostor služby
        )]
    public interface ISimulationService
    {
        /// <summary>
        /// Operace služby pro zjištění implementovaných algoritmů generátoru. 
        /// </summary>
        /// <param name="id">Vstupní identita uživatele.</param>
        /// <returns>Seznam implementovaných algoritmů</returns>
        [OperationContract(Name="GetAlgorithmInfo")]
        [FaultContract(typeof(GeneratorServiceFault))]
        List<AlgorithmInfo> GetImplementedAlgorithmInfo(UserIdentity id);//vnitřní jméno api
      
        /// <summary>
        /// Operace služby zajišťující provádění simulace.
        /// </summary>
        /// <param name="sparams">Parametry simulace</param>
        /// <returns>Výsledky simulace</returns>
        [OperationContract(Name="DoSimulation")]
        [FaultContract(typeof(GeneratorServiceFault))]
        List<SimulationResult> DoSimulation(SimulationParams sparams);

       
     
    }

 
}
