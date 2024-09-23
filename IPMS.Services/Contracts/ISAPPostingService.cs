using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceContract]
    public interface ISAPPostingService
    {
        /// <summary>
        /// To Get VCN Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SAPPostingVO> GetSAPPostingVCN(string searchValue);

        /// <summary>
        ///  To Get SAP Posting Details by VCN
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SAPPostingVO> GetSAPPostingDetailsByVCN(string VCN);


        /// <summary>
        /// To Get SAP Posting  Reference data While initialization
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        SAPPostingVO GetSAPPostingReferenceVO();

        /// <summary>
        ///  To Get SAP Posting Add Details by VCN
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        string GetSAPPostingDetailsAddDetails(string VCN, string MsgType, string ReceavedARRNO, string MarineAccNo, int MarinePostingId, string PostingStatus, string isRevenueUpd, string RevAgentAccNo);

        /// <summary>
        /// To Add SAP Posting Data
        /// </summary>
        /// <param name="supcatdata"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        SAPPostingVO AddSAPPosting(SAPPostingVO sapdata);

        /// <summary>
        ///  To Get SAP Posting Details by VCN
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SAPPostingVO> GetSAPPostingAccountDetails(string VCN);

        /// <summary>
        ///  To Get SAP Posting Details by VCN
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        SAPPostingVO GetSAPPostingDetails(int SAPPostingID);

        /// <summary>
        /// To Add SAP Posting Invoice Data
        /// </summary>
        /// <param name="supcatdata"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        SAPPostingVO AddSAPPostingInvoice(SAPPostingVO sapdata);

        /// <summary>
        /// To Get SAP Posting Invoice Response Data
        /// </summary>
        /// <param name="supcatdata"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        SAPInvoiceItem GetSAPInvoiceResponseDetails(string MarineOrderNo);
    }
}
