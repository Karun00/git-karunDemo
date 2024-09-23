using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Domain.DTOS
{
    public static class DivingCheckListHazardMapExtension
    {
        /// <summary>
        /// Data List Transfer from Entity to DTO
        /// </summary>
        /// <param name="divingCheckListHazards"></param>
        /// <returns></returns>
        public static List<DivingCheckListHazardVO> MapToDto(this List<DivingCheckListHazard> divingCheckListHazards)
        {
            List<DivingCheckListHazardVO> divingchecklisthazardvos = new List<DivingCheckListHazardVO>();

            if (divingCheckListHazards != null)
            {
                foreach (var data in divingCheckListHazards)
                {
                    divingchecklisthazardvos.Add(data.MapToDto());
                }
            }

            return divingchecklisthazardvos;
        }

        /// <summary>
        /// Data List Transfer from DTO to Entity
        /// </summary>
        /// <param name="divingCheckListHazardVos"></param>
        /// <returns></returns>
        public static List<DivingCheckListHazard> MapToEntity(this List<DivingCheckListHazardVO> divingCheckListHazardVos)
        {
            List<DivingCheckListHazard> divingchecklisthazards = new List<DivingCheckListHazard>();

            if (divingCheckListHazardVos != null)
            {
                foreach (var vo in divingCheckListHazardVos)
                {
                    divingchecklisthazards.Add(vo.MapToEntity());
                }
            }
            return divingchecklisthazards;
        }

        /// <summary>
        /// Data Transfer from Entity to DTO
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DivingCheckListHazardVO MapToDto(this DivingCheckListHazard data)
        {
            return new DivingCheckListHazardVO

            {
                DivingCheckListHazardID = data.DivingCheckListHazardID,
                DivingCheckListID = data.DivingCheckListID,
                Hazard = data.Hazard,
                Cause = data.Cause,
                Action = data.Action,
                RecordStatus = data.RecordStatus,
                CreatedBy = data.CreatedBy,
                CreatedDate = data.CreatedDate,
                ModifiedBy = data.ModifiedBy,
                ModifiedDate = data.ModifiedDate
            };
        }

        /// <summary>
        /// Data Transfer from DTO to Entity
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public static DivingCheckListHazard MapToEntity(this DivingCheckListHazardVO vo)
        {
            return new DivingCheckListHazard
            {
                DivingCheckListHazardID = vo.DivingCheckListHazardID,
                DivingCheckListID = vo.DivingCheckListID,
                Hazard = vo.Hazard,
                Cause = vo.Cause,
                Action = vo.Action,
                RecordStatus = vo.RecordStatus,
                CreatedBy = vo.CreatedBy,
                CreatedDate = vo.CreatedDate,
                ModifiedBy = vo.ModifiedBy,
                ModifiedDate = vo.ModifiedDate
            };
        }
    }
}
