using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IPilotExemptionRequestService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<PioltVO> GetPilotExemptionRequestlist();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        PioltVO AddPilotExemptionRequest(PioltVO pilotexemptionrequestdata);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        PioltVO ModifyPilotExemptionRequest(PioltVO pilotexemptionrequestdata);


        [OperationContract]
        [FaultContract(typeof(Exception))]
        PilotexemptionRequestReferenceVO GetPilotExemptionRequestReferencesVO();


        [OperationContract]
        [FaultContract(typeof(Exception))]
        void ApprovePilotExemptionRegistration(string pilotid, string remarks, string taskcode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void RejectPilotExemptionRegistration(string pilotid, string remarks, string taskcode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        PioltVO GetPilotExemptionRequest(int id);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<VesselVO> GetVesselNamesautoComplete(string searchvalue);

    }
}
