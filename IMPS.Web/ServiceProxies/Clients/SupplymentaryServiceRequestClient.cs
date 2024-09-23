using System.Collections.Generic;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using IPMS.Domain.Models;

namespace IPMS.ServiceProxies.Clients
{
    public class SupplymentaryServiceRequestClient : UserClientBase<ISupplymentaryServiceRequestService>, ISupplymentaryServiceRequestService
    {
        public List<SubCategoryVO> GetServiceType()
        {
            return WrapOperationWithException(() => Channel.GetServiceType());
        }

        public List<SuppServiceRequestVO> GetSupplymentaryServiceRequestList(string frmdate, string todate, string vcnSearch, string vesselName)
        {
            return WrapOperationWithException(() => Channel.GetSupplymentaryServiceRequestList(frmdate, todate, vcnSearch, vesselName));
        }
        public List<SuppServiceRequestVO> GetSupplymentaryServiceRequestListVcn(string VCN)
        {
            return WrapOperationWithException(() => Channel.GetSupplymentaryServiceRequestListVcn(VCN));
        }
        public SuppServiceRequestVO GetSupplymentaryServiceRequest(string SuppServiceRequestId)
        {
            return WrapOperationWithException(() => Channel.GetSupplymentaryServiceRequest(SuppServiceRequestId));
        }

        public SuppServiceRequestVO PostSupplymentaryServiceRequest(SuppServiceRequestVO suppServiceRequestVO)
        {
            return WrapOperationWithException(() => Channel.PostSupplymentaryServiceRequest(suppServiceRequestVO));
        }

        public SuppServiceRequestVO ModifySupplymentaryServiceRequest(SuppServiceRequestVO suppServiceRequestVO)
        {
            return WrapOperationWithException(() => Channel.ModifySupplymentaryServiceRequest(suppServiceRequestVO));
        }

        public List<SuppServiceRequestVO> AllSuppHotWorkInspectionDetails()
        {
            return WrapOperationWithException(() => Channel.AllSuppHotWorkInspectionDetails());
        }
        public List<SuppServiceRequestVO> AllSuppDockUnDockTimeDetails()
        {
            return WrapOperationWithException(() => Channel.AllSuppDockUnDockTimeDetails());
        }
        public void ApproveSupplymentaryServiceRequest(string suppservicerequestid, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.ApproveSupplymentaryServiceRequest(suppservicerequestid, remarks, taskcode));
        }

        public void VerifySupplymentaryServiceRequest(string suppservicerequestid, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.VerifySupplymentaryServiceRequest(suppservicerequestid, remarks, taskcode));
        }

        public void RejectSupplymentaryServiceRequest(string suppservicerequestid, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.RejectSupplymentaryServiceRequest(suppservicerequestid, remarks, taskcode));
        }
        public List<IMDGInformationVO> GetIMDGForSupplymentaryServiceRequest(string vcn)
        {
            return WrapOperationWithException(() => Channel.GetIMDGForSupplymentaryServiceRequest(vcn));
        }
        public VesselCallMovementVO GetEtbEtubFromVcn(string vcn)
        {
            return WrapOperationWithException(() => Channel.GetEtbEtubFromVcn(vcn));
        }

        public SuppServiceRequestVO Cancel(SuppServiceRequestVO servicedata)
        {
            return WrapOperationWithException(() => Channel.Cancel(servicedata));
        }

        public List<ServiceRequestVCNDetails> GetVCNDetailsForSuppServiceRequest(string searchValue)
        {
            return WrapOperationWithException(() => Channel.GetVCNDetailsForSuppServiceRequest(searchValue));
        }

        public List<SuppServiceRequestVO> GetSupplementaryGridDetails(string frmdate, string todate, string vcnSearch, string vesselName)
        {
            return WrapOperationWithException(() => Channel.GetSupplementaryGridDetails(frmdate, todate, vcnSearch, vesselName));
        }
    }
}