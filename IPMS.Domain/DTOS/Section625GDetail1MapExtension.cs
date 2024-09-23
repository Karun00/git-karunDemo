using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class Section625GDetail1MapExtension
    {
        public static List<Section625GDetail1VO> MapToDTO(this IEnumerable<Section625GDetail1> entities)
        {
            List<Section625GDetail1VO> Vos = new List<Section625GDetail1VO>();
            foreach (var item in entities)
            {
                Vos.Add(item.MapToDTO());
            }
            return Vos;
        }

        public static List<Section625GDetail1> MapToEntity(this IEnumerable<Section625GDetail1VO> Vos)
        {
            List<Section625GDetail1> entities = new List<Section625GDetail1>();
            foreach (var item in Vos)
            {
                entities.Add(item.MapToEntity());
            }

            return entities;
        }
        public static Section625GDetail1VO MapToDTO(this Section625GDetail1 data)
        {
            Section625GDetail1VO VO = new Section625GDetail1VO();
            VO.Section625GDetail1ID = data.Section625GDetail1ID;
            VO.Section625GID = data.Section625GID;
            VO.RISubCatCode = data.RISubCatCode;    
            return VO;
        }

        public static Section625GDetail1 MapToEntity(this Section625GDetail1VO VO)
        {
            Section625GDetail1 data = new Section625GDetail1();
            data.Section625GDetail1ID = VO.Section625GDetail1ID;
            data.Section625GID = VO.Section625GID;
            data.RISubCatCode = VO.RISubCatCode;  
            return data;
        }



        public static List<string> MapToRecordingofIncidentArray(this ICollection<Section625GDetail1> section625gdetails)
        {
            List<string> selectedRecordingofIncident = new List<string>();
            if (section625gdetails != null)
            {
                foreach (var section625gdetail in section625gdetails)
                {                   
                        selectedRecordingofIncident.Add(section625gdetail.RISubCatCode);              }
                }
            return selectedRecordingofIncident;
        }
    }
}
