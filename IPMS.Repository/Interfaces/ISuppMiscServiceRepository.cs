using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Repository
{
    public interface ISuppMiscServiceRepository
    {
        List<SuppDryDockVO> GetSuppMiscServiceDetails(string portCode);

        List<SuppMiscServiceVO> GetSuppMiscServiceRecordDetails(int suppdrydockid);

        List<SuppMiscServiceVO> GetServiceTypes();
    }
}
