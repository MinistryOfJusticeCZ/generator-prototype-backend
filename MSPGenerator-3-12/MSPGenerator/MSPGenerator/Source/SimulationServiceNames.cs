using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSPGenerator
{
    /// <summary>
    /// Třída pro zapouzdření informací nutných pro implementování standardů JSB_IS
    /// </summary>
    class SimulationServiceNames
    {
        public const string ServiceName = "SimulationService";
        public const string DomainName = "GENERATOR";
        public const string ServiceNS = "urn:cz:justice:jsb:services:" + DomainName + ":" + ServiceName;
    }
}