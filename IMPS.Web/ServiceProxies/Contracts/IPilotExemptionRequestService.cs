using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IPilotExemptionRequestService : IDisposable
    {
        [OperationContract]
        List<PioltVO> GetPilotExemptionRequestlist();
        [OperationContract]
        PioltVO AddPilotExemptionRequest(PioltVO pilotexemptionrequestdata);
        [OperationContract]
        PioltVO ModifyPilotExemptionRequest(PioltVO pilotexemptionrequestdata);
        [OperationContract]
        PilotexemptionRequestReferenceVO GetPilotExemptionRequestReferencesVO();
        //[OperationContract]
        //Task<List<PioltVO>> GetPilotExemptionRequestlistAsync();
        //[OperationContract]
        //Task<PioltVO> AddPilotExemptionRequestAsync(PioltVO PilotExemptionRequestdata);
        //[OperationContract]
        //Task<PioltVO> ModifyPilotExemptionRequestAsync(PioltVO PilotExemptionRequestdata);
        //[OperationContract]
        //Task<PilotexemptionRequestReferenceVO> GetPilotExemptionRequestReferencesVOAsync();

        [OperationContract]
        void ApprovePilotExemptionRegistration(string pilotid, string remarks, string taskcode);
        [OperationContract]
        void RejectPilotExemptionRegistration(string pilotid, string remarks, string taskcode);
        [OperationContract]
        PioltVO GetPilotExemptionRequest(int id);
        [OperationContract]
        List<VesselVO> GetVesselNamesautoComplete(string searchvalue);
    }
}