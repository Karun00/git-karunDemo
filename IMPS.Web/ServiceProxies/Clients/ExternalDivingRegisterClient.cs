using System.Collections.Generic;
using System.ServiceModel;
using IPMS.ServiceProxies.Contracts;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Web.ServiceProxies;

namespace IPMS.ServiceProxies.Clients
{
    public class ExternalDivingRegisterClient : UserClientBase<IExternalDivingRegisterService>, IExternalDivingRegisterService
    {
        public List<ExternalDivingRegisterVO> AllExternalDivingRegisterDetails()
        {
            return WrapOperationWithException(() => Channel.AllExternalDivingRegisterDetails());
        }

        public ExternalDivingRegisterVO AddExternalDivingRegister(ExternalDivingRegisterVO externalDivingRegisterData)
        {
            return WrapOperationWithException(() => Channel.AddExternalDivingRegister(externalDivingRegisterData));
        }

        public ExternalDivingRegisterVO ModifyExternalDivingRegister(ExternalDivingRegisterVO externalDivingRegisterData)
        {
            return WrapOperationWithException(() => Channel.ModifyExternalDivingRegister(externalDivingRegisterData));
        }

        public ExternalDivingRegisterVO DeleteExternalDivingRegister(long id)
        {
            return WrapOperationWithException(() => Channel.DeleteExternalDivingRegister(id));
        }

        public List<LicenseRequestVO> GetAllCompanies()
        {
            return WrapOperationWithException(() => Channel.GetAllCompanies());
        }
        public List<VesselVO> GetAllVessels()
        {
            return WrapOperationWithException(() => Channel.GetAllVessels());
        }

        public ExternalDivingRegisterVO GetDivingReferenceData()
        {
            return WrapOperationWithException(() => Channel.GetDivingReferenceData());
        }
    }
}