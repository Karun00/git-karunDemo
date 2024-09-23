using Core.Repository;
using System;
namespace IPMS.Domain.Models
{
    public partial class NewsPort : EntityBase
    {
        public int NewsPortID { get; set; }
        public int NewsID { get; set; }
        public string PortCode { get; set; }        
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }  
        public virtual Port Port{get; set;}
        public virtual News News { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
