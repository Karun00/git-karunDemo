using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IMaterialCodeMasterService : IDisposable
    {
        /// <summary>
        /// To get Material Code Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<MaterialCodeMasterVO> GetMaterialCodeDetails();      
    }
}
