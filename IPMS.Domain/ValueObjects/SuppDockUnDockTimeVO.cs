using System;
using System.Runtime.Serialization;
namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public partial class SuppDockUnDockTimeVO
    {
        [DataMember]
        public int SuppDockUnDockTimeID { get; set; }
        [DataMember]
        public int SuppDryDockID { get; set; }
        [DataMember]
        public string Chamber { get; set; }
        [DataMember]
        public Nullable<System.DateTime> EnteredDockDateTime { get; set; }
        [DataMember]
        public Nullable<System.DateTime> OnBlocksDateTime { get; set; }
        [DataMember]
        public Nullable<System.DateTime> DryDockDateTime { get; set; }
        [DataMember]
        public Nullable<System.DateTime> FinishedDockDateTime { get; set; }
        [DataMember]
        public Nullable<System.DateTime> OffBlocksDateTime { get; set; }
        [DataMember]
        public Nullable<System.DateTime> LeftDockDateTime { get; set; }
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

        [DataMember]
        public string BarkeelCode { get; set; }       
        [DataMember]
        public string TermsandConditionStatus { get; set; }
        [DataMember]
        public string VCN { get; set; }
        [DataMember]
        public string FromDate { get; set; }
        [DataMember]
        public string ToDate { get; set; }       


        [DataMember]
        public virtual ArrivalNotificationVO ArrivalNotificationVo { get; set; }
        //[DataMember]
        //public virtual List<SuppFloatingCraneVO> SuppFloatingCranesVO { get; set; }
        //[DataMember]
        //public virtual SuppHotColdWorkPermitVO SuppHotColdWorkPermitsVO { get; set; }
        [DataMember]
        public string AnyDangerousGoodsonBoard { get; set; }
        [DataMember]
        public string DangerousGoodsClass { get; set; }
        [DataMember]
        public string UNNo { get; set; }
        [DataMember]
        public virtual SuppServiceRequestVO SuppServiceRequestVO { get; set; }
    }
}
