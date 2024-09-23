using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Repository
{
   public interface IPortEntryPassApplicationRepository
    {
       List<PermitRequest> GetPortEntryPassRequestlist(string portcode);
       PermitRequest GetPortEntryPassRequest(string refrencenumber,int flag,string portcode);
       List<PermitRequest> GetPortEntryPassRequestlistForSaps(string portcode);
       List<PermitRequest> GetPortEntryPassRequestlistForSsa(string portcode);
       List<PermitRequest> GetApprovedPortEntryPassRequestlist(string portcode);
       List<PermitRequest> GetInternalEmployeePermitlist(string portcode);
       List<PermitRequest> GetInternalEmployeePermittobeapprovedlist(string portcode);
       PermitRequest GetPortEntryPassdetailsByid(string refrencenumber, string portcode);
       Entity GetEnties(string entityCode);
       CompanyVO GetUserDetails(int userid);
       int GetvalidatePortEntryPassRequestforSsasaps(int id, string flag, string portcode);
       List<PermitRequest> GetPortEntryPassRequestlistSearch(PermitRequestSearchVO Searchmdl);
       List<PermitRequest> GetPortEntryPassRequestlistForSsaSearch(PermitRequestSearchVO Searchmdl);
       List<PermitRequest> GetInternalEmployeePermittobeapprovedlistSearch(PermitRequestSearchVO Searchmdl);
       List<PermitRequest> GetPortEntryPassRequestlistForSapsSearch(PermitRequestSearchVO Searchmdl);
       List<PermitRequest> GetInternalEmployeePermitlistSearch(PermitRequestSearchVO Searchmdl);
       
       List<PermitRequest> GetApprovedPortEntryPassRequestlistSearch(PermitRequestSearchVO Searchmdl);
       

    }
}
