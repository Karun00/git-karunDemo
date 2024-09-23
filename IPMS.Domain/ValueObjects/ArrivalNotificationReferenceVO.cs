using System.Collections.Generic;
 
namespace IPMS.Domain.ValueObjects
{
    /// <summary>
    /// Data Transfer Object for Arrival Notification Common data While loading
    /// </summary>
    public class ArrivalNotificationReferenceVO
    {
        public AgentDetailsVO agent { get; set; }
        public ICollection<BerthVO> Berths { get; set; }
        public ICollection<BerthVO> DryDocBerths { get; set; }
        public ICollection<SubCategoryCodeNameVO> Docks { get; set; }
        public ICollection<SubCategoryCodeNameVO> CargoTypes { get; set; }
        public ICollection<SubCategoryCodeNameVO> Purpose { get; set; }
        public ICollection<SubCategoryCodeNameVO> Uoms { get; set; }
        public ICollection<SubCategoryCodeNameVO> ReasonForVisit { get; set; }
        public ICollection<SubCategoryCodeNameVO> DangerousGoods { get; set; }
        public ICollection<SubCategoryCodeNameVO> Doctypes { get; set; }
        public ICollection<SubCategoryCodeNameVO> Tankers { get; set; }
        public ICollection<SubCategoryCodeNameVO> BunkerService { get; set; }
        public ICollection<SubCategoryCodeNameVO> Commoditys { get; set; }
        public ICollection<PioltVO> Pilots { get; set; }
        public ICollection<VesselVO> VesselDetails { get; set; }
        public ICollection<TerminalOperatorVO> BirthTos { get; set; }
        public List<ArrivalNotificationDraftVO> DraftDetails { get; set; }
        public ICollection<LicenseRequestVO> BunkersDetails { get; set; }
        public ICollection<PortCodeNameVO> PortDetails { get; set; }
        public ICollection<UserMasterVO> UserDetails { get; set; }

        public ICollection<MarpolGroupVO> Marpol { get; set; }
        public ICollection<LicenseRequestVO> WasteDclServiceProvider { get; set; }
        public ICollection<MarpolVO> WasteDclClass { get; set; }

        public bool RefUserType { get; set; }

        
        public ICollection<SubCategoryCodeNameVO> BunkersRequiredDetails { get; set; }
        public ICollection<SubCategoryCodeNameVO> BunkersMethodDetails { get; set; }
                
                
        
    }
}
