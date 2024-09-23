using System;

namespace IPMS.Domain.ValueObjects
{
   public class NewsPortVO
    {
       public int NewsPortID { get; set; }
        public int NewsID { get; set; }       
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string PortCode { get; set; }
    }
}
