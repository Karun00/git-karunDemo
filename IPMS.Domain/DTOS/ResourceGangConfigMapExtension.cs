using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class ResourceGangConfigMapExtension
    {
        public static ResourceGangConfig MapToEntity(this ResourceGangConfigVO vo)
        {
            ResourceGangConfig rgc = new ResourceGangConfig();
            rgc.ResourceGangConfigID = vo.ResourceGangConfigID;
            rgc.ResourceAllocationConfigRuleID = vo.ResourceAllocationConfigRuleID;
            rgc.FromMeter = vo.FromMeter;
            rgc.ToMeter = vo.ToMeter;
            rgc.NoOfGangs = vo.NoOfGangs;
            return rgc;
        }
        public static ResourceGangConfigVO MapToDTO(this ResourceGangConfig data)
        {
            ResourceGangConfigVO rgc = new ResourceGangConfigVO();
            rgc.ResourceGangConfigID = data.ResourceGangConfigID;
            rgc.ResourceAllocationConfigRuleID = data.ResourceAllocationConfigRuleID;
            rgc.FromMeter = data.FromMeter;
            rgc.ToMeter = data.ToMeter;
            rgc.NoOfGangs = data.NoOfGangs;
            return rgc;
        }

        public static List<ResourceGangConfig> MapToEntity(this IEnumerable<ResourceGangConfigVO> vos)
        {
            List<ResourceGangConfig> rgc = new List<ResourceGangConfig>();
            foreach (var rsg in vos)
            {
                rgc.Add(rsg.MapToEntity());
            }
            return rgc;
        }

        public static List<ResourceGangConfigVO> MapToDTO(this IEnumerable<ResourceGangConfig> data)
        {
            List<ResourceGangConfigVO> rgcvo = new List<ResourceGangConfigVO>();
            foreach (var rsg in data)
            {
                rgcvo.Add(rsg.MapToDTO());
            }
            return rgcvo;
        }
    }
}
