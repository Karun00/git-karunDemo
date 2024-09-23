using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
     public class Section625GVO
    {
        public int Section625GID { get; set; }
        public int Section625ABCDID { get; set; }
        public System.DateTime IncidentDateTime { get; set; }
        //public System.DateTime TimeReported { get; set; }
        public string TimeReported { get; set; }
        public int Hour24Report625ID { get; set; }
        public string WIWitnessName1 { get; set; }
        public string WIContactNo1 { get; set; }
        public string WIWitnessName2 { get; set; }
        public string WIContactNo2 { get; set; }
        public string IncidentDescription { get; set; }
        public string IncidentExtent { get; set; }
        public string QuantityVolumeMaterial { get; set; }
        public string EstimateDistanceNearestWaterway { get; set; }
        public string ActivityTypeIncident { get; set; }
        public string IncidentIdentified { get; set; }
        public string NameOfComplainant { get; set; }
        public string ContactNoOfComplainant { get; set; }
        public string LIMinorEnvironmentalIncident { get; set; }
        public string LISignificantEnvironmentalIncident { get; set; }
        public string LIMajorEnvironmentalIncident { get; set; }
        public string ImmediateReleventActionsTaken { get; set; }
        public string EnvironmentalImpactDescription { get; set; }
        public string LikelyUnderlyingCauses { get; set; }         
        public string ContributingFactorsCourses { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    }
}
