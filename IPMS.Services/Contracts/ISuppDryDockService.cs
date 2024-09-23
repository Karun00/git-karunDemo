using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceContract]
    public interface ISuppDryDockService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SuppDryDockVO> GetSuppDryDockApplicationList();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        SuppDryDockVO PostSuppDryDockApplication(SuppDryDockVO data);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        SuppDryDockVO PutSuppDryDockApplication(SuppDryDockVO data);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void ApproveSuppDryDock(string SuppDryDockID, string remarks, string taskcode);


        [OperationContract]
        [FaultContract(typeof(Exception))]
        void RejectSuppDryDock(string SuppDryDockID, string remarks, string taskcode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void ConfirmSuppDryDock(string SuppDryDockID, string comments, string taskcode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void CancelSuppDryDock(string SuppDryDockID, string comments, string taskcode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SuppDryDockVO> GetSuppDryDock(int SuppDryDockID);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ServiceRequestVCNDetails> GetSuppVCNDetails(string searchvalue);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        SuppDryDockVO GetSuppDryDockVCN(string vcn);

        [OperationContract]
        List<SubCategory> GetDocumentTypes();

        //Add by Srinivas

        [OperationContract]
        [FaultContract(typeof(Exception))]
        SuppDryDockVO Cancel(SuppDryDockVO servicedata);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void ApproveCancelConfirmSuppDryDock(string SuppDryDockID, string comments, string taskcode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void RejectCancelConfirmSuppDryDock(string SuppDryDockID, string comments, string taskcode);
    }
}
