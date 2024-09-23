using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class VoyageMonitoring : EntityBase
    {
        public VoyageMonitoring()
        {
        }

        public string VCN { get; set; }

        public int VesselID { get; set; }

        public string VesselName { get; set; }

        public string ReasonForVisit { get; set; }

        public string VesselType { get; set; }

        public string CallSign { get; set; }

        public System.DateTime ETA { get; set; }

        public System.DateTime ETD { get; set; }

        public string IMONo { get; set; }

        public Nullable<long> LengthOverallInM { get; set; }

        public Nullable<long> BeamInM { get; set; }

        public Nullable<long> GrossRegisteredTonnageInMT { get; set; }

        public Nullable<long> DeadWeightTonnageInMT { get; set; }

        public string LastPortOfCall { get; set; }

        public string NextPortOfCall { get; set; }

        public string Tidal { get; set; }

        public string DaylightRestriction { get; set; }

        public string VesselNationality { get; set; }

        public string ArrDraft { get; set; }

        public string VoyageIn { get; set; }

        public virtual ArrivalNotification ArrivalNotification { get; set; }
        public virtual Vessel Vessel { get; set; }
        public virtual VesselCall VesselCall { get; set; }
    }
}
