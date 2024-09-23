using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web.Mvc;
using Core.Repository.Providers.EntityFramework;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IExternalDivingRegisterService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        ExternalDivingRegisterVO AddExternalDivingRegister(ExternalDivingRegisterVO externalDivingRegisterData);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        ExternalDivingRegisterVO ModifyExternalDivingRegister(ExternalDivingRegisterVO externalDivingRegisterData);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        ExternalDivingRegisterVO DeleteExternalDivingRegister(long id);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ExternalDivingRegisterVO> AllExternalDivingRegisterDetails();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<VesselVO> GetAllVessels();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<LicenseRequestVO> GetAllCompanies();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        ExternalDivingRegisterVO GetDivingReferenceData();
    }
}
