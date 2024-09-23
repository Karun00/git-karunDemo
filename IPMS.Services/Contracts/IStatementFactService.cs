using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IStatementFactService
    {
        /// <summary>
        /// To Get Statement Fact Details 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<StatementVCNVO> StatementFactDetails(string vcnSearch, string vesselName);

        /// <summary>
        /// To Get Reference Data 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        StatementFactReferenceDataVO GetStatementFactReferenceDataVO();

        /// <summary>
        /// To Get Key Event Types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategoryVO> GetKeyEventTypes();

        /// <summary>
        /// To Get VCN Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<StatementVCNVO> GetStatementVCNS(string searchValue);

        /// <summary>
        /// To Get Vessel Information By VCN
        /// </summary>
        /// <param name="VCN"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        StatementVCNVO GetVesselByVCN(string VCN);

        /// <summary>
        /// To Add Berth Data
        /// </summary>
        /// <param name="statementdata"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        StatementVCNVO AddStatementFact(StatementVCNVO data);

        /// <summary>
        /// To Modify Berth Data
        /// </summary>
        /// <param name="statementdata"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        StatementVCNVO ModifyStatementFact(StatementVCNVO data);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        StatementVCNVO GetTugsByVCN(string VCN);
    }
}
