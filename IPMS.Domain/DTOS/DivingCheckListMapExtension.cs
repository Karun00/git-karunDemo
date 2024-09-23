using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Globalization;

namespace IPMS.Domain.DTOS
{
    public static class DivingCheckListMapExtension
    {

        /// <summary>
        /// Data List Transfer from Entity to DTO
        /// </summary>
        /// <param name="divingCheckLists"></param>
        /// <returns></returns>
        public static List<DivingCheckListVO> MapToDto(this List<DivingCheckList> divingCheckLists)
        {
            List<DivingCheckListVO> divingchecklistvos = new List<DivingCheckListVO>();

            if (divingCheckLists != null)
            {
                foreach (var data in divingCheckLists)
                {
                    divingchecklistvos.Add(data.MapToDto());
                }
            }

            return divingchecklistvos;
        }

        /// <summary>
        /// Data List Transfer from Entity DTO
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DivingCheckListVO MapToDto(this DivingCheckList data)
        {
            DivingCheckListVO DivingCheckListVO = new DivingCheckListVO();
            if (data != null)
            {
            DivingCheckListVO.DivingCheckListID = data.DivingCheckListID;
            DivingCheckListVO.DivingRequestID = data.DivingRequestID;
            DivingCheckListVO.DiveReferenceNo = data.DiveReferenceNo;                        
            DivingCheckListVO.DivingSupervisorName = data.DivingSupervisorName;
            DivingCheckListVO.Date = data.Date == null ? null : Convert.ToString(data.Date, CultureInfo.InvariantCulture);
            DivingCheckListVO.WBPSDDEDivingStatus = data.WBPSDDEDiving == "Y" ? true : false;
            DivingCheckListVO.WBPSheetPileInspectionStatus = data.WBPSheetPileInspection == "Y" ? true : false;
            DivingCheckListVO.WBPLiftingOperationsStatus = data.WBPLiftingOperations == "Y" ? true : false;
            DivingCheckListVO.WBPBouyInspectionStatus = data.WBPBouyInspection == "Y" ? true : false;
            
            //Work Being Performed
            DivingCheckListVO.WBPQuayWallInspectionStatus = data.WBPQuayWallInspection == "Y" ? true : false;
            DivingCheckListVO.WBPConcretePileInspectionStatus = data.WBPConcretePileInspection == "Y" ? true : false;
            DivingCheckListVO.WBPObjectRecoveryStatus = data.WBPObjectRecovery == "Y" ? true : false;
            DivingCheckListVO.WBPDockyardInspectionStatus = data.WBPDockyardInspection == "Y" ? true : false;
            DivingCheckListVO.WBPCordlessComsScubaStatus = data.WBPCordlessComsScuba == "Y" ? true : false;
            DivingCheckListVO.WBPCraftInspectionStatus = data.WBPCraftInspection == "Y" ? true : false;
            DivingCheckListVO.WBPHotWorkStatus = data.WBPHotWork == "Y" ? true : false;
            DivingCheckListVO.WBPOtherStatus = data.WBPOther == "Y" ? true : false;
            DivingCheckListVO.WBPOtherDescription = data.WBPOtherDescription;
           
            //PPE
            DivingCheckListVO.PPEHardHotStatus = data.PPEHardHot == "Y" ? true : false;
            DivingCheckListVO.PPEReflectiveVestsStatus = data.PPEReflectiveVests == "Y" ? true : false;
            DivingCheckListVO.PPESafetyGlossesStatus = data.PPESafetyGlosses == "Y" ? true : false;
            DivingCheckListVO.PPEGlovesStatus = data.PPEGloves == "Y" ? true : false;
            DivingCheckListVO.PPEOverallStatus = data.PPEOverall == "Y" ? true : false;
            DivingCheckListVO.PPESafetyShoesStatus = data.PPESafetyShoes == "Y" ? true : false;
            DivingCheckListVO.PPELifeJacketStatus = data.PPELifeJacket == "Y" ? true : false;
            DivingCheckListVO.PPEOtherStatus = data.PPEOther == "Y" ? true : false;
            DivingCheckListVO.PPEOtherDescription = data.PPEOtherDescription;
            
            //Equipement
            DivingCheckListVO.EQPUsedCorrectlyStatus = data.EQPUsedCorrectly == "Y" ? true : false;
                DivingCheckListVO.EQPCompetentToUseEquipmentStatus = data.EQPCompetentToUseEquipment == "Y"
                    ? true
                    : false;
            DivingCheckListVO.EQPGoodConditionStatus = data.EQPGoodCondition == "Y" ? true : false;
            DivingCheckListVO.EQPInDateStatus = data.EQPInDate == "Y" ? true : false;
            DivingCheckListVO.EQPSecuredStatus = data.EQPSecured == "Y" ? true : false;
            DivingCheckListVO.EQPSafetyDevicesInPlaceStatus = data.EQPSafetyDevicesInPlace == "Y" ? true : false;
            DivingCheckListVO.EQPDailyChecksCompletedStatus = data.EQPDailyChecksCompleted == "Y" ? true : false;
            DivingCheckListVO.EQPOtherStatus = data.EQPOther == "Y" ? true : false;
            DivingCheckListVO.EQPOtherDescription = data.EQPOtherDescription;
            
            //Practices
            DivingCheckListVO.PRADivePermitStatus = data.PRADivePermit == "Y" ? true : false;
            DivingCheckListVO.PRALockOutProcStatus = data.PRALockOutProc == "Y" ? true : false;
            DivingCheckListVO.PRAFlogAlphaStatus = data.PRAFlogAlpha == "Y" ? true : false;
                DivingCheckListVO.PRACommunicationNetworkCompletedStatus = data.PRACommunicationNetworkCompleted == "Y"
                    ? true
                    : false;
            DivingCheckListVO.PRAWorkPlaceTidyStatus = data.PRAWorkPlaceTidy == "Y" ? true : false;
            
            //Procedures
            DivingCheckListVO.PRORequiredStatus = data.PRORequired == "Y" ? true : false;
            DivingCheckListVO.PROSuppliedStatus = data.PROSupplied == "Y" ? true : false;
            DivingCheckListVO.PRORiskAssessmentStatus = data.PRORiskAssessment == "Y" ? true : false;
            DivingCheckListVO.PROTaskKnownUnderstoodStatus = data.PROTaskKnownUnderstood == "Y" ? true : false;
            DivingCheckListVO.PROOnsiteHazardIDStatus = data.PROOnsiteHazardID == "Y" ? true : false;

            DivingCheckListVO.RecordStatus = data.RecordStatus;
            DivingCheckListVO.CreatedBy = data.CreatedBy;
            DivingCheckListVO.CreatedDate = data.CreatedDate;
            DivingCheckListVO.ModifiedBy = data.ModifiedBy;
            DivingCheckListVO.ModifiedDate = data.ModifiedDate;

            if (data.DivingCheckListHazards.Count > 0)
            {
                List<DivingCheckListHazard> lstdivingchecklist = new List<DivingCheckListHazard>();

                    foreach (
                        DivingCheckListHazard divingchecklisthazard in
                            data.DivingCheckListHazards as List<DivingCheckListHazard>)
                {
                    lstdivingchecklist.Add(divingchecklisthazard);
                }

                DivingCheckListVO.DivingCheckListHazard = lstdivingchecklist.MapToDto();
            }
            }
            return DivingCheckListVO;
        }

