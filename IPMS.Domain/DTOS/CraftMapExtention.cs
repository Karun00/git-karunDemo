using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;
using IPMS.Domain.DTOS;
using System.Globalization;

namespace IPMS.Domain.DTOS
{
    public static class CraftMapExtention
    {
        /// <summary>
        /// Data List Transfer from DTO to Entity
        /// </summary>
        /// <param name="craftvolist"></param>
        /// <returns></returns>
        public static List<Craft> ListMapToEntityList(this List<CraftVO> craftvolist)
        {
            List<Craft> craftEntities = new List<Craft>();
            if (craftvolist != null)
            {
                foreach (var craftvo in craftvolist)
                {
                    craftEntities.Add(craftvo.MapToEntity());
                }
            }
            return craftEntities;
        }

        /// <summary>
        /// Data List Transfer from Entity to DTO
        /// </summary>
        /// <param name="craftlist"></param>
        /// <returns></returns>
        public static List<CraftVO> ListMapToDtoList(this List<Craft> craftlist)
        {
            List<CraftVO> craftvolist = new List<CraftVO>();
            if (craftlist != null)
            {
                foreach (var craft in craftlist)
                {
                    craftvolist.Add(craft.MapToDto());
                }
            }
            return craftvolist;
        }

        /// <summary>
        /// Data Transfer from Entity to DTO
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static CraftVO MapToDto(this Craft data)
        {
            CraftVO craftvo = new CraftVO();
            if (data != null)
            {                
                craftvo.CraftID = data.CraftID;
                craftvo.CraftCode = data.CraftCode.ToUpper(CultureInfo.InvariantCulture);
                craftvo.CraftName = data.CraftName;
                craftvo.IMONo = data.IMONo;
                craftvo.CallSign = data.CallSign;
                craftvo.ExCallSign = data.ExCallSign;
                craftvo.CraftType = data.CraftType;
                craftvo.CraftBuildDate = data.CraftBuildDate;
                craftvo.BuildDate = data.CraftBuildDate != null ? Convert.ToString(data.CraftBuildDate, CultureInfo.InvariantCulture) : "";

                craftvo.DateOfDelivery = data.DateOfDelivery;
                craftvo.CraftNationality = data.CraftNationality;
                craftvo.ClassificationSociety = data.ClassificationSociety;
                craftvo.CommissionDate = data.CommissionDate;
                craftvo.AFCInMetricTon = data.AFCInMetricTon;
                craftvo.FuelType = data.FuelType;
                craftvo.PortOfRegistry = data.PortOfRegistry;
                craftvo.EnginePower = data.EnginePower;
                craftvo.EngineType = data.EngineType;
                craftvo.PropulsionType = data.PropulsionType;
                craftvo.NoOfPropellers = data.NoOfPropellers;
                craftvo.MaxManeuveringSpeed = data.MaxManeuveringSpeed;
                craftvo.BeamM = data.BeamM;
                craftvo.RegisteredLengthM = data.RegisteredLengthM;
                craftvo.ForwardDraftM = data.ForwardDraftM;
                craftvo.AftDraftM = data.AftDraftM;
                craftvo.GrossRegisteredTonnageMT = data.GrossRegisteredTonnageMT;
                craftvo.NetRegisteredTonnageMT = data.NetRegisteredTonnageMT;
                craftvo.DeadWeightTonnageMT = data.DeadWeightTonnageMT;
                craftvo.InitialFuelQuantityMT = data.InitialFuelQuantityMT;
                craftvo.LOROBMT = data.LOROBMT;
                craftvo.HYDROBMT = data.HYDROBMT;
                craftvo.FreshwaterROBMT = data.FreshwaterROBMT;
                craftvo.CraftCommissionStatus = data.CraftCommissionStatus;
                craftvo.BollardPullMT = data.BollardPullMT;
                craftvo.OwnersName = data.OwnersName;
                craftvo.Address = data.Address;
                craftvo.PhoneNumber = data.PhoneNumber;
                craftvo.EmailID = data.EmailID;
                craftvo.CreatedBy = data.CreatedBy;
                craftvo.CreatedDate = data.CreatedDate;
                craftvo.ModifiedBy = data.ModifiedBy;
                craftvo.ModifiedDate = data.ModifiedDate;
                craftvo.RecordStatus = data.RecordStatus;
                craftvo.PortCode = data.PortCode;

                if (data.SubCategory5 != null)
                {
                    craftvo.FuelTypeName = data.SubCategory5.SubCatName;
                }
                if (data.SubCategory3 != null)
                {
                    craftvo.CraftTypeName = data.SubCategory3.SubCatName;
                }
                if (data.SubCategory1 != null)
                {
                    craftvo.CommissionStatus = data.SubCategory1.SubCatName;
                }
                if (data.CraftReminderConfigs.Count > 0)
                {
                    craftvo.CraftReminderConfigs = data.CraftReminderConfigs.ToList().MapToDTO();
                }

                craftvo.DredgerColorCode = data.DredgerColorCode;
            }
            return craftvo;
        }

