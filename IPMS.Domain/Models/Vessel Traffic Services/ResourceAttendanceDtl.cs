using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class ResourceAttendanceDtl : EntityBase
    {
        [DataMember]
        public int ResourceAttendanceDtlID { get; set; }
        [DataMember]
        public int ResourceAttendanceID { get; set; }
        [DataMember]
        public int EmployeeID { get; set; }
        [DataMember]
        public string AttendanceStatus { get; set; }
        [DataMember]
        public int ShiftID { get; set; }
        [DataMember]
        public System.DateTime AttendanceDate { get; set; }
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
        public virtual Employee Employee { get; set; }
        [DataMember]
        public virtual ResourceAttendance ResourceAttendance { get; set; }
        [DataMember]
        public virtual User User { get; set; }
        [DataMember]
        public virtual User User1 { get; set; }
        [DataMember]
        public Shift Shift { get; set; }
    }
}
