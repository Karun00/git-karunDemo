using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class AddressMapExtension
    {
        public static AddressVO MapToDTO(this Address data)
        {
            AddressVO addressVo = new AddressVO();
            if (data != null)
            {
                addressVo.AddressID = data.AddressID;
                addressVo.AddressType = data.AddressType;
                addressVo.NumberStreet = data.NumberStreet;
                addressVo.Suburb = data.Suburb;
                addressVo.TownCity = data.TownCity;
                addressVo.PostalCode = data.PostalCode;
                addressVo.CountryCode = data.CountryCode;
                addressVo.RecordStatus = data.RecordStatus;
                addressVo.CreatedBy = data.CreatedBy;
                addressVo.CreatedDate = data.CreatedDate;
                addressVo.ModifiedBy = data.ModifiedBy;
                addressVo.ModifiedDate = data.ModifiedDate;
            }
            return addressVo;
        }
        public static Address MapToEntity(this AddressVO vo)
        {
            Address address = new Address();
            if (vo != null)
            {
                address.AddressID = vo.AddressID;
                address.AddressType = vo.AddressType;
                address.NumberStreet = vo.NumberStreet;
                address.Suburb = vo.Suburb;
                address.TownCity = vo.TownCity;
                address.PostalCode = vo.PostalCode;
                address.CountryCode = vo.CountryCode;
                address.RecordStatus = vo.RecordStatus;
                address.CreatedBy = vo.CreatedBy;
                address.CreatedDate = vo.CreatedDate;
                address.ModifiedBy = vo.ModifiedBy;
                address.ModifiedDate = vo.ModifiedDate;
            }
            return address;
        }

        public static List<Address> MapToEntity(this List<AddressVO> vos)
        {
            List<Address> addressEntities = new List<Address>();
            if (vos != null)
            {
                foreach (var addressvo in vos)
                {
                    addressEntities.Add(addressvo.MapToEntity());
                }
            }
            return addressEntities;
        }
    }
}