        /// <summary>
        /// Data List Transfer from DTO to Entity
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public static DivingCheckList MapToEntity(this DivingCheckListVO vo)
        {
            DivingCheckList DivingCheckList = new DivingCheckList();
            if (vo != null)
            {
            DivingCheckList.DivingCheckListID = vo.DivingCheckListID;
            DivingCheckList.DivingRequestID = vo.DivingRequestID;
            DivingCheckList.DiveReferenceNo = vo.DiveReferenceNo;
            DivingCheckList.DivingSupervisorName = vo.DivingSupervisorName;
            
            if (! string.IsNullOrEmpty(vo.Date))
            {
                DivingCheckList.Date = DateTime.Parse(vo.Date, CultureInfo.InvariantCulture);
            }
            
            DivingCheckList.WBPSDDEDiving = vo.WBPSDDEDivingStatus == true ? "Y" : "N";
            DivingCheckList.WBPSheetPileInspection = vo.WBPSheetPileInspectionStatus == true ? "Y" : "N";
            DivingCheckList.WBPLiftingOperations = vo.WBPLiftingOperationsStatus == true ? "Y" : "N";
            DivingCheckList.WBPBouyInspection = vo.WBPBouyInspectionStatus == true ? "Y" : "N";
            
            //Work being Performed
            GetValueDiving(vo, DivingCheckList);

                // Practices
            DivingCheckList.PRADivePermit = vo.PRADivePermitStatus == true ? "Y" : "N";
            DivingCheckList.PRALockOutProc = vo.PRALockOutProcStatus == true ? "Y" : "N";
            DivingCheckList.PRAFlogAlpha = vo.PRAFlogAlphaStatus == true ? "Y" : "N";
                DivingCheckList.PRACommunicationNetworkCompleted = vo.PRACommunicationNetworkCompletedStatus == true
                    ? "Y"
                    : "N";
            DivingCheckList.PRAWorkPlaceTidy = vo.PRAWorkPlaceTidyStatus == true ? "Y" : "N";
            
            //Procedures
            DivingCheckList.PRORequired = vo.PRORequiredStatus == true ? "Y" : "N";
            DivingCheckList.PROSupplied = vo.PROSuppliedStatus == true ? "Y" : "N";
            DivingCheckList.PRORiskAssessment = vo.PRORiskAssessmentStatus == true ? "Y" : "N";
            DivingCheckList.PROTaskKnownUnderstood = vo.PROTaskKnownUnderstoodStatus == true ? "Y" : "N";
            DivingCheckList.PROOnsiteHazardID = vo.PROOnsiteHazardIDStatus == true ? "Y" : "N";

            DivingCheckList.RecordStatus = vo.RecordStatus;
            DivingCheckList.CreatedBy = vo.CreatedBy;
            DivingCheckList.CreatedDate = vo.CreatedDate;
            DivingCheckList.ModifiedBy = vo.ModifiedBy;
            DivingCheckList.ModifiedDate = vo.ModifiedDate;
            }
            return DivingCheckList;
        }

