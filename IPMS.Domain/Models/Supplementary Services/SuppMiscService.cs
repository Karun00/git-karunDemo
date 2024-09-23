using System.Runtime.Serialization;
using Core.Repository;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class SuppMiscService : EntityBase
    {
        [DataMember]
        public int SuppMiscServiceID { get; set; }
        [DataMember]
        public int SuppDryDockID { get; set; }
        [DataMember]
        public int ServiceTypeID { get; set; }
        [DataMember]
        public string Phase { get; set; }
        [DataMember]
        public System.DateTime FromDateTime { get; set; }
        [DataMember]
        public System.DateTime ToDateTime { get; set; }
        [DataMember]
        public long Quantity { get; set; }       
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
        [DataMember]
        public string ServiceTypeCode { get; set; }
        //Added by divya on 30OCt 2017
        [DataMember]
        public long StartMeterReading { get; set; }
        [DataMember]
        public long EndMeterReading { get; set; }
        //End
        [DataMember]
        public virtual SubCategory SubCategory { get; set; }       
        [DataMember]
        public virtual SuppDryDock SuppDryDock { get; set; }
        [DataMember]
        public virtual User User { get; set; }
        [DataMember]
        public virtual User User1 { get; set; }      
        [DataMember]
        public virtual ServiceType ServiceType { get; set; }
    }
}
