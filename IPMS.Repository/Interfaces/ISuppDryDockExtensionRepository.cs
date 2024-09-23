using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Repository
{
    public interface ISuppDryDockExtensionRepository
    {
        List<ServiceRequestVCNsForDryDockExts> GetSuppVCNDetailsForExtension(string portcode, int userid);

        AgentVO GetSuppVCNDetailsForExtensionByVCN(string vcn);

        List<ServiceRequestVCNsForDryDockExts> GetSuppDryDockExtensionList(string portcode);

        List<ServiceRequestVCNsForDryDockExts> GetSuppDryDockExtensionByID(string portcode, string SuppDryDockExtensionID);
        

        SuppDryDockExtension GetSuppDryDockExtensionApproveid(string suppdrydockextensionid);

        SuppDryDockExtensionVO GetSuppDryDockExtensionRequestDetailsByID(string suppdrydockextensionid);
    }
}
