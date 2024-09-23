using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Domain.DTOS
{
    public static class RevenueAccountStatusMaptoExtension
    {
        public static List<RevenueAccountStatus> MapToEntity(this IEnumerable<RevenueAccountStatusVO> vos)
        {
            List<RevenueAccountStatus> revenueaccountstatus = new List<RevenueAccountStatus>();
            foreach (var vo in vos)
            {
                revenueaccountstatus.Add(vo.MapToEntity());
            }
            return revenueaccountstatus;
        }
        public static List<RevenueAccountStatusVO> MapToDTO(this IEnumerable<RevenueAccountStatus> Revenueaccountstatusentities)
        {
            List<RevenueAccountStatusVO> revenueaccountstatusvos = new List<RevenueAccountStatusVO>();
            foreach (var Revenueaccountstatusentity in Revenueaccountstatusentities)
            {
                revenueaccountstatusvos.Add(Revenueaccountstatusentity.MapToDTO());
            }
            return revenueaccountstatusvos;

        }
        public static RevenueAccountStatusVO MapToDTO(this RevenueAccountStatus data)
        {
            RevenueAccountStatusVO Vo = new RevenueAccountStatusVO();
            Vo.RevenueStopListID = data.RevenueStopListID;
            Vo.AccountStatusCode = data.AccountStatusCode;
            Vo.RevenueAccountStatusID = data.RevenueAccountStatusID;    
            return Vo;
        }

        public static RevenueAccountStatus MapToEntity(this RevenueAccountStatusVO VO)
        {
            RevenueAccountStatus Data = new RevenueAccountStatus();
            Data.RevenueStopListID = VO.RevenueStopListID;
            Data.AccountStatusCode = VO.AccountStatusCode;
            Data.RevenueAccountStatusID = VO.RevenueAccountStatusID;  
            return Data;
        }


        public static List<string> MapTorevenueaccountstatusarray(this ICollection<RevenueAccountStatus> revenueaccountstatus)
        {
            List<string> selectedrevenueaccountstatus = new List<string>();
            if (revenueaccountstatus != null)
            {

                foreach (var revenueaccountst in revenueaccountstatus)
                { selectedrevenueaccountstatus.Add(revenueaccountst.AccountStatusCode); }
            }
            return selectedrevenueaccountstatus;
        }
    }
}
