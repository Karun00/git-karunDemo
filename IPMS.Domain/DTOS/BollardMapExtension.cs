using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class BollardMapExtension
    {
        public static List<BollardVO> MapToDTO(this List<Bollard> bollardList)
        {
            List<BollardVO> bollradvoList = new List<BollardVO>();
            if (bollardList != null)
                foreach (var data in bollardList)
                {
                    bollradvoList.Add(data.MapToDTO());

                }
            return bollradvoList;
        }
        public static BollardVO MapToDTO(this Bollard data)
        {
            BollardVO bollardvo = new BollardVO();
            if (data != null)
            {
                bollardvo.BollardCode = data.BollardCode;
                bollardvo.BollardName = data.BollardName;
                bollardvo.ShortName = data.ShortName;
                bollardvo.PortCode = data.PortCode;
                bollardvo.PortName = null;
                bollardvo.QuayCode = data.QuayCode;
                bollardvo.QuayName = null;
                bollardvo.BerthCode = data.BerthCode;
                bollardvo.BerthName = null;
                bollardvo.FromMeter = data.FromMeter;
                bollardvo.ToMeter = data.ToMeter;
                bollardvo.Continous = data.Continuous;
                bollardvo.ContinousStatus = false;
                bollardvo.Description = data.Description;
                bollardvo.RecordStatus = data.RecordStatus;
                bollardvo.CreatedBy = data.CreatedBy;
                bollardvo.CreatedDate = data.CreatedDate;
                bollardvo.ModifiedBy = data.ModifiedBy;
                bollardvo.ModifiedDate = data.ModifiedDate;
                bollardvo.BolardKey = data.PortCode + "." + data.QuayCode + "." + data.BerthCode + "." +
                                      data.BollardCode;
                bollardvo.FromBollardKey = data.PortCode + "." + data.QuayCode + "." + data.BerthCode + "." +
                                           data.BollardCode;
                bollardvo.ToBollardKey = data.PortCode + "." + data.QuayCode + "." + data.BerthCode + "." +
                                         data.BollardCode;
            }
            return bollardvo;
        }
        public static Bollard MapToEntity(this BollardVO bollardvo)
        {
            Bollard bollard = new Bollard();
            if (bollardvo != null)
            {
                bollard.BollardCode = bollardvo.BollardCode;
                bollard.BollardName = bollardvo.BollardName;
                bollard.ShortName = bollardvo.ShortName;
                bollard.PortCode = bollardvo.PortCode;
                bollard.QuayCode = bollardvo.QuayCode;
                bollard.BerthCode = bollardvo.BerthCode;
                bollard.FromMeter = bollardvo.FromMeter;
                bollard.ToMeter = bollardvo.ToMeter;
                bollard.Continuous = bollardvo.Continous;
                bollard.Description = bollardvo.Description;
                bollard.RecordStatus = bollardvo.RecordStatus;
                bollard.CreatedBy = bollardvo.CreatedBy;
                bollard.CreatedDate = bollardvo.CreatedDate;
                bollard.ModifiedBy = bollardvo.ModifiedBy;
                bollard.ModifiedDate = bollardvo.ModifiedDate;
            }
            return bollard;
        }
    }
}
