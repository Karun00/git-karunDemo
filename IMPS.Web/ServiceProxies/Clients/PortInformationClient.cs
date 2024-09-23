using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPMS.ServiceProxies.Clients
{
    public class PortInformationClient : UserClientBase<IPortInformationService>, IPortInformationService
    {
        public PortContentVO AddPortContent(PortContentVO portContentData)
        {
            return WrapOperationWithException(() => Channel.AddPortContent(portContentData));
        }

        public IEnumerable<PortContent> GetPortContentForTreeView()
        {
            return WrapOperationWithException(() => Channel.GetPortContentForTreeView());
        }

        public PortContentVO ModifyPortContent(PortContentVO portContentData)
        {
            return WrapOperationWithException(() => Channel.ModifyPortContent(portContentData));
        }

        public List<PortContentRoleVO> GetPortContentRoles(int id)
        {
            return WrapOperationWithException(() => Channel.GetPortContentRoles(id));
        }

        public PortInformationReferenceVO GetPortInformationReferenceData()
        {
            return WrapOperationWithException(() => Channel.GetPortInformationReferenceData());
        }

        public Document GetDocumentDetails(int id)
        {
            return WrapOperationWithException(() => Channel.GetDocumentDetails(id));
        }

        public List<Role> GetRolesForEmployee()
        {
            return WrapOperationWithException(() => Channel.GetRolesForEmployee());
        }
    }
}