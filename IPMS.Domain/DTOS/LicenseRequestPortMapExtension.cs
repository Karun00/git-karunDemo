using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Domain.DTOS
{
    public static class LicenseRequestPortMapExtension
    {
        /// <summary>
        /// Data List Transfer from Entity to DTO
        /// </summary>
        /// <param name="licenseRequestPort"></param>
        /// <returns></returns>
        public static List<LicenseRequestPortVO> MapToDto(this IEnumerable<LicenseRequestPort> licenseRequestPort)
        {
            var licenseRequestPortVoList = new List<LicenseRequestPortVO>();
            if (licenseRequestPort != null)
            {
                foreach (var item in licenseRequestPort)
                {
                    licenseRequestPortVoList.Add(item.MapToDto());
                }
            }
            return licenseRequestPortVoList;
        }

        /// <summary>
        /// Data List Transfer from DTO to Entity
        /// </summary>
        /// <param name="licenseRequestPortVoList"></param>
        /// <returns></returns>
        public static List<LicenseRequestPort> MapToEntity(this IEnumerable<LicenseRequestPortVO> licenseRequestPortVoList)
        {
            var licenseRequestPort = new List<LicenseRequestPort>();
            if (licenseRequestPortVoList != null)
            {
                foreach (var item in licenseRequestPortVoList)
                {
                    licenseRequestPort.Add(item.MapToEntity());
                }
            }
            return licenseRequestPort;
        }


        /// <summary>
        /// Data Transfer from Entity to DTO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static LicenseRequestPortVO MapToDto(this LicenseRequestPort data)
        {
            LicenseRequestPortVO licenseRequestPortVO = new LicenseRequestPortVO();
            if (data != null)
            {
                licenseRequestPortVO.LicenseRequestID = data.LicenseRequestID;
                licenseRequestPortVO.LicenseRequestPortID = data.LicenseRequestPortID;
                licenseRequestPortVO.PortCode = data.PortCode;
                licenseRequestPortVO.WFStatus = data.WFStatus;
                licenseRequestPortVO.VerifiedBy = data.VerifiedBy;
                licenseRequestPortVO.VerifiedDate = data.VerifiedDate;
                licenseRequestPortVO.ApprovedBy = data.ApprovedBy;
                licenseRequestPortVO.ApprovedDate = data.ApprovedDate;
                licenseRequestPortVO.RejectComments = data.RejectComments;
                licenseRequestPortVO.WorkflowInstanceID = data.WorkflowInstanceID;
                licenseRequestPortVO.RecordStatus = data.RecordStatus;
                licenseRequestPortVO.CreatedBy = data.CreatedBy;
                licenseRequestPortVO.CreatedDate = data.CreatedDate;
                licenseRequestPortVO.ModifiedBy = data.ModifiedBy;
                licenseRequestPortVO.ModifiedDate = data.ModifiedDate;
            }
            return licenseRequestPortVO;


        }

        /// <summary>
        /// Data Transfer from DTO to Entity
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public static LicenseRequestPort MapToEntity(this LicenseRequestPortVO vo)
        {
            LicenseRequestPort document = new LicenseRequestPort();
            if (vo != null)
            {
                document.LicenseRequestID = vo.LicenseRequestID;
                document.LicenseRequestPortID = vo.LicenseRequestPortID;
                document.PortCode = vo.PortCode;
                document.WFStatus = vo.WFStatus;
                document.VerifiedBy = vo.VerifiedBy;
                document.VerifiedDate = vo.VerifiedDate;
                document.ApprovedBy = vo.ApprovedBy;
                document.ApprovedDate = vo.ApprovedDate;
                document.RejectComments = vo.RejectComments;
                document.WorkflowInstanceID = vo.WorkflowInstanceID;
                document.RecordStatus = vo.RecordStatus;
                document.CreatedBy = vo.CreatedBy;
                document.CreatedDate = vo.CreatedDate;
                document.ModifiedBy = vo.ModifiedBy;
                document.ModifiedDate = vo.ModifiedDate;
            }
            return document;
        }


        /// <summary>
        /// Data Transfer from LicenseRequestPort List data to Array
        /// </summary>
        /// <param name="licenseRequestPorts"></param>
        /// <returns></returns>
        public static List<string> MapToPortArray(this ICollection<LicenseRequestPort> licenseRequestPorts)
        {
            List<string> LicensePortArray = new List<string>();
            if (licenseRequestPorts != null)
            {
                foreach (var licenseRequestPort in licenseRequestPorts)
                {
                    LicensePortArray.Add(licenseRequestPort.PortCode);

                }
            }
            return LicensePortArray;
        }

        public static List<string> MapToPortWfArray(this ICollection<LicenseRequestPort> licenseRequestPorts)
        {
            List<string> LicensePortWFArray = new List<string>();
            if (licenseRequestPorts != null)
            {
                foreach (var licenseRequestPort in licenseRequestPorts)
                {
                    LicensePortWFArray.Add(licenseRequestPort.PortCode + "@" + licenseRequestPort.WorkflowInstance.WorkflowTaskCode);
                }
            }
            return LicensePortWFArray;
        }


        /// <summary>
        /// Data Transfer from Array to LicenseRequestPort List
        /// </summary>
        /// <param name="LicensePortArrays"></param>
        /// <param name="licenseRequestID"></param>
        /// <returns></returns>
        public static List<LicenseRequestPort> MapToEntityPort(this List<string> LicensePortArrays, int licenseRequestID)
        {


            List<LicenseRequestPort> licenseRequestPorts = new List<LicenseRequestPort>();
            if (LicensePortArrays != null)
            {
                foreach (var LicensePortArray in LicensePortArrays)
                {
                    LicenseRequestPort licenseRequestPort = new LicenseRequestPort();
                    licenseRequestPort.LicenseRequestID = licenseRequestID;
                    licenseRequestPort.PortCode = LicensePortArray;

                    licenseRequestPorts.Add(licenseRequestPort);
                }
            }
            return licenseRequestPorts;
        }


    }
}
