using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Domain.DTOS
{
   public static class ArrivalIMDGTankerMapExtension
    {

       /// <summary>
        /// Data List Transfer from Entity to DTO
       /// </summary>
       /// <param name="arrivalIMDGTankers"></param>
       /// <returns></returns>
        public static List<ArrivalIMDGTankerVo> MapToDto(this IEnumerable<ArrivalIMDGTanker> arrivalIMDGTankers)
        {
            var arrivalIMDGTankerVoList = new List<ArrivalIMDGTankerVo>();
            if (arrivalIMDGTankers != null)
            {
                foreach (var item in arrivalIMDGTankers)
                {
                    arrivalIMDGTankerVoList.Add(item.MapToDto());
                }
            }
            return arrivalIMDGTankerVoList;
        }

       /// <summary>
        /// Data List Transfer from DTO to Entity
       /// </summary>
       /// <param name="arrivalIMDGTankerVoList"></param>
       /// <returns></returns>
        public static List<ArrivalIMDGTanker> MapToEntity(this IEnumerable<ArrivalIMDGTankerVo> arrivalIMDGTankerVoList)
        {
            var arrivalIMDGTankers = new List<ArrivalIMDGTanker>();
            if (arrivalIMDGTankerVoList != null)
            {
                foreach (var item in arrivalIMDGTankerVoList)
                {
                    arrivalIMDGTankers.Add(item.MapToEntity());
                }
            }
            return arrivalIMDGTankers;

        }

       /// <summary>
        /// Data List Transfer from Entity to DTO
       /// </summary>
       /// <param name="data"></param>
       /// <returns></returns>
       public static ArrivalIMDGTankerVo MapToDto(this ArrivalIMDGTanker data)
       {
           if (data == null)
               return new ArrivalIMDGTankerVo();

           return new ArrivalIMDGTankerVo
           {
               ArrivalIMDGTankerID = data.ArrivalIMDGTankerID,
               VCN = data.VCN,
               Purpose = data.Purpose,
               Commodity = data.Commodity,
               Quantity = data.Quantity,
               FromTank = data.FromTank,
               RecordStatus = data.RecordStatus,
               CreatedBy = data.CreatedBy,
               CreatedDate = data.CreatedDate,
               ModifiedBy = data.ModifiedBy,
               ModifiedDate = data.ModifiedDate
           };

       }

       /// <summary>
       /// Data List Transfer from DTO to Entity
       /// </summary>
       /// <param name="data"></param>
       /// <returns></returns>
       public static ArrivalIMDGTanker MapToEntity(this ArrivalIMDGTankerVo data)
       {
           if (data == null)
               return new ArrivalIMDGTanker();

           return new ArrivalIMDGTanker
           {
               ArrivalIMDGTankerID = data.ArrivalIMDGTankerID,
               VCN = data.VCN,
               Purpose = data.Purpose,
               Commodity = data.Commodity,
               Quantity = data.Quantity,
               FromTank = data.FromTank,
               RecordStatus = data.RecordStatus,
               CreatedBy = data.CreatedBy,
               CreatedDate = data.CreatedDate,
               ModifiedBy = data.ModifiedBy,
               ModifiedDate = data.ModifiedDate
           };

       }
      
    }
}
