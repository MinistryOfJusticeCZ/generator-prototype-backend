﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServicesTests.HostedSimulationService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UserID", Namespace="urn:cz:justice:jsb:services:GENERATOR:GeneratorBase")]
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
    [System.Runtime.Serialization.DataContractAttribute(Name="AlgorithmInfo", Namespace="urn:cz:justice:jsb:services:GENERATOR:SimulationService")]
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
    [System.Runtime.Serialization.DataContractAttribute(Name="GeneratorServiceFault", Namespace="urn:cz:justice:jsb:services:GENERATOR:GeneratorBase")]
    [System.SerializableAttribute()]
    public partial class GeneratorServiceFault : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
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
        public string Message {
            get {
                return this.MessageField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageField, value) != true)) {
                    this.MessageField = value;
                    this.RaisePropertyChanged("Message");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="SimulationParams", Namespace="urn:cz:justice:jsb:services:GENERATOR:SimulationService")]
    [System.SerializableAttribute()]
    public partial class SimulationParams : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private System.Collections.Generic.List<int> AlgorithmsToSimulateField;
        
        private int CasesToDistributionField;
        
        private int IterationsCountField;
        
        private System.Collections.Generic.List<ServicesTests.HostedSimulationService.Senate> SenatesField;
        
        private ServicesTests.HostedSimulationService.UserID UserField;
        
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
        public System.Collections.Generic.List<int> AlgorithmsToSimulate {
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
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
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
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int IterationsCount {
            get {
                return this.IterationsCountField;
            }
            set {
                if ((this.IterationsCountField.Equals(value) != true)) {
                    this.IterationsCountField = value;
                    this.RaisePropertyChanged("IterationsCount");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public System.Collections.Generic.List<ServicesTests.HostedSimulationService.Senate> Senates {
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
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public ServicesTests.HostedSimulationService.UserID User {
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
    [System.Runtime.Serialization.DataContractAttribute(Name="Senate", Namespace="urn:cz:justice:jsb:services:GENERATOR:GeneratorBase")]
    [System.SerializableAttribute()]
    public partial class Senate : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private int ActiveCasesField;
        
        private bool EnabledField;
        
        private string IDField;
        
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
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
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
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
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
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SimulationResult", Namespace="urn:cz:justice:jsb:services:GENERATOR:SimulationService")]
    [System.SerializableAttribute()]
    public partial class SimulationResult : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private System.Collections.Generic.List<System.Collections.Generic.List<int>> DataField;
        
        private System.Collections.Generic.List<int> MaxDifferenceField;
        
        private int UsedAlgorithmField;
        
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
        public System.Collections.Generic.List<System.Collections.Generic.List<int>> Data {
            get {
                return this.DataField;
            }
            set {
                if ((object.ReferenceEquals(this.DataField, value) != true)) {
                    this.DataField = value;
                    this.RaisePropertyChanged("Data");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public System.Collections.Generic.List<int> MaxDifference {
            get {
                return this.MaxDifferenceField;
            }
            set {
                if ((object.ReferenceEquals(this.MaxDifferenceField, value) != true)) {
                    this.MaxDifferenceField = value;
                    this.RaisePropertyChanged("MaxDifference");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int UsedAlgorithm {
            get {
                return this.UsedAlgorithmField;
            }
            set {
                if ((this.UsedAlgorithmField.Equals(value) != true)) {
                    this.UsedAlgorithmField = value;
                    this.RaisePropertyChanged("UsedAlgorithm");
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
    [System.ServiceModel.ServiceContractAttribute(Namespace="urn:cz:justice:jsb:services:GENERATOR:SimulationService", ConfigurationName="HostedSimulationService.SimulationService")]
    public interface SimulationService {
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:cz:justice:jsb:services:GENERATOR:SimulationService/SimulationService/GetAlgo" +
            "rithmInfo", ReplyAction="urn:cz:justice:jsb:services:GENERATOR:SimulationService/SimulationService/GetAlgo" +
            "rithmInfoResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(ServicesTests.HostedSimulationService.GeneratorServiceFault), Action="urn:cz:justice:jsb:services:GENERATOR:SimulationService/SimulationService/GetAlgo" +
            "rithmInfoGeneratorServiceFaultFault", Name="GeneratorServiceFault", Namespace="urn:cz:justice:jsb:services:GENERATOR:GeneratorBase")]
        System.Collections.Generic.List<ServicesTests.HostedSimulationService.AlgorithmInfo> GetAlgorithmInfo(ServicesTests.HostedSimulationService.UserID id);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:cz:justice:jsb:services:GENERATOR:SimulationService/SimulationService/GetAlgo" +
            "rithmInfo", ReplyAction="urn:cz:justice:jsb:services:GENERATOR:SimulationService/SimulationService/GetAlgo" +
            "rithmInfoResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<ServicesTests.HostedSimulationService.AlgorithmInfo>> GetAlgorithmInfoAsync(ServicesTests.HostedSimulationService.UserID id);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:cz:justice:jsb:services:GENERATOR:SimulationService/SimulationService/DoSimul" +
            "ation", ReplyAction="urn:cz:justice:jsb:services:GENERATOR:SimulationService/SimulationService/DoSimul" +
            "ationResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(ServicesTests.HostedSimulationService.GeneratorServiceFault), Action="urn:cz:justice:jsb:services:GENERATOR:SimulationService/SimulationService/DoSimul" +
            "ationGeneratorServiceFaultFault", Name="GeneratorServiceFault", Namespace="urn:cz:justice:jsb:services:GENERATOR:GeneratorBase")]
        System.Collections.Generic.List<ServicesTests.HostedSimulationService.SimulationResult> DoSimulation(ServicesTests.HostedSimulationService.SimulationParams sparams);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:cz:justice:jsb:services:GENERATOR:SimulationService/SimulationService/DoSimul" +
            "ation", ReplyAction="urn:cz:justice:jsb:services:GENERATOR:SimulationService/SimulationService/DoSimul" +
            "ationResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<ServicesTests.HostedSimulationService.SimulationResult>> DoSimulationAsync(ServicesTests.HostedSimulationService.SimulationParams sparams);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface SimulationServiceChannel : ServicesTests.HostedSimulationService.SimulationService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SimulationServiceClient : System.ServiceModel.ClientBase<ServicesTests.HostedSimulationService.SimulationService>, ServicesTests.HostedSimulationService.SimulationService {
        
        public SimulationServiceClient() {
        }
        
        public SimulationServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SimulationServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SimulationServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SimulationServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.List<ServicesTests.HostedSimulationService.AlgorithmInfo> GetAlgorithmInfo(ServicesTests.HostedSimulationService.UserID id) {
            return base.Channel.GetAlgorithmInfo(id);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<ServicesTests.HostedSimulationService.AlgorithmInfo>> GetAlgorithmInfoAsync(ServicesTests.HostedSimulationService.UserID id) {
            return base.Channel.GetAlgorithmInfoAsync(id);
        }
        
        public System.Collections.Generic.List<ServicesTests.HostedSimulationService.SimulationResult> DoSimulation(ServicesTests.HostedSimulationService.SimulationParams sparams) {
            return base.Channel.DoSimulation(sparams);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<ServicesTests.HostedSimulationService.SimulationResult>> DoSimulationAsync(ServicesTests.HostedSimulationService.SimulationParams sparams) {
            return base.Channel.DoSimulationAsync(sparams);
        }
    }
}
