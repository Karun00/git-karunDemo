using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPMS.ServiceProxies.Clients
{
    public class PortEntryPassApplicationServiceClient : UserClientBase<IPortEntryPassApplicationService>, IPortEntryPassApplicationService
    {
        public PortEntryPassApplicationReferenceVO GetPortEntryPassReferenceData()
        {
            return WrapOperationWithException(() => Channel.GetPortEntryPassReferenceData());
        }
        public PermitRequestVO AddPortEntryPass(PermitRequestVO permitrequest)
        {
            return WrapOperationWithException(() => Channel.AddPortEntryPass(permitrequest));
        }
        public List<PermitRequestVO> GetPortEntryPassRequestlist()
        {
            return WrapOperationWithException(() => Channel.GetPortEntryPassRequestlist());
        }
        public List<PermitRequestVO> GetPortEntryPassRequestlistSearch(PermitRequestSearchVO Searchmdl)
        {
            return WrapOperationWithException(() => Channel.GetPortEntryPassRequestlistSearch(Searchmdl));
        }
        public PermitRequestVO GetPortEntryPassRequest(string refrenceNumber, int flag, string portcode)
        {
            return WrapOperationWithException(() => Channel.GetPortEntryPassRequest(refrenceNumber, flag, portcode));
        }
        public PermitRequestVO EditPortEntryPass(PermitRequestVO permitrequest)
        {
            return WrapOperationWithException(() => Channel.EditPortEntryPass(permitrequest));
        }
        public PermitRequestVO UpdateRecodeWithComments(PermitRequestVO permitrequest)
        {
            return WrapOperationWithException(() => Channel.UpdateRecodeWithComments(permitrequest));
        }
        public PermitRequestVO ForwordRecodeWithComments(PermitRequestVO permitrequest)
        { return WrapOperationWithException(() => Channel.ForwordRecodeWithComments(permitrequest)); }

        public List<PermitRequestVO> GetPortEntryPassRequestlistForSsa()
        {
            return WrapOperationWithException(() => Channel.GetPortEntryPassRequestlistForSsa());
        }
        public List<PermitRequestVO> GetPortEntryPassRequestlistForSsaSearch(PermitRequestSearchVO Searchmdl)
        {
            return WrapOperationWithException(() => Channel.GetPortEntryPassRequestlistForSsaSearch(Searchmdl));
        }
        public List<PermitRequestVO> GetPortEntryPassRequestlistForSapsSearch(PermitRequestSearchVO Searchmdl)
        {
            return WrapOperationWithException(() => Channel.GetPortEntryPassRequestlistForSapsSearch(Searchmdl));
        }


        public List<PermitRequestVO> GetInternalEmployeePermitlistSearch(PermitRequestSearchVO Searchmdl)
        {
            return WrapOperationWithException(() => Channel.GetInternalEmployeePermitlistSearch(Searchmdl));
        }
        public List<PermitRequestVO> GetApprovedPermitrequestlistSearch(PermitRequestSearchVO Searchmdl)
        {
            return WrapOperationWithException(() => Channel.GetApprovedPermitrequestlistSearch(Searchmdl));
        }

        public List<PermitRequestVO> GetInternalEmployeetobeapprovedPermitlistSearch(PermitRequestSearchVO Searchmdl)
        {
            return WrapOperationWithException(() => Channel.GetInternalEmployeetobeapprovedPermitlistSearch(Searchmdl)); 
        }

        public List<PermitRequestVO> GetPortEntryPassRequestlistForSaps()
        {
            return WrapOperationWithException(() => Channel.GetPortEntryPassRequestlistForSaps());
        }
        public PermitRequestVO AddverificationDetails(PermitRequestVO permitrequest)
        { return WrapOperationWithException(() => Channel.AddverificationDetails(permitrequest)); }
        public PermitRequestVO ApprovalDenyPortEntryPass(PermitRequestVO permitrequest)
        { return WrapOperationWithException(() => Channel.ApprovalDenyPortEntryPass(permitrequest)); }

        public PermitRequestVO AppealApproveDenyPortEntryPass(PermitRequestVO permitrequest)
        { return WrapOperationWithException(() => Channel.AppealApproveDenyPortEntryPass(permitrequest)); }

        public PermitRequestVO AddInternalEmployeePermitDetails(PermitRequestVO permitrequest)
        { return WrapOperationWithException(() => Channel.AddInternalEmployeePermitDetails(permitrequest)); }

        public List<PermitRequestVO> GetApprovedPermitrequestlist()
        {
            return WrapOperationWithException(() => Channel.GetApprovedPermitrequestlist());
        }


        public List<PermitRequestVO> GetInternalEmployeePermitlist()
        {
            return WrapOperationWithException(() => Channel.GetInternalEmployeePermitlist());
        }
        public PermitRequestVO IssuePortEntryPass(PermitRequestVO permitrequest)
        {
            return WrapOperationWithException(() => Channel.IssuePortEntryPass(permitrequest));
        }
        public List<PermitRequestVO> GetInternalEmployeetobeapprovedPermitlist()
        { return WrapOperationWithException(() => Channel.GetInternalEmployeetobeapprovedPermitlist()); }

        public PermitRequestVO ApproveInternalEmployeePermitDetails(PermitRequestVO permitrequest)
        { return WrapOperationWithException(() => Channel.ApproveInternalEmployeePermitDetails(permitrequest)); }

        public int GetvalidatePortEntryPassRequestforSsasaps(int id, string flag)
        { return WrapOperationWithException(() => Channel.GetvalidatePortEntryPassRequestforSsasaps(id, flag)); }
        public List<SubCategoryCodeNameWithSupCatCodeVO> GetSubAccessAreasForRB(string supCatCode)
        {
            return WrapOperationWithException(() => Channel.GetSubAccessAreasForRB(supCatCode));
        }

        public List<SubCategoryCodeNameWithSupCatCodeVO> GetAreas(string supCatCode)
        {
            return WrapOperationWithException(() => Channel.GetAreas(supCatCode));
        }

    }
}