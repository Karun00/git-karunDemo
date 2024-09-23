using System.Collections.Generic;

namespace IPMS.Domain.ValueObjects
{
    /// <summary>
    /// Data Transfer Object for Craft Reference Data
    /// </summary>
    public class CraftReferenceVO
    {
        public ICollection<SubCategoryVO> CraftType { get; set; }
        public ICollection<SubCategoryVO> EngineType { get; set; }
        public ICollection<SubCategoryVO> CraftNationality { get; set; }
        public ICollection<SubCategoryVO> FuelType { get; set; }
        public ICollection<SubCategoryVO> CraftCommissionStatus { get; set; }
        public ICollection<SubCategoryVO> ClassificationSociety { get; set; }
        public ICollection<SubCategoryVO> PortOfRegistry { get; set; }
        public ICollection<SubCategoryVO> PropulsionType { get; set; }

       // BY mahesh: To get particulars and calenders for Craft Remainder Config
        public ICollection<SubCategoryVO> ParticularTypes { get; set; }
        public ICollection<SubCategoryVO> CalenderTypes { get; set; }

    }
}
