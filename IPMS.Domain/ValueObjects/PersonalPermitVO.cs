using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public partial class PersonalPermitVO
    {
        public int PersonalPermitID { get; set; }
        public int PermitRequestID { get; set; }
        public string PermitCategoryCode { get; set; }
        public string AllNPASites { get; set; }
        public string SpecificNPASites { get; set; }
        public string SpecifyArea { get; set; }
        public string LeaseholdSite { get; set; }
        public string PhysicalAddress { get; set; }
        public string AdhocPermits { get; set; }
        public string TemporaryPermits { get; set; }
        public string AllPorts { get; set; }
        public string ConstructionArea { get; set; }
        public string PermanentPermits { get; set; }
        public string Reason { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string permittype { get; set; }
        //public System.DateTime? TempFromDate { get; set; }
        //public System.DateTime? TempToDate { get; set; }
        //public System.DateTime? PerFromDate { get; set; }
        //public System.DateTime? PerToDate { get; set; }
        //public string IsCamera { get; set; }
        //public string CameraDetails { get; set; }
        //public string IsTools { get; set; }
        //public string ToolsDetails { get; set; }
        //public string IsSpclEquipment { get; set; }
        //public string SpclEquipmentDetails { get; set; }
        //public string Authorised_Surname_Initial_Signatory { get; set; }
        //public string TelephoneWork { get; set; }
        //public string AuthorisedMobile { get; set; }
        //public string AuthorisedIdentityNumber { get; set; }
        //public string AuthorisedEmail { get; set; }
        //public string AuthorisedSignature { get; set; }
        //public DateTime Date_Signatory { get; set; }
    }
}
