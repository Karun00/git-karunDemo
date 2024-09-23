using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Repository
{
    public interface IPortInformationRepository
    {
        PortContentVO AddPortContent(PortContentVO portContentData, int userId, string portCode);
        PortContentVO ModifyPortContent(PortContentVO portContentData, int userId, string portCode);
        List<Role> GetRolesForEmployee();
        List<PortContentRoleVO> GetPortContentRoles(int id);
        Document GetDocumentDetails(int id);
        IEnumerable<PortContent> GetPortContentForTreeView(int userId, string loginName);
    }
}
