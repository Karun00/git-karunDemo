using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.DTOS;
using System.Globalization; 

namespace IPMS.Domain.DTOS
{

    public static class LicensingRequestMapExtenstion
    {
        /// <summary>
        /// Data List Transfer from Entity to DTO
        /// </summary>
        /// <param name="licenserequests"></param>
        /// <returns></returns>
        public static List<LicenseRequestVO> MapToDTO(this IEnumerable<LicenseRequest> licenserequests)
        {
            List<LicenseRequestVO> licenserequestVOs = new List<LicenseRequestVO>();
            if (licenserequests != null)
            {
                foreach (var licenserequest in licenserequests)
                {
                    licenserequestVOs.Add(licenserequest.MapToDto());
                }
            }
            return licenserequestVOs;
        }

        /// <summary>
        /// Data List Transfer from DTO tO Entity 
        /// </summary>
        /// <param name="licenserequestVoList"></param>
        /// <returns></returns>
        public static List<LicenseRequest> MapToListEntity(this IEnumerable<LicenseRequestVO> licenserequestVoList)
        {
            List<LicenseRequest> licenserequestlist = new List<LicenseRequest>();

            if (licenserequestVoList != null)
            {
                foreach (var data in licenserequestVoList)
                {
                    licenserequestlist.Add(data.MapToEntity());

                }
            }
            return licenserequestlist;
        }        
              /// <summary>
        /// Data Transfer from Entity to DTO
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static LicenseRequestVO MapToDtoWithPortcode(this LicenseRequest data, string portcode)
        {
            LicenseRequestVO licenserequestVO = new LicenseRequestVO();
            if (data != null)
            {
                licenserequestVO.LicenseRequestID = data.LicenseRequestID;
                licenserequestVO.LicenseRequestType = data.LicenseRequestType;
                licenserequestVO.ReferenceNo = data.ReferenceNo;
                licenserequestVO.RegisteredName = data.RegisteredName;
                licenserequestVO.TradingName = data.TradingName;
                licenserequestVO.RegistrationNumber = data.RegistrationNumber;
                licenserequestVO.VATNumber = data.VATNumber;
                licenserequestVO.IncomeTaxNumber = data.IncomeTaxNumber;
                licenserequestVO.SkillsDevLevyNumber = data.SkillsDevLevyNumber;
                licenserequestVO.BusinessAddressID = data.BusinessAddressID;
                licenserequestVO.PostalAddressID = data.PostalAddressID;
                licenserequestVO.TelephoneNo1 = data.TelephoneNo1;
                licenserequestVO.TelephoneNo2 = data.TelephoneNo2;
                licenserequestVO.FaxNo = data.FaxNo;
                licenserequestVO.AuthorizedContactPersonID = data.AuthorizedContactPersonID;
                licenserequestVO.ValidTaxClearanceCertificate = data.ValidTaxClearanceCertificate;
                licenserequestVO.BBBEEStatus = data.BBBEEStatus;
                licenserequestVO.VerifiedBBBEEStatus = data.VerifiedBBBEEStatus;
                licenserequestVO.BBBEEExemptedMicroEnterprise = data.BBBEEExemptedMicroEnterprise;
                licenserequestVO.PublicLiabilityInsurance = data.PublicLiabilityInsurance;
                // licenserequestVO.WorkflowInstanceID=data.LicenseRequestPorts
                licenserequestVO.CreatedBy = data.CreatedBy;
                licenserequestVO.RecordStatus = data.RecordStatus;
                licenserequestVO.CreatedDate = data.CreatedDate;
                licenserequestVO.ModifiedBy = data.ModifiedBy;
                licenserequestVO.ModifiedDate = data.ModifiedDate;
                licenserequestVO.BusinessAddress = data.BusinessAddress.MapToDTO();
                licenserequestVO.PostalAddress = data.PostalAddress.MapToDTO();
                licenserequestVO.AuthorizedContactPerson = data.AuthorizedContactPerson.MapToDTO();
                licenserequestVO.Bunkerings = data.Bunkerings.MapToDTOObj();
                licenserequestVO.Divings = data.Divings.MapToDtoObj();
                licenserequestVO.FireEquipments = data.FireEquipments.MapToDTOObj();
                licenserequestVO.FireProtections = data.FireProtections.MapToDTOObj();
                licenserequestVO.FloatingCranes = data.FloatingCranes.MapToDTOObj();
                licenserequestVO.PestControls = data.PestControls.MapToDTOObj();
                licenserequestVO.PollutionControls = data.PollutionControls.MapToDTOObj();
                licenserequestVO.Stevedores = data.Stevedores.MapToDTOObj();
                licenserequestVO.LicenseRequestPortsArr = data.LicenseRequestPorts.MapToPortArray();
                licenserequestVO.LicensePortWFArray = data.LicenseRequestPorts.MapToPortWfArray();
                licenserequestVO.AddressCheckbox = CheckUncheck(licenserequestVO.BusinessAddress, licenserequestVO.PostalAddress);

                foreach (var item in data.LicenseRequestPorts)
                {
                    if (item.PortCode == portcode)
                    {
                        licenserequestVO.ReferenceWorkflowInstenceID = item.WorkflowInstanceID;
                    }
                }
                //ReferenceWorkflowInstenceID


                licenserequestVO.LicenseRequestDocuments = data.LicenseRequestDocuments.MapToDto();
            }
            return licenserequestVO;
        }  
        /// <summary>
        /// Data Transfer from Entity to DTO
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static LicenseRequestVO MapToDto(this LicenseRequest data)
        {
            //LicenseRequestVO licenserequestVO = new LicenseRequestVO();
            //if (data != null)
            //{
                 LicenseRequestVO licenserequestVO = new LicenseRequestVO();
                 if (data != null)
                 {
                     licenserequestVO.LicenseRequestID = data.LicenseRequestID;
                     licenserequestVO.LicenseRequestType = data.LicenseRequestType;
                     licenserequestVO.ReferenceNo = data.ReferenceNo;
                     licenserequestVO.RegisteredName = data.RegisteredName;
                     licenserequestVO.TradingName = data.TradingName;
                     licenserequestVO.RegistrationNumber = data.RegistrationNumber;
                     licenserequestVO.VATNumber = data.VATNumber;
                     licenserequestVO.IncomeTaxNumber = data.IncomeTaxNumber;
                     licenserequestVO.SkillsDevLevyNumber = data.SkillsDevLevyNumber;
                     licenserequestVO.BusinessAddressID = data.BusinessAddressID;
                     licenserequestVO.PostalAddressID = data.PostalAddressID;
                     licenserequestVO.TelephoneNo1 = data.TelephoneNo1;
                     licenserequestVO.TelephoneNo2 = data.TelephoneNo2;
                     licenserequestVO.FaxNo = data.FaxNo;
                     licenserequestVO.AuthorizedContactPersonID = data.AuthorizedContactPersonID;
                     licenserequestVO.ValidTaxClearanceCertificate = data.ValidTaxClearanceCertificate;
                     licenserequestVO.BBBEEStatus = data.BBBEEStatus;
                     licenserequestVO.VerifiedBBBEEStatus = data.VerifiedBBBEEStatus;
                     licenserequestVO.BBBEEExemptedMicroEnterprise = data.BBBEEExemptedMicroEnterprise;
                     licenserequestVO.PublicLiabilityInsurance = data.PublicLiabilityInsurance;
                     licenserequestVO.CreatedBy = data.CreatedBy;
                     licenserequestVO.RecordStatus = data.RecordStatus;
                     licenserequestVO.CreatedDate = data.CreatedDate;
                     licenserequestVO.ModifiedBy = data.ModifiedBy;
                     licenserequestVO.ModifiedDate = data.ModifiedDate;
                     licenserequestVO.BusinessAddress = data.BusinessAddress.MapToDTO();
                     licenserequestVO.PostalAddress = data.PostalAddress.MapToDTO();
                     licenserequestVO.AuthorizedContactPerson = data.AuthorizedContactPerson.MapToDTO();
                     licenserequestVO.Bunkerings = data.Bunkerings.MapToDTOObj();
                     licenserequestVO.Divings = data.Divings.MapToDtoObj();
                     licenserequestVO.FireEquipments = data.FireEquipments.MapToDTOObj();
                     licenserequestVO.FireProtections = data.FireProtections.MapToDTOObj();
                     licenserequestVO.FloatingCranes = data.FloatingCranes.MapToDTOObj();
                     licenserequestVO.PestControls = data.PestControls.MapToDTOObj();
                     licenserequestVO.PollutionControls = data.PollutionControls.MapToDTOObj();
                     licenserequestVO.Stevedores = data.Stevedores.MapToDTOObj();
                     licenserequestVO.LicenseRequestPortsArr = data.LicenseRequestPorts.MapToPortArray();
                     licenserequestVO.LicensePortWFArray = data.LicenseRequestPorts.MapToPortWfArray();
                     licenserequestVO.AddressCheckbox = CheckUncheck(licenserequestVO.BusinessAddress, licenserequestVO.PostalAddress);


                     licenserequestVO.LicenseRequestDocuments = data.LicenseRequestDocuments.MapToDto();
                 }
                return licenserequestVO;
           
        }
        /// <summary>
        /// Data Transfer from DTO to Entity
        /// </summary>
        /// <param name="licenserequestvo"></param>
        /// <returns></returns>
        public static LicenseRequest MapToEntity(this LicenseRequestVO licenserequestvo)
        {
            LicenseRequest licenserequest = new LicenseRequest();
            if (licenserequestvo != null)
            {
                licenserequest.LicenseRequestID = licenserequestvo.LicenseRequestID;
                licenserequest.LicenseRequestType = licenserequestvo.LicenseRequestType;
                licenserequest.ReferenceNo = licenserequestvo.ReferenceNo;
                licenserequest.RegisteredName = licenserequestvo.RegisteredName;
                licenserequest.TradingName = licenserequestvo.TradingName;
                licenserequest.RegistrationNumber = licenserequestvo.RegistrationNumber;
                licenserequest.VATNumber = licenserequestvo.VATNumber;
                licenserequest.IncomeTaxNumber = licenserequestvo.IncomeTaxNumber;
                licenserequest.SkillsDevLevyNumber = licenserequestvo.SkillsDevLevyNumber;
                licenserequest.BusinessAddressID = licenserequestvo.BusinessAddressID;
                licenserequest.PostalAddressID = licenserequestvo.PostalAddressID;
                licenserequest.TelephoneNo1 = licenserequestvo.TelephoneNo1;
                licenserequest.TelephoneNo2 = licenserequestvo.TelephoneNo2;

                licenserequest.FaxNo = licenserequestvo.FaxNo;
                licenserequest.AuthorizedContactPersonID = licenserequestvo.AuthorizedContactPersonID;
                licenserequest.ValidTaxClearanceCertificate = licenserequestvo.ValidTaxClearanceCertificate;
                licenserequest.BBBEEStatus = licenserequestvo.BBBEEStatus;
                licenserequest.VerifiedBBBEEStatus = licenserequestvo.VerifiedBBBEEStatus;
                licenserequest.BBBEEExemptedMicroEnterprise = licenserequestvo.BBBEEExemptedMicroEnterprise;
                licenserequest.PublicLiabilityInsurance = licenserequestvo.PublicLiabilityInsurance;
                licenserequest.CreatedBy = licenserequestvo.CreatedBy;
                licenserequest.RecordStatus = licenserequestvo.RecordStatus;
                licenserequest.CreatedDate = licenserequestvo.CreatedDate;
                licenserequest.ModifiedBy = licenserequestvo.ModifiedBy;
                licenserequest.ModifiedDate = licenserequestvo.ModifiedDate;
                licenserequest.BusinessAddress = licenserequestvo.BusinessAddress.MapToEntity();
                licenserequest.PostalAddress = licenserequestvo.PostalAddress.MapToEntity();
                licenserequest.AuthorizedContactPerson = licenserequestvo.AuthorizedContactPerson.MapToEntity();
                licenserequest.LicenseRequestPorts = licenserequestvo.LicenseRequestPortsArr.MapToEntityPort(licenserequestvo.LicenseRequestID);

                licenserequest.LicenseRequestDocuments = licenserequestvo.LicenseRequestDocuments.MapToEntity();

                licenserequest.Bunkerings = new List<Bunkering>();
                licenserequest.Bunkerings.Add(licenserequestvo.Bunkerings.MapToEntity());

                licenserequest.Divings = new List<Diving>();
                licenserequest.Divings.Add(licenserequestvo.Divings.MapToEntity());

                licenserequest.FireEquipments = new List<FireEquipment>();
                licenserequest.FireEquipments.Add(licenserequestvo.FireEquipments.MapToEntity());

                licenserequest.FireProtections = new List<FireProtection>();
                licenserequest.FireProtections.Add(licenserequestvo.FireProtections.MapToEntity());

                licenserequest.FloatingCranes = new List<FloatingCrane>();
                licenserequest.FloatingCranes.Add(licenserequestvo.FloatingCranes.MapToEntity());


                licenserequest.PestControls = new List<PestControl>();
                licenserequest.PestControls.Add(licenserequestvo.PestControls.MapToEntity());

                licenserequest.PollutionControls = new List<PollutionControl>();
                licenserequest.PollutionControls.Add(licenserequestvo.PollutionControls.MapToEntity());

                licenserequest.Stevedores = new List<Stevedore>();
                licenserequest.Stevedores.Add(licenserequestvo.Stevedores.MapToEntity());
            }
            return licenserequest;
        }


        public static Boolean CheckUncheck(AddressVO BusinessAddress, AddressVO PostalAddress)
        {
            
                int check = 4;
                Boolean AddressCheckbox = false;
                if (PostalAddress != null)
                {
                if (BusinessAddress != null)
                {
                    if ((BusinessAddress.NumberStreet.ToString()).Trim() ==
                        (PostalAddress.NumberStreet.ToString()).Trim())
                    {
                        check = check - 1;
                    }
                    if ((BusinessAddress.Suburb.ToString()).Trim() == (PostalAddress.Suburb.ToString()).Trim())
                    {
                        check = check - 1;
                    }
                    if ((BusinessAddress.TownCity.ToString()).Trim() == (PostalAddress.TownCity.ToString()).Trim())
                    {
                        check = check - 1;
                    }
                    if ((BusinessAddress.PostalCode.ToString()).Trim() == (PostalAddress.PostalCode.ToString()).Trim())
                    {
                        check = check - 1;
                    }
                    if (check <= 0)
                    {
                        AddressCheckbox = true;
                    }
                    else
                    {
                        AddressCheckbox = false;
                    }
                }
            }
            return AddressCheckbox;
        }
    
    
    }
}
