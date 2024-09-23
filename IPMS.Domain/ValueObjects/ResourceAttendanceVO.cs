using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    /// <summary>
    ///  Data Transfer Object for ResourceAttendance
    /// </summary>
    [DataContract]
    public class ResourceAttendanceVO
    {
        [DataMember]
        public int ResourceAttendanceID { get; set; }
        [DataMember]
        public string AttendanceDate { get; set; }
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
        public virtual List<ResourceAttendanceDtlVO> ResourceAttendanceDtls { get; set; }
    }
}
