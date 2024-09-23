
using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class ContractorPermitEmployeeDetails : EntityBase
     
    {
        public int ContractorPermitEmployeeID { get; set; }
        public int PermitRequestID { get; set; }      
        public string EmployeeName { get; set; }
        public string IDNumber { get; set; }
        public string JobTitle { get; set; }
        public string CriminalRecord { get; set; }       
        public string EmpSignature { get; set; }  
        public string RecordStatus { get; set; }
       
     

        public virtual PermitRequest PermitRequest { get; set; }      
        
     
    }
}
