using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSPGenerator.GeneratingService
{
     /// <summary>
    /// Třída pro zapouzdření informací nutných pro implementování stabdardů JSB_IS
    /// </summary>
    public class GeneratingServiceNames
    {  
            public const string GeneratorServiceName = "GeneratingService";
            public const string DomainName = "GENERATOR";
            public const string GeneratorServiceNS = "urn:cz:justice:jsb:services:" + DomainName + ":" + GeneratorServiceName;
        
    }
}