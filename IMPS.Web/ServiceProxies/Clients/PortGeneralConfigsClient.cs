using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using IPMS.ServiceProxies.Contracts;
using IPMS.Domain.Models;
using System.Threading.Tasks;
using System.Web.Mvc;
using IPMS.Domain.ValueObjects;
using IPMS.Web.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;

namespace IPMS.ServiceProxies.Clients
{
    public class PortGeneralConfigsClient : UserClientBase<IPortGeneralConfigsService>, IPortGeneralConfigsService
    {
        public List<PortGeneralConfigsVO> GetAllPortGeneralConfigsDetails()
        {
            return WrapOperationWithException(() => Channel.GetAllPortGeneralConfigsDetails());
        }
        
        public List<PortGeneralConfigsVO> GetAllGroupNames(string GroupName)
        {
            return WrapOperationWithException(() => Channel.GetAllGroupNames(GroupName));
        }


        public int UpdatePortGeneralConfigs(PortGeneralConfigsVO portgeneralconfigdata)
        {
            return WrapOperationWithException(() => Channel.UpdatePortGeneralConfigs(portgeneralconfigdata));

        }
    }
}