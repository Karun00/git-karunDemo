using System;
using IPMS.Domain.Models;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class DivingCheckListVO
    {
        [DataMember]
        public int DivingCheckListID { get; set; }
        [DataMember]
        public int DivingRequestID { get; set; }
        [DataMember]
        public string DiveReferenceNo { get; set; }
        [DataMember]
        public string DivingSupervisorName { get; set; }
        [DataMember]
        public string Date { get; set; }
        [DataMember]
        public string WBPSDDEDiving { get; set; }
        [DataMember]
        public string WBPSheetPileInspection { get; set; }
        [DataMember]
        public string WBPLiftingOperations { get; set; }
        [DataMember]
        public string WBPBouyInspection { get; set; }
        [DataMember]
        public string WBPQuayWallInspection { get; set; }
        [DataMember]
        public string WBPConcretePileInspection { get; set; }
        [DataMember]
        public string WBPObjectRecovery { get; set; }
        [DataMember]
        public string WBPDockyardInspection { get; set; }
        [DataMember]
        public string WBPCordlessComsScuba { get; set; }
        [DataMember]
        public string WBPCraftInspection { get; set; }
        [DataMember]
        public string WBPHotWork { get; set; }
        [DataMember]
        public string WBPOther { get; set; }
        [DataMember]
        public string WBPOtherDescription { get; set; }
        [DataMember]
        public string PPEHardHot { get; set; }
        [DataMember]
        public string PPEReflectiveVests { get; set; }
        [DataMember]
        public string PPESafetyGlosses { get; set; }
        [DataMember]
        public string PPEGloves { get; set; }
        [DataMember]
        public string PPEOverall { get; set; }
        [DataMember]
        public string PPESafetyShoes { get; set; }
        [DataMember]
        public string PPELifeJacket { get; set; }
        [DataMember]
        public string PPEOther { get; set; }
        [DataMember]
        public string PPEOtherDescription { get; set; }
        [DataMember]
        public string EQPUsedCorrectly { get; set; }
        [DataMember]
        public string EQPCompetentToUseEquipment { get; set; }
        [DataMember]
        public string EQPGoodCondition { get; set; }
        [DataMember]
        public string EQPInDate { get; set; }
        [DataMember]
        public string EQPSecured { get; set; }
        [DataMember]
        public string EQPSafetyDevicesInPlace { get; set; }
        [DataMember]
        public string EQPDailyChecksCompleted { get; set; }
        [DataMember]
        public string EQPOther { get; set; }
        [DataMember]
        public string EQPOtherDescription { get; set; }
        [DataMember]
        public string PRADivePermit { get; set; }
        [DataMember]
        public string PRALockOutProc { get; set; }
        [DataMember]
        public string PRAFlogAlpha { get; set; }
        [DataMember]
        public string PRACommunicationNetworkCompleted { get; set; }
        [DataMember]
        public string PRAWorkPlaceTidy { get; set; }
        [DataMember]
        public string PRORequired { get; set; }
        [DataMember]
        public string PROSupplied { get; set; }
        [DataMember]
        public string PRORiskAssessment { get; set; }
        [DataMember]
        public string PROTaskKnownUnderstood { get; set; }
        [DataMember]
        public string PROOnsiteHazardID { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
        [DataMember]
        public bool WBPSDDEDivingStatus { get; set; }
        [DataMember]
        public bool WBPSheetPileInspectionStatus { get; set; }
        [DataMember]
        public bool WBPLiftingOperationsStatus { get; set; }
        [DataMember]
        public bool WBPBouyInspectionStatus { get; set; }
        [DataMember]
        public bool WBPQuayWallInspectionStatus { get; set; }
        [DataMember]
        public bool WBPConcretePileInspectionStatus { get; set; }
        [DataMember]
        public bool WBPObjectRecoveryStatus { get; set; }
        [DataMember]
        public bool WBPDockyardInspectionStatus { get; set; }
        [DataMember]
        public bool WBPCordlessComsScubaStatus { get; set; }
        [DataMember]
        public bool WBPCraftInspectionStatus { get; set; }
        [DataMember]
        public bool WBPHotWorkStatus { get; set; }
        [DataMember]
        public bool WBPOtherStatus { get; set; }
        [DataMember]
        public bool PPEHardHotStatus { get; set; }
        [DataMember]
        public bool PPEReflectiveVestsStatus { get; set; }
        [DataMember]
        public bool PPESafetyGlossesStatus { get; set; }
        [DataMember]
        public bool PPEGlovesStatus { get; set; }
        [DataMember]
        public bool PPEOverallStatus { get; set; }
        [DataMember]
        public bool PPESafetyShoesStatus { get; set; }
        [DataMember]
        public bool PPELifeJacketStatus { get; set; }
        [DataMember]
        public bool PPEOtherStatus { get; set; }
        [DataMember]
        public bool EQPUsedCorrectlyStatus { get; set; }
        [DataMember]
        public bool EQPCompetentToUseEquipmentStatus { get; set; }
        [DataMember]
        public bool EQPGoodConditionStatus { get; set; }
        [DataMember]
        public bool EQPInDateStatus { get; set; }
        [DataMember]
        public bool EQPSecuredStatus { get; set; }
        [DataMember]
        public bool EQPSafetyDevicesInPlaceStatus { get; set; }
        [DataMember]
        public bool EQPDailyChecksCompletedStatus { get; set; }
        [DataMember]
        public bool EQPOtherStatus { get; set; }
        [DataMember]
        public bool PRADivePermitStatus { get; set; }
        [DataMember]
        public bool PRALockOutProcStatus { get; set; }
        [DataMember]
        public bool PRAFlogAlphaStatus { get; set; }
        [DataMember]
        public bool PRACommunicationNetworkCompletedStatus { get; set; }
        [DataMember]
        public bool PRAWorkPlaceTidyStatus { get; set; }
        [DataMember]
        public bool PRORequiredStatus { get; set; }
        [DataMember]
        public bool PROSuppliedStatus { get; set; }
        [DataMember]
        public bool PRORiskAssessmentStatus { get; set; }
        [DataMember]
        public bool PROTaskKnownUnderstoodStatus { get; set; }
        [DataMember]
        public bool PROOnsiteHazardIDStatus { get; set; }
        [DataMember]
        public List<DivingCheckListHazardVO> DivingCheckListHazard { get; set; }

    }
}
