using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface ISAPPostingService
    {
        /// <summary>
        /// To Get VCN Details
        /// </summary>
        /// <returns></returns>
       [OperationContract]
       List<SAPPostingVO> GetSAPPostingVCN(string searchValue);

       /// <summary>
       /// To Get SAP Posting Grid Details
       /// </summary>
       /// <returns></returns>
       [OperationContract]
       List<SAPPostingVO> GetSAPPostingDetailsByVCN(string VCN);

       /// <summary>
       /// To Get SAP Posting Reference data While initialization
       /// </summary>
       /// <returns></returns>
       [OperationContract]
       SAPPostingVO GetSAPPostingReferenceVO();

       /// <summary>
       /// To Get SAP Posting Add Details
       /// </summary>
       /// <returns></returns>
       [OperationContract]
       string GetSAPPostingDetailsAddDetails(string VCN, string MsgType, string ReceavedARRNO, string MarineAccNo, int MarinePostingId, string PostingStatus, string isRevenueUpd, string RevAgentAccNo);

       /// <summary>
       /// To Add SAP Posting Data
       /// </summary>
       /// <param name="supcatdata"></param>
       /// <returns></returns>
       [OperationContract]
       SAPPostingVO AddSAPPosting(SAPPostingVO sapdata);

       /// <summary>
       /// To Get SAP Posting Account Details
       /// </summary>
       /// <returns></returns>
       [OperationContract]
       List<SAPPostingVO> GetSAPPostingAccountDetails(string VCN);

       /// <summary>
       /// To Get SAP Posting Account Details
       /// </summary>
       /// <returns></returns>
       [OperationContract]
       SAPPostingVO GetSAPPostingDetails(int SAPPostingID);

       /// <summary>
       /// To Add SAP Posting Invoice Data
       /// </summary>
       /// <param name="supcatdata"></param>
       /// <returns></returns>
       [OperationContract]
       SAPPostingVO AddSAPPostingInvoice(SAPPostingVO sapdata);


       /// <summary>
       /// To Get SAP Posting Invoice Details
       /// </summary>
       /// <returns></returns>
       [OperationContract]
       SAPInvoiceItem GetSAPInvoiceResponseDetails(string MarineOrderNo);
    }
}
