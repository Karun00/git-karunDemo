using System.Collections.Generic;

namespace IPMS.Domain.ValueObjects
{
    public class RAConfigruleReferenceVO
    {
        public List<SubCategoryCodeNameVO> PilotCapacity { get; set; }
       // public ICollection<SubCategoryVO> Movement_Types { get; set; }
        public List<ServiceTypeVO> ServiceTypes { get; set; }
    }
}
