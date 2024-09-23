using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Domain.DTOS
{
    public static class Section625ABCDMapExtension
    {
        public static List<Section625ABCDVO> MapToListDTO(this IEnumerable<Section625ABCD> sections)
        {
            List<Section625ABCDVO> sectionsVos = new List<Section625ABCDVO>();
            foreach (var item in sections)
            {
                sectionsVos.Add(item.MapToDTO());
            }
            return sectionsVos;
        }

        /// <summary>
        /// Data List Transfer from DTO to Entity
        /// </summary>
        /// <param name="incidentVos"></param>
        /// <returns></returns>
        public static List<Section625ABCD> MapToListEntity(this IEnumerable<Section625ABCDVO> sectionsVos)
        {
            List<Section625ABCD> sections = new List<Section625ABCD>();
            foreach (var item in sectionsVos)
            {
                sections.Add(item.MapToEntity());
            }

            return sections;
        }

        public static Section625ABCDVO MapToDTO(this Section625ABCD data)
        {
            Section625ABCDVO sectionVO = new Section625ABCDVO();
            sectionVO.Section625ABCDID = data.Section625ABCDID;
            sectionVO.TOMSLogEntryNo = data.TOMSLogEntryNo;
            sectionVO.OperatorName = data.OperatorName;
            sectionVO.LincseNumber = data.LincseNumber;
            sectionVO.CompanyRegistrationNumber = data.CompanyRegistrationNumber;
            sectionVO.SiteTerminal = data.SiteTerminal;
            sectionVO.ChangeControlDateTime = data.ChangeControlDateTime;
            sectionVO.CDName = data.CDName;
            sectionVO.CDDesignation = data.CDDesignation;
            sectionVO.CDContactNumber = data.CDContactNumber;
            sectionVO.CDMobileNumber = data.CDMobileNumber;
            sectionVO.CDEmailID = data.CDEmailID;
            sectionVO.CDAddress = data.CDAddress;
            sectionVO.ChangeControlLicensedOperator = data.ChangeControlLicensedOperator;
            sectionVO.AnticipatedImpactOnBBBEERating = data.AnticipatedImpactOnBBBEERating;
            sectionVO.Hour24Report625ID = data.Hour24Report625ID;
            sectionVO.RecordStatus = data.RecordStatus;
            sectionVO.CreatedBy = data.CreatedBy;
            sectionVO.CreatedDate = data.CreatedDate;
            sectionVO.ModifiedBy = data.ModifiedBy;
            sectionVO.ModifiedDate = data.ModifiedDate;
            return sectionVO;

        }

        public static Section625ABCD MapToEntity(this Section625ABCDVO VO)
        {
            Section625ABCD section = new Section625ABCD();
            section.Section625ABCDID = VO.Section625ABCDID;
            section.TOMSLogEntryNo = VO.TOMSLogEntryNo;
            section.OperatorName = VO.OperatorName;
            section.LincseNumber = VO.LincseNumber;
            section.CompanyRegistrationNumber = VO.CompanyRegistrationNumber;
            section.SiteTerminal = VO.SiteTerminal;
            section.ChangeControlDateTime = VO.ChangeControlDateTime;
            section.CDName = VO.CDName;
            section.CDDesignation = VO.CDDesignation;
            section.CDContactNumber = VO.CDContactNumber;
            section.CDMobileNumber = VO.CDMobileNumber;
            section.CDEmailID = VO.CDEmailID;
            section.CDAddress = VO.CDAddress;
            section.ChangeControlLicensedOperator = VO.ChangeControlLicensedOperator;
            section.AnticipatedImpactOnBBBEERating = VO.AnticipatedImpactOnBBBEERating;
            section.Hour24Report625ID = VO.Hour24Report625ID;
            section.RecordStatus = VO.RecordStatus;
            section.CreatedBy = VO.CreatedBy;
            section.CreatedDate = VO.CreatedDate;
            section.ModifiedBy = VO.ModifiedBy;
            section.ModifiedDate = VO.ModifiedDate;
            return section;

        }

    }
}
