using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;
using IPMS.Domain.DTOS;

namespace IPMS.Domain.DTOS
{
    public static class CraftReminderConfigMapExtention
    {
       /// <summary>
        /// Data List Transfer from DTO to Entity
       /// </summary>
       /// <param name="craftvolist"></param>
       /// <returns></returns>
        public static List<CraftReminderConfig> MapToEntity(this List<CraftReminderConfigVO> craftconfigvolist)
        {
            List<CraftReminderConfig> craftEntities = new List<CraftReminderConfig>();
            if (craftconfigvolist != null)
            {
                foreach (var craftvo in craftconfigvolist)
                {
                    craftEntities.Add(craftvo.MapToEntity());
                }
            }
            return craftEntities;
        }

       /// <summary>
        /// Data List Transfer from Entity to DTO
       /// </summary>
       /// <param name="craftlist"></param>
       /// <returns></returns>
        public static List<CraftReminderConfigVO> MapToDTO(this List<CraftReminderConfig> craftconfigvolist)
        {
            List<CraftReminderConfigVO> craftconfigvo = new List<CraftReminderConfigVO>();
            if (craftconfigvolist != null)
            {
                foreach (var craft in craftconfigvolist)
                {
                    craftconfigvo.Add(craft.MapToDTO());
                }
            }
            return craftconfigvo;
        }

       /// <summary>
        /// Data Transfer from Entity to DTO
       /// </summary>
       /// <param name="data"></param>
       /// <returns></returns>
        public static CraftReminderConfigVO MapToDTO(this CraftReminderConfig data)
        {
            CraftReminderConfigVO craftvo = new CraftReminderConfigVO();
            if (data != null)
            {
                craftvo.CraftReminderConfigID = data.CraftReminderConfigID;
                craftvo.CraftID = data.CraftID;
                craftvo.ParticularsNo = data.ParticularsNo;
                craftvo.ReminderName = data.ReminderName;
                if (data.SubCategory4 != null)
                {
                    craftvo.ParticularsName = data.SubCategory4.SubCatName;
                }
                craftvo.IssuingAuthority = data.IssuingAuthority;
                craftvo.DateOfIssue = data.DateOfIssue;
                craftvo.DateOfValidity = data.DateOfValidity;
                craftvo.AlertOccurance1 = data.AlertOccurance1;
                craftvo.AlertPeriod1 = data.AlertPeriod1;
                craftvo.AlertOccurance2 = data.AlertOccurance2;
                craftvo.AlertPeriod2 = data.AlertPeriod2;
                craftvo.AlertOccurance3 = data.AlertOccurance3;
                craftvo.AlertPeriod3 = data.AlertPeriod3;
                craftvo.ReminderStatus = data.ReminderStatus;
                craftvo.ExitReminderConfig = data.ExitReminderConfig;
                craftvo.RecordStatus = data.RecordStatus;
                craftvo.CreatedBy = data.CreatedBy;
                craftvo.CreatedDate = data.CreatedDate;
                craftvo.ModifiedBy = data.ModifiedBy;
                craftvo.ModifiedDate = data.ModifiedDate;
            }
          //  craftvo.Craft = data.Craft.MapToDTO();
            return craftvo;
        }

       /// <summary>
        /// Data Transfer from DTO to Entity
       /// </summary>
       /// <param name="vo"></param>
       /// <returns></returns>
        public static CraftReminderConfig MapToEntity(this CraftReminderConfigVO vo)
        {
            CraftReminderConfig craft = new CraftReminderConfig();
            if (vo != null)
            {
                craft.CraftReminderConfigID = vo.CraftReminderConfigID;
                craft.CraftID = vo.CraftID;
                craft.ReminderName = vo.ReminderName;
                craft.ParticularsNo = vo.ParticularsNo;
                craft.IssuingAuthority = vo.IssuingAuthority;
                craft.DateOfIssue = vo.DateOfIssue;
                craft.DateOfValidity = vo.DateOfValidity;
                craft.AlertOccurance1 = vo.AlertOccurance1;
                craft.AlertPeriod1 = vo.AlertPeriod1;
                craft.AlertOccurance2 = vo.AlertOccurance2;
                craft.AlertPeriod2 = vo.AlertPeriod2;
                craft.AlertOccurance3 = vo.AlertOccurance3;
                craft.AlertPeriod3 = vo.AlertPeriod3;
                craft.ReminderStatus = vo.ReminderStatus;
                craft.ExitReminderConfig = vo.ExitReminderConfig;
                craft.RecordStatus = vo.RecordStatus;
                craft.CreatedBy = vo.CreatedBy;
                craft.CreatedDate = vo.CreatedDate;
                craft.ModifiedBy = vo.ModifiedBy;
                craft.ModifiedDate = vo.ModifiedDate;
            }
            return craft;
        }
     
    }
}
