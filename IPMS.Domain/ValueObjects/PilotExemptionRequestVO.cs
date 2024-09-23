
namespace IPMS.Domain.ValueObjects
{
    /// <summary>
    /// Data Transfer Object for PilotExemptionRequest
    /// </summary>
    public partial class PilotExemptionRequestVO
    {
        public int PilotExemptionRequestID { get; set; }
        public int PilotID { get; set; }
        public string MovementDate { get; set; }
        public int VesselID { get; set; }
        public string VesselName { get; set; }
        public string PilotRoleCode { get; set; }
        public string MovementTypeCode { get; set; }
        public string Remarks { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    }
}
