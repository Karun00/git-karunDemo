using System;
using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Domain.DTOS
{
    public static class UserPreferenceMapExtension
    {
        public static UserPreferenceVO MapToDTO(this UserPreference data)
        {
            UserPreferenceVO vo = new UserPreferenceVO();
            vo.DashBoardConfig = data.DashBoardConfig;
            vo.Userid = data.UserID;
            vo.RecordStatus = data.RecordStatus;
            vo.CreatedBy = data.CreatedBy;
            vo.CreatedDate = data.CreatedDate;
            vo.ModifiedBy = data.ModifiedBy;
            vo.ModifiedDate = data.ModifiedDate;
            return vo;
        }
        public static UserPreference MapToEntity(this UserPreferenceVO vo)
        {
            UserPreference pref = new UserPreference();
            pref.UserID = vo.Userid;
            pref.DashBoardConfig = vo.DashBoardConfig;
            pref.User = null;

            pref.RecordStatus = vo.RecordStatus;
            pref.CreatedBy = vo.CreatedBy;
            pref.CreatedDate = vo.CreatedDate;
            pref.ModifiedBy = vo.ModifiedBy;
            pref.ModifiedDate = vo.ModifiedDate;
            return pref;
        }

        public static List<UserPreferenceVO> MapToDTO(this List<UserPreference> List)
        {
            List<UserPreferenceVO> voList = new List<UserPreferenceVO>();
            if (List != null)
                foreach (var data in List)
                {
                    voList.Add(data.MapToDTO());

                }
            return voList;
        }
      
    }
}
