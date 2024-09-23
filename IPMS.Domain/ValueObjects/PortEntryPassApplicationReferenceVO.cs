using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public class PortEntryPassApplicationReferenceVO
    {
        public List<SubCategoryCodeNameVO> ApplicationCategory { get; set; }
        public List<SubCategoryCodeNameVO> PermitTypes { get; set; }    
        public List<SubCategoryCodeNameVO> AreaOfConstruction { get; set; }
        public List<SubCategoryCodeNameVO> TemporaryPermits { get; set; }
        public List<SubCategoryCodeNameVO> AdhocPermits { get; set; }
        public List<SubCategoryCodeNameVO> OperatedTerminals { get; set; }
        public List<SubCategoryCodeNameVO> PermitDocumentTypes { get; set; }
        public List<SubCategoryCodeNameVO> SecurityDocumentTypes { get; set; }
        public List<SubCategoryCodeNameVO> PermitRequeirements { get; set; }
        public List<SubCategoryCodeNameVO> AccessGates { get; set; }
        public List<SubCategoryCodeNameVO> PermitRequeirementstypes { get; set; }
        public List<SubCategoryCodeNameVO> PermitRequeirementsDuration { get; set; }
        public List<SubCategoryCodeNameVO> PermitCodes { get; set; }
        public List<SubCategoryCodeNameVO> PermitStatus { get; set; }
        public List<SubCategoryCodeNameVO> IndividualTemporaryPermits { get; set; }
        public List<SubCategoryCodeNameVO> AccessAreasForRBs { get; set; }
        public List<SubCategoryCodeNameVO> AccessAreasForCTs { get; set; }
        public List<SubCategoryCodeNameVO> AccessAreasForDBs { get; set; }
        public List<SubCategoryCodeNameVO> AccessAreasForELs { get; set; }
        public List<SubCategoryCodeNameVO> AccessAreasForNGs { get; set; }
        public List<SubCategoryCodeNameVO> AccessAreasForMBs { get; set; }
        public List<SubCategoryCodeNameVO> AccessAreasForSBs { get; set; }
        public List<SubCategoryCodeNameVO> AccessAreasForPEs { get; set; }
        public List<SubCategoryCodeNameVO> ContractorTemporaryPermits { get; set; }
        public List<SubCategoryCodeNameVO> ContractorPermanentPermits { get; set; }
        public List<SubCategoryCodeNameVO> IndividualPermanentPermits { get; set; }
        public List<SubCategoryCodeNameVO> ReasonForPermits { get; set; }
        public List<SubCategoryCodeNameVO> ContractorReasonForPermits { get; set; }
        public ICollection<SubCategoryVO> Country { get; set; }

        public ICollection<PortVO> Ports { get; set; }
    }
}
