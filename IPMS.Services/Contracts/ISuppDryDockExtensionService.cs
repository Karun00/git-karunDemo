using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceContract]
    public interface ISuppDryDockExtensionService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        SuppDryDockExtensionVO PostSuppDryDockExtension(SuppDryDockExtensionVO data);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ServiceRequestVCNsForDryDockExts> GetSuppVCNDetailsForExtension();


        [OperationContract]
        [FaultContract(typeof(Exception))]
        AgentVO GetSuppVCNDetailsForExtensionByVCN(string vcn);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ServiceRequestVCNsForDryDockExts> GetSuppDryDockExtensionList();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ServiceRequestVCNsForDryDockExts> GetSuppDryDockExtensionByID(string SuppDryDockExtensionID);


        [OperationContract]
        [FaultContract(typeof(Exception))]
        SuppDryDockExtensionVO PutSuppDryDockExtension(SuppDryDockExtensionVO data);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void ApproveSuppDryDockExtension(string suppdrydockextensionid, string remarks, string taskcode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void RejectSuppDryDockExtension(string suppdrydockextensionid, string remarks, string taskcode);
    }
}
