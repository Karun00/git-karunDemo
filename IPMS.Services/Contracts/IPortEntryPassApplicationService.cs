using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IPortEntryPassApplicationService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        PortEntryPassApplicationReferenceVO GetPortEntryPassReferenceData();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        PermitRequestVO AddPortEntryPass(PermitRequestVO permitrequest);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<PermitRequestVO> GetPortEntryPassRequestlist();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<PermitRequestVO> GetPortEntryPassRequestlistSearch(PermitRequestSearchVO Searchmdl);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<PermitRequestVO> GetPortEntryPassRequestlistForSsaSearch(PermitRequestSearchVO Searchmdl);

        
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<PermitRequestVO> GetPortEntryPassRequestlistForSapsSearch(PermitRequestSearchVO Searchmdl);

        
        

        [OperationContract]
        [FaultContract(typeof(Exception))]
        PermitRequestVO GetPortEntryPassRequest(string refrenceNumber, int flag, string portcode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        PermitRequestVO EditPortEntryPass(PermitRequestVO permitrequest);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        PermitRequestVO UpdateRecodeWithComments(PermitRequestVO permitrequest);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        PermitRequestVO ForwordRecodeWithComments(PermitRequestVO permitrequest);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<PermitRequestVO> GetPortEntryPassRequestlistForSsa();


        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<PermitRequestVO> GetApprovedPermitrequestlist();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<PermitRequestVO> GetPortEntryPassRequestlistForSaps();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        PermitRequestVO AddverificationDetails(PermitRequestVO permitrequest);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        PermitRequestVO ApprovalDenyPortEntryPass(PermitRequestVO permitrequest);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        PermitRequestVO AppealApproveDenyPortEntryPass(PermitRequestVO permitrequest);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        PermitRequestVO AddInternalEmployeePermitDetails(PermitRequestVO permitrequest);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<PermitRequestVO> GetInternalEmployeePermitlist();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        PermitRequestVO IssuePortEntryPass(PermitRequestVO permitrequest);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<PermitRequestVO> GetInternalEmployeetobeapprovedPermitlist();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        PermitRequestVO ApproveInternalEmployeePermitDetails(PermitRequestVO permitrequest);


        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<PermitRequestVO> GetInternalEmployeePermitlistSearch(PermitRequestSearchVO Searchmdl);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<PermitRequestVO> GetInternalEmployeetobeapprovedPermitlistSearch(PermitRequestSearchVO Searchmdl);


        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<PermitRequestVO> GetApprovedPermitrequestlistSearch(PermitRequestSearchVO Searchmdl);


        [OperationContract]
        [FaultContract(typeof(Exception))]
        Entity GetEntities(string entityCode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        int GetvalidatePortEntryPassRequestforSsasaps(int id, string flag);


        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategoryCodeNameWithSupCatCodeVO> GetSubAccessAreasForRB(string supCatCode);


        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategoryCodeNameWithSupCatCodeVO> GetAreas(string supCatCode);

        
    }
}
