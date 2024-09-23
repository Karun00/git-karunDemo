using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface ISupplymentaryServiceRequestService : IDisposable
    {
        [OperationContract]
        List<SubCategoryVO> GetServiceType();

        [OperationContract]
        List<SuppServiceRequestVO> GetSupplymentaryServiceRequestList(string frmdate, string todate, string vcnSearch, string vesselName);

        [OperationContract]
        List<SuppServiceRequestVO> GetSupplymentaryServiceRequestListVcn(string VCN);

        [OperationContract]
        SuppServiceRequestVO PostSupplymentaryServiceRequest(SuppServiceRequestVO suppServiceRequestVO);

        [OperationContract]
        SuppServiceRequestVO ModifySupplymentaryServiceRequest(SuppServiceRequestVO suppServiceRequestVO);

        [OperationContract]
        List<SuppServiceRequestVO> AllSuppHotWorkInspectionDetails();

        [OperationContract]
        SuppServiceRequestVO GetSupplymentaryServiceRequest(string SuppServiceRequestId);

        //  Added by Srini
        [OperationContract]
        void ApproveSupplymentaryServiceRequest(string suppservicerequestid, string remarks, string taskcode);
        [OperationContract]
        void VerifySupplymentaryServiceRequest(string suppservicerequestid, string remarks, string taskcode);
        [OperationContract]
        void RejectSupplymentaryServiceRequest(string suppservicerequestid, string remarks, string taskcode);

        [OperationContract]
        List<SuppServiceRequestVO> AllSuppDockUnDockTimeDetails();

        [OperationContract]
        List<IMDGInformationVO> GetIMDGForSupplymentaryServiceRequest(string vcn);

        [OperationContract]
        VesselCallMovementVO GetEtbEtubFromVcn(string vcn);
        [OperationContract]
        SuppServiceRequestVO Cancel(SuppServiceRequestVO servicedata);

        [OperationContract]
        List<ServiceRequestVCNDetails> GetVCNDetailsForSuppServiceRequest(string searchValue);
        [OperationContract]
        List<SuppServiceRequestVO> GetSupplementaryGridDetails(string frmdate, string todate, string vcnSearch, string vesselName);

    }
}
