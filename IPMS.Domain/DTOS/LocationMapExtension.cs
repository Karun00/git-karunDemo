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
    public static class LocationMapExtension
    {

        public static List<LocationVO> MapToDTO(this List<Location> location)
        {
            List<LocationVO> lstLocationVO = new List<LocationVO>();
            if (location != null)
            {
            foreach (Location loc in location)
            {
                lstLocationVO.Add(loc.MapToDTO());
            }
            }

            return lstLocationVO;
        }

        public static LocationVO MapToDTO(this Location data)
        {
            LocationVO locationvo = new LocationVO();
            if (data != null)
            {
            locationvo.LocationID = data.LocationID;
            locationvo.LocationName = data.LocationName;
           // locationvo.PortCode = data.PortCode;
            locationvo.RecordStatus = data.RecordStatus;
            locationvo.CreatedBy = data.CreatedBy;
            locationvo.CreatedDate = data.CreatedDate;
            locationvo.ModifiedBy = Convert.ToInt32(data.ModifiedBy, CultureInfo.InvariantCulture);
            locationvo.ModifiedDate = data.ModifiedDate;
            locationvo.LocationPortCode = data.PortCode;
            }
            return locationvo;
        }

        public static Location MapToEntity(this LocationVO locationvo)
        {
            Location location = new Location();
            if (locationvo != null)
            {
            location.LocationID = locationvo.LocationID;
            location.LocationName = locationvo.LocationName;
           // location.PortCode = locationvo.PortCode;
            location.RecordStatus = locationvo.RecordStatus;
            location.CreatedBy = locationvo.CreatedBy;
            location.CreatedDate = locationvo.CreatedDate;
            location.ModifiedBy = locationvo.ModifiedBy;
            location.ModifiedDate = locationvo.ModifiedDate;
            location.PortCode = locationvo.LocationPortCode;
            }
            return location;
        }      

        
    }
}
