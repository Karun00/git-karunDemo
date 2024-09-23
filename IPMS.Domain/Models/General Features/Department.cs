using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models

{
    [DataContract]
    public partial class Department : EntityBase
    {
        //public Department()
        //{
        //    this.BerthMaintenances = new List<BerthMaintenance>();
        //}

        [DataMember]
        public int DepartmentID { get; set; }
        [DataMember]
        public string DepartmentName { get; set; }
        [DataMember]
        public string DepartmentDescription { get; set; }
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
        //[DataMember]
        //public virtual ICollection<BerthMaintenance> BerthMaintenances { get; set; }
        [DataMember]
        public virtual User User { get; set; }
        [DataMember]
        public virtual User User1 { get; set; }
    }
}
