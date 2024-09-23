using System;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Repository
{
    public interface IVesselETAChangeRepository
    {
        VesselETAChangeVO GetVesselEtaChangeDetailsById(string value);
        List<VesselETAChangeVO> GetArrivalVcns(string portCode);
        List<VesselETAChangeVO> GetArrivalVcnsOnAgentBased(string portCode, int agentId);
        List<VesselETAChangeVO> ChangeEtaDetailsOnAgentBased(string portCode, int agentId, string vcn, string vesselName, string etaFrom, string etaTo, string agentNameSearch);
        VesselETAChangeVO GetVesselInfoByVcn(string vcn);
        List<VesselETAChangeVO> ChangeEtaDetails(string portCode, string vcn, string vesselName, string etaFrom, string etaTo, string agentNameSearch);
        List<VesselETAChangeVO> ChangezEtaDetails(string vcn, int? vesselEatChangeId);
    }
}
