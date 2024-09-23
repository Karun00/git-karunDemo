using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Services.WorkFlow
{
    public interface IWorkFlowEngine<T> where T : IWorkFlowEntity
    {
        void Process(T workflowEntity, string workFlowTaskCode);
    }
}
