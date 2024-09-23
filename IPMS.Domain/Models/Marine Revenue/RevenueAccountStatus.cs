using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Repository;

namespace IPMS.Domain.Models
{
    public partial class RevenueAccountStatus : EntityBase
    {
        public int RevenueAccountStatusID { get; set; }
        public int RevenueStopListID { get; set; }
        public string AccountStatusCode { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual RevenueStopList RevenueStopList { get; set; }
    }
}
