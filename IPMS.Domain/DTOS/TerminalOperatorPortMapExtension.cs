using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Domain.DTOS
{
    public static class TerminalOperatorPortMapExtension
    {
        /// <summary>
        /// Data List Transfer from Entity to DTO
        /// </summary>
        /// <param name="licenseRequestPort"></param>
        /// <returns></returns>
        public static List<TerminalOperatorPortVO> MapToDTO(this IEnumerable<TerminalOperatorPort> terminaloperatorPort)
        {
            var terminaloperatorPortVoList = new List<TerminalOperatorPortVO>();
            foreach (var item in terminaloperatorPort)
            {
                terminaloperatorPortVoList.Add(item.MapToDTO());
            }
            return terminaloperatorPortVoList;
        }

        /// <summary>
        /// Data List Transfer from DTO to Entity
        /// </summary>
        /// <param name="licenseRequestPortVoList"></param>
        /// <returns></returns>
        public static List<TerminalOperatorPort> MapToEntity(this IEnumerable<TerminalOperatorPortVO> terminaloperatorPortVoList)
        {
            var terminaloperatorPort = new List<TerminalOperatorPort>();
            foreach (var item in terminaloperatorPortVoList)
            {
                terminaloperatorPort.Add(item.MapToEntity());
            }
            return terminaloperatorPort;
        }


        /// <summary>
        /// Data Transfer from Entity to DTO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static TerminalOperatorPortVO MapToDTO(this TerminalOperatorPort data)
        {
            TerminalOperatorPortVO terminaloperatorPortVo = new TerminalOperatorPortVO();
            terminaloperatorPortVo.TerminalOperatorID = data.TerminalOperatorID;
            terminaloperatorPortVo.TerminalOperatorPortID = data.TerminalOperatorPortID;
            terminaloperatorPortVo.PortCode = data.PortCode;
            terminaloperatorPortVo.RecordStatus = data.RecordStatus;
            terminaloperatorPortVo.CreatedBy = data.CreatedBy;
            terminaloperatorPortVo.CreatedDate = data.CreatedDate;
            terminaloperatorPortVo.ModifiedBy = data.ModifiedBy;
            terminaloperatorPortVo.ModifiedDate = data.ModifiedDate;
            return terminaloperatorPortVo;


        }
        
        /// <summary>
        /// Data Transfer from DTO to Entity
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public static TerminalOperatorPort MapToEntity(this TerminalOperatorPortVO vo)
        {
            TerminalOperatorPort document = new TerminalOperatorPort();
            document.TerminalOperatorID = vo.TerminalOperatorID;
            document.TerminalOperatorPortID = vo.TerminalOperatorPortID;
            document.PortCode = vo.PortCode;
            document.RecordStatus = vo.RecordStatus;
            document.CreatedBy = vo.CreatedBy;
            document.CreatedDate = vo.CreatedDate;
            document.ModifiedBy = vo.ModifiedBy;
            document.ModifiedDate = vo.ModifiedDate;

            return document;
        }

        public static List<string> MapToPortArrayT(this ICollection<TerminalOperatorPort> terminalOperatorPorts)
        {
            List<string> TerminalOperatorPortArray = new List<string>();
            if (terminalOperatorPorts != null)
            {
                foreach (var licenseRequestPort in terminalOperatorPorts)
                {
                    TerminalOperatorPortArray.Add(licenseRequestPort.PortCode);
                }
            }
            return TerminalOperatorPortArray;
        }

        /// <summary>
        /// Data Transfer from Array to LicenseRequestPort List
        /// </summary>
        /// <param name="LicensePortArrays"></param>
        /// <param name="licenseRequestID"></param>
        /// <returns></returns>
        public static List<TerminalOperatorPort> MapToEntityPortT(this List<string> LicensePortArrays, int terminalperatorID)
        {


            List<TerminalOperatorPort> terminalOperatorPorts = new List<TerminalOperatorPort>();
            if (LicensePortArrays != null)
            {
                foreach (var TerminalPortArray in LicensePortArrays)
                {
                    TerminalOperatorPort terminaloperatorPort = new TerminalOperatorPort();
                    terminaloperatorPort.TerminalOperatorID = terminalperatorID;
                    terminaloperatorPort.PortCode = TerminalPortArray;

                    terminalOperatorPorts.Add(terminaloperatorPort);
                }
            }
            return terminalOperatorPorts;
        }


    }
}
