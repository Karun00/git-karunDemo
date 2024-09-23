using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Clients
{
    public class SuppMiscServiceRecordingClient : UserClientBase<ISuppMiscServiceRecordingService>, ISuppMiscServiceRecordingService
    {
        public List<SuppDryDockVO> SuppMiscServiceDetails()
        {
            return WrapOperationWithException(() => Channel.SuppMiscServiceDetails());
        }
        public SuppMiscServiceVO GetSuppMiscReferenceVO()
        {
            return WrapOperationWithException(() => Channel.GetSuppMiscReferenceVO());
        }

        public List<SuppMiscServiceVO> SuppMiscServiceRecordingDetails(int SuppDryDockID)
        {
            return WrapOperationWithException(() => Channel.SuppMiscServiceRecordingDetails(SuppDryDockID));
        }

        public SuppMiscServiceVO ModifySuppMiscServiceRecord(SuppMiscServiceVO data)
        {
            return WrapOperationWithException(() => Channel.ModifySuppMiscServiceRecord(data));
        }



    }
}