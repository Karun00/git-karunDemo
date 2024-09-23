using Core.Repository;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class Section625G:EntityBase
    {
        public Section625G()
        {
            this.Section625GDetail1 = new List<Section625GDetail1>();
            this.Section625GDetail2 = new List<Section625GDetail2>();
        }

        public int Section625GID { get; set; }
        public int Section625ABCDID { get; set; }
        public System.DateTime IncidentDateTime { get; set; }
        public System.DateTime TimeReported { get; set; }
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
        public string ContributingFactorsCourses { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string LikelyUnderlyingCauses { get; set; }
        public int Hour24Report625ID { get; set; }
        public  Hour24Report625 Hour24Report625 { get; set; }
        public  Section625ABCD Section625ABCD { get; set; }
        public  User User { get; set; }
        public  User User1 { get; set; }
        public  ICollection<Section625GDetail1> Section625GDetail1 { get; set; }
        public  ICollection<Section625GDetail2> Section625GDetail2 { get; set; }
    }
}
