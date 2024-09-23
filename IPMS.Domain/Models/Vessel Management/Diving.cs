using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class Diving : EntityBase
    {        

        public int DivingID { get; set; }
        public int LicenseRequestID { get; set; }
        public string QualificationsCompetencies { get; set; }
        public string ProvideDivingPorts { get; set; }
        public int YearsProvidingDiving { get; set; }
        public string RegisteredDepartmentLabour { get; set; }
        public string EquipmentPersProtClothing { get; set; }
        public string EquipmentRegisterTestCert { get; set; }
        public string EquipmentIncludeTwoRadioSets { get; set; }
        public string QualifyPublLiabInsurance { get; set; }
        public string BBBEE { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public virtual User User { get; set; }
        public virtual LicenseRequest LicenseRequest { get; set; }
        public virtual User User1 { get; set; }        
    }
}
