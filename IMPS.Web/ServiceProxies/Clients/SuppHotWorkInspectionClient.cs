using System.Collections.Generic;
using System.ServiceModel;
using IPMS.ServiceProxies.Contracts;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Web.ServiceProxies;

namespace IPMS.ServiceProxies.Clients
{
    public class SuppHotWorkInspectionClient : UserClientBase<ISuppHotWorkInspectionService>, ISuppHotWorkInspectionService
    {

        public List<SuppHotWorkInspectionVO> AllSuppHotWorkInspectionDetails()
        {
            return WrapOperationWithException(() => Channel.AllSuppHotWorkInspectionDetails());
        }

        public SuppHotWorkInspectionVO AddSuppHotWorkInspection(SuppHotWorkInspectionVO SuppHotWorkInspectiondata)
        {
            return WrapOperationWithException(() => Channel.AddSuppHotWorkInspection(SuppHotWorkInspectiondata));
        }

        //public SuppHotWorkInspectionVO ModifySuppHotWorkInspection(SuppHotWorkInspectionVO SuppHotWorkInspectiondata)
        //{
        //    return WrapOperationWithException(() => Channel.ModifySuppHotWorkInspection(SuppHotWorkInspectiondata));
        //}

        public SuppServiceRequestVO ModifySuppHotWorkInspection(SuppServiceRequestVO SuppHotWorkInspectiondata)
        {
            return WrapOperationWithException(() => Channel.ModifySuppHotWorkInspection(SuppHotWorkInspectiondata));
        }



        public SuppHotWorkInspectionVO DeleteSuppHotWorkInspection(long id)
        {
            return WrapOperationWithException(() => Channel.DeleteSuppHotWorkInspection(id));
        }

        public string ModifyHotWorkInspectionPermitStatus(long id)
        {
           return WrapOperationWithException(() => Channel.ModifyHotWorkInspectionPermitStatus(id));
        }

    }
}