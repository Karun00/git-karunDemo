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
    public static class Section625BMapExtension
    {
        public static List<Section625BVO> MapToDTO(this IEnumerable<Section625B> Section625Bs)
        {
            List<Section625BVO> Section625BVos = new List<Section625BVO>();
            foreach (var Section625B in Section625Bs)
            {
                Section625BVos.Add(Section625B.MapToDTO());

            }
            return Section625BVos;
        }


        public static List<Section625B> MapToEntity(this IEnumerable<Section625BVO> Section625BVos)
        {
            List<Section625B> Section625Bs = new List<Section625B>();
            foreach (var Section625BVo in Section625BVos)
            {
                Section625Bs.Add(Section625BVo.MapToEntity());

            }
            return Section625Bs;
        }


        public static Section625BVO MapToDTO(this Section625B data)
        {
            Section625BVO section625bvo = new Section625BVO();
            section625bvo.Section625BID = data.Section625BID;
            section625bvo.Section625ABCDID = data.Section625ABCDID;
            section625bvo.Hour24Report625ID = data.Hour24Report625ID;
            section625bvo.IDIndustrialDisputeDateTime = data.IDIndustrialDisputeDateTime;
            if (data.IDTimeReported != null)
            {
                section625bvo.IDTimeReported = Convert.ToDateTime(data.IDTimeReported).ToString("HH:mm", CultureInfo.InvariantCulture);
            }
            section625bvo.IDDisputeSpecificLocation = data.IDDisputeSpecificLocation;
            section625bvo.IDTradeUnionName = data.IDTradeUnionName;
            section625bvo.IDTotalNoOfEmployees = data.IDTotalNoOfEmployees;
            section625bvo.IDStrikeStatuS = data.IDStrikeStatuS;
            if (data.IDImpactOperations != null)
            {
                section625bvo.IDImpactOperations = Convert.ToString(data.IDImpactOperations, CultureInfo.InvariantCulture);
            }
            section625bvo.IDViolencePresent = data.IDViolencePresent;
            section625bvo.IndustrialDisputeDescription = data.IndustrialDisputeDescription;
            section625bvo.RecordStatus = data.RecordStatus;
            section625bvo.CreatedBy = data.CreatedBy;
            section625bvo.CreatedDate = data.CreatedDate;
            section625bvo.ModifiedBy = data.ModifiedBy;
            section625bvo.ModifiedDate = data.ModifiedDate;
            //section625bvo.Section625BUnionVO = data.Section625BUnion.MapToDTO();            
            return section625bvo;
        }

        public static Section625B MapToEntity(this Section625BVO vo)
        {
            Section625B Section625B = new Section625B();
            Section625B.Section625BID = vo.Section625BID;
            Section625B.Section625ABCDID = vo.Section625ABCDID;
            Section625B.Hour24Report625ID = vo.Hour24Report625ID;
            Section625B.IDIndustrialDisputeDateTime = vo.IDIndustrialDisputeDateTime;
            if (vo.IDTimeReported != "")
            {
                Section625B.IDTimeReported = DateTime.Parse(Convert.ToDateTime(vo.IDTimeReported, CultureInfo.InvariantCulture).ToString("HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
            }
            Section625B.IDDisputeSpecificLocation = vo.IDDisputeSpecificLocation;
            Section625B.IDTradeUnionName = vo.IDTradeUnionName;
            Section625B.IDTotalNoOfEmployees = vo.IDTotalNoOfEmployees;
            Section625B.IDStrikeStatuS = vo.IDStrikeStatuS;
            if (vo.IDImpactOperations != "")
            {
                Section625B.IDImpactOperations = Convert.ToInt32(vo.IDImpactOperations, CultureInfo.InvariantCulture);
            }
            Section625B.IDViolencePresent = vo.IDViolencePresent;
            Section625B.IndustrialDisputeDescription = vo.IndustrialDisputeDescription;
            Section625B.RecordStatus = vo.RecordStatus;
            Section625B.CreatedBy = vo.CreatedBy;
            Section625B.CreatedDate = vo.CreatedDate;
            Section625B.ModifiedBy = vo.ModifiedBy;
            Section625B.ModifiedDate = vo.ModifiedDate;
            //Section625B.Section625BUnion = vo.Section625BUnionVO.MapToEntity();
            return Section625B;
        }
    
    }
}
