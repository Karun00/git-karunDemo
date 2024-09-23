using System.Collections.Generic;
using IPMSFeedService.ValueObjects;

namespace IPMSFeedService.Repository
{
    public interface IIpmsFeedRepository
    {
        List<IPMSFeedVO> GetIpmsServiceFeedDetails(string portCode, string movementFromDate, string movementToDate, string vcn, string imono, string vesselname);

        List<IPMSANFeedVO> GetIpmsANFeedDetails(string portCode, string etaFromDate, string etaToDate, string vcn, string imono, string vesselname);
        
        List<IPMSLocationVO> GetIpmsFeedLocationDetails(string portCode, string portLimitInFromDate, string portLimitInToDate, string vcn, string imono, string vesselname);
    }
}
