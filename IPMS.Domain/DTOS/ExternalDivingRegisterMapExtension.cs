using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class ExternalDivingRegisterMapExtension
    {
        public static List<ExternalDivingRegisterVO> MapToDTO(this List<ExternalDivingRegister> divingrequestList)
        {
            List<ExternalDivingRegisterVO> externalDivingRegistervoList = new List<ExternalDivingRegisterVO>();
            if (divingrequestList != null)
            {
                foreach (var item in divingrequestList)
                {
                    externalDivingRegistervoList.Add(item.MapToDTO());
                }
            }

            return externalDivingRegistervoList;
        }

        public static ExternalDivingRegisterVO MapToDTO(this ExternalDivingRegister data)
        {
            ExternalDivingRegisterVO VO = new ExternalDivingRegisterVO();
            if (data != null)
            {
                 VO.ExternalDivingRegisterID = data.ExternalDivingRegisterID;
                 VO.DivingLogDateTime = Convert.ToString(data.DivingLogDateTime, CultureInfo.InvariantCulture);
                 VO.CompanyName = data.CompanyName;                                                   
                 VO.VesselID = data.VesselID;
                 VO.PersonInCharge = data.PersonInCharge;
                 VO.StartTime = Convert.ToString(data.StartTime, CultureInfo.InvariantCulture);
                 VO.StopTime = Convert.ToString(data.StopTime, CultureInfo.InvariantCulture);
                 VO.OnsiteSupervisorContNo = data.OnsiteSupervisorContNo;
                 VO.OffsiteSupervisorContNo = data.OffsiteSupervisorContNo;
                 VO.ClearanceNo = data.ClearanceNo;
                 VO.NoOfDrivers = data.NoOfDrivers;
                 VO.PermissionObtained = data.PermissionObtained;
                 VO.RecordStatus = data.RecordStatus;
                 VO.CreatedBy = data.CreatedBy;
                 VO.CreatedDate = data.CreatedDate;
                 VO.ModifiedBy = data.ModifiedBy;
                 VO.ModifiedDate = data.ModifiedDate;            
                 VO.isPermissionObtained = data.PermissionObtained == "Y" ? true : false;
            }

            return VO;
        }

        public static ExternalDivingRegister MapToEntity(this ExternalDivingRegisterVO DivingRequestvo)
        {
            string[] fields = DivingRequestvo.BerthKey.Split('.');

            string portCode = fields[0];
            string quayCode = fields[1];
            string berthCode = fields[2];

            ExternalDivingRegister externaldivingregister = new ExternalDivingRegister();
            if (DivingRequestvo != null)
            {
            externaldivingregister.ExternalDivingRegisterID = DivingRequestvo.ExternalDivingRegisterID;
            externaldivingregister.DivingLogDateTime = DateTime.Parse(DivingRequestvo.DivingLogDateTime, CultureInfo.InvariantCulture);
            externaldivingregister.CompanyName = DivingRequestvo.CompanyName;           
            externaldivingregister.PortCode = portCode;
            externaldivingregister.QuayCode = quayCode;
            externaldivingregister.BerthCode = berthCode;
            externaldivingregister.VesselID = DivingRequestvo.VesselID;
            externaldivingregister.PersonInCharge = DivingRequestvo.PersonInCharge;
            externaldivingregister.StartTime = DateTime.Parse(DivingRequestvo.StartTime, CultureInfo.InvariantCulture);
            if (DivingRequestvo.StopTime != "" && DivingRequestvo.StopTime != null)
            {
                externaldivingregister.StopTime = DateTime.Parse(DivingRequestvo.StopTime, CultureInfo.InvariantCulture);
            }
            else
            {
                externaldivingregister.StopTime = null;
            }
            externaldivingregister.OnsiteSupervisorContNo = DivingRequestvo.OnsiteSupervisorContNo.Replace("-", "");
            externaldivingregister.OffsiteSupervisorContNo = DivingRequestvo.OffsiteSupervisorContNo.Replace("-", "");
            externaldivingregister.ClearanceNo = DivingRequestvo.ClearanceNo;
            externaldivingregister.NoOfDrivers = DivingRequestvo.NoOfDrivers;
            externaldivingregister.PermissionObtained = DivingRequestvo.isPermissionObtained == true ? "Y" : "N";
            externaldivingregister.RecordStatus = DivingRequestvo.RecordStatus;

            externaldivingregister.CreatedBy = DivingRequestvo.CreatedBy;
            externaldivingregister.CreatedDate = DivingRequestvo.CreatedDate;
            externaldivingregister.ModifiedBy = DivingRequestvo.ModifiedBy;
            externaldivingregister.ModifiedDate = DivingRequestvo.ModifiedDate;
            }
            return externaldivingregister;
        }
    }
}
