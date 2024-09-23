using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPMS.Domain.Models
{
    public partial class PermitRequest : EntityBase
    {
        public PermitRequest()
        {
            this.VehiclePermitRequirementCodes = new List<VehiclePermitRequirementCode>();
            this.PermitRequestAccessGates = new List<PermitRequestAccessGates>();
            this.PermitRequestAreas = new List<PermitRequestArea>();
            this.PermitRequestContractors = new List<PermitRequestContractor>();
            this.PermitRequestDocuments = new List<PermitRequestDocument>();
            this.PermitRequestVerifyedDetails = new List<PermitRequestVerifyedDetail>();
            this.VehiclePermits = new List<VehiclePermit>();
            this.VisitorPermits = new List<VisitorPermit>();
            this.WharfVehiclePermits = new List<WharfVehiclePermit>();
            this.PersonalPermits = new List<PersonalPermit>();
            this.IndividualPermitApplicationDetails = new List<IndividualPermitApplicationDetails>();
           this.IndividualVehiclePermits = new List<IndividualVehiclePermit>();
           this.IndividualPersonalPermits = new List<IndividualPersonalPermit>();
           this.PermitReasons = new List<PermitReason>();
           this.PermitRequestSubAreas = new List<PermitRequestSubArea>();
           this.ContractorPermitApplicationDetails = new List<ContractorPermitApplicationDetails>();
           this.ContractorPermitEmployeeDetails = new List<ContractorPermitEmployeeDetails>();
         
        }

        public int PermitRequestID { get; set; }
        public string PortCode { get; set; }
        public string PermitRequestTypeCode { get; set; }
        public string CompanyName { get; set; }
        public string DepartmentName { get; set; }
        public string ApplicantFullName { get; set; }
        public string ApplicantSurName { get; set; }
        public string PensionEmployeeNo { get; set; }
        public string IDPassportNo { get; set; }
        public string Occupation { get; set; }
        public string HomeAddress { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyTelephoneNo { get; set; }
        public string CompanyFaxNo { get; set; }
        public Nullable<int> WorkflowInstanceId { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string ReferenceNo { get; set; }
        public string permitStatus { get; set; }
        public string PSOremarkes { get; set; }
        public string AppealRemarks { get; set; }
        public string AppealBoardRemarks { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public  User User { get; set; }
        public  User User1 { get; set; }
        public  SubCategory SubCategory { get; set; }
        public  SubCategory SubCategory1 { get; set; }
        public  Port Port { get; set; }
        public  WorkflowInstance WorkflowInstance { get; set; }
        public  ICollection<PermitRequestArea> PermitRequestAreas { get; set; }
        public  ICollection<PermitRequestContractor> PermitRequestContractors { get; set; }
        public  ICollection<PermitRequestDocument> PermitRequestDocuments { get; set; }
        public  ICollection<PermitRequestVerifyedDetail> PermitRequestVerifyedDetails { get; set; }
        public  ICollection<VehiclePermit> VehiclePermits { get; set; }
        public  ICollection<VisitorPermit> VisitorPermits { get; set; }
        public  ICollection<WharfVehiclePermit> WharfVehiclePermits { get; set; }
        public  ICollection<PersonalPermit> PersonalPermits { get; set; }
        public  ICollection<VehiclePermitRequirementCode> VehiclePermitRequirementCodes { get; set; }
        public  ICollection<PermitRequestAccessGates> PermitRequestAccessGates { get; set; }
        public ICollection<IndividualPermitApplicationDetails> IndividualPermitApplicationDetails { get; set; }
        public ICollection<IndividualVehiclePermit> IndividualVehiclePermits { get; set; }
        public ICollection<IndividualPersonalPermit> IndividualPersonalPermits { get; set; }
        public ICollection<PermitReason> PermitReasons { get; set; }
        public ICollection<PermitRequestSubArea> PermitRequestSubAreas{ get; set; }
       public ICollection<ContractorPermitApplicationDetails> ContractorPermitApplicationDetails { get; set; }
       public ICollection<ContractorPermitEmployeeDetails> ContractorPermitEmployeeDetails { get; set; }
     
        [NotMapped]
        public string PermitRequestType { get; set; }
        [NotMapped]
        public string Remarks { get; set; }
        [NotMapped]
        public string PermitNO { get; set; }
        [NotMapped]
        public string PortName { get; set; }
        [NotMapped]
        public string PermittedAreaName { get; set; }
        [NotMapped]
        public System.DateTime FromDate { get; set; }
        [NotMapped]
        public System.DateTime ToDate { get; set; }
        
       
    }


}
