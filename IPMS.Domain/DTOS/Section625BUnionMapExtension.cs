using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class Section625BUnionMapExtension
    {
        public static List<Section625BUnionVO> MapToDTO(this IEnumerable<Section625BUnion> Section625BUnions)
        {
            List<Section625BUnionVO> Section625BUnionVos = new List<Section625BUnionVO>();
            foreach (var Section625BUnion in Section625BUnions)
            {
                Section625BUnionVos.Add(Section625BUnion.MapToDTO());

            }
            return Section625BUnionVos;
        }
        public static List<Section625BUnion> MapToEntity(this IEnumerable<Section625BUnionVO> Section625BUnionVos)
        {
            List<Section625BUnion> Section625BUnions = new List<Section625BUnion>();
            foreach (var Section625BUnionVo in Section625BUnionVos)
            {
                Section625BUnions.Add(Section625BUnionVo.MapToEntity());

            }
            return Section625BUnions;
        }

        public static Section625BUnionVO MapToDTO(this Section625BUnion data)
        {
            Section625BUnionVO section625bunionvo = new Section625BUnionVO();
            section625bunionvo.Section625BUnionID = data.Section625BUnionID;
            section625bunionvo.Section625BID = data.Section625BID;
            section625bunionvo.UnionName = data.UnionName;
            section625bunionvo.TotalMembership = data.TotalMembership;
            section625bunionvo.TotalRosteredForShift = data.TotalRosteredForShift;
            section625bunionvo.TotalPresent = data.TotalPresent;
            section625bunionvo.TotalStrike = data.TotalStrike;
            section625bunionvo.TotalLeave = data.TotalLeave;
            section625bunionvo.TotalSick = data.TotalSick;
            section625bunionvo.ReplacementLeave = data.ReplacementLeave;
            return section625bunionvo;
        }

        public static Section625BUnion MapToEntity(this Section625BUnionVO VO)
        {
            Section625BUnion section625bunion = new Section625BUnion();
            section625bunion.Section625BUnionID = VO.Section625BUnionID;
            section625bunion.Section625BID = VO.Section625BID;
            section625bunion.UnionName = VO.UnionName;
            section625bunion.TotalMembership = VO.TotalMembership;
            section625bunion.TotalRosteredForShift = VO.TotalRosteredForShift;
            section625bunion.TotalPresent = VO.TotalPresent;
            section625bunion.TotalStrike = VO.TotalStrike;
            section625bunion.TotalLeave = VO.TotalLeave;
            section625bunion.TotalSick = VO.TotalSick;
            section625bunion.ReplacementLeave = VO.ReplacementLeave;
            return section625bunion;
        }
    }
}