        /// <summary>
        /// Data Transfer from DTO to Entity
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public static Craft MapToEntity(this CraftVO vo)
        {
            Craft craft = new Craft();
            if (vo != null)
            {
                craft.CraftID = vo.CraftID;
                craft.CraftCode = vo.CraftCode.ToUpper(CultureInfo.InvariantCulture);
                craft.CraftName = vo.CraftName;
                craft.IMONo = vo.IMONo;
                craft.CallSign = vo.CallSign;
                craft.ExCallSign = vo.ExCallSign;
                craft.CraftType = vo.CraftType;
                craft.CraftBuildDate = vo.CraftBuildDate;
                craft.DateOfDelivery = vo.DateOfDelivery;
                craft.CraftNationality = vo.CraftNationality;
                craft.ClassificationSociety = vo.ClassificationSociety;
                craft.CommissionDate = vo.CommissionDate;
                craft.AFCInMetricTon = vo.AFCInMetricTon;
                craft.FuelType = vo.FuelType;
                craft.PortOfRegistry = vo.PortOfRegistry;
                craft.EnginePower = vo.EnginePower;
                craft.EngineType = vo.EngineType;
                craft.PropulsionType = vo.PropulsionType;
                craft.NoOfPropellers = vo.NoOfPropellers;
                craft.MaxManeuveringSpeed = vo.MaxManeuveringSpeed;
                craft.BeamM = vo.BeamM;
                craft.RegisteredLengthM = vo.RegisteredLengthM;
                craft.ForwardDraftM = vo.ForwardDraftM;
                craft.AftDraftM = vo.AftDraftM;
                craft.GrossRegisteredTonnageMT = vo.GrossRegisteredTonnageMT;
                craft.NetRegisteredTonnageMT = vo.NetRegisteredTonnageMT;
                craft.DeadWeightTonnageMT = vo.DeadWeightTonnageMT;
                craft.InitialFuelQuantityMT = vo.InitialFuelQuantityMT;
                craft.LOROBMT = vo.LOROBMT;
                craft.HYDROBMT = vo.HYDROBMT;
                craft.FreshwaterROBMT = vo.FreshwaterROBMT;
                craft.CraftCommissionStatus = vo.CraftCommissionStatus;
                craft.BollardPullMT = vo.BollardPullMT;
                craft.OwnersName = vo.OwnersName;
                craft.Address = vo.Address;
                craft.PhoneNumber = vo.PhoneNumber;
                craft.EmailID = vo.EmailID;
                craft.CreatedBy = vo.CreatedBy;
                craft.CreatedDate = vo.CreatedDate;
                craft.ModifiedBy = vo.ModifiedBy;
                craft.ModifiedDate = vo.ModifiedDate;
                craft.RecordStatus = vo.RecordStatus;
                craft.PortCode = vo.PortCode;
                craft.DredgerColorCode = vo.DredgerColorCode;
            }
            return craft;
        }
    }
}
