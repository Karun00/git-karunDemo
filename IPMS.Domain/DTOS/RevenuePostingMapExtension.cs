using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;

namespace IPMS.Domain.DTOS
{
    public static class RevenuePostingMapExtension
    {
        public static List<RevenuePostingVO> MapToDTO(this List<RevenuePosting> revenuePostings)
        {
            List<RevenuePostingVO> revenuepostingvos = new List<RevenuePostingVO>();
            foreach (var revenuePosting in revenuePostings)
            {
                revenuepostingvos.Add(revenuePosting.MapToDTO());
            }
            return revenuepostingvos;
        }

        public static RevenuePostingVO MapToDTO(this RevenuePosting data)
        {
            RevenuePostingVO revenuepostingvo = new RevenuePostingVO();
            revenuepostingvo.AgentID = data.AgentID;
            var agentAccountList = data.Agent != null ? (data.Agent.AgentAccounts != null ? data.Agent.AgentAccounts.ToList() : null) : null;
            revenuepostingvo.AgentAccountID = agentAccountList != null ? agentAccountList.Find(a => a.AccountNo == data.SAPAccNo).AgentAccountID : 0;
            revenuepostingvo.AccountNo = data.SAPAccNo;
            revenuepostingvo.PortCode = data.PortCode;
            revenuepostingvo.CreatedBy = data.CreatedBy;
            revenuepostingvo.CreatedDate = data.CreatedDate;
            revenuepostingvo.ModifiedBy = data.ModifiedBy;
            revenuepostingvo.ModifiedDate = data.ModifiedDate;
            revenuepostingvo.PostedDate = data.PostedDate;
            revenuepostingvo.PostingStatus = data.PostingStatus;
            revenuepostingvo.RevenuePostingID = data.RevenuePostingID;
            revenuepostingvo.SAPAccNo = data.SAPAccNo;
            revenuepostingvo.vcn = data.vcn;
            revenuepostingvo.AnyDangerousGoodsonBoard = data.ArrivalNotification.AnyDangerousGoodsonBoard;
            revenuepostingvo.VesselName = data.ArrivalNotification != null ? (data.ArrivalNotification.Vessel != null ? data.ArrivalNotification.Vessel.VesselName : "") : "";
            revenuepostingvo.VesselType = data.ArrivalNotification != null ? (data.ArrivalNotification.Vessel != null ? (data.ArrivalNotification.Vessel.SubCategory3 != null ? data.ArrivalNotification.Vessel.SubCategory3.SubCatName : "") : "") : "";
         
            //   revenuepostingvo.ReasonForVisit = data.ArrivalNotification != null ? (data.ArrivalNotification.SubCategory3 != null ? data.ArrivalNotification.SubCategory3.SubCatName : string.Empty) : string.Empty;

            if (data.ArrivalNotification.ArrivalReasons.Count > 0)
            {
                foreach (ArrivalReason ar in data.ArrivalNotification.ArrivalReasons)
                {

                    if (revenuepostingvo.ReasonForVisit == null)
                    {
                        if (ar.SubCategory.SubCatName != null)
                        {
                            revenuepostingvo.ReasonForVisit = ar.SubCategory.SubCatName;
                        }

                    }
                    else
                    {
                        if (ar.SubCategory.SubCatName != null)
                        {
                            revenuepostingvo.ReasonForVisit = revenuepostingvo.ReasonForVisit + ',' + ar.SubCategory.SubCatName;
                        }
                    }
                }
            }




            
            //if (data.ArrivalNotification.VesselCalls.Count > 0)
            //{
            //    revenuepostingvo.ATA = data.ArrivalNotification.VesselCalls.FirstOrDefault().ATA;
            //    revenuepostingvo.ATD = data.ArrivalNotification.VesselCalls.FirstOrDefault().ATD;
            //}
            revenuepostingvo.RegisteredName = data.Agent != null ? data.Agent.RegisteredName : "";
            revenuepostingvo.ATA = data.ArrivalNotification.ETA;
            revenuepostingvo.ATD = data.ArrivalNotification.ETD;

            return revenuepostingvo;
        }

        public static RevenuePosting MapToEntity(this RevenuePostingVO revenuepostingvo)
        {
            RevenuePosting data = new RevenuePosting();
            data.AgentID = revenuepostingvo.AgentID;
            data.PortCode = revenuepostingvo.PortCode;
            data.CreatedBy = revenuepostingvo.CreatedBy;
            data.CreatedDate = revenuepostingvo.CreatedDate;
            data.ModifiedBy = revenuepostingvo.ModifiedBy;
            data.ModifiedDate = revenuepostingvo.ModifiedDate;
            data.PostedDate = revenuepostingvo.PostedDate;
            data.PostingStatus = revenuepostingvo.PostingStatus;
            data.RevenuePostingID = revenuepostingvo.RevenuePostingID;
            data.SAPAccNo = revenuepostingvo.SAPAccNo;
            data.vcn = revenuepostingvo.vcn;

            return data;
        }
    }
}