        private static void GetValueDiving(DivingCheckListVO vo, DivingCheckList DivingCheckList)
        {
            DivingCheckList.WBPQuayWallInspection = vo.WBPQuayWallInspectionStatus == true ? "Y" : "N";
            DivingCheckList.WBPConcretePileInspection = vo.WBPConcretePileInspectionStatus == true ? "Y" : "N";
            DivingCheckList.WBPObjectRecovery = vo.WBPObjectRecoveryStatus == true ? "Y" : "N";
            DivingCheckList.WBPDockyardInspection = vo.WBPDockyardInspectionStatus == true ? "Y" : "N";
            DivingCheckList.WBPCordlessComsScuba = vo.WBPCordlessComsScubaStatus == true ? "Y" : "N";
            DivingCheckList.WBPCraftInspection = vo.WBPCraftInspectionStatus == true ? "Y" : "N";
            DivingCheckList.WBPHotWork = vo.WBPHotWorkStatus == true ? "Y" : "N";
            DivingCheckList.WBPOther = vo.WBPOtherStatus == true ? "Y" : "N";
            DivingCheckList.WBPOtherDescription = vo.WBPOtherDescription;

            //PPE
            DivingCheckList.PPEHardHot = vo.PPEHardHotStatus == true ? "Y" : "N";
            DivingCheckList.PPEReflectiveVests = vo.PPEReflectiveVestsStatus == true ? "Y" : "N";
            DivingCheckList.PPESafetyGlosses = vo.PPESafetyGlossesStatus == true ? "Y" : "N";
            DivingCheckList.PPEGloves = vo.PPEGlovesStatus == true ? "Y" : "N";
            DivingCheckList.PPEOverall = vo.PPEOverallStatus == true ? "Y" : "N";
            DivingCheckList.PPESafetyShoes = vo.PPESafetyShoesStatus == true ? "Y" : "N";
            DivingCheckList.PPELifeJacket = vo.PPELifeJacketStatus == true ? "Y" : "N";
            DivingCheckList.PPEOther = vo.PPEOtherStatus == true ? "Y" : "N";
            DivingCheckList.PPEOtherDescription = vo.PPEOtherDescription;

            //Equipement
            DivingCheckList.EQPUsedCorrectly = vo.EQPUsedCorrectlyStatus == true ? "Y" : "N";
            DivingCheckList.EQPCompetentToUseEquipment = vo.EQPCompetentToUseEquipmentStatus == true ? "Y" : "N";
            DivingCheckList.EQPGoodCondition = vo.EQPGoodConditionStatus == true ? "Y" : "N";
            DivingCheckList.EQPInDate = vo.EQPInDateStatus == true ? "Y" : "N";
            DivingCheckList.EQPSecured = vo.EQPSecuredStatus == true ? "Y" : "N";
            DivingCheckList.EQPSafetyDevicesInPlace = vo.EQPSafetyDevicesInPlaceStatus == true ? "Y" : "N";
            DivingCheckList.EQPDailyChecksCompleted = vo.EQPDailyChecksCompletedStatus == true ? "Y" : "N";
            DivingCheckList.EQPOther = vo.EQPOtherStatus == true ? "Y" : "N";
            DivingCheckList.EQPOtherDescription = vo.EQPOtherDescription;
        }
    }
}
