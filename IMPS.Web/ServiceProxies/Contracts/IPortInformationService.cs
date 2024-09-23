using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Web;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IPortInformationService : IDisposable
    {
        [OperationContract]
        PortContentVO AddPortContent(PortContentVO portContentData);

        [OperationContract]
        IEnumerable<PortContent> GetPortContentForTreeView();

        [OperationContract]
        PortContentVO ModifyPortContent(PortContentVO portContentData);

        [OperationContract]
        List<PortContentRoleVO> GetPortContentRoles(int id);

        [OperationContract]
        PortInformationReferenceVO GetPortInformationReferenceData();

        [OperationContract]
        Document GetDocumentDetails(int id);

        [OperationContract]
        List<Role> GetRolesForEmployee();
    }
}