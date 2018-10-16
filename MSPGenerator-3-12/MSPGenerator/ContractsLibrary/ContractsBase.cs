using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Diagnostics;


namespace ContractsLibrary
{

   
    [DataContract]
    public class UserIdentityBase
    {
        [DataMember(IsRequired = true)]
        public string ID { get; protected set; }
        /// <summary>
        /// Konstruktor pro vytvoření identity uživatele.
        /// </summary>
        /// <param name="id"></param>
        public UserIdentityBase(string id) { ID = id; Debug.WriteLine("UserIdentity created: " + ID); }
        public UserIdentityBase() { ID = "unknown"; Debug.WriteLine("Created UNKNOWN user!!!!!"); }
    }

    [DataContract]
    public class AlgorithmInfoBase
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

        public AlgorithmInfoBase(string id,string name,string description)
        {
            AlgorithmID = id;
            AlgorithmName = name;
            AlgorithmDescription = description;
            Debug.WriteLine("AlgorithmInfoBase created.");
        }


        public AlgorithmInfoBase() { Debug.WriteLine("Default AlgorithmInfoBase created."); }
    }
}
