﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace tester1.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UserID", Namespace="urn:cz:justice:jsb:services:GENERATOR:FrontEndService")]
    [System.SerializableAttribute()]
    public partial class UserID : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string IDField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string ID {
            get {
                return this.IDField;
            }
            set {
                if ((object.ReferenceEquals(this.IDField, value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AlgorithmInfo", Namespace="urn:cz:justice:jsb:services:GENERATOR:FrontEndService")]
    [System.SerializableAttribute()]
    public partial class AlgorithmInfo : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string AlgorithmDescriptionField;
        
        private string AlgorithmIDField;
        
        private string AlgorithmNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string AlgorithmDescription {
            get {
                return this.AlgorithmDescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.AlgorithmDescriptionField, value) != true)) {
                    this.AlgorithmDescriptionField = value;
                    this.RaisePropertyChanged("AlgorithmDescription");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string AlgorithmID {
            get {
                return this.AlgorithmIDField;
            }
            set {
                if ((object.ReferenceEquals(this.AlgorithmIDField, value) != true)) {
                    this.AlgorithmIDField = value;
                    this.RaisePropertyChanged("AlgorithmID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string AlgorithmName {
            get {
                return this.AlgorithmNameField;
            }
            set {
                if ((object.ReferenceEquals(this.AlgorithmNameField, value) != true)) {
                    this.AlgorithmNameField = value;
                    this.RaisePropertyChanged("AlgorithmName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SimulationParams", Namespace="http://schemas.datacontract.org/2004/07/MSPGenerator")]
    [System.SerializableAttribute()]
    public partial class SimulationParams : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string[] AlgorithmsToSimulateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CasesToDistributionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private tester1.ServiceReference1.Senate[] SenatesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int SimulationCountField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private tester1.ServiceReference1.UserID UserField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string[] AlgorithmsToSimulate {
            get {
                return this.AlgorithmsToSimulateField;
            }
            set {
                if ((object.ReferenceEquals(this.AlgorithmsToSimulateField, value) != true)) {
                    this.AlgorithmsToSimulateField = value;
                    this.RaisePropertyChanged("AlgorithmsToSimulate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int CasesToDistribution {
            get {
                return this.CasesToDistributionField;
            }
            set {
                if ((this.CasesToDistributionField.Equals(value) != true)) {
                    this.CasesToDistributionField = value;
                    this.RaisePropertyChanged("CasesToDistribution");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public tester1.ServiceReference1.Senate[] Senates {
            get {
                return this.SenatesField;
            }
            set {
                if ((object.ReferenceEquals(this.SenatesField, value) != true)) {
                    this.SenatesField = value;
                    this.RaisePropertyChanged("Senates");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int SimulationCount {
            get {
                return this.SimulationCountField;
            }
            set {
                if ((this.SimulationCountField.Equals(value) != true)) {
                    this.SimulationCountField = value;
                    this.RaisePropertyChanged("SimulationCount");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public tester1.ServiceReference1.UserID User {
            get {
                return this.UserField;
            }
            set {
                if ((object.ReferenceEquals(this.UserField, value) != true)) {
                    this.UserField = value;
                    this.RaisePropertyChanged("User");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Senate", Namespace="http://schemas.datacontract.org/2004/07/MSPGenerator")]
    [System.SerializableAttribute()]
    public partial class Senate : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ActiveCasesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool EnabledField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double LoadField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ActiveCases {
            get {
                return this.ActiveCasesField;
            }
            set {
                if ((this.ActiveCasesField.Equals(value) != true)) {
                    this.ActiveCasesField = value;
                    this.RaisePropertyChanged("ActiveCases");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Enabled {
            get {
                return this.EnabledField;
            }
            set {
                if ((this.EnabledField.Equals(value) != true)) {
                    this.EnabledField = value;
                    this.RaisePropertyChanged("Enabled");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ID {
            get {
                return this.IDField;
            }
            set {
                if ((object.ReferenceEquals(this.IDField, value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Load {
            get {
                return this.LoadField;
            }
            set {
                if ((this.LoadField.Equals(value) != true)) {
                    this.LoadField = value;
                    this.RaisePropertyChanged("Load");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="urn:cz:justice:jsb:services:GENERATOR:FrontEndService", ConfigurationName="ServiceReference1.FrontEndService")]
    public interface FrontEndService {
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:cz:justice:jsb:services:GENERATOR:FrontEndService/FrontEndService/GetAlgorith" +
            "mInfo", ReplyAction="urn:cz:justice:jsb:services:GENERATOR:FrontEndService/FrontEndService/GetAlgorith" +
            "mInfoResponse")]
        tester1.ServiceReference1.AlgorithmInfo[] GetAlgorithmInfo(tester1.ServiceReference1.UserID id);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:cz:justice:jsb:services:GENERATOR:FrontEndService/FrontEndService/GetAlgorith" +
            "mInfo", ReplyAction="urn:cz:justice:jsb:services:GENERATOR:FrontEndService/FrontEndService/GetAlgorith" +
            "mInfoResponse")]
        System.Threading.Tasks.Task<tester1.ServiceReference1.AlgorithmInfo[]> GetAlgorithmInfoAsync(tester1.ServiceReference1.UserID id);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:cz:justice:jsb:services:GENERATOR:FrontEndService/FrontEndService/DoSimulatio" +
            "n", ReplyAction="urn:cz:justice:jsb:services:GENERATOR:FrontEndService/FrontEndService/DoSimulatio" +
            "nResponse")]
        void DoSimulation(tester1.ServiceReference1.SimulationParams sparams);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:cz:justice:jsb:services:GENERATOR:FrontEndService/FrontEndService/DoSimulatio" +
            "n", ReplyAction="urn:cz:justice:jsb:services:GENERATOR:FrontEndService/FrontEndService/DoSimulatio" +
            "nResponse")]
        System.Threading.Tasks.Task DoSimulationAsync(tester1.ServiceReference1.SimulationParams sparams);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface FrontEndServiceChannel : tester1.ServiceReference1.FrontEndService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class FrontEndServiceClient : System.ServiceModel.ClientBase<tester1.ServiceReference1.FrontEndService>, tester1.ServiceReference1.FrontEndService {
        
        public FrontEndServiceClient() {
        }
        
        public FrontEndServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public FrontEndServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FrontEndServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FrontEndServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public tester1.ServiceReference1.AlgorithmInfo[] GetAlgorithmInfo(tester1.ServiceReference1.UserID id) {
            return base.Channel.GetAlgorithmInfo(id);
        }
        
        public System.Threading.Tasks.Task<tester1.ServiceReference1.AlgorithmInfo[]> GetAlgorithmInfoAsync(tester1.ServiceReference1.UserID id) {
            return base.Channel.GetAlgorithmInfoAsync(id);
        }
        
        public void DoSimulation(tester1.ServiceReference1.SimulationParams sparams) {
            base.Channel.DoSimulation(sparams);
        }
        
        public System.Threading.Tasks.Task DoSimulationAsync(tester1.ServiceReference1.SimulationParams sparams) {
            return base.Channel.DoSimulationAsync(sparams);
        }
    }
}