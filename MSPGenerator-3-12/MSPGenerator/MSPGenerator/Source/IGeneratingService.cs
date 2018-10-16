using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MSPGenerator.GeneratingService
{
   [ServiceContract(
        Name = GeneratingServiceNames.GeneratorServiceName,//jméno služby
        Namespace = GeneratingServiceNames.GeneratorServiceNS//jmenný prostor služby
        )]
    public interface IGeneratingService
    {
       /// <summary>
       /// Metoda přijímá informace nutné pro přidělení případu a případ přídělí.
       /// </summary>
       /// <remarks>
       /// Metoda slouží jako fasáda pro volání generátoru přidělování pomocí zvoleného algoritmu. Volba použitého algoritmu je obsažena ve vstupních parametrech. Metoda zároveň loguje použití generátoru a jeho výstupy.
       /// Předpokládá se úprava metody při integraci služby do systému, např. volba algoritmu bude pevně dána.
       /// </remarks>
       /// <param name="gparams">Vstupní parametry obsahují seznam možných senátů k přidělení, identifikátor případu, identifikátor uživatele a volbu algoritmu.</param>
       /// <returns>Metoda vrací identifikátor senátu kterému byl případ přidělen.</returns>
       [OperationContract(Name = "AssignCase")]
       [FaultContract(typeof(GeneratorServiceFault))]
        string AssignCase(AssignCaseParams gparams);
    }
}
