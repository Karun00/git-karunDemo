using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{

    public partial class PermitRequestSearchVO
    {
        public System.DateTime RequestFrom { get; set; }

        public System.DateTime RequestTo { get; set; }

        public string PortCode { get; set; }

    }
    public partial class PermitRequestVO
    {
        public int PermitRequestID { get; set; }
        public string PortCode { get; set; }
        public string PortName { get; set; }
        public string PermitRequestTypeCode { get; set; }
        public string PermitRequestTypeCodeName { get; set; }
        public string ReferenceNo { get; set; }
        public string permitStatus { get; set; }
        public string PSOremarkes { get; set; }
        public string AppealRemarks { get; set; }
        public string AppealBoardRemarks { get; set; }
        public string permitStatusName { get; set; }
        public string CompanyName { get; set; }
        public string DepartmentName { get; set; }
        public string ApplicantFullName { get; set; }
        public string ApplicantSurName { get; set; }
        public string PensionEmployeeNo { get; set; }
        public string IDPassportNo { get; set; }
        public string Occupation { get; set; }
        public string HomeAddress { get; set; }
        //public string EmailAddress { get; set; }
        public string CompanyAddress { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        // ClearenceFlag
        public bool FlagSAPS { get; set; }
        public bool FlagPSSA { get; set; }
        public string CompanyTelephoneNo { get; set; }
        public string CompanyFaxNo { get; set; }
        public int Flag { get; set; }
        public string Status { get; set; }
        public Nullable<int> WorkflowInstanceId { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public  WorkFlowInstanceVO WorkflowInstance { get; set; }
        //public  List<PermitRequestAreaVO> PermitRequestAreas { get; set; }
        public List<string> PermitRequestAreas { get; set; }
        //public ICollection<PermitRequestSubAreaVO> PermitRequestSubAreas { get; set; }
        public List<string> PermitRequestSubAreas { get; set; }
        public PermitRequestContractorVO PermitRequestContractors { get; set; }
        public List<PermitRequestDocumentVO> PermitRequestDocuments { get; set; }
        public VehiclePermitVO VehiclePermits { get; set; }
        public VisitorPermitVO VisitorPermits { get; set; }
        public WharfVehiclePermitVO WharfVehiclePermits { get; set; }
        public PersonalPermitVO PersonalPermits { get; set; }
        public IndividualPermitAppliactionDetailsVO IndividualPermitApplicationDetails { get; set; }

        public PermitRequestVerifyedDetailVO PermitRequestVerifyedDetailsverifyedbySSA  { get; set; }
        public PermitRequestVerifyedDetailVO PermitRequestVerifyedDetailsverifyedbySAPS{ get; set; }
        public List<PermitRequestVerifyedDocumentVO> PermitRequestverifyedbySSADocuments { get; set; }
        public List<PermitRequestVerifyedDocumentVO> PermitRequestverifyedbySAPSDocuments { get; set; }
        public List<string> selectedAccessGates { get; set; }
        public List<string> selectedPermitRequirementCode { get; set; }
      //  public IndividualVehiclePermitVO IndividualVehiclePermits { get; set; }
        public IndividualPersonalPermitVO IndividualPersonalPermits { get; set; }
        public List<string> PermitReasons { get; set; }
        public ContractorPermitApplicationDetailsVO ContractorPermitApplicationDetails { get; set; }
        public List<ContractorPermitEmployeeDetailsVO> ContractorPermitEmployeeDetails { get; set; }
        public List<IndividualVehiclePermitVO> IndividualVehiclePermits { get; set; }

        //public bool FlagSAPS { get; set; }
        //public bool FlagPSSA { get; set; }

        //public bool FlagSAPS(string privilegeCode)
        //{
        //    return FlagSAPS(privilegeCode);
        //}
        //public bool FlagPSSA
        //{
        //    get
        //    {
        //        return FlagPSSA("ADD");
        //    }
        //}
        //public bool FlagSAPS
        //{
        //    get
        //    {
        //        return FlagSAPS("VIEW");
        //    }
        //}

    }
}
