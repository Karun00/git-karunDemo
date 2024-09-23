using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;

namespace IPMS.Domain.DTOS
{
    public static class ResourceEmployeeGroupsMapExtension
    {
        public static ResourceEmployeeGroup MapToEntity(this ResourceEmployeeGroupVO vo)
        {
            ResourceEmployeeGroup rgp = new ResourceEmployeeGroup();

            rgp.ResourceEmployeeGroupID = vo.ResourceEmployeeGroupID;
            rgp.ResourceGroupID = vo.ResourceGroupID;
            rgp.EmployeeID = vo.EmployeeID;
            rgp.RecordStatus = vo.RecordStatus;
            rgp.CreatedBy = vo.CreatedBy;
            rgp.CreatedDate = vo.CreatedDate;
            rgp.ModifiedBy = vo.ModifiedBy;
            return rgp;
        }

        public static ResourceEmployeeGroupVO MapToDTO(this ResourceEmployeeGroup data)
        {
            ResourceEmployeeGroupVO rgp = new ResourceEmployeeGroupVO();

            rgp.ResourceEmployeeGroupID = data.ResourceEmployeeGroupID;
            rgp.ResourceGroupID = data.ResourceGroupID;
            rgp.EmployeeID = data.EmployeeID;
            rgp.RecordStatus = data.RecordStatus;
            rgp.CreatedBy = data.CreatedBy;
            rgp.CreatedDate = data.CreatedDate;
            rgp.ModifiedBy = data.ModifiedBy;
            return rgp;
        }

        public static List<ResourceEmployeeGroup> MapToEntity(this List<ResourceEmployeeGroupVO> vos)
        {
            List<ResourceEmployeeGroup> rsgrp = new List<ResourceEmployeeGroup>();
            foreach (var rsg in vos)
            {
                rsgrp.Add(rsg.MapToEntity());
            }

            return rsgrp;
        }

        public static List<ResourceEmployeeGroupVO> MapToDTO(this List<ResourceEmployeeGroup> data)
        {
            List<ResourceEmployeeGroupVO> rsgrp = new List<ResourceEmployeeGroupVO>();
            foreach (var rsg in data)
            {
                rsgrp.Add(rsg.MapToDTO());
            }
            return rsgrp;
        }
    }
}
