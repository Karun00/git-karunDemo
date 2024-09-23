using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;


namespace IPMS.ServiceProxies.Clients
{
    public class MarpolClient : UserClientBase<IMarpolService>, IMarpolService
    {
        public List<MarpolVO> GetMarpolDetails()
        {
            return WrapOperationWithException(() => Channel.GetMarpolDetails());
        }

        public MarpolVO SaveMarpolDetails(MarpolVO data)
        {
            return WrapOperationWithException(() => Channel.SaveMarpolDetails(data));
        }

        public MarpolVO ModifyMarpolDetails(MarpolVO data)
        {
            return WrapOperationWithException(() => Channel.ModifyMarpolDetails(data));
        }
        public MarpolVO GetMarpolReferenceData()
        {
            return WrapOperationWithException(() => Channel.GetMarpolReferenceData());
        }

    }
}