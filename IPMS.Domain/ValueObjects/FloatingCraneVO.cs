﻿using System;

namespace IPMS.Domain.ValueObjects
{
    /// <summary>
    /// Data Transfer Object for FloatingCrane
    /// </summary>
   
    public partial class FloatingCraneVO
    {
        
        public int FloatingCraneID { get; set; }

        
        public int LicenseRequestID { get; set; }
        
        public string QualificationsCompetencies { get; set; }
        
        public string EmployQualifiedTrainedPers { get; set; }
        
        public string HardHat { get; set; }
        
        public string SafetyShoes { get; set; }
        
        public string ReflectiveJacket { get; set; }
        
        public string SelfInflatingLifeJacket { get; set; }
        
        public string QualifyPublicLiabilityInsu { get; set; }
        
        public string RecordStatus { get; set; }
        
        public int CreatedBy { get; set; }
        
        public System.DateTime CreatedDate { get; set; }
        
        public Nullable<int> ModifiedBy { get; set; }
        
        public System.DateTime ModifiedDate { get; set; }
        //  public  LicenseRequestVO LicenseRequest { get; set; }

    }
}
