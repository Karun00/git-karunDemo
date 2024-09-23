using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class SAPPostingVO
    {
        [DataMember]
        public string VCN { get; set; }
        [DataMember]
        public string VesselName { get; set; }
        [DataMember]
        public string Item { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public int SAPPostingID { get; set; }
        [DataMember]
        public string MessageType { get; set; }
        [DataMember]
        public string ReferenceNo { get; set; }
        [DataMember]
        public string PostingStatus { get; set; }
        [DataMember]
        public string TransmitData { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }

        //By Mahesh : For Arrival Reason visit and vesseltype data.
        [DataMember]
        public string ReasonForVisit { get; set; }
        [DataMember]
        public string VesselType { get; set; }
         [DataMember]
        public string ReceivedMarineOrderNo { get; set; }
        
        //

        [DataMember]
        public string MsgType { get; set; }

        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public int AgentAccountID { get; set; }
        [DataMember]
        public string AccountNo { get; set; }
        [DataMember]
        public string NotificationTemplateCode { get; set; }
        [DataMember]
        public string SAPReferenceNo { get; set; }
        [DataMember]
        public string SystemNotificationStatus { get; set; }
        [DataMember]
        public string SMSStatus { get; set; }
        [DataMember]
        public string EmailStatus { get; set; }
        [DataMember]
        public string UserType { get; set; }
        [DataMember]
        public int UserTypeId { get; set; }
        [DataMember]
        public string Reason { get; set; }

        [DataMember]
        public string ReceavedARRNO { get; set; }
        [DataMember]
        public string Isadd { get; set; }
        [DataMember]
        public string IsView { get; set; }
        [DataMember]
        public string IsUpdate { get; set; }
        [DataMember]
        public string RevinueAccountNo { get; set; }
        [DataMember]
        public string MarineAccNo { get; set; }
        [DataMember]
        public string PostStatus { get; set; }
        [DataMember]
        public int MarinePostingId { get; set; }
        
        [DataMember]
        public string SAPInvoice { get; set; }
        [DataMember]
        public string IsSapview { get; set; }

        [DataMember]
        public string IsRepost { get; set; }

        [DataMember]
        public string IMONo { get; set; }
        
        [DataMember]
        public string RevenueAgentAccNo { get; set; }

        [DataMember]
        public ICollection<SubCategoryVO> Reasons { get; set; }
    }

    [DataContract]
    public class VesselSAPPostingVO
    {
        [DataMember]
        public string VesselName { get; set; }
        [DataMember]
        public int? VesselID { get; set; }
        [DataMember]
        public string IMONo { get; set; }
        [DataMember]
        public string VesselType { get; set; }
        [DataMember]
        public string VesselTypeName { get; set; }
        [DataMember]
        public Nullable<decimal> LengthOverallInM { get; set; }
        [DataMember]
        public Nullable<decimal> BeamInM { get; set; }
        [DataMember]
        public string CallSign { get; set; }
        [DataMember]
        public string VesselNationality { get; set; }
        [DataMember]
        public Nullable<decimal> GrossRegisteredTonnageInMT { get; set; }
        [DataMember]
        public Nullable<decimal> DeadWeightTonnageInMT { get; set; }
        [DataMember]
        public string SAPAccountNo { get; set; }
        [DataMember]
        public string TransmitData { get; set; }


        [DataMember]
        public string VSNAME { get; set; }

        [DataMember]
        public string PortOfRegistry { get; set; }

        [DataMember]
        public string NationalityCode { get; set; }


        [DataMember]
        public string sappoststatus { get; set; }



        [DataMember]
        public System.DateTime IDATE { get; set; }

        [DataMember]
        public string VKORG { get; set; }



    }
}
