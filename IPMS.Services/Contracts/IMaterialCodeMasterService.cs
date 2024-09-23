using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IMaterialCodeMasterService
    {
        /// <summary>
        /// To get Material Code Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<MaterialCodeMasterVO> GetMaterialCodeDetails();
    }
}
