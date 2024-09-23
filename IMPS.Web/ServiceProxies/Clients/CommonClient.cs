using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using IPMS.ServiceProxies.Contracts;
using IPMS.Domain.Models;
using System.Threading.Tasks;
using IPMS.Web.ServiceProxies;


namespace IPMS.ServiceProxies.Clients
{
    public class CommonClient : UserClientBase<ICommonService>, ICommonService
    {
        public List<SubCategory> GetSubCategories(string SupCatCode)
        {
            return WrapOperationWithException(() => Channel.GetSubCategories(SupCatCode));
        }
    }
}