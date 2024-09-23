using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public partial class SuppMiscServiceVO
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
        public string FromDateTime { get; set; }
        [DataMember]
        public string ToDateTime { get; set; }
        [DataMember]
        public long Quantity { get; set; }
        //Added by divya on 30OCt 2017
        [DataMember]
        public long StartMeterReading { get; set; }
        [DataMember]
        public long EndMeterReading { get; set; }
        //End
        [DataMember]
        public string UOMCode { get; set; }
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
        [DataMember]
        public ICollection<SuppMiscServiceVO> MiscServiceTypes { get; set; }

        [DataMember]
        public ICollection<SubCategoryVO> PhaseTypes { get; set; }

        [DataMember]
        public string ServiceTypeName { get; set; }       
        [DataMember]
        public string VCN { get; set; }
        [DataMember]
        public string VesselName { get; set; }
        [DataMember]
        public string VesselAgent { get; set; }
        [DataMember]
        public string RequestFromDate { get; set; }
        [DataMember]
        public string RequestToDate { get; set; }

      
    }
}
