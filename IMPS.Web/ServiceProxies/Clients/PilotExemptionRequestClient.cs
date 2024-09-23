using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Clients
{
    public class PilotExemptionRequestClient : UserClientBase<IPilotExemptionRequestService>, IPilotExemptionRequestService
    {
        public List<PioltVO> GetPilotExemptionRequestlist()
        {
            return WrapOperationWithException(() => Channel.GetPilotExemptionRequestlist());
        }

        public PioltVO AddPilotExemptionRequest(PioltVO pilotexemptionrequestdata)
        {
            return WrapOperationWithException(() => Channel.AddPilotExemptionRequest(pilotexemptionrequestdata));
        }

        public PioltVO ModifyPilotExemptionRequest(PioltVO pilotexemptionrequestdata)
        {
            return WrapOperationWithException(() => Channel.ModifyPilotExemptionRequest(pilotexemptionrequestdata));
        }

        public PilotexemptionRequestReferenceVO GetPilotExemptionRequestReferencesVO()
        {
            return WrapOperationWithException(() => Channel.GetPilotExemptionRequestReferencesVO());
        }

        //public Task<List<PioltVO>> GetPilotExemptionRequestlistAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetPilotExemptionRequestlistAsync());
        //}

        //public Task<PioltVO> AddPilotExemptionRequestAsync(PioltVO PilotExemptionRequestdata)
        //{
        //    return WrapOperationWithException(() => Channel.AddPilotExemptionRequestAsync(PilotExemptionRequestdata));
        //}

        //public Task<PioltVO> ModifyPilotExemptionRequestAsync(PioltVO PilotExemptionRequestdata)
        //{
        //    return WrapOperationWithException(() => Channel.ModifyPilotExemptionRequestAsync(PilotExemptionRequestdata));
        //}
        //public Task<PilotexemptionRequestReferenceVO> GetPilotExemptionRequestReferencesVOAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetPilotExemptionRequestReferencesVOAsync());
        //}

        public void ApprovePilotExemptionRegistration(string pilotid, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.ApprovePilotExemptionRegistration(pilotid, remarks, taskcode));
        }

        public void RejectPilotExemptionRegistration(string pilotid, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.RejectPilotExemptionRegistration(pilotid, remarks, taskcode));
        }

        public PioltVO GetPilotExemptionRequest(int id)
        {
            return WrapOperationWithException(() => Channel.GetPilotExemptionRequest(id));
        }
        public List<VesselVO> GetVesselNamesautoComplete(string searchValue)
        { return WrapOperationWithException(() => Channel.GetVesselNamesautoComplete(searchValue)); }

    }
}
