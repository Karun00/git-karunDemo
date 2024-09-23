using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
   public class Section625BVO
    {
        public int Section625BID { get; set; }
        public int Section625ABCDID { get; set; }
        public System.DateTime IDIndustrialDisputeDateTime { get; set; }
        //public System.DateTime IDTimeReported { get; set; }
        public string IDTimeReported { get; set; }
        public string IDDisputeSpecificLocation { get; set; }
        public string IDTradeUnionName { get; set; }
        public Nullable<int> IDTotalNoOfEmployees { get; set; }
        public string IDStrikeStatuS { get; set; }
        public string IDImpactOperations { get; set; }
        public string IDViolencePresent { get; set; }
        public string IndustrialDisputeDescription { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public int Hour24Report625ID { get; set; }
        //public Section625ABCD Section625ABCD { get; set; }      
        //public  ICollection<Section625BUnionVO> Section625BUnionVO { get; set; }
    }
}
