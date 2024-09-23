using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
using IPMS.Domain.Models;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class UserMasterVO : EntityBase
    {
        [DataMember]
        public int UserID { get; set; }
        [DataMember]
        public string SubCatCode { get; set; }
        [DataMember]
        public string UserType { get; set; }
        [DataMember]
        public string SubCatName { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public int UserTypeID { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string RoleName { get; set; }
        [DataMember]
        public string ContactNo { get; set; }
        [DataMember]
        public string Designation { get; set; }
        [DataMember]
        public string EmailID { get; set; }
        [DataMember]
        public string ReferenceNo { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public virtual ICollection<RoleVO> Roles { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public Nullable<int> WorkflowInstanceId { get; set; }
        [DataMember]
        public string AnonymousUserYn { get; set; }
        [DataMember]
        public string PWD { get; set; }
        [DataMember]
        public string IsFirstTimeLogin { get; set; }
        [DataMember]
        public Nullable<System.DateTime> PwdExpirtyDate { get; set; }
        [DataMember]
        public int IncorrectLogins { get; set; }
        [DataMember]
        public Nullable<System.DateTime> LoginTime { get; set; }
        [DataMember]
        public string DormantStatus { get; set; }

        [DataMember]
        public string ReasonForAccess { get; set; }
        [DataMember]
        public string ValidFromDate { get; set; }
        [DataMember]
        public string ValidToDate { get; set; }

        [DataMember]
        public List<string> PortNames { get; set; }
        [DataMember]
        public string ArrivalCreatedAgent { get; set; }

        [DataMember]
        public virtual ICollection<UserPortVO> UserPorts { get; set; }
    }
}
