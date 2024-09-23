using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Domain.DTOS
{
    public static class ArrivalCommodityMapExtension
    {

        /// <summary>
        /// Data List Transfer from Entity to DTO
        /// </summary>
        /// <param name="arrivalCommodities"></param>
        /// <returns></returns>
        public static List<ArrivalCommodityVo> MapToDto(this IEnumerable<ArrivalCommodity> arrivalCommodities)
        {
            var arrivalCommodityVoList = new List<ArrivalCommodityVo>();
            if (arrivalCommodities != null)
            {
                foreach (var item in arrivalCommodities)
                {
                    arrivalCommodityVoList.Add(item.MapToDto());
                }
            }
            return arrivalCommodityVoList;
        }

        /// <summary>
        /// Data Transfer from Entity to DTO
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ArrivalCommodityVo MapToDto(this ArrivalCommodity data)
        {
            ArrivalCommodityVo arrival = new ArrivalCommodityVo();
            if (data != null)
            {
                arrival.ArrivalCommodityID = data.ArrivalCommodityID;
                arrival.VCN = data.VCN;
                arrival.PortCode = data.PortCode;
                arrival.QuayCode = data.QuayCode;
                arrival.BerthCode = data.BerthCode;
                arrival.CommodityBerthKey = data.PortCode + "." + data.QuayCode + "." + data.BerthCode;
                arrival.CargoType = data.CargoType;
                arrival.Commodity = data.Commodity !=null ?data.Commodity : "GENR"; 
                arrival.Package = data.Package;
                arrival.UOM = data.UOM;
                arrival.Quantity = data.Quantity;
                arrival.RecordStatus = data.RecordStatus;
                arrival.CreatedBy = data.CreatedBy;
                arrival.CreatedDate = data.CreatedDate;
                arrival.ModifiedBy = data.ModifiedBy;
                arrival.ModifiedDate = data.ModifiedDate;
                arrival.BerthName = data.Berth != null ? data.Berth.BerthName : "NA";
                arrival.PackageName = data.SubCategory1 != null ? data.SubCategory1.SubCatName : "NA";
                arrival.UOMName = data.SubCategory2 != null ? data.SubCategory2.SubCatName : "NA";
                arrival.CargoName = data.SubCategory != null ? data.SubCategory.SubCatName : "NA";
               
            }
            return arrival;
        }

        /// <summary>
        /// Data List Transfer from DTO to Entity 
        /// </summary>
        /// <param name="arrivalCommodityVoList"></param>
        /// <returns></returns>
        public static List<ArrivalCommodity> MapToEntity(this IEnumerable<ArrivalCommodityVo> arrivalCommodityVoList)
        {
            List<ArrivalCommodity> arrivalCommodities = new List<ArrivalCommodity>();
            if (arrivalCommodityVoList != null)
            {
                foreach (var item in arrivalCommodityVoList)
                {
                    arrivalCommodities.Add(item.MapToEntity());
                }
            }
            return arrivalCommodities;
        }

        /// <summary>
        /// Data Transfer from DTO to Entity 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ArrivalCommodity MapToEntity(this ArrivalCommodityVo data)
        {
            if (data != null)
            {
            }
            string[] fields = data.CommodityBerthKey.Split('.');

                string portCode = fields[0];
                string quayCode = fields[1];
                string berthCode = fields[2];
            
            ArrivalCommodity arrival = new ArrivalCommodity();

           
                arrival.ArrivalCommodityID = data.ArrivalCommodityID;
                arrival.VCN = data.VCN;
                arrival.PortCode = portCode;
                arrival.QuayCode = quayCode;
                arrival.BerthCode = berthCode;
                arrival.CargoType = data.CargoType;
                arrival.Commodity = data.Commodity !=null ?data.Commodity : "GENR" ; 
                arrival.Package = data.Package;
                arrival.UOM = data.UOM;
                arrival.Quantity = data.Quantity;
                arrival.RecordStatus = data.RecordStatus;
                arrival.CreatedBy = data.CreatedBy;
                arrival.CreatedDate = data.CreatedDate;
                arrival.ModifiedBy = data.ModifiedBy;
                arrival.ModifiedDate = data.ModifiedDate;
            
            return arrival;
        }

    }
}
