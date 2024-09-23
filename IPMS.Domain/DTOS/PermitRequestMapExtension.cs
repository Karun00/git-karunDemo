using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class PermitRequestMapExtension
    {
        public static List<PermitRequest> MapToEntity(this IEnumerable<PermitRequestVO> vos)
        {
            List<PermitRequest> entities = new List<PermitRequest>();
            if (vos != null)
            {
                foreach (var vo in vos)
                {
                    entities.Add(vo.MapToEntity());
                }
            }
            return entities;
        }
        public static List<PermitRequestVO> MapToDTO(this IEnumerable<PermitRequest> entities)
        {
            List<PermitRequestVO> vos = new List<PermitRequestVO>();
            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    vos.Add(entity.MapToDTO());
                }
            }
            return vos;

        }
        public static PermitRequestVO MapToDTO(this PermitRequest data)
        {
            PermitRequestVO Vo = new PermitRequestVO();
            if (data != null)
            {

                Vo.PermitRequestID = data.PermitRequestID;
                Vo.PortCode = data.PortCode;
                Vo.PermitRequestTypeCode = (data.PermitRequestTypeCode).ToString();
                if (data.permitStatus != null)
                {
                    Vo.permitStatus = (data.permitStatus).ToString();
                }
                if (data.SubCategory1 != null)
                {
                    Vo.permitStatusName = data.SubCategory1.SubCatName;
                }
                Vo.CompanyName = data.CompanyName;
                Vo.DepartmentName = data.DepartmentName;
                Vo.ApplicantFullName = data.ApplicantFullName;
                if (data.ApplicantSurName != null)
                {
                    if (data.ApplicantSurName != "")
                    {
                        Vo.ApplicantSurName = data.ApplicantSurName;
                    }
                }
                if (data.PensionEmployeeNo != null)
                {
                    if (data.PensionEmployeeNo != "")
                    {
                        Vo.PensionEmployeeNo = data.PensionEmployeeNo;
                    }
                }
                if (data.Port != null)
                {
                    Vo.PortName = data.Port.PortName;
                }
                if (data.SubCategory != null)
                {
                    Vo.PermitRequestTypeCodeName = data.SubCategory.SubCatName;
                }
                if (data.IDPassportNo != null)
                {
                    if (data.IDPassportNo != "")
                    {
                        Vo.IDPassportNo = data.IDPassportNo;
                    }
                }
             
               Vo.MobileNo = data.MobileNo;
                Vo.Email = data.Email;
                Vo.Occupation = data.Occupation;
                Vo.HomeAddress = data.HomeAddress;
                Vo.CompanyAddress = data.CompanyAddress;
                Vo.CompanyTelephoneNo = data.CompanyTelephoneNo;
                Vo.CompanyFaxNo = data.CompanyFaxNo;
                Vo.ReferenceNo = data.ReferenceNo;
                Vo.AppealRemarks = data.AppealRemarks;
                Vo.PSOremarkes = data.PSOremarkes;
                Vo.AppealBoardRemarks = data.AppealBoardRemarks;     
                Vo.WorkflowInstanceId = data.WorkflowInstanceId;
                Vo.RecordStatus = data.RecordStatus;
                Vo.CreatedBy = data.CreatedBy;
                Vo.CreatedDate = data.CreatedDate;
                Vo.ModifiedBy = data.ModifiedBy;
                Vo.ModifiedDate = data.ModifiedDate;            
                if (data.PermitRequestAreas != null)
                {
                    Vo.PermitRequestAreas = data.PermitRequestAreas.MapToPermitRequestAreaArray();
                }
                if (data.PermitRequestContractors != null)
                {
                    Vo.PermitRequestContractors = data.PermitRequestContractors.MapToDTOObj();

                }
                if (data.PermitRequestDocuments != null)
                {
                    Vo.PermitRequestDocuments = data.PermitRequestDocuments.ToList().MapToDTO();
                }
                if (data.VehiclePermits != null)
                {
                    Vo.VehiclePermits = data.VehiclePermits.MapToDTOObj();
                    Vo.selectedPermitRequirementCode = data.VehiclePermitRequirementCodes.MapToPermitRequirementCodeArray();
                }
                if (data.VisitorPermits != null)
                {
                    Vo.VisitorPermits = data.VisitorPermits.MapToDTOObj();
                }
                if (data.WharfVehiclePermits != null)
                {
                    Vo.WharfVehiclePermits = data.WharfVehiclePermits.MapToDTOObj();
                    Vo.selectedAccessGates = data.PermitRequestAccessGates.MapToAccessGatesArray();
                }
                if (data.PersonalPermits != null)
                {
                    Vo.PersonalPermits = data.PersonalPermits.MapToDTOObj();
                }
                if (data.IndividualPermitApplicationDetails != null)
                {
                    Vo.IndividualPermitApplicationDetails = data.IndividualPermitApplicationDetails.MapToDTOObj();
                }
                ////////////////////
                if (data.IndividualPersonalPermits != null)
                {
                    Vo.IndividualPersonalPermits = data.IndividualPersonalPermits.MapToDTOObj();
                }
                if (data.IndividualVehiclePermits != null)
                {
                    Vo.IndividualVehiclePermits = data.IndividualVehiclePermits.ToList().MapToDTO();
                }
                if (data.PermitRequestSubAreas != null)
                {
                    Vo.PermitRequestSubAreas = data.PermitRequestSubAreas.MapToPermitRequestSubAreaArray();
                }
                if (data.PermitReasons != null)
                {
                    Vo.PermitReasons = data.PermitReasons.MapToPermitReasonArray();
                }
                if (data.ContractorPermitApplicationDetails != null)
                {
                    Vo.ContractorPermitApplicationDetails = data.ContractorPermitApplicationDetails.MapToDTOObj();
                }
                if (data.ContractorPermitEmployeeDetails != null)
                {
                    Vo.ContractorPermitEmployeeDetails = data.ContractorPermitEmployeeDetails.ToList().MapToDTO();
                }

                ///////////////////
                Vo.FlagSAPS = false;
                Vo.FlagPSSA = false;
                if (data.PermitRequestVerifyedDetails != null)
                {
                    foreach (var lst in data.PermitRequestVerifyedDetails)
                    {
                        if (lst.Flag == "SAPS")
                        {
                            Vo.FlagSAPS = true;
                           // Vo.PermitRequestVerifyedDetailsverifyedbySAPS = data.PermitRequestVerifyedDetails.MapToDTOverifyedbySAPSObj("SAPS");
                        }
                        else { Vo.FlagSAPS = false; }
                        if (lst.Flag == "PSSA")
                        {
                            Vo.FlagPSSA = true;
                            //Vo.PermitRequestVerifyedDetailsverifyedbySSA = data.PermitRequestVerifyedDetails.MapToDTOverifyedbySSAObj("PSSA");
                        }
                        else { Vo.FlagPSSA = false; }
  
                    }
                    Vo.PermitRequestVerifyedDetailsverifyedbySAPS = data.PermitRequestVerifyedDetails.MapToDTOverifyedbySAPSObj("SAPS");
                    Vo.PermitRequestVerifyedDetailsverifyedbySSA = data.PermitRequestVerifyedDetails.MapToDTOverifyedbySSAObj("PSSA");
                   
                  

                }
            }
            return Vo;

        }
        public static PermitRequest MapToEntity(this PermitRequestVO VO)
        {
            PermitRequest data = new PermitRequest();
            if (VO != null)
            {
                data.PermitRequestID = VO.PermitRequestID;
                data.PortCode = VO.PortCode;
                data.PermitRequestTypeCode = VO.PermitRequestTypeCode;
                data.CompanyName = VO.CompanyName;
                data.DepartmentName = VO.DepartmentName;
                data.ApplicantFullName = VO.ApplicantFullName;
                data.ApplicantSurName = VO.ApplicantSurName;
                data.PensionEmployeeNo = VO.PensionEmployeeNo;
                data.IDPassportNo = VO.IDPassportNo;
                data.Occupation = VO.Occupation;
                data.HomeAddress = VO.HomeAddress;
                data.CompanyAddress = VO.CompanyAddress;
                data.CompanyTelephoneNo = VO.CompanyTelephoneNo;
                data.CompanyFaxNo = VO.CompanyFaxNo;
                data.ReferenceNo = VO.ReferenceNo;
                data.AppealRemarks = VO.AppealRemarks;
                data.PSOremarkes = VO.PSOremarkes;
                data.AppealBoardRemarks = VO.AppealBoardRemarks;
                data.WorkflowInstanceId = VO.WorkflowInstanceId;
                data.RecordStatus = VO.RecordStatus;
                data.CreatedBy = VO.CreatedBy;
                data.CreatedDate = VO.CreatedDate;
                data.ModifiedBy = VO.ModifiedBy;
                data.ModifiedDate = VO.ModifiedDate;
                data.MobileNo = VO.MobileNo;
                data.Email = VO.Email;
                data.PermitRequestAreas = VO.PermitRequestAreas.MapToEntityPermitRequestArea(VO.PermitRequestID);
                data.PermitRequestSubAreas = VO.PermitRequestSubAreas.MapToEntityPermitRequestSubArea(VO.PermitRequestID);
                data.PermitReasons = VO.PermitReasons.MapToEntityPermitReason(VO.PermitRequestID);//.MapToEntityPermitRequestArea(VO.PermitRequestID);
               // data.ContractorPermitApplicationDetails = VO.ContractorPermitApplicationDetails.MapToEntity();



                if (VO.PermitRequestContractors != null)
                {
                    data.PermitRequestContractors = new List<PermitRequestContractor>();
                    data.PermitRequestContractors.Add(VO.PermitRequestContractors.MapToEntity());

                }
                if (VO.VehiclePermits != null)
                {
                    data.VehiclePermits = new List<VehiclePermit>();
                    data.VehiclePermits.Add(VO.VehiclePermits.MapToEntity());
                }
                if (VO.VisitorPermits != null)
                {
                    data.VisitorPermits = new List<VisitorPermit>();
                    data.VisitorPermits.Add(VO.VisitorPermits.MapToEntity());
                }
                if (VO.WharfVehiclePermits != null)
                {
                    data.WharfVehiclePermits = new List<WharfVehiclePermit>();
                    data.WharfVehiclePermits.Add(VO.WharfVehiclePermits.MapToEntity());
                }
                if (VO.PersonalPermits != null)
                {
                    data.PersonalPermits = new List<PersonalPermit>();
                    data.PersonalPermits.Add(VO.PersonalPermits.MapToEntity());
                }
                if (VO.IndividualPermitApplicationDetails != null)
                {
                    data.IndividualPermitApplicationDetails = new List<IndividualPermitApplicationDetails>();
                    data.IndividualPermitApplicationDetails.Add(VO.IndividualPermitApplicationDetails.MapToEntity());
                }
                //if (VO.ContractorPermitApplicationDetails != null)
                //{
                //    data.ContractorPermitApplicationDetails = new List<ContractorPermitApplicationDetails>();
                //    data.ContractorPermitApplicationDetails.Add(VO.ContractorPermitApplicationDetails.MapToEntity());
                //}
                    
                //if (VO.IndividualVehiclePermits != null)
                //{
                //    data.IndividualVehiclePermits = new List<IndividualVehiclePermit>();
                //    data.IndividualVehiclePermits.Add(VO.IndividualVehiclePermits.MapToEntity());
                //}
                
               
            }
            return data;
        }
    }
}
