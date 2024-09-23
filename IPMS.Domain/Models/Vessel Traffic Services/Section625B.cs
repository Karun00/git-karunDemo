using Core.Repository;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models 
{
    public partial class Section625B:EntityBase
    {
        public Section625B()
        {
            this.Section625BUnion = new List<Section625BUnion>();
        }

        public int Section625BID { get; set; }
        public int Section625ABCDID { get; set; }
        public System.DateTime IDIndustrialDisputeDateTime { get; set; }
        public System.DateTime IDTimeReported { get; set; }
        public string IDDisputeSpecificLocation { get; set; }
        public string IDTradeUnionName { get; set; }
        public Nullable<int> IDTotalNoOfEmployees { get; set; }
        public string IDStrikeStatuS { get; set; }
        public Nullable<int> IDImpactOperations { get; set; }
        public string IDViolencePresent { get; set; }
        public string IndustrialDisputeDescription { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public int Hour24Report625ID { get; set; }
        public  Hour24Report625 Hour24Report625 { get; set; }
        public  Section625ABCD Section625ABCD { get; set; }
        public  User User { get; set; }
        public  User User1 { get; set; }
        public  ICollection<Section625BUnion> Section625BUnion { get; set; }
    }
}
