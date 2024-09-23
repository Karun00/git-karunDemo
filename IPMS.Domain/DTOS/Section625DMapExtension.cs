using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class Section625DMapExtension
    {
        public static List<Section625D> MapToEntity(this IEnumerable<Section625DVO> vos)
        {
            List<Section625D> entities = new List<Section625D>();
            foreach (var vo in vos)
            {
                entities.Add(vo.MapToEntity());
            }
            return entities;
        }
        public static List<Section625DVO> MapToDTO(this IEnumerable<Section625D> entities)
        {
            List<Section625DVO> vos = new List<Section625DVO>();
            foreach (var entity in entities)
            {
                vos.Add(entity.MapToDTO());
            }
            return vos;

        }

        public static Section625DVO MapToDTO(this Section625D data)
        {
            Section625DVO Vo = new Section625DVO();  
            Vo.Section625DID = data.Section625DID;
            Vo.Section625ABCDID = data.Section625ABCDID;
            Vo.Hour24Report625ID = data.Hour24Report625ID;
            Vo.IncidentDateTime = data.IncidentDateTime;
            if (data.TimeReported != null)
            {
                Vo.TimeReported = Convert.ToDateTime(data.TimeReported).ToString("HH:mm", CultureInfo.InvariantCulture);
            }
            Vo.SpecifyLocationOfFire = data.SpecifyLocationOfFire;
            Vo.FireDepartmentAttend = data.FireDepartmentAttend;
            Vo.OthersSpecify = data.OthersSpecify;
            Vo.FICommercial = data.FICommercial;
            Vo.FIStorage = data.FIStorage;
            Vo.FIIndustry = data.FIIndustry;
            Vo.FITransport = data.FITransport;
            Vo.FIOthers = data.FIOthers;
            Vo.FIMiscillaniousSpecify = data.FIMiscillaniousSpecify;
            Vo.ICOthersSpecify = data.ICOthersSpecify;
            Vo.DEROthersSpecify = data.DEROthersSpecify;
            Vo.APDDamage = data.APDDamage;
            Vo.APDMaximumEstimatedFinancialLoss = data.APDMaximumEstimatedFinancialLoss;
            Vo.APDActualLoss = data.APDActualLoss;
            Vo.MEByWhom = data.MEByWhom;
            Vo.MEWithWhatBeforeFire = data.MEWithWhatBeforeFire;
            Vo.MEWithWhatAfterFire = data.MEWithWhatAfterFire;
            Vo.FurtherInformation = data.FurtherInformation;
            Vo.WCWeather = data.WCWeather;
            Vo.WCTemperature = data.WCTemperature;
            Vo.WCWindSpeed = data.WCWindSpeed;
            Vo.WCWindDirection = data.WCWindDirection;
            Vo.Remarks = data.Remarks;
            Vo.RecordStatus = data.RecordStatus;
            Vo.CreatedBy = data.CreatedBy;
            Vo.CreatedDate = data.CreatedDate;
            Vo.ModifiedBy = data.ModifiedBy;
            Vo.ModifiedDate = data.ModifiedDate;
            return Vo;
        }

        public static Section625D MapToEntity(this Section625DVO VO)
        {
            Section625D Data = new Section625D();
            Data.Section625DID = VO.Section625DID;
            Data.Section625ABCDID = VO.Section625ABCDID;
            Data.Hour24Report625ID = VO.Hour24Report625ID;
            Data.IncidentDateTime = VO.IncidentDateTime;
            if (VO.TimeReported != "")
            {
                Data.TimeReported = DateTime.Parse(Convert.ToDateTime(VO.TimeReported, CultureInfo.InvariantCulture).ToString("HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
            }
            Data.SpecifyLocationOfFire = VO.SpecifyLocationOfFire;
            Data.FireDepartmentAttend = VO.FireDepartmentAttend;
            Data.OthersSpecify = VO.OthersSpecify;
            Data.FICommercial = VO.FICommercial;
            Data.FIStorage = VO.FIStorage;
            Data.FIIndustry = VO.FIIndustry;
            Data.FITransport = VO.FITransport;
            Data.FIOthers = VO.FIOthers;
            Data.FIMiscillaniousSpecify = VO.FIMiscillaniousSpecify;
            Data.ICOthersSpecify = VO.ICOthersSpecify;
            Data.DEROthersSpecify = VO.DEROthersSpecify;
            Data.APDDamage = VO.APDDamage;
            Data.APDMaximumEstimatedFinancialLoss = VO.APDMaximumEstimatedFinancialLoss;
            Data.APDActualLoss = VO.APDActualLoss;
            Data.MEByWhom = VO.MEByWhom;
            Data.MEWithWhatBeforeFire = VO.MEWithWhatBeforeFire;
            Data.MEWithWhatAfterFire = VO.MEWithWhatAfterFire;
            Data.FurtherInformation = VO.FurtherInformation;
            Data.WCWeather = VO.WCWeather;
            Data.WCTemperature = VO.WCTemperature;
            Data.WCWindSpeed = VO.WCWindSpeed;
            Data.WCWindDirection = VO.WCWindDirection;
            Data.Remarks = VO.Remarks;
            Data.RecordStatus = VO.RecordStatus;
            Data.CreatedBy = VO.CreatedBy;
            Data.CreatedDate = VO.CreatedDate;
            Data.ModifiedBy = VO.ModifiedBy;
            Data.ModifiedDate = VO.ModifiedDate;
                     return Data;
        }
    
    }
}
