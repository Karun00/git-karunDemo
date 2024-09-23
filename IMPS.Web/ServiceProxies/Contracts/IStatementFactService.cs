using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IStatementFactService : IDisposable
    {
        /// <summary>
        /// To Get Statement Fact Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<StatementVCNVO> StatementFactDetails(string vcnSearch, string vesselName);

        /// <summary>
        /// To Get Reference Data 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        StatementFactReferenceDataVO GetStatementFactReferenceDataVO();

        /// <summary>
        /// To Get Key Event Types   
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<SubCategoryVO> GetKeyEventTypes();

        /// <summary>
        /// To Get VCN Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<StatementVCNVO> GetStatementVCNS(string searchValue);

        /// <summary>
        /// To Get Vessel Information By VCN
        /// </summary>
        /// <param name="VCN"></param>
        /// <returns></returns>
        [OperationContract]
        StatementVCNVO GetVesselByVCN(string VCN);

        /// <summary>
        /// To Add Statement Of Fact 
        /// </summary>
        /// <param name="statementdata"></param>
        /// <returns></returns>
        [OperationContract]
        StatementVCNVO AddStatementFact(StatementVCNVO data);

        /// <summary>
        /// To Modify Statement Of Fact
        /// </summary>
        /// <param name="statementdata"></param>
        /// <returns></returns>
        [OperationContract]
        StatementVCNVO ModifyStatementFact(StatementVCNVO data);

        /// To Modify Statement Of Fact
        /// </summary>
        /// <param name="statementdata"></param>
        /// <returns></returns>
        [OperationContract]
        StatementVCNVO GetTugsByVCN(string VCN);


    }
}
