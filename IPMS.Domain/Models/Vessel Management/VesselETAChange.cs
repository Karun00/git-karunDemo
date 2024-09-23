using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPMS.Domain.Models
{
    public partial class VesselETAChange : EntityBase
    {
        public int VesselETAChangeID { get; set; }

        public string VCN { get; set; }

        public string VoyageIn { get; set; }

        public string VoyageOut { get; set; }

        public System.DateTime ETA { get; set; }

        public System.DateTime ETD { get; set; }

        public string Remarks { get; set; }

        public string RecordStatus { get; set; }

        public int CreatedBy { get; set; }

        public System.DateTime CreatedDate { get; set; }

        public int ModifiedBy { get; set; }

        public System.DateTime ModifiedDate { get; set; }

        public virtual ArrivalNotification ArrivalNotification { get; set; }

        public virtual User User { get; set; }

        public virtual User User1 { get; set; }

        public DateTime OldETA { get; set; }

        public DateTime OldETD { get; set; }

        public Nullable<System.DateTime> PlanDateTimeOfBerth { get; set; }

        public Nullable<System.DateTime> PlanDateTimeToStartCargo { get; set; }

        public Nullable<System.DateTime> PlanDateTimeToCompleteCargo { get; set; }

        public Nullable<System.DateTime> PlanDateTimeToVacateBerth { get; set; }

        public Nullable<System.DateTime> OldPlanDateTimeOfBerth { get; set; }

        public Nullable<System.DateTime> OldPlanDateTimeToStartCargo { get; set; }

        public Nullable<System.DateTime> OldPlanDateTimeToCompleteCargo { get; set; }

        public Nullable<System.DateTime> OldPlanDateTimeToVacateBerth { get; set; }


        [NotMapped]
        public string AnyDangerousGoodsonBoard { get; set; }
        [NotMapped]
        public string PortCode { get; set; }
        [NotMapped]
        public string PortName { get; set; }
    }
}
