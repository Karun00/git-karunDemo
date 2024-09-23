using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects

{
    public class NotificationPortVO
    {
      
        public string NotificationTemplateCode { get; set; }
       
        public string PortCode { get; set; }
        
        public string RecordStatus { get; set; }
  
        public int CreatedBy { get; set; }
    
        public System.DateTime CreatedDate { get; set; }
      
        public Nullable<int> ModifiedBy { get; set; }
  
        public System.DateTime ModifiedDate { get; set; }
           

    }
}
