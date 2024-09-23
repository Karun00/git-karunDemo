using System.Collections.Generic;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;
using System;
using IPMS.Domain.Models;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface ISuppDryDockService : IDisposable
    {
        [OperationContract]
        List<SuppDryDockVO> GetSuppDryDockApplicationList();

        [OperationContract]
        SuppDryDockVO PostSuppDryDockApplication(SuppDryDockVO data);

        [OperationContract]
        SuppDryDockVO PutSuppDryDockApplication(SuppDryDockVO data);

        [OperationContract]
        void ApproveSuppDryDock(string SuppDryDockID, string remarks, string taskcode);

        [OperationContract]
        void RejectSuppDryDock(string SuppDryDockID, string remarks, string taskcode);

        [OperationContract]
        void ConfirmSuppDryDock(string SuppDryDockID, string comments, string taskcode);

        [OperationContract]
        void CancelSuppDryDock(string SuppDryDockID, string comments, string taskcode);

        [OperationContract]
        List<SuppDryDockVO> GetSuppDryDock(int SuppDryDockID);

        [OperationContract]
        List<ServiceRequestVCNDetails> GetSuppVCNDetails(string searchvalue);

        [OperationContract]
        SuppDryDockVO GetSuppDryDockVCN(string vcn);

        [OperationContract]
        List<SubCategory> GetDocumentTypes();

        //Add by srinivas
        [OperationContract]
        SuppDryDockVO Cancel(SuppDryDockVO servicedata);

        [OperationContract]
        void ApproveCancelConfirmSuppDryDock(string SuppDryDockID, string comments, string taskcode);

        [OperationContract]
        void RejectCancelConfirmSuppDryDock(string SuppDryDockID, string comments, string taskcode);

    }
}
