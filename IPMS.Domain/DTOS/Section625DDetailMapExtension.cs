using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class Section625DDetailMapExtension
    {
        public static List<Section625DDetail> MapToEntity(this IEnumerable<Section625DDetailVO> vos)
        {
            List<Section625DDetail> entities = new List<Section625DDetail>();
            foreach (var vo in vos)
            {
                entities.Add(vo.MapToEntity());
            }
            return entities;
        }
        public static List<Section625DDetailVO> MapToDTO(this IEnumerable<Section625DDetail> entities)
        {
            List<Section625DDetailVO> vos = new List<Section625DDetailVO>();
            foreach (var entity in entities)
            {
                vos.Add(entity.MapToDTO());
            }
            return vos;

        }

        public static Section625DDetailVO MapToDTO(this Section625DDetail data)
        {
            Section625DDetailVO Vo = new Section625DDetailVO();
            Vo.Section625DDetailID = data.Section625DDetailID;
            Vo.Section625DID = data.Section625DID;
            Vo.GroupCode = data.GroupCode;
            Vo.DetailCode = data.DetailCode;
 
            return Vo;
        }

        public static Section625DDetail MapToEntity(this Section625DDetailVO VO)
        {
            Section625DDetail Data = new Section625DDetail();
            Data.Section625DDetailID = VO.Section625DDetailID;
            Data.Section625DID = VO.Section625DID;
            Data.GroupCode = VO.GroupCode;
            Data.DetailCode = VO.DetailCode;
            return Data;
        }
        public static List<string> MapToFireDepartmentArray(this ICollection<Section625DDetail> section625ddetails)
        {
            List<string> selectedFireDepartment = new List<string>();
            if (section625ddetails != null)
            {

                foreach (var section625ddetail in section625ddetails)
                {
                    if (section625ddetail.GroupCode == "6DFD")
                    {
                        selectedFireDepartment.Add(section625ddetail.DetailCode);
                    }

                }
            }
            return selectedFireDepartment;
        }

        public static List<string> MapToIncidentClassificationArray(this ICollection<Section625DDetail> section625ddetails)
        {
            List<string> selectedIncidentClassification = new List<string>();
            if (section625ddetails != null)
            {

                foreach (var section625ddetail in section625ddetails)
                {
                    if (section625ddetail.GroupCode == "6DIC")
                    {
                        selectedIncidentClassification.Add(section625ddetail.DetailCode);
                    }

                }
            }
            return selectedIncidentClassification;
        }


        public static List<string> MapToDiscriptionofExposedRiskArray(this ICollection<Section625DDetail> section625ddetails)
        {
            List<string> selectedDiscriptionofExposedRisk = new List<string>();
            if (section625ddetails != null)
            {

                foreach (var section625ddetail in section625ddetails)
                {
                    if (section625ddetail.GroupCode == "6DER")
                    {
                        selectedDiscriptionofExposedRisk.Add(section625ddetail.DetailCode);
                    }

                }
            }
            return selectedDiscriptionofExposedRisk;
        }
    
    }
}
