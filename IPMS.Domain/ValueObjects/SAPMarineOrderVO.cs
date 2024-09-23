using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class SAPMarineOrderVO
    {
        [DataMember]
        public string ZZTIMEFROM { get; set; }
        [DataMember]
        public string ZZTIMETO { get; set; }
        [DataMember]
        public string ZZDATEFROM { get; set; }
        [DataMember]
        public string ZZDATETO { get; set; }
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
        public int PONO { get; set; }
        [DataMember]
        public string ZZDOCKTIME { get; set; }
        [DataMember]
        public string ITEMNO { get; set; }

        [DataMember]
        public string ORDER { get; set; }

        [DataMember]
        public string ERRMSG { get; set; }
        [DataMember]
        public System.DateTime CREATEDDATE { get; set; }
        [DataMember]
        public int SAPPOSTINGID { get; set; }
        [DataMember]
        public string SALESDOCUMENT { get; set; }

        [DataMember]
        public int REVENUEPOSTINGID { get; set; }

        [DataMember]
        public string MESSAGETYPE { get; set; }


        [DataMember]
        public List<SAPMarineOrderItemsInVO> ORDERITEMSIN { get; set; }
       
        [DataMember]
        public List<SAPMarineOrderScheduleLineSXVO> OrderScheduleLineSX { get; set; }
        [DataMember]
        public List<SAPMarineOrderScheduleLinesVO> OrderScheduleLines { get; set; }


        [DataMember]
        public List<SAPMarineOrderOrderHeaderInXVO> ORDERHEADERINX { get; set; }

        [DataMember]
        public List<SAPMarineOrderOrderItemsInXVO> ORDERITEMSINX { get; set; }

        [DataMember]
        public List<SAPMarineOrderReturnVO> RETURN { get; set; }

    }
}
