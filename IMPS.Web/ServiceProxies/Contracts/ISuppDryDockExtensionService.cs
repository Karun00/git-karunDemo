using System.Collections.Generic;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;
using System;
using IPMS.Domain.Models;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface ISuppDryDockExtensionService : IDisposable
    {
        [OperationContract]
        SuppDryDockExtensionVO PostSuppDryDockExtension(SuppDryDockExtensionVO data);

        [OperationContract]
        SuppDryDockExtensionVO PutSuppDryDockExtension(SuppDryDockExtensionVO data);

        [OperationContract]
        List<ServiceRequestVCNsForDryDockExts> GetSuppVCNDetailsForExtension();


        [OperationContract]
        AgentVO GetSuppVCNDetailsForExtensionByVCN(string vcn);

        [OperationContract]
        List<ServiceRequestVCNsForDryDockExts> GetSuppDryDockExtensionList();

        [OperationContract]
        List<ServiceRequestVCNsForDryDockExts> GetSuppDryDockExtensionByID(string SuppDryDockExtensionID);

        [OperationContract]
        void ApproveSuppDryDockExtension(string suppdrydockextensionid, string remarks, string taskcode);

        [OperationContract]
        void RejectSuppDryDockExtension(string suppdrydockextensionid, string remarks, string taskcode);
    }
}
