using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Repository
{
    public interface IPilotExemptionRequestRepository
    {

        Pilot GetPilotRequestDetailsByid(int value);
        List<Pilot> GetPilotExemptionRequestlist(string portcode);
        Pilot GetPilotRequestDetailsforFormByid(int pilotreqId);
       
    }
}

