using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Clients
{
    public class StatementFactClient : UserClientBase<IStatementFactService>, IStatementFactService
    {
        public List<StatementVCNVO> StatementFactDetails(string vcnSearch, string vesselName)
        {
            return WrapOperationWithException(() => Channel.StatementFactDetails(vcnSearch, vesselName));
        }
        public StatementFactReferenceDataVO GetStatementFactReferenceDataVO()
        {
            return WrapOperationWithException(() => Channel.GetStatementFactReferenceDataVO());
        }
        public List<SubCategoryVO> GetKeyEventTypes()
        {
            return WrapOperationWithException(() => Channel.GetKeyEventTypes());
        }
        public List<StatementVCNVO> GetStatementVCNS(string searchValue)
        {
            return WrapOperationWithException(() => Channel.GetStatementVCNS(searchValue));
        }

        public StatementVCNVO GetVesselByVCN(string VCN)
        {
            return WrapOperationWithException(() => Channel.GetVesselByVCN(VCN));
        }

        public StatementVCNVO AddStatementFact(StatementVCNVO data)
        {
            return WrapOperationWithException(() => Channel.AddStatementFact(data));
        }

        public StatementVCNVO ModifyStatementFact(StatementVCNVO data)
        {
            return WrapOperationWithException(() => Channel.ModifyStatementFact(data));
        }

        public StatementVCNVO GetTugsByVCN(string VCN)
        {
            return WrapOperationWithException(() => Channel.GetTugsByVCN(VCN));
        }
    }
}