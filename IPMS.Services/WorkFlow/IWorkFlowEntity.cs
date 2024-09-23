using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Services.WorkFlow
{
    public interface IWorkFlowEntity
    {
        //void Create();
        void ExecuteTask(string workflowTaskCode);
        void UpdateStatus();
        //void SetWorkFlowData(out WorkflowInstance instance);
        Entity Entity { get; }
        string ReferenceId { get; }
        string Remarks { get; }
        string ReferenceData { get; }
        //Some Entities have Multiple Port codes eg. Agent Registration etc.,
        List<string> PortCodes { get; }
        //int userid { get; } //Maintained in ServiceBase
        void SetWorkFlowId(int workFlowInstanceId, string portCode);
        CompanyVO GetCompanyDetails(int step);
    }
}
