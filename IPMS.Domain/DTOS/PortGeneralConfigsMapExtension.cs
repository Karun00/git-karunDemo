using System;
using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Domain.DTOS
{
    public static class PortGeneralConfigsMapExtension
    {
        public static PortGeneralConfigsVO MapToDTO(this PortGeneralConfig data)
        {
            PortGeneralConfigsVO portgeneralconfigsvo = new PortGeneralConfigsVO();
            if (data != null)
            {
                portgeneralconfigsvo.PortGeneralConfigID = data.PortGeneralConfigID;
                portgeneralconfigsvo.PortCode = data.PortCode;
                portgeneralconfigsvo.GroupName = data.GroupName;
                portgeneralconfigsvo.ConfigName = data.ConfigName;
                portgeneralconfigsvo.ConfigLabelName = data.ConfigLabelName;
                portgeneralconfigsvo.ConfigValue = data.ConfigValue;
                portgeneralconfigsvo.RecordStatus = data.RecordStatus;
                portgeneralconfigsvo.CreatedBy = data.CreatedBy;
                portgeneralconfigsvo.CreatedDate = data.CreatedDate;
                portgeneralconfigsvo.ModifiedBy = data.ModifiedBy;
                portgeneralconfigsvo.ModifiedDate = data.ModifiedDate;
            }
            return portgeneralconfigsvo;
        }
        public static PortGeneralConfig MapToEntity(this PortGeneralConfigsVO vo)
        {
            PortGeneralConfig portgeneralconfigs = new PortGeneralConfig();
            if (vo != null)
            {
                portgeneralconfigs.PortGeneralConfigID = vo.PortGeneralConfigID;
                portgeneralconfigs.PortCode = vo.PortCode;
                portgeneralconfigs.GroupName = vo.GroupName;
                portgeneralconfigs.ConfigName = vo.ConfigName;
                portgeneralconfigs.ConfigLabelName = vo.ConfigLabelName;
                portgeneralconfigs.ConfigValue = vo.ConfigValue;
                portgeneralconfigs.RecordStatus = vo.RecordStatus;
                portgeneralconfigs.CreatedBy = vo.CreatedBy;
                portgeneralconfigs.CreatedDate = vo.CreatedDate;
                portgeneralconfigs.ModifiedBy = vo.ModifiedBy;
                portgeneralconfigs.ModifiedDate = vo.ModifiedDate;
            }
    
            return portgeneralconfigs;
        }

        public static List<PortGeneralConfigsVO> MapToDTO(this List<PortGeneralConfig> portgeneralconfigsList)
        {
            List<PortGeneralConfigsVO> portgeneralconfigsvoList = new List<PortGeneralConfigsVO>();
            if (portgeneralconfigsList != null)
                foreach (var data in portgeneralconfigsList)
                {
                    portgeneralconfigsvoList.Add(data.MapToDTO());

                }
            return portgeneralconfigsvoList;
        }
        public static List<PortGeneralConfig> MapToEntity(this List<PortGeneralConfigsVO> PortGeneralConfigListVO)
        {
            List<PortGeneralConfig> PortGeneralConfigList = new List<PortGeneralConfig>();
            if (PortGeneralConfigListVO != null)
                foreach (var data in PortGeneralConfigListVO)
                {
                    PortGeneralConfigList.Add(data.MapToEntity());

                }
            return PortGeneralConfigList;
        }
  
    }
}
