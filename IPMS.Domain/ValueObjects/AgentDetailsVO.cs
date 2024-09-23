using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
   public class AgentDetailsVO
    {
       public int AgentID { get; set; }
      
       public string CellularNo { get; set; }
       public string EmailID { get; set; }
       public string FirstName { get; set; }
       public string SurName { get; set; }
       public string TelephoneNo1 { get; set; }
       public string FaxNo { get; set; }

       public string ReferenceNo { get; set; }
       public string RegisteredName { get; set; }
       public string TradingName { get; set; }
       public string RegistrationNumber { get; set; }
       public string VATNumber { get; set; }
       public string IncomeTaxNumber { get; set; }
       public Nullable<System.DateTime> FromDate { get; set; }
       public Nullable<System.DateTime> ToDate { get; set; }
       public string PortCode { get; set; }

    }
}
