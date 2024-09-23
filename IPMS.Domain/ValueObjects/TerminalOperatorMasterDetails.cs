using Core.Repository;
using System;

namespace IPMS.Domain.Models
{

    public class TerminalOperatorMasterDetails:EntityBase
    {
       
       
        public decimal CreatedBy { get; set; }
       
        public DateTime CreatedDate { get; set; }
       
        public String ModifiedBy { get; set; }
       
        public DateTime ModifiedDate { get; set; }
       
        public string PortCode { get; set; }
       
        public string PortName { get; set; }
       
        public string  QuayCode { get; set; }
       
        public string QuayShortName { get; set; }
       
        public string QuayName { get; set; }
       
        public string BerthCode { get; set; }
       
        public string BerthName { get; set; }
       
        public string ShortName { get; set; }
       
        public string BerthType { get; set; }
       
        public string SubCatCode { get; set; }
       
        public string SubCatName { get; set; }
       
        public string SupCatCode { get; set; }
       
        public string SupCatName { get; set; }



    }
}
