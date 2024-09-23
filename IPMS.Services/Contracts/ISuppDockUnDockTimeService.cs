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
    public interface ISuppDockUnDockTimeService
    {

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SuppDryDockVO> AllSuppDockUnDockTimeDetails();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        SuppDryDockVO ModifySuppDockUnDockTime(SuppDryDockVO data);
    }
}
