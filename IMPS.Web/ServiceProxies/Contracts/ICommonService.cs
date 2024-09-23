using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;


namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface ICommonService : IDisposable
    {
        [OperationContract]
        List<SubCategory> GetSubCategories(string SupCatCode);
    }
}