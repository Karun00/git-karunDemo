using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    /// <summary>
    ///  Data Transfer Object for ResourceAttendanceDtl
    /// </summary>
    [DataContract]
    public class ResourceAttendanceDtlVO
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
        public string RecordStatus { get; set; }
        [DataMember]
        public int ShiftID { get; set; }
        [DataMember]
        public System.DateTime JoiningDate { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
    }
}
