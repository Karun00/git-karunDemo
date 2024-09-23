using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using IPMS.Web.ServiceProxies.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Clients
{
    public class SAPPostingClient : UserClientBase<ISAPPostingService>, ISAPPostingService
    {
        public List<SAPPostingVO> GetSAPPostingVCN(string searchValue)
        {
            return WrapOperationWithException(() => Channel.GetSAPPostingVCN(searchValue));
        }

        public List<SAPPostingVO> GetSAPPostingDetailsByVCN(string VCN)
        {
            return WrapOperationWithException(() => Channel.GetSAPPostingDetailsByVCN(VCN));
        }
        public SAPPostingVO GetSAPPostingReferenceVO()
        {
            return WrapOperationWithException(() => Channel.GetSAPPostingReferenceVO());
        }

        public string GetSAPPostingDetailsAddDetails(string VCN, string MsgType, string ReceavedARRNO, string MarineAccNo, int MarinePostingId, string PostingStatus, string isRevenueUpd, string RevAgentAccNo)
        {
            return WrapOperationWithException(() => Channel.GetSAPPostingDetailsAddDetails(VCN, MsgType, ReceavedARRNO, MarineAccNo,  MarinePostingId, PostingStatus, isRevenueUpd, RevAgentAccNo));
        }
        public SAPPostingVO AddSAPPosting(SAPPostingVO sapdata)
        {
            return WrapOperationWithException(() => Channel.AddSAPPosting(sapdata));
        }

        public List<SAPPostingVO> GetSAPPostingAccountDetails(string VCN)
        {
            return WrapOperationWithException(() => Channel.GetSAPPostingAccountDetails(VCN));
        }

        public SAPPostingVO GetSAPPostingDetails(int SAPPostingID)
        {
            return WrapOperationWithException(() => Channel.GetSAPPostingDetails(SAPPostingID));
        }
        public SAPPostingVO AddSAPPostingInvoice(SAPPostingVO sapdata)
        {
            return WrapOperationWithException(() => Channel.AddSAPPostingInvoice(sapdata));
        }
        public SAPInvoiceItem GetSAPInvoiceResponseDetails(string MarineOrderNo)
        {
            return WrapOperationWithException(() => Channel.GetSAPInvoiceResponseDetails(MarineOrderNo));
        }

        

    }
}