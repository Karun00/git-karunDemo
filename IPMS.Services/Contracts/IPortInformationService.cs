using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IPortInformationService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        PortContentVO AddPortContent(PortContentVO portContentData);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        IEnumerable<PortContent> GetPortContentForTreeView();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        PortContentVO ModifyPortContent(PortContentVO portContentData);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<PortContentRoleVO> GetPortContentRoles(int id);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        PortInformationReferenceVO GetPortInformationReferenceData();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        Document GetDocumentDetails(int id);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<Role> GetRolesForEmployee();
    }
}
