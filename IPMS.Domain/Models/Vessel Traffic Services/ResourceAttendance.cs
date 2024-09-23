using Core.Repository;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class ResourceAttendance : EntityBase
    {
        public ResourceAttendance()
        {
            this.ResourceAttendanceDtls = new List<ResourceAttendanceDtl>();
        }
        [DataMember]
        public int ResourceAttendanceID { get; set; }
        [DataMember]
        public System.DateTime AttendanceDate { get; set; }
        [DataMember]
        public string Position { get; set; }
        [DataMember]
        public int ShiftID { get; set; }
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
        public  User User { get; set; }
        [DataMember]
        public  User User1 { get; set; }
        [DataMember]
        public  SubCategory SubCategory { get; set; }
        [DataMember]
        public  Shift Shift { get; set; }
        [DataMember]
        public  ICollection<ResourceAttendanceDtl> ResourceAttendanceDtls { get; set; }
    }
}
