using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface ISuppHotWorkInspectionService : IDisposable
    {
        [OperationContract]
        SuppHotWorkInspectionVO AddSuppHotWorkInspection(SuppHotWorkInspectionVO SuppHotWorkInspectiondata);

        //[OperationContract]
        //SuppHotWorkInspectionVO ModifySuppHotWorkInspection(SuppHotWorkInspectionVO SuppHotWorkInspectiondata);


        [OperationContract]
        SuppServiceRequestVO ModifySuppHotWorkInspection(SuppServiceRequestVO SuppHotWorkInspectiondata);



        [OperationContract]
        SuppHotWorkInspectionVO DeleteSuppHotWorkInspection(long id);



        [OperationContract]
        List<SuppHotWorkInspectionVO> AllSuppHotWorkInspectionDetails();

        [OperationContract]
        string ModifyHotWorkInspectionPermitStatus(long id);
    }
}
