using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Core.Repository;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class DivingCheckList : EntityBase
    {
        public DivingCheckList()
        {
            this.DivingCheckListHazards = new List<DivingCheckListHazard>();
        }

        [DataMember]
        public int DivingCheckListID { get; set; }
        [DataMember]
        public int DivingRequestID { get; set; }
        [DataMember]
        public string DiveReferenceNo { get; set; }
        [DataMember]
        public string DivingSupervisorName { get; set; }
        [DataMember]
        public Nullable<System.DateTime> Date { get; set; }
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
        public  DivingRequest DivingRequest { get; set; }
        [DataMember]
        public  User User { get; set; }
        [DataMember]
        public  User User1 { get; set; }
        [DataMember]
        public  ICollection<DivingCheckListHazard> DivingCheckListHazards { get; set; }
    }
}
