using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IPMS.Domain.DTOS
{
    public static class SuppHotWorkInspectionMapExtension
    {
        public static List<SuppHotWorkInspectionVO> MapToDTO(this List<SuppHotWorkInspection> suppServiceRequests)
        {
            List<SuppHotWorkInspectionVO> SuppHotWorkInspectionvoList = new List<SuppHotWorkInspectionVO>();
            
            return SuppHotWorkInspectionvoList;

        }
        public static SuppHotWorkInspectionVO MapToDTO(this SuppHotWorkInspection data)
        {
            SuppHotWorkInspectionVO SuppHotWorkInspectionVo = new SuppHotWorkInspectionVO();
            SuppHotWorkInspectionVo.SuppHotWorkInspectionID = data.SuppHotWorkInspectionID;


            SuppHotWorkInspectionVo.SuppServiceRequestID = data.SuppServiceRequestID;
            SuppHotWorkInspectionVo.EmergencyProcedure = data.EmergencyProcedure;
            SuppHotWorkInspectionVo.FireRiskAssessment = data.FireRiskAssessment;
            SuppHotWorkInspectionVo.FlammableGases = data.FlammableGases;
            SuppHotWorkInspectionVo.GasMonitoring = data.GasMonitoring;
            SuppHotWorkInspectionVo.FireDetectors = data.FireDetectors;
            SuppHotWorkInspectionVo.EquipmentCondition = data.EquipmentCondition;
            SuppHotWorkInspectionVo.ConductiveMetals = data.ConductiveMetals;
            SuppHotWorkInspectionVo.EquipmentStandby = data.EquipmentStandby;
            SuppHotWorkInspectionVo.HighProtection = data.HighProtection;
            SuppHotWorkInspectionVo.AdequateVentilation = data.AdequateVentilation;
            SuppHotWorkInspectionVo.BarricadesRequired = data.BarricadesRequired;
            SuppHotWorkInspectionVo.SymbolicSafetyScience = data.SymbolicSafetyScience;
            SuppHotWorkInspectionVo.PersonalProtective = data.PersonalProtective;
            SuppHotWorkInspectionVo.TrainedFireWatch = data.TrainedFireWatch;
            SuppHotWorkInspectionVo.PostWelding = data.PostWelding;
            SuppHotWorkInspectionVo.HouseKeepingPractices = data.HouseKeepingPractices;
            SuppHotWorkInspectionVo.AdditionalConditions = data.AdditionalConditions;
            SuppHotWorkInspectionVo.PermitStatus = data.PermitStatus;
            SuppHotWorkInspectionVo.Remarks = data.Remarks;
            SuppHotWorkInspectionVo.RecordStatus = data.RecordStatus;

            SuppHotWorkInspectionVo.CreatedBy = data.CreatedBy;
            SuppHotWorkInspectionVo.CreatedDate = data.CreatedDate;
            SuppHotWorkInspectionVo.ModifiedBy = data.ModifiedBy;
            SuppHotWorkInspectionVo.ModifiedDate = data.ModifiedDate;
            SuppHotWorkInspectionVo.HWPN = data.HWPN;
            SuppHotWorkInspectionVo.ArrivalNotificationVo = data.SuppServiceRequest.ArrivalNotification.MapToDto();


            return SuppHotWorkInspectionVo;
        }
        public static SuppHotWorkInspection MapToEntity(this SuppHotWorkInspectionVO suppHotWorkInspectionVO)
        {

            SuppHotWorkInspection SuppHotWorkInspection = new SuppHotWorkInspection();

            SuppHotWorkInspection.SuppHotWorkInspectionID = suppHotWorkInspectionVO.SuppHotWorkInspectionID;
         

            SuppHotWorkInspection.SuppServiceRequestID = suppHotWorkInspectionVO.SuppServiceRequestID;
            SuppHotWorkInspection.EmergencyProcedure = suppHotWorkInspectionVO.EmergencyProcedure;
            SuppHotWorkInspection.FireRiskAssessment = suppHotWorkInspectionVO.FireRiskAssessment;
            SuppHotWorkInspection.FlammableGases = suppHotWorkInspectionVO.FlammableGases;
            SuppHotWorkInspection.GasMonitoring = suppHotWorkInspectionVO.GasMonitoring;
            SuppHotWorkInspection.FireDetectors = suppHotWorkInspectionVO.FireDetectors;
            SuppHotWorkInspection.EquipmentCondition = suppHotWorkInspectionVO.EquipmentCondition;
            SuppHotWorkInspection.ConductiveMetals = suppHotWorkInspectionVO.ConductiveMetals;
            SuppHotWorkInspection.EquipmentStandby = suppHotWorkInspectionVO.EquipmentStandby;
            SuppHotWorkInspection.HighProtection = suppHotWorkInspectionVO.HighProtection;
            SuppHotWorkInspection.AdequateVentilation = suppHotWorkInspectionVO.AdequateVentilation;
            SuppHotWorkInspection.BarricadesRequired = suppHotWorkInspectionVO.BarricadesRequired;
            SuppHotWorkInspection.SymbolicSafetyScience = suppHotWorkInspectionVO.SymbolicSafetyScience;
            SuppHotWorkInspection.PersonalProtective = suppHotWorkInspectionVO.PersonalProtective;
            SuppHotWorkInspection.TrainedFireWatch = suppHotWorkInspectionVO.TrainedFireWatch;
            SuppHotWorkInspection.PostWelding = suppHotWorkInspectionVO.PostWelding;
            SuppHotWorkInspection.HouseKeepingPractices = suppHotWorkInspectionVO.HouseKeepingPractices;
            SuppHotWorkInspection.AdditionalConditions = suppHotWorkInspectionVO.AdditionalConditions;
            SuppHotWorkInspection.PermitStatus = suppHotWorkInspectionVO.PermitStatus;
            SuppHotWorkInspection.Remarks = suppHotWorkInspectionVO.Remarks;
            SuppHotWorkInspection.RecordStatus = suppHotWorkInspectionVO.RecordStatus;

            SuppHotWorkInspection.CreatedBy = suppHotWorkInspectionVO.CreatedBy;
            SuppHotWorkInspection.CreatedDate = suppHotWorkInspectionVO.CreatedDate;
            SuppHotWorkInspection.ModifiedBy = suppHotWorkInspectionVO.ModifiedBy;
            SuppHotWorkInspection.ModifiedDate = suppHotWorkInspectionVO.ModifiedDate;
            SuppHotWorkInspection.HWPN = suppHotWorkInspectionVO.HWPN;
            return SuppHotWorkInspection;

        }

    }
}
