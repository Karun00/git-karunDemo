using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{

   public class SuperCategoryVO
    {
     
        public string SupCatCode { get; set; }
      
        public string SupCatName { get; set; }
       
        public string RecordStatus { get; set; }
       
        public int CreatedBy { get; set; }
       
        public System.DateTime CreatedDate { get; set; }
      
        public int ModifiedBy { get; set; }
    
        public System.DateTime ModifiedDate { get; set; }    
     
    }
}
