using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPMS.Domain.Models
{
    public partial class IndividualPersonalPermit : EntityBase
    {
        public int IndividualPersonalPermitID { get; set; }
        public int PermitRequestID { get; set; }        
        public string permittype { get; set; }
        public string IndividualTemporaryPermits { get; set; }
        public string IndividualPermanentPermits { get; set; }
        public System.DateTime? TempFromDate { get; set; }
        public System.DateTime? TempToDate { get; set; }
        public System.DateTime? PerFromDate { get; set; }
        public System.DateTime? PerToDate { get; set; }
        public string IsCamera { get; set; }
        public string CameraDetails { get; set; }
        public string IsTools { get; set; }
        public string ToolsDetails { get; set; }
        public string IsSpclEquipment { get; set; }
        public string SpclEquipmentDetails { get; set; }
        public string AuthorisedSurname { get; set; }
        public string TelephoneWork { get; set; }
        public string AuthorisedMobile { get; set; }
        public string AuthorisedIdentityNumber { get; set; }
        public string AuthorisedEmail { get; set; }
        public string AuthorisedSignature { get; set; }
        public DateTime SignatoryDate { get; set; }
        public string ContractorTemporaryPermits{ get; set; }
        public string ContractorPermanentPermits { get; set; }
        public System.DateTime? ContractorTempFromDate { get; set; }
        public System.DateTime? ContractorTempToDate { get; set; }
        public System.DateTime? ContractorPerFromDate { get; set; }
        public System.DateTime? ContractorPerToDate { get; set; }

        public virtual PermitRequest PermitRequest { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual SubCategory SubCategory1 { get; set; }
        public virtual SubCategory SubCategory2 { get; set; }
        public virtual SubCategory SubCategory3 { get; set; }
       
    }
}
