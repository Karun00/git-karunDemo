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
    public static class Section625CMapExtension
    {
        public static List<Section625C> MapToEntity(this IEnumerable<Section625CVO> vos)
        {
            List<Section625C> entities = new List<Section625C>();
            foreach (var vo in vos)
            {
                entities.Add(vo.MapToEntity());
            }
            return entities;
        }
        public static List<Section625CVO> MapToDTO(this IEnumerable<Section625C> entities)
        {
            List<Section625CVO> vos = new List<Section625CVO>();
            foreach (var entity in entities)
            {
                vos.Add(entity.MapToDTO());
            }
            return vos;

        }
        public static Section625CVO MapToDTO(this Section625C data)
        {
            Section625CVO Vo = new Section625CVO();
            Vo.Section625ABCDID = data.Section625ABCDID;
            Vo.Section625CID = data.Section625CID;
            Vo.Hour24Report625ID = data.Hour24Report625ID;
            Vo.IDIncidentDateTime = data.IDIncidentDateTime;
            if (data.IDTimeReported != null)
            {
                Vo.IDTimeReported = Convert.ToDateTime(data.IDTimeReported).ToString("HH:mm", CultureInfo.InvariantCulture);
            }
            Vo.IDIncidentSpecificLocation = data.IDIncidentSpecificLocation;
            Vo.WIWitnessName1 = data.WIWitnessName1;
            Vo.WITelephoneNo1 = data.WITelephoneNo1;
            Vo.WIWitnessName2 = data.WIWitnessName2;
            Vo.WITelephoneNo2 = data.WITelephoneNo2;
            Vo.PIName = data.PIName;
            Vo.PISurname = data.PISurname;
            Vo.PIEmployeeNo = data.PIEmployeeNo;
            Vo.PINoOfDaysLost = data.PINoOfDaysLost;
            Vo.PIGender = data.PIGender;
            Vo.PIAge = data.PIAge;
            Vo.PIGradePosition = data.PIGradePosition;
            Vo.PIPartOfBody = data.PIPartOfBody;
            Vo.IncidentDescription = data.IncidentDescription;
            Vo.IIInvestigatorName = data.IIInvestigatorName;
            Vo.IIDesignation = data.IIDesignation;
            Vo.IIInvestigationDate = data.IIInvestigationDate;
            Vo.GAOthersSpecify = data.GAOthersSpecify;
            Vo.GAOHAOthersSpecify = data.GAOHAOthersSpecify;
            Vo.IDCOthersSpecify = data.IDCOthersSpecify;
            Vo.CurrentControls = data.CurrentControls;
            Vo.RecordStatus = data.RecordStatus;
            Vo.CreatedBy = data.CreatedBy;
            Vo.CreatedDate = data.CreatedDate;
            Vo.ModifiedBy = data.ModifiedBy;
            Vo.ModifiedDate = data.ModifiedDate;
            return Vo;
        }
        public static Section625C MapToEntity(this Section625CVO VO)
        {
            Section625C data = new Section625C();
            data.Section625ABCDID = VO.Section625ABCDID;
            data.Hour24Report625ID = VO.Hour24Report625ID;
            data.Section625CID = VO.Section625CID;
            data.IDIncidentDateTime = VO.IDIncidentDateTime;
            if (VO.IDTimeReported != "")
            {
                data.IDTimeReported = DateTime.Parse(Convert.ToDateTime(VO.IDTimeReported, CultureInfo.InvariantCulture).ToString("HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
            }
            data.IDIncidentSpecificLocation = VO.IDIncidentSpecificLocation;
            data.WIWitnessName1 = VO.WIWitnessName1;
            data.WITelephoneNo1 = VO.WITelephoneNo1;
            data.WIWitnessName2 = VO.WIWitnessName2;
            data.WITelephoneNo2 = VO.WITelephoneNo2;
            data.PIName = VO.PIName;
            data.PISurname = VO.PISurname;
            data.PIEmployeeNo = VO.PIEmployeeNo;
            data.PINoOfDaysLost = VO.PINoOfDaysLost;
            data.PIGender = VO.PIGender;
            data.PIAge = VO.PIAge;
            data.PIGradePosition = VO.PIGradePosition;
            data.PIPartOfBody = VO.PIPartOfBody;
            data.IncidentDescription = VO.IncidentDescription;
            data.IIInvestigatorName = VO.IIInvestigatorName;
            data.IIDesignation = VO.IIDesignation;
            data.IIInvestigationDate = VO.IIInvestigationDate;
            data.GAOthersSpecify = VO.GAOthersSpecify;
            data.GAOHAOthersSpecify = VO.GAOHAOthersSpecify;
            data.IDCOthersSpecify = VO.IDCOthersSpecify;
            data.CurrentControls = VO.CurrentControls;
            data.RecordStatus = VO.RecordStatus;
            data.CreatedBy = VO.CreatedBy;
            data.CreatedDate = VO.CreatedDate;
            data.ModifiedBy = VO.ModifiedBy;
            data.ModifiedDate = VO.ModifiedDate;
            return data;
        }
    }
}
