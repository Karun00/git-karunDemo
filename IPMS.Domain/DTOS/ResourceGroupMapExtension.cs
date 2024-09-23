using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;

namespace IPMS.Domain.DTOS
{
    public static class ResourceGroupMapExtension
    {
        public static ResourceGroup MapToEntity(this ResourceGroupVO vo)
        {
            ResourceGroup rgp = new ResourceGroup();

            rgp.ResourceGroupID = vo.ResourceGroupID;
            rgp.PortCode = vo.PortCode;
            rgp.ResourceGroupName = vo.ResourceGroupName;
            rgp.Position = vo.Position;
            rgp.RecordStatus = vo.RecordStatus;
            rgp.CreatedBy = vo.CreatedBy;
            rgp.CreatedDate = vo.CreatedDate;
            rgp.ModifiedBy = vo.ModifiedBy;
            rgp.ModifiedDate = vo.ModifiedDate;
            rgp.ResourceGroupCode = vo.ResourceGroupCode.ToUpper();
            if (vo.ResourceEmployeeGroups != null)
            {
                rgp.ResourceEmployeeGroups = vo.ResourceEmployeeGroups.MapToEntity();
            }

            return rgp;
        }

        public static ResourceGroupVO MapToDTO(this ResourceGroup data)
        {
            ResourceGroupVO rgp = new ResourceGroupVO();

            rgp.ResourceGroupID = data.ResourceGroupID;
            rgp.PortCode = data.PortCode;
            rgp.ResourceGroupName = data.ResourceGroupName;
            rgp.Position = data.Position;
            rgp.RecordStatus = data.RecordStatus;
            rgp.CreatedBy = data.CreatedBy;
            rgp.CreatedDate = data.CreatedDate;
            rgp.ModifiedBy = data.ModifiedBy;
            rgp.ModifiedDate = data.ModifiedDate;
            rgp.ResourceGroupCode = data.ResourceGroupCode.ToUpper();
            rgp.ResourceEmployeeGroups = data.ResourceEmployeeGroups.MapToDTO();
            rgp.Designation = data.SubCategory.SubCatName;
            rgp.DesignationCode = data.SubCategory.SubCatCode;

            return rgp;
        }

        public static List<ResourceGroup> MapToEntity(this List<ResourceGroupVO> vos)
        {
            List<ResourceGroup> rsgrp = new List<ResourceGroup>();
            foreach (var rsg in vos)
            {
                rsgrp.Add(rsg.MapToEntity());
            }
            return rsgrp;
        }

        public static List<ResourceGroupVO> MapToDTO(this List<ResourceGroup> data)
        {
            List<ResourceGroupVO> rsgrp = new List<ResourceGroupVO>();
            foreach (var rsg in data)
            {
                rsgrp.Add(rsg.MapToDTO());
            }
            return rsgrp;
        }
    }
}
