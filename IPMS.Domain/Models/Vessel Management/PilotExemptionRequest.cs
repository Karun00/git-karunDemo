using Core.Repository;

namespace IPMS.Domain.Models
{
    public partial class PilotExemptionRequest:EntityBase
    {
        public int PilotExemptionRequestID { get; set; }
        public int PilotID { get; set; }
        public System.DateTime MovementDate { get; set; }
        public int VesselID { get; set; }
        public string PilotRoleCode { get; set; }
        public string MovementTypeCode { get; set; }
        public string Remarks { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public virtual Pilot Pilot { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual SubCategory SubCategory1 { get; set; }
        public virtual Vessel Vessel { get; set; }
      
       

    
    }
}
