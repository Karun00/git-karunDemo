using System.Collections.Generic;

namespace IPMS.Domain.ValueObjects
{
    /// <summary>
    /// Data Transfer Object for Pilotexemption Reference Data
    /// </summary>
    public class PilotexemptionRequestReferenceVO
    {
        public ICollection<SubCategoryVO> Pilot_Nationality { get; set; }
        public ICollection<SubCategoryVO> MomentTypes { get; set; }
        public ICollection<SubCategoryVO> PilotRoleCode { get; set; }
        public ICollection<SubCategoryVO> Doctypes { get; set; }
        public ICollection<PioltVO> Pilots { get; set; }
        public ICollection<PortVO> Ports { get; set; }
        public ICollection<VesselVO> VesselDetails { get; set; }
        public ICollection<RoleVO> RoleDetails { get; set; }
    }
}
