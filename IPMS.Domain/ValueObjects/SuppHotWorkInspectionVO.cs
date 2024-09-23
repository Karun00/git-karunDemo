using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace IPMS.Domain.ValueObjects
{
   [DataContract]
    public partial class  SuppHotWorkInspectionVO
    {

        [DataMember]
        public int SuppHotWorkInspectionID { get; set; }
        [DataMember]
        public int SuppServiceRequestID { get; set; }
        [DataMember]
        public string EmergencyProcedure { get; set; }
        [DataMember]
        public string FireRiskAssessment { get; set; }
        [DataMember]
        public string FlammableGases { get; set; }
        [DataMember]
        public string GasMonitoring { get; set; }
        [DataMember]
        public string FireDetectors { get; set; }
        [DataMember]
        public string EquipmentCondition { get; set; }
        [DataMember]
        public string ConductiveMetals { get; set; }
        [DataMember]
        public string EquipmentStandby { get; set; }
        [DataMember]
        public string HighProtection { get; set; }
        [DataMember]
        public string AdequateVentilation { get; set; }
        [DataMember]
        public string BarricadesRequired { get; set; }
        [DataMember]
        public string SymbolicSafetyScience { get; set; }
        [DataMember]
        public string PersonalProtective { get; set; }
        [DataMember]
        public string TrainedFireWatch { get; set; }
        [DataMember]
        public string PostWelding { get; set; }
        [DataMember]
        public string HouseKeepingPractices { get; set; }
        [DataMember]
        public string AdditionalConditions { get; set; }
        [DataMember]
        public string PermitStatus { get; set; }
        [DataMember]
        public string Remarks { get; set; }
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
        public virtual ArrivalNotificationVO ArrivalNotificationVo { get; set; }
        //[DataMember]
        //public virtual List<SuppFloatingCraneVO> SuppFloatingCranesVO { get; set; }
        //[DataMember]
        //public virtual SuppHotColdWorkPermitVO SuppHotColdWorkPermitsVO { get; set; }
        [DataMember]
        public string AnyDangerousGoodsonBoard { get; set; }
        [DataMember]
        public string DangerousGoodsClass { get; set; }
        [DataMember]
        public string UNNo { get; set; }
        [DataMember]
        public virtual SuppServiceRequestVO SuppServiceRequestVO { get; set; }

        [DataMember]
        public string HWPN { get; set; }
    }
}
