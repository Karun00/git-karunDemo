using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class SAPInvoiceItemVO
    {
        [DataMember]
        public string ORDERNUMBER {get; set;}
        [DataMember]
        public string ITEMNUMBER {get; set;}
        [DataMember]
        public string MATNR {get; set;}
        [DataMember]
        public string SERVICE {get; set;}
        [DataMember]
        public string UOM {get; set;}
        [DataMember]
        public string QUANTITY {get; set;}
        [DataMember]
        public string TARIFF {get; set;}
        [DataMember]
        public string TARIFF2 {get; set;}
        [DataMember]
        public string AMOUNT {get; set;}
        [DataMember]
        public string VAT {get; set;}
        [DataMember]
        public string NETAMNT {get; set;}
        [DataMember]
        public string KUNNR {get; set;}
        [DataMember]
        public string AGENTNAME {get; set;}
        [DataMember]
        public string ADDRESS {get; set;}
        [DataMember]
        public string CONTACTT {get; set;}
        [DataMember]
        public string CONTACTF {get; set;}
        [DataMember]
        public string ACCOUNT {get; set;}
        [DataMember]
        public string VESSELID {get; set;}
        [DataMember]
        public string VESSELNAME {get; set;}
        [DataMember]
        public string VESSELTON {get; set;}
        [DataMember]
        public string VESSELCAP {get; set;}
        [DataMember]
        public string VESSELLEN {get; set;}
        [DataMember]
        public string ARRIVALID {get; set;}
        [DataMember]
        public string ARRIVALDATE {get; set;}
        [DataMember]
        public string ARRIVALTIME {get; set;}
        [DataMember]
        public string DEPARTUREDATE {get; set;}
        [DataMember]
        public string DEPARTURETIME {get; set;}
        [DataMember]
        public string VOYAGERI  {get; set;}
        [DataMember]
        public string VOYAGERO { get; set; }
    
    }
}
