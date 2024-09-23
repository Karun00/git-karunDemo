using System.Collections.Generic;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;

namespace IPMS.Domain.DTOS
{
    public static class RosterMapExtension
    {
        public static List<RosterVO> MapToListDTO(this IEnumerable<Roster> rosterList)
        {
            List<RosterVO> rostervoList = new List<RosterVO>();
            if (rosterList != null)
            {
                foreach (var data in rosterList)
                {
                    rostervoList.Add(data.MapToDTO());
                }
            }

            return rostervoList;
        }

        public static List<Roster> MapToListEntity(this IEnumerable<RosterVO> rostervoList)
        {
            List<Roster> rosterList = new List<Roster>();
            if (rostervoList != null)
            {
                foreach (var data in rostervoList)
                {
                    rosterList.Add(data.MapToEntity());
                }
            }

            return rosterList;
        }

        public static RosterVO MapToDTO(this Roster data)
        {
            RosterVO rostervo = new RosterVO();
            if (data != null)
            {
                rostervo.RosterID = data.RosterID;
                rostervo.RosterCode = data.RosterCode;
                rostervo.Year = data.Year;
                //  rostervo.WeekNo = data.Week;
                rostervo.RecordStatus = data.RecordStatus;
                rostervo.CreatedBy = data.CreatedBy;
                rostervo.CreatedDate = data.CreatedDate;
                rostervo.ModifiedBy = data.ModifiedBy;
                rostervo.ModifiedDate = data.ModifiedDate;
                rostervo.Designation = data.Designation;
                //   rostervo.RosterGroups = data.RosterGroups.MapToDTO();
                //rostervo.SubCategory = data.SubCategory.MapToDTO();
            }
            return rostervo;
        }

        public static Roster MapToEntity(this RosterVO data)
        {
            Roster roster = new Roster();
            if (data != null)
            {
                roster.RosterID = data.RosterID;
                roster.RosterCode = data.RosterCode;
                roster.Year = data.Year;
                //  roster.Week = data.WeekNo;
                roster.RecordStatus = data.RecordStatus;
                roster.CreatedBy = data.CreatedBy;
                roster.CreatedDate = data.CreatedDate;
                roster.ModifiedBy = data.ModifiedBy;
                roster.ModifiedDate = data.ModifiedDate;
                roster.Designation = data.Designation;
                //  roster.RosterGroups = data.RosterGroups.MapToEntity();
                // roster.SubCategory = data.SubCategoryvo.MapToEntity();
            }

            return roster;
        }

    }
}
