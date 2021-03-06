﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServicesTests.HostedGeneratingService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AssignCaseParams", Namespace="urn:cz:justice:jsb:services:GENERATOR:GeneratingService")]
    [System.SerializableAttribute()]
    public partial class AssignCaseParams : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private int AlgorithmToUseField;
        
        private string CaseIdentificatorField;
        
        private System.Collections.Generic.List<ServicesTests.HostedGeneratingService.Senate> SenatesField;
        
        private ServicesTests.HostedGeneratingService.UserID UserField;
        
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
        public int AlgorithmToUse {
            get {
                return this.AlgorithmToUseField;
            }
            set {
                if ((this.AlgorithmToUseField.Equals(value) != true)) {
                    this.AlgorithmToUseField = value;
                    this.RaisePropertyChanged("AlgorithmToUse");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string CaseIdentificator {
            get {
                return this.CaseIdentificatorField;
            }
            set {
                if ((object.ReferenceEquals(this.CaseIdentificatorField, value) != true)) {
                    this.CaseIdentificatorField = value;
                    this.RaisePropertyChanged("CaseIdentificator");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public System.Collections.Generic.List<ServicesTests.HostedGeneratingService.Senate> Senates {
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
        public ServicesTests.HostedGeneratingService.UserID User {
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="urn:cz:justice:jsb:services:GENERATOR:GeneratingService", ConfigurationName="HostedGeneratingService.GeneratingService")]
    public interface GeneratingService {
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:cz:justice:jsb:services:GENERATOR:GeneratingService/GeneratingService/AssignC" +
            "ase", ReplyAction="urn:cz:justice:jsb:services:GENERATOR:GeneratingService/GeneratingService/AssignC" +
            "aseResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(ServicesTests.HostedGeneratingService.GeneratorServiceFault), Action="urn:cz:justice:jsb:services:GENERATOR:GeneratingService/GeneratingService/AssignC" +
            "aseGeneratorServiceFaultFault", Name="GeneratorServiceFault", Namespace="urn:cz:justice:jsb:services:GENERATOR:GeneratorBase")]
        string AssignCase(ServicesTests.HostedGeneratingService.AssignCaseParams gparams);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:cz:justice:jsb:services:GENERATOR:GeneratingService/GeneratingService/AssignC" +
            "ase", ReplyAction="urn:cz:justice:jsb:services:GENERATOR:GeneratingService/GeneratingService/AssignC" +
            "aseResponse")]
        System.Threading.Tasks.Task<string> AssignCaseAsync(ServicesTests.HostedGeneratingService.AssignCaseParams gparams);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface GeneratingServiceChannel : ServicesTests.HostedGeneratingService.GeneratingService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GeneratingServiceClient : System.ServiceModel.ClientBase<ServicesTests.HostedGeneratingService.GeneratingService>, ServicesTests.HostedGeneratingService.GeneratingService {
        
        public GeneratingServiceClient() {
        }
        
        public GeneratingServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public GeneratingServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public GeneratingServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public GeneratingServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string AssignCase(ServicesTests.HostedGeneratingService.AssignCaseParams gparams) {
            return base.Channel.AssignCase(gparams);
        }
        
        public System.Threading.Tasks.Task<string> AssignCaseAsync(ServicesTests.HostedGeneratingService.AssignCaseParams gparams) {
            return base.Channel.AssignCaseAsync(gparams);
        }
    }
}
