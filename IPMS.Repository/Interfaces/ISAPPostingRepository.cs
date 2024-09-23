using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Repository
{
    public interface ISAPPostingRepository
    {
        List<SAPPostingVO> GetSAPPostingVCN(string searchValue, string portCode);

        List<SAPPostingVO> GetSAPPostingDetailsByVCN(string VCN);

        string GetSAPPostingDetailsAddDetails(string VCN, string portCode, string MsgType, string ReceavedARRNO, string MarineAccNo, int MarinePostingId, string PostingStatus, string isRevenueUpd, string RevAgentAccNo);

        List<SAPPostingVO> GetSAPPostingAccountDetails(string VCN);

        SAPPostingVO GetSAPPostingDetails(int SAPPostingID);

        SAPInvoiceItem GetSAPInvoiceResponseDetails(string MarineOrderNo);

        string AutoArrivalUpdateForETAChange(string VCN, string portCode, string SAPVslNo);

        SAPPosting GetDetailsByVCN(string VCN);        

        List<SAPArrivalVO> GetSAPVesselPostingDetails(SAPArrivalVO arrivalvo);

        List<SAPArrivalVO> GetAutoSAPVesselDetails();
    }
}
