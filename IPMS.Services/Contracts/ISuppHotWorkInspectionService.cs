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
    public interface ISuppHotWorkInspectionService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SuppHotWorkInspectionVO> AllSuppHotWorkInspectionDetails();


        [OperationContract]
        [FaultContract(typeof(Exception))]
        SuppHotWorkInspectionVO AddSuppHotWorkInspection(SuppHotWorkInspectionVO SuppHotWorkInspectiondata);


        //[OperationContract]
        //[FaultContract(typeof(Exception))]
        //SuppHotWorkInspectionVO ModifySuppHotWorkInspection(SuppHotWorkInspectionVO SuppHotWorkInspectiondata);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        SuppServiceRequestVO ModifySuppHotWorkInspection(SuppServiceRequestVO SuppHotWorkInspectiondata);


        [OperationContract]
        [FaultContract(typeof(Exception))]
        SuppHotWorkInspectionVO DeleteSuppHotWorkInspection(long id);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        string ModifyHotWorkInspectionPermitStatus(long id);
    }
}
