using System.Collections.Generic;

namespace IPMS.Domain.ValueObjects
{
    /// <summary>
    /// Data Transfer Object for LicenseRequest Reference Data
    /// </summary>
    public class LicenseRequestReferenceVO
    {
        public ICollection<SubCategoryVO> DocumentsTypes { get; set; }
        public ICollection<SubCategoryVO> LicensingRequestTypes { get; set; }
    }
}

