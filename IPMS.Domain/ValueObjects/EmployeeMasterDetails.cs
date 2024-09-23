using Core.Repository;
using System;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class EmployeeMasterDetails : EntityBase
    {
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
        public string Name { get; set; }
        [DataMember]
        public string EmpName { get; set; }
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
        public string PersonalNumber { get; set; }
        [DataMember]
        public string Gender { get; set; }
        [DataMember]
        public string GenderCode { get; set; }
        [DataMember]
        public string DepartmentCode { get; set; }
        [DataMember]
        public string DepartmentName { get; set; }
        [DataMember]
        public string DesignationCode { get; set; }
        [DataMember]
        public string DesignationName { get; set; }
        [DataMember]
        public string BusinessUnitCode { get; set; }
        [DataMember]
        public string BusinessUnitName { get; set; }
        [DataMember]
        public string CostCenterCode { get; set; }
        [DataMember]
        public string CostCenterName { get; set; }
        [DataMember]
        public string PayrollAreaCode { get; set; }
        [DataMember]
        public string PayrollAreaName { get; set; }
        [DataMember]
        public string IDNo { get; set; }
        [DataMember]
        public string PSGroupCode { get; set; }
        [DataMember]
        public string PSGroupName { get; set; }
        [DataMember]
        public string PersonalSubAreaCode { get; set; }
        [DataMember]
        public string PersonalSubAreaName { get; set; }
        [DataMember]
        public string OrganizationalUnitCode { get; set; }
        [DataMember]
        public string OrganizationalUnitName { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
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
        public string PSGroup { get; set; }
        [DataMember]
        public string PersonalSubArea { get; set; }
        [DataMember]
        public string OrganizationalUnit { get; set; }
        [DataMember]
        public string AttendanceStatus { get; set; }
        [DataMember]
        public int ResourceAttendanceID { get; set; }
        [DataMember]
        public string ShiftName { get; set; }
        [DataMember]
        public int ShiftID { get; set; }

        // -- Added by sandeep on 06-10-2014
        [DataMember]
        public Nullable<long> GrossWeightTonnage { get; set; }
        [DataMember]
        public Nullable<long> DeadWeightTonnage { get; set; }
        // -- end
    }
}
