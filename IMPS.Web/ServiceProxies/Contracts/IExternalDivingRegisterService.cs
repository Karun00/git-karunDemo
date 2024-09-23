using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IExternalDivingRegisterService : IDisposable
    {
        [OperationContract]
        ExternalDivingRegisterVO AddExternalDivingRegister(ExternalDivingRegisterVO externalDivingRegisterData);

        [OperationContract]
        ExternalDivingRegisterVO ModifyExternalDivingRegister(ExternalDivingRegisterVO externalDivingRegisterData);

        [OperationContract]
        ExternalDivingRegisterVO DeleteExternalDivingRegister(long id);

        [OperationContract]
        List<ExternalDivingRegisterVO> AllExternalDivingRegisterDetails();

        [OperationContract]
        List<LicenseRequestVO> GetAllCompanies();

        [OperationContract]
        List<VesselVO> GetAllVessels();
        
        [OperationContract]
        ExternalDivingRegisterVO GetDivingReferenceData();        
    }
}
