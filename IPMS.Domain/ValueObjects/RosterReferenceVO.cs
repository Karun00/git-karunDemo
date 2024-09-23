using System.Collections.Generic;

namespace IPMS.Domain.ValueObjects
{
     public class RosterReferenceVO
    {
         public ICollection<SubCategoryVO> Designations { get; set; }
         public ICollection<ShiftVO> Shifts { get; set; }
         public ICollection<Months> Months { get; set; }
         public ICollection<Months> Years { get; set; } 
    }
}
