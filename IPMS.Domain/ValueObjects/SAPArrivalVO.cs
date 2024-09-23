using System;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class SAPArrivalVO
    {
        [DataMember]
        public string ARRNO { get; set; }
        [DataMember]
        public string CODE { get; set; }
        [DataMember]
        public System.DateTime EDA { get; set; }
        [DataMember]
        public System.DateTime EDD { get; set; }

        [DataMember]
        public string STREDA { get; set; }
        [DataMember]
        public string STREDD { get; set; }

        [DataMember]
        public string KUNNR { get; set; }
        [DataMember]
        public string PORTCALL { get; set; }
        [DataMember]
        public string PORTORIGIN { get; set; }
        [DataMember]
        public string VKORG { get; set; }
        [DataMember]
        public string VOYIN { get; set; }
        [DataMember]
        public string VOYOUT { get; set; }
        [DataMember]
        public string ZZBERTH { get; set; }
        [DataMember]
        public string VCN { get; set; }
        [DataMember]
        public string STATUS { get; set; }
        [DataMember]
        public string ZZARRNO { get; set; }

        [DataMember]
        public int SAPPostingID { get; set; }
        [DataMember]
        public string ERRMSG { get; set; }
        [DataMember]
        public string MESSAGETYPE { get; set; }
        
        [DataMember]
        public string AED{ get; set; }
        [DataMember]
        public string AET{ get; set; }
        [DataMember]
        public string DED{ get; set; }
        [DataMember]
        public string DET { get; set; }



        [DataMember]
        public string ZZTIMETO { get; set; }

        [DataMember]
        public string ZZTIMEFROM { get; set; }

        [DataMember]
        public Nullable<System.DateTime> ZZDATEFROM { get; set; }

        [DataMember]
        public Nullable<System.DateTime> ZZDATETO { get; set; }

        [DataMember]
        public string ORDERTYPE { get; set; }

        [DataMember]
        public string SALESORGANIZATION { get; set; }

        [DataMember]
        public string DISTRIBUTIONCHANNEL { get; set; }

        [DataMember]
        public string DIVISION { get; set; }

        [DataMember]
        public string SOLDTOPARTY { get; set; }

        [DataMember]
        public string SHIPTOPARTY { get; set; }

        [DataMember]
        public string PONUMBER { get; set; }

        [DataMember]
        public Nullable<System.DateTime> CREATEDDATE { get; set; }

        [DataMember]
        public string REVENUEPOSTINGID { get; set; }

        [DataMember]
        public string SAPPOSTINGID { get; set; }

        [DataMember]
        public string ITEMNO { get; set; }

        [DataMember]
        public string SALESDOCUMENT { get; set; }

        [DataMember]
        public string ZZDOCKTIME { get; set; }


        [DataMember]
        public string PORTCODE { get; set; }

        [DataMember]
        public int USERID { get; set; }

        [DataMember]
        public int VesselID { get; set; }

    }

    public class SAPMarineDetailsVO
    {
        [DataMember]
        public string Units { get; set; }

        [DataMember]
        public string MaterialCode { get; set; }

        [DataMember]
        public string ITMNUMBER { get; set; }

        [DataMember]
        public int mrrevid { get; set; }

        [DataMember]
        public string ZZTIMETO { get; set; }

        [DataMember]
        public string ZZTIMEFROM { get; set; }

        [DataMember]
        public string ZZDATEFROM { get; set; }

        [DataMember]
        public string ZZDATETO { get; set; }

    }
}
