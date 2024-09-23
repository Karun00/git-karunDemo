using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;

namespace IPMS.Domain.ValueObjects
{
    public class VesselRegistrationReferenceVO
    {
        public ICollection<SubCategoryVO> GetClassificationSocity { get; set; }
        public ICollection<SubCategoryVO> GetVesselTypes { get; set; }
        //public ICollection<SubCategoryVO> GetPOR { get; set; }
        //public ICollection<PortVO> GetPOR { get; set; }
        public ICollection<PortRegistryVO> GetPOR { get; set; }
        public ICollection<SubCategoryVO> GetVesselNationality { get; set; }
        public ICollection<SubCategoryVO> GetEngineTypes { get; set; }
        public ICollection<SubCategoryVO> GetPropulsionTypes { get; set; }
        public ICollection<SubCategoryVO> GetCertificateNames { get; set; }
        public ICollection<SubCategoryVO> GetSafeWorkingLoad { get; set; }
    }
}
