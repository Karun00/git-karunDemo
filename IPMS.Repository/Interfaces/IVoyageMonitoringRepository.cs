using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Repository
{
    public interface IVoyageMonitoringRepository
    {
        Vessel GetVesselDetails(int vesselId);
        List<VoyageMonitoringVO> GetVcnDetailsVoyage(string portCode, string searchValue, int agentId);
        List<VoyageMonitoringVO> GetVcnDetailsVoyageVcn(string vcn, string portCode);
        List<VoyageMonitoringVO> GetServiceRequestDetails(string vcn);
        List<VoyageMonitoringVO> GetChangeAtaAndAtdDetails(string vcn);
        List<VoyageMonitoringVO> GetAnchorageDetails(string vcn);
        List<VoyageMonitoringVO> GetBerthDetails(string vcn, string portCode);
        List<VoyageMonitoringVO> GetPortAndBreakLimitDetails(string vcn);
        UserMasterVO GetUserDetails(int UserID, string PortCode);
    }
}
