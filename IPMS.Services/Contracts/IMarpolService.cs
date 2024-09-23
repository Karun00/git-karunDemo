using System;
using Core.Repository.Providers.EntityFramework;
using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Data.Context;
using System.Collections.Generic;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IMarpolService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<MarpolVO> GetMarpolDetails();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        MarpolVO SaveMarpolDetails(MarpolVO data);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        MarpolVO ModifyMarpolDetails(MarpolVO data);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        MarpolVO GetMarpolReferenceData();
    }
}
