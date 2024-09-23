using IPMS.Domain.Models; 
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IPMS.Repository
{
    public interface IServiceRequestRepository
    {


        List<ServiceRequestVCNDetails> GetVCNDetailsForServiceRequest(string PortCode, int AgentUserID, int ToUserID, string searchValue);
        List<ServiceRequest_VO> GetServiceRequestDetails(string portCode, int AgentUserID, int ToUserID, int EmpID, string frmdate, string todate, string vcnSearch, string vesselName, string MovementType);
        List<ServiceRequestCureentBerthnBollards> GetCurrentBerthAndBollards(string vcn, string PortCode);
        List<Bollard> GetBollardAtBerth(string BerthCode, string PortCode);
        List<ServiceRequest_VO> GetServiceRequest(string serviceid);
        ServiceRequest_VO GetServiceRequestByID(string value);
        ArrivalNotificationDetails GetArrivalNotificationByID(string value);

        ServiceRequest GetServiceRequestDetailsForWorkFlow(string ServiceRequestID);
        
        List<VesselCallMovement> GetVesselCallMovement(int ServiceRequestID);
        List<ServiceRequest> GetServRequestDeatailsForValidation(string VCN);
        List<ServiceRequest> GetServRequestDeatailsForValidationForArrival(string VCN);

        VesselCallMovementVO GetVCallMovtAtVCN(string VCN, string PortCode);

        VesselCallMovement GetVesselCallMovementAtVCN(string VCN, string portcode);

        UserMasterVO GetTerminalOperatorForUser(int UserID, string PortCode);

        List<ServiceRequestVCNDetails> GetVCNDetails(string PortCode);

        VesselCallMovementVO GetVCallMovtAtVCNArrival(string VCN, string PortCode);

        ServiceRequest GetAllServRequestDeatailsForValidationForArrival(string VCN);

        ServiceRequest GetAllServRequestDeatailsAllMoments(string VCN, string MomentType);

        List<VesselCallMovementVO> GetAllVesselCallMovements(string VCN, string portcode);

        List<SlotVO> GetSlotDetails(string portcode);

        SlotVO GetSlotPeriodBySlotDate(DateTime PreferredDate, string portCode);

        List<AutomatedSlotBlockingVO> GetBlockedSlots(string portCode);

        AutomatedSlotConfiguration GetAutoConfiguredSlots(DateTime movementDate, string portCode);

        List<VesselCallMovementVO> GetTotalSlotsAvailable(DateTime movementDate, string slotPeriod, string portCode);

        List<SlotVO> GetSlotPeriodFromDateToDate(AutomatedSlotBlockingVO data, string portCode);
        List<ServiceRequest_VO> GetIncompleteMovementDetailsById(string vcn);

     //   string GetMomentTypeCount(string MovementSlot, DateTime momentDate,string momenttype,string portcode);

    }
}
