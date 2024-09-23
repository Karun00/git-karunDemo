using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IPortEntryPassApplicationService : IDisposable
    {
        [OperationContract]
        PortEntryPassApplicationReferenceVO GetPortEntryPassReferenceData();
        [OperationContract]
        PermitRequestVO AddPortEntryPass(PermitRequestVO permitrequest);
        [OperationContract]
        List<PermitRequestVO> GetPortEntryPassRequestlist();
        [OperationContract]
        List<PermitRequestVO> GetPortEntryPassRequestlistForSsa();
        [OperationContract]
        List<PermitRequestVO> GetPortEntryPassRequestlistForSaps();
        [OperationContract]
        PermitRequestVO GetPortEntryPassRequest(string refrenceNumber, int flag, string portcode);
        [OperationContract]
        PermitRequestVO EditPortEntryPass(PermitRequestVO permitrequest);
        [OperationContract]
        PermitRequestVO UpdateRecodeWithComments(PermitRequestVO permitrequest);
        [OperationContract]
        PermitRequestVO ForwordRecodeWithComments(PermitRequestVO permitrequest);
        [OperationContract]
        PermitRequestVO AddverificationDetails(PermitRequestVO permitrequest);
        [OperationContract]
        PermitRequestVO ApprovalDenyPortEntryPass(PermitRequestVO permitrequest);
        [OperationContract]
        PermitRequestVO AppealApproveDenyPortEntryPass(PermitRequestVO permitrequest);

        [OperationContract]
        PermitRequestVO AddInternalEmployeePermitDetails(PermitRequestVO permitrequest);

        [OperationContract]
        List<PermitRequestVO> GetApprovedPermitrequestlist();

        [OperationContract]
        List<PermitRequestVO> GetInternalEmployeePermitlist();
        [OperationContract]
        PermitRequestVO IssuePortEntryPass(PermitRequestVO permitrequest);
        [OperationContract]
        List<PermitRequestVO> GetInternalEmployeetobeapprovedPermitlist();
        [OperationContract]
        PermitRequestVO ApproveInternalEmployeePermitDetails(PermitRequestVO permitrequest);
        [OperationContract]
        int GetvalidatePortEntryPassRequestforSsasaps(int id, string flag);
        [OperationContract]
        List<SubCategoryCodeNameWithSupCatCodeVO> GetSubAccessAreasForRB(string supCatCode);
        [OperationContract]
        List<SubCategoryCodeNameWithSupCatCodeVO> GetAreas(string supCatCode);
        [OperationContract]
        List<PermitRequestVO> GetPortEntryPassRequestlistSearch(PermitRequestSearchVO Searchmdl);
        [OperationContract]
        List<PermitRequestVO> GetInternalEmployeetobeapprovedPermitlistSearch(PermitRequestSearchVO Searchmdl);
        [OperationContract]
        List<PermitRequestVO> GetPortEntryPassRequestlistForSsaSearch(PermitRequestSearchVO Searchmdl);
        [OperationContract]
        List<PermitRequestVO> GetPortEntryPassRequestlistForSapsSearch(PermitRequestSearchVO Searchmdl);

        [OperationContract]
        List<PermitRequestVO> GetInternalEmployeePermitlistSearch(PermitRequestSearchVO Searchmdl);

        [OperationContract]
        List<PermitRequestVO> GetApprovedPermitrequestlistSearch(PermitRequestSearchVO Searchmdl); 
    }
}
