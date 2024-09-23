using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class Employee : EntityBase
    {
        public Employee()
        {
            this.ResourceEmployeeGroups = new List<ResourceEmployeeGroup>();
            this.ResourceAttendanceDtls = new List<ResourceAttendanceDtl>();
            // -- Added by sandeep on 20-8-2014
            this.SuppServiceResourceAllocs = new List<SuppServiceResourceAlloc>();
            // -- end
        }

        [DataMember]
        public int EmployeeID { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string SAPNumber { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Initials { get; set; }
        [DataMember]
        public System.DateTime BirthDate { get; set; }
        [DataMember]
        public Nullable<byte> Age { get; set; }
        [DataMember]
        public System.DateTime JoiningDate { get; set; }
        [DataMember]
        public Nullable<byte> YearsofService { get; set; }
        [DataMember]
        public string OfficialMobileNo { get; set; }
        [DataMember]
        public string PersonalMobileNo { get; set; }
        [DataMember]
        public string EmailID { get; set; }
        [DataMember]
        public string Gender { get; set; }
        [DataMember]
        public string Department { get; set; }
        [DataMember]
        public string Designation { get; set; }
        [DataMember]
        public string BusinessUnit { get; set; }
        [DataMember]
        public string CostCenter { get; set; }
        [DataMember]
        public string PayrollArea { get; set; }
        [DataMember]
        public string IDNo { get; set; }
        [DataMember]
        public string PSGroup { get; set; }
        [DataMember]
        public string PersonalSubArea { get; set; }
        [DataMember]
        public string OrganizationalUnit { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        [DataMember]
        public  SubCategory SubCategory { get; set; }
        [DataMember]
        public  SubCategory SubCategory1 { get; set; }
        [DataMember]
        public  User User { get; set; }
        [DataMember]
        public  SubCategory SubCategory2 { get; set; }
        [DataMember]
        public  SubCategory SubCategory3 { get; set; }
        [DataMember]
        public  SubCategory SubCategory4 { get; set; }
        [DataMember]
        public  User User1 { get; set; }
        [DataMember]
        public  SubCategory SubCategory5 { get; set; }
        [DataMember]
        public  SubCategory SubCategory6 { get; set; }
        [DataMember]
        public  SubCategory SubCategory7 { get; set; }
        [DataMember]
        public  SubCategory SubCategory8 { get; set; }
        [DataMember]
        public  ICollection<ResourceEmployeeGroup> ResourceEmployeeGroups { get; set; }
        [DataMember]
        public  ICollection<ResourceAttendanceDtl> ResourceAttendanceDtls { get; set; }
        // -- Added by sandeep on 20-8-2014

        [DataMember]
        public  ICollection<SuppServiceResourceAlloc> SuppServiceResourceAllocs { get; set; }
        [DataMember]
        public Nullable<long> GrossWeightTonnage { get; set; }
        [DataMember]
        public Nullable<long> DeadWeightTonnage { get; set; }

        // -- end

    }

}
