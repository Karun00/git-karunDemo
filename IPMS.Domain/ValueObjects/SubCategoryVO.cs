using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
   public class SubCategoryVO
    {
        [DataMember]
        public string SubCatCode { get; set; }
        [DataMember]
        public string SupCatCode { get; set; }
        [DataMember]
        public string SubCatName { get; set; }
        [DataMember]
        public string SupCatName { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        //[DataMember]
        //public Nullable<decimal> ModifiedBy { get; set; }
        //[DataMember]
        //public Nullable<System.DateTime> ModifiedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
    }

     public class SubCategoryCodeNameVO
    {        
        public string SubCatCode { get; set; }        
        public string SubCatName { get; set; }        
    }

     public class SubCategoryCodeNameWithSupCatCodeVO
     {
         public string SubCatCode { get; set; }
         public string SubCatName { get; set; }
         public string SupCatCode { get; set; }
     }

}
