using System;
using System.Collections.Generic;

namespace IPMS.Domain.Models
{
    public partial class ArrivalNotification
    {
        public ArrivalNotification()
        {
          //  this.ChangeETAs = new List<ChangeETA>();
        }

        public string VCNNo { get; set; }
        public string PortCode { get; set; }
        public int AgentID { get; set; }
        public int VesselID { get; set; }
        public System.DateTime ETA { get; set; }
        public System.DateTime ETD { get; set; }
        public string ReasonForVisit { get; set; }
        public string IsTerminalOperator { get; set; }
        public Nullable<int> TerminalOperatorID { get; set; }
        public string LastPortOfCall { get; set; }
        public string NextPortOfCall { get; set; }
        public Nullable<System.DateTime> NominationDate { get; set; }
        public string AppliedForISPS { get; set; }
        public Nullable<System.DateTime> AppliedDate { get; set; }
        public string Clearance { get; set; }
        public string ISPSReferenceNo { get; set; }
        public string PilotExemption { get; set; }
        public Nullable<int> ExemptionID { get; set; }
        public string Tidal { get; set; }
        public string BallastWater { get; set; }
        public string WasteDeclaration { get; set; }
        public string DaylightRestriction { get; set; }
        public string ExceedPortLimitations { get; set; }
        public Nullable<System.DateTime> PlanDateTimeOfBerth { get; set; }
        public Nullable<System.DateTime> PlanDateTimeToVacateBerth { get; set; }
        public Nullable<System.DateTime> PlanDateTimeToStartCargo { get; set; }
        public Nullable<System.DateTime> PlanDateTimeToCompleteCargo { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual Agent Agent { get; set; }
        public virtual User User { get; set; }
      //  public virtual Pilot Pilot { get; set; }
        public virtual User User1 { get; set; }
        public virtual Port Port { get; set; }
        public virtual TerminalOperator TerminalOperator { get; set; }
        //public virtual Vessel Vessel { get; set; }
        //public virtual ICollection<ChangeETA> ChangeETAs { get; set; }
    }
}
