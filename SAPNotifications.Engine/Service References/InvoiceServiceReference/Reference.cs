﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SAPNotifications.Engine.InvoiceServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="InvoiceServiceReference.BiztalkInvoiceService")]
    public interface BiztalkInvoiceService {
        
        // CODEGEN: Generating message contract since the operation BiztalkInvoiceOperation is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="BiztalkInvoiceOperation", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        SAPNotifications.Engine.InvoiceServiceReference.BiztalkInvoiceOperationResponse BiztalkInvoiceOperation(SAPNotifications.Engine.InvoiceServiceReference.BiztalkInvoiceOperationRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="BiztalkInvoiceOperation", ReplyAction="*")]
        System.Threading.Tasks.Task<SAPNotifications.Engine.InvoiceServiceReference.BiztalkInvoiceOperationResponse> BiztalkInvoiceOperationAsync(SAPNotifications.Engine.InvoiceServiceReference.BiztalkInvoiceOperationRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://IPMSConnect_SAP.InvoiceCreate")]
    public partial class Invoice : object, System.ComponentModel.INotifyPropertyChanged {
        
        private InvoiceItem[] eINVOICEField;
        
        private string vBELNField;
        
        private string bILLINGDOCField;
        
        private int eSUBRCField;
        
        private string nETVALUEField;
        
        private string mESSAGETYPEField;
        
        private int sAPPOSTINGIDField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public InvoiceItem[] EINVOICE {
            get {
                return this.eINVOICEField;
            }
            set {
                this.eINVOICEField = value;
                this.RaisePropertyChanged("EINVOICE");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string VBELN {
            get {
                return this.vBELNField;
            }
            set {
                this.vBELNField = value;
                this.RaisePropertyChanged("VBELN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string BILLINGDOC {
            get {
                return this.bILLINGDOCField;
            }
            set {
                this.bILLINGDOCField = value;
                this.RaisePropertyChanged("BILLINGDOC");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=3)]
        public int ESUBRC {
            get {
                return this.eSUBRCField;
            }
            set {
                this.eSUBRCField = value;
                this.RaisePropertyChanged("ESUBRC");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=4)]
        public string NETVALUE {
            get {
                return this.nETVALUEField;
            }
            set {
                this.nETVALUEField = value;
                this.RaisePropertyChanged("NETVALUE");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=5)]
        public string MESSAGETYPE {
            get {
                return this.mESSAGETYPEField;
            }
            set {
                this.mESSAGETYPEField = value;
                this.RaisePropertyChanged("MESSAGETYPE");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=6)]
        public int SAPPOSTINGID {
            get {
                return this.sAPPOSTINGIDField;
            }
            set {
                this.sAPPOSTINGIDField = value;
                this.RaisePropertyChanged("SAPPOSTINGID");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://IPMSConnect_SAP.InvoiceCreate")]
    public partial class InvoiceItem : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string oRDERNUMBERField;
        
        private string iTEMNUMBERField;
        
        private string mATNRField;
        
        private string sERVICEField;
        
        private string uOMField;
        
        private string qUANTITYField;
        
        private string tARIFFField;
        
        private string tARIFF2Field;
        
        private string aMOUNTField;
        
        private string vATField;
        
        private string nETAMNTField;
        
        private string kUNNRField;
        
        private string aGENTNAMEField;
        
        private string aDDRESSField;
        
        private string cONTACTTField;
        
        private string cONTACTFField;
        
        private string aCCOUNTField;
        
        private string vESSELIDField;
        
        private string vESSELNAMEField;
        
        private string vESSELTONField;
        
        private string vESSELCAPField;
        
        private string vESSELLENField;
        
        private string aRRIVALIDField;
        
        private string aRRIVALDATEField;
        
        private string aRRIVALTIMEField;
        
        private string dEPARTUREDATEField;
        
        private string dEPARTURETIMEField;
        
        private string vOYAGERIField;
        
        private string vOYAGEROField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string ORDERNUMBER {
            get {
                return this.oRDERNUMBERField;
            }
            set {
                this.oRDERNUMBERField = value;
                this.RaisePropertyChanged("ORDERNUMBER");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string ITEMNUMBER {
            get {
                return this.iTEMNUMBERField;
            }
            set {
                this.iTEMNUMBERField = value;
                this.RaisePropertyChanged("ITEMNUMBER");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string MATNR {
            get {
                return this.mATNRField;
            }
            set {
                this.mATNRField = value;
                this.RaisePropertyChanged("MATNR");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=3)]
        public string SERVICE {
            get {
                return this.sERVICEField;
            }
            set {
                this.sERVICEField = value;
                this.RaisePropertyChanged("SERVICE");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=4)]
        public string UOM {
            get {
                return this.uOMField;
            }
            set {
                this.uOMField = value;
                this.RaisePropertyChanged("UOM");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=5)]
        public string QUANTITY {
            get {
                return this.qUANTITYField;
            }
            set {
                this.qUANTITYField = value;
                this.RaisePropertyChanged("QUANTITY");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=6)]
        public string TARIFF {
            get {
                return this.tARIFFField;
            }
            set {
                this.tARIFFField = value;
                this.RaisePropertyChanged("TARIFF");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=7)]
        public string TARIFF2 {
            get {
                return this.tARIFF2Field;
            }
            set {
                this.tARIFF2Field = value;
                this.RaisePropertyChanged("TARIFF2");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=8)]
        public string AMOUNT {
            get {
                return this.aMOUNTField;
            }
            set {
                this.aMOUNTField = value;
                this.RaisePropertyChanged("AMOUNT");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=9)]
        public string VAT {
            get {
                return this.vATField;
            }
            set {
                this.vATField = value;
                this.RaisePropertyChanged("VAT");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=10)]
        public string NETAMNT {
            get {
                return this.nETAMNTField;
            }
            set {
                this.nETAMNTField = value;
                this.RaisePropertyChanged("NETAMNT");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=11)]
        public string KUNNR {
            get {
                return this.kUNNRField;
            }
            set {
                this.kUNNRField = value;
                this.RaisePropertyChanged("KUNNR");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=12)]
        public string AGENTNAME {
            get {
                return this.aGENTNAMEField;
            }
            set {
                this.aGENTNAMEField = value;
                this.RaisePropertyChanged("AGENTNAME");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=13)]
        public string ADDRESS {
            get {
                return this.aDDRESSField;
            }
            set {
                this.aDDRESSField = value;
                this.RaisePropertyChanged("ADDRESS");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=14)]
        public string CONTACTT {
            get {
                return this.cONTACTTField;
            }
            set {
                this.cONTACTTField = value;
                this.RaisePropertyChanged("CONTACTT");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=15)]
        public string CONTACTF {
            get {
                return this.cONTACTFField;
            }
            set {
                this.cONTACTFField = value;
                this.RaisePropertyChanged("CONTACTF");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=16)]
        public string ACCOUNT {
            get {
                return this.aCCOUNTField;
            }
            set {
                this.aCCOUNTField = value;
                this.RaisePropertyChanged("ACCOUNT");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=17)]
        public string VESSELID {
            get {
                return this.vESSELIDField;
            }
            set {
                this.vESSELIDField = value;
                this.RaisePropertyChanged("VESSELID");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=18)]
        public string VESSELNAME {
            get {
                return this.vESSELNAMEField;
            }
            set {
                this.vESSELNAMEField = value;
                this.RaisePropertyChanged("VESSELNAME");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=19)]
        public string VESSELTON {
            get {
                return this.vESSELTONField;
            }
            set {
                this.vESSELTONField = value;
                this.RaisePropertyChanged("VESSELTON");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=20)]
        public string VESSELCAP {
            get {
                return this.vESSELCAPField;
            }
            set {
                this.vESSELCAPField = value;
                this.RaisePropertyChanged("VESSELCAP");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=21)]
        public string VESSELLEN {
            get {
                return this.vESSELLENField;
            }
            set {
                this.vESSELLENField = value;
                this.RaisePropertyChanged("VESSELLEN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=22)]
        public string ARRIVALID {
            get {
                return this.aRRIVALIDField;
            }
            set {
                this.aRRIVALIDField = value;
                this.RaisePropertyChanged("ARRIVALID");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=23)]
        public string ARRIVALDATE {
            get {
                return this.aRRIVALDATEField;
            }
            set {
                this.aRRIVALDATEField = value;
                this.RaisePropertyChanged("ARRIVALDATE");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=24)]
        public string ARRIVALTIME {
            get {
                return this.aRRIVALTIMEField;
            }
            set {
                this.aRRIVALTIMEField = value;
                this.RaisePropertyChanged("ARRIVALTIME");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=25)]
        public string DEPARTUREDATE {
            get {
                return this.dEPARTUREDATEField;
            }
            set {
                this.dEPARTUREDATEField = value;
                this.RaisePropertyChanged("DEPARTUREDATE");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=26)]
        public string DEPARTURETIME {
            get {
                return this.dEPARTURETIMEField;
            }
            set {
                this.dEPARTURETIMEField = value;
                this.RaisePropertyChanged("DEPARTURETIME");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=27)]
        public string VOYAGERI {
            get {
                return this.vOYAGERIField;
            }
            set {
                this.vOYAGERIField = value;
                this.RaisePropertyChanged("VOYAGERI");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=28)]
        public string VOYAGERO {
            get {
                return this.vOYAGEROField;
            }
            set {
                this.vOYAGEROField = value;
                this.RaisePropertyChanged("VOYAGERO");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://IPMSConnect_SAP.StatusMsg")]
    public partial class Status : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string messageField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string Message {
            get {
                return this.messageField;
            }
            set {
                this.messageField = value;
                this.RaisePropertyChanged("Message");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class BiztalkInvoiceOperationRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://IPMSConnect_SAP.InvoiceCreate", Order=0)]
        public SAPNotifications.Engine.InvoiceServiceReference.Invoice Invoice;
        
        public BiztalkInvoiceOperationRequest() {
        }
        
        public BiztalkInvoiceOperationRequest(SAPNotifications.Engine.InvoiceServiceReference.Invoice Invoice) {
            this.Invoice = Invoice;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class BiztalkInvoiceOperationResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://IPMSConnect_SAP.StatusMsg", Order=0)]
        public SAPNotifications.Engine.InvoiceServiceReference.Status Status;
        
        public BiztalkInvoiceOperationResponse() {
        }
        
        public BiztalkInvoiceOperationResponse(SAPNotifications.Engine.InvoiceServiceReference.Status Status) {
            this.Status = Status;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface BiztalkInvoiceServiceChannel : SAPNotifications.Engine.InvoiceServiceReference.BiztalkInvoiceService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class BiztalkInvoiceServiceClient : System.ServiceModel.ClientBase<SAPNotifications.Engine.InvoiceServiceReference.BiztalkInvoiceService>, SAPNotifications.Engine.InvoiceServiceReference.BiztalkInvoiceService {
        
        public BiztalkInvoiceServiceClient() {
        }
        
        public BiztalkInvoiceServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public BiztalkInvoiceServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BiztalkInvoiceServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BiztalkInvoiceServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        SAPNotifications.Engine.InvoiceServiceReference.BiztalkInvoiceOperationResponse SAPNotifications.Engine.InvoiceServiceReference.BiztalkInvoiceService.BiztalkInvoiceOperation(SAPNotifications.Engine.InvoiceServiceReference.BiztalkInvoiceOperationRequest request) {
            return base.Channel.BiztalkInvoiceOperation(request);
        }
        
        public SAPNotifications.Engine.InvoiceServiceReference.Status BiztalkInvoiceOperation(SAPNotifications.Engine.InvoiceServiceReference.Invoice Invoice) {
            SAPNotifications.Engine.InvoiceServiceReference.BiztalkInvoiceOperationRequest inValue = new SAPNotifications.Engine.InvoiceServiceReference.BiztalkInvoiceOperationRequest();
            inValue.Invoice = Invoice;
            SAPNotifications.Engine.InvoiceServiceReference.BiztalkInvoiceOperationResponse retVal = ((SAPNotifications.Engine.InvoiceServiceReference.BiztalkInvoiceService)(this)).BiztalkInvoiceOperation(inValue);
            return retVal.Status;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<SAPNotifications.Engine.InvoiceServiceReference.BiztalkInvoiceOperationResponse> SAPNotifications.Engine.InvoiceServiceReference.BiztalkInvoiceService.BiztalkInvoiceOperationAsync(SAPNotifications.Engine.InvoiceServiceReference.BiztalkInvoiceOperationRequest request) {
            return base.Channel.BiztalkInvoiceOperationAsync(request);
        }
        
        public System.Threading.Tasks.Task<SAPNotifications.Engine.InvoiceServiceReference.BiztalkInvoiceOperationResponse> BiztalkInvoiceOperationAsync(SAPNotifications.Engine.InvoiceServiceReference.Invoice Invoice) {
            SAPNotifications.Engine.InvoiceServiceReference.BiztalkInvoiceOperationRequest inValue = new SAPNotifications.Engine.InvoiceServiceReference.BiztalkInvoiceOperationRequest();
            inValue.Invoice = Invoice;
            return ((SAPNotifications.Engine.InvoiceServiceReference.BiztalkInvoiceService)(this)).BiztalkInvoiceOperationAsync(inValue);
        }
    }
}
