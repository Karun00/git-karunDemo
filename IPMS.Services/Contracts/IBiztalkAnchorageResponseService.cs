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
  public interface IBiztalkAnchorageResponseService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<AnchorageDataVO> BiztalkAnchorageResponse(List<AnchorageDataVO> objAnchoragedatalst);
    }
}
