using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;

namespace IPMS.Domain.DTOS
{
    public static class SAPPostingMapExtension
    {

        public static SAPPostingVO MapToDTO(this SAPPosting data)
        {
            SAPPostingVO VO = new SAPPostingVO();

                VO.SAPPostingID = data.SAPPostingID;
                VO.MessageType = data.MessageType;
                VO.ReferenceNo = data.ReferenceNo;
                VO.PostingStatus = data.PostingStatus;
                VO.TransmitData = data.TransmitData;
                VO.Remarks = data.Remarks;
                VO.RecordStatus = data.RecordStatus;
                VO.CreatedBy = data.CreatedBy;
                VO.CreatedDate = data.CreatedDate;
                VO.ModifiedBy = data.ModifiedBy;
                VO.ModifiedDate = data.ModifiedDate;
                VO.SAPReferenceNo = data.SAPReferenceNo != null ? data.SAPReferenceNo : "";
                VO.ReferenceNo = data.ReferenceNo != null ? data.ReferenceNo : "";
                VO.RevinueAccountNo = data.RevinueAccountNo != null ? data.RevinueAccountNo : "";
                VO.RevenueAgentAccNo = data.RevenueAgentAccNo;
            return VO;
        }
        public static SAPPosting MapToEntity(this SAPPostingVO VO)
        {
            SAPPosting data = new SAPPosting();
            data.SAPPostingID = VO.SAPPostingID;
            data.MessageType = VO.MessageType;
            data.ReferenceNo = VO.VCN;
            data.PostingStatus = VO.PostingStatus;
            data.TransmitData = VO.TransmitData;
            data.Remarks = VO.Remarks;
            data.RecordStatus = VO.RecordStatus;
            data.CreatedBy = VO.CreatedBy;
            data.CreatedDate = VO.CreatedDate;
            data.ModifiedBy = VO.ModifiedBy;
            data.ModifiedDate = VO.ModifiedDate;
            data.Reason = VO.Reason;
            data.EmailStatus = VO.EmailStatus;
            data.SMSStatus = VO.SMSStatus;
            data.SystemNotificationStatus = VO.SystemNotificationStatus;
            data.PortCode = VO.PortCode;
            data.RevinueAccountNo = VO.RevinueAccountNo;
            if (data.RevinueAccountNo == "")
            {
                data.RevinueAccountNo = VO.MarineAccNo;
            }
            data.MarinePostingId = VO.MarinePostingId;
            data.SAPReferenceNo = VO.SAPReferenceNo;
            data.RevenueAgentAccNo = VO.RevenueAgentAccNo;

            return data;
        }
    }
}
