using System.Collections.Generic;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;

namespace IPMS.Domain.DTOS
{
    public static class ResourceRosterMapExtension
    {
        public static ResourceRoster MapToEntity(this ResourceRosterVO vo)
        {
            ResourceRoster rgp = new ResourceRoster();

            if (vo != null)
            {
                rgp.ResourceRosterID = vo.ResourceRosterID;
                rgp.ResourceGroupID = vo.ResourceGroupID;
                rgp.Weekday = vo.Weekday;
                rgp.RecordStatus = vo.RecordStatus;
                rgp.CreatedBy = vo.CreatedBy;
                rgp.CreatedDate = vo.CreatedDate;
                rgp.ModifiedBy = vo.ModifiedBy;
            }
            return rgp;
        }

        public static ResourceRosterVO MapToDTO(this ResourceRoster data)
        {
            ResourceRosterVO rgp = new ResourceRosterVO();

            if (data != null)
            {
                rgp.ResourceRosterID = data.ResourceRosterID;
                rgp.ResourceGroupID = data.ResourceGroupID;
                rgp.Weekday = data.Weekday;
                rgp.RecordStatus = data.RecordStatus;
                rgp.CreatedBy = data.CreatedBy;
                rgp.CreatedDate = data.CreatedDate;
                rgp.ModifiedBy = data.ModifiedBy;
            }

            return rgp;
        }

        public static List<ResourceRoster> MapToEntity(this List<ResourceRosterVO> vos)
        {
            List<ResourceRoster> rsgrp = new List<ResourceRoster>();
            if (vos != null)
            {
                foreach (var rsg in vos)
                {
                    rsgrp.Add(rsg.MapToEntity());
                }
            }

            return rsgrp;
        }

        public static List<ResourceRosterVO> MapToDTO(this List<ResourceRoster> data)
        {
            List<ResourceRosterVO> rsgrp = new List<ResourceRosterVO>();
            if (data != null)
            {
                foreach (var rsg in data)
                {
                    rsgrp.Add(rsg.MapToDTO());
                }
            }

            return rsgrp;
        }
    }
}
