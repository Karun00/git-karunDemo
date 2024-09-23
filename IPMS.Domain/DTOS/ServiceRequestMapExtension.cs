using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class ServiceRequestMapExtension
    {
        public static List<ServiceRequest_VO> MapToDto(this List<ServiceRequest> servicerequests)
        {
            List<ServiceRequest_VO> servicerequestVos = new List<ServiceRequest_VO>();
            if (servicerequests != null)
            {
                foreach (var serviceRequest in servicerequests)
                {
                    servicerequestVos.Add(serviceRequest.MapToDto());
                }
            }
            return servicerequestVos;
        }
        public static ServiceRequest_VO MapToDto(this ServiceRequest data)
        {
            ServiceRequest_VO servicerequestVo = new ServiceRequest_VO();
            if (data != null)
            {
                servicerequestVo.ServiceRequestID = data.ServiceRequestID;
                servicerequestVo.VCN = data.VCN;
                servicerequestVo.MovementType = data.MovementType;
                servicerequestVo.MovementDateTime = data.MovementDateTime.ToString("yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture);
                servicerequestVo.SideAlongSideCode = data.SideAlongSideCode;
                servicerequestVo.SubmittedDateTime = data.CreatedDate;
                if (data.ArrivalNotification != null)
                {
                    if (data.ArrivalNotification.Vessel != null)
                    {
                        servicerequestVo.VesselName = data.ArrivalNotification.Vessel.VesselName;
                    }
                    if (data.ArrivalNotification.ArrivalReasons.Count > 0)
                    {
                        if (data.ArrivalNotification.ArrivalReasons.FirstOrDefault().SubCategory != null)
                        {
                            servicerequestVo.ReasonForVisit = (data.ArrivalNotification.ArrivalReasons.Select(k => k.SubCategory.SubCatName)).ToList<String>();
                        }

                    }
                }


                if (data.SubCategory != null)
                { servicerequestVo.MovementName = data.SubCategory.SubCatName; }
                servicerequestVo.OwnSteam = data.OwnSteam == "Y" ? true : false;
                servicerequestVo.IsTidal = data.IsTidal == "Y" ? true : false;
                servicerequestVo.NoMainEngine = data.NoMainEngine == "Y" ? true : false;
                servicerequestVo.Comments = data.Comments;
                servicerequestVo.RecordStatus = data.RecordStatus;
                servicerequestVo.CreatedBy = data.CreatedBy;
                servicerequestVo.CreatedDate = data.CreatedDate;
                servicerequestVo.ModifiedBy = data.ModifiedBy;
                servicerequestVo.ModifiedDate = data.ModifiedDate;
                servicerequestVo.workflowRemarks = null;
                servicerequestVo.WorkflowInstanceId = data.WorkflowInstanceId;
                servicerequestVo.BPWorkflowInstanceId = data.BPWorkflowInstanceId;

                if (data.PreferredDateTime != null)
                {
                    servicerequestVo.PreferredDateTime = Convert.ToString(data.PreferredDateTime); 
                }
                servicerequestVo.SlotPeriod = data.SlotPeriod;
                servicerequestVo.MovementSlot = data.MovementSlot;
                

                if (data.WorkflowInstance != null)
                {
                    servicerequestVo.WorkflowTaskCode = data.WorkflowInstance.WorkflowTaskCode;
                    if (data.WorkflowInstance.SubCategory != null)
                    {
                        servicerequestVo.WorkflowInstanceTaskName = data.WorkflowInstance.SubCategory.SubCatName;
                        if (data.VesselCallMovements.Count != 0)
                        {
                            if (data.VesselCallMovements.FirstOrDefault().MapToDto().MovementStatus == "BERT" || data.VesselCallMovements.FirstOrDefault().MapToDto().MovementStatus == "SALD")
                            {
                                servicerequestVo.WorkflowInstanceTaskName = "Completed";
                            }
                        }
                    }
                }
                else
                {
                    if (data.WorkflowInstance1 != null)
                    {
                        servicerequestVo.WorkflowTaskCode = data.WorkflowInstance1.WorkflowTaskCode;
                        if (data.WorkflowInstance1.SubCategory != null)
                        {
                            servicerequestVo.WorkflowInstanceTaskName = data.WorkflowInstance1.SubCategory.SubCatName;
                        }

                    }
                }



                servicerequestVo.IsFinal = data.IsFinal;
                servicerequestVo.IsRecordingCompleted = data.IsRecordingCompleted;


                if (data.ArrivalNotification != null)
                {
                    if (data.ArrivalNotification.VesselCalls.Count > 0)
                    {
                        servicerequestVo.ETA = data.ArrivalNotification.VesselCalls.FirstOrDefault().MapToDtoForServiceRquest().ETA;
                        servicerequestVo.ETD = data.ArrivalNotification.VesselCalls.FirstOrDefault().MapToDtoForServiceRquest().ETD;                       
                    }
                }



                if (data.ServiceRequestShiftings != null)
                {
                    servicerequestVo.ServiceRequestShifting = data.ServiceRequestShiftings.Count != 0 ? data.ServiceRequestShiftings.FirstOrDefault().MapToDto() : null;
                }
                if (data.ServiceRequestSailings != null)
                {
                    servicerequestVo.ServiceRequestSailing = data.ServiceRequestSailings.Count != 0 ? data.ServiceRequestSailings.FirstOrDefault().MapToDto() : null;
                }
                if (data.ServiceRequestWarpings != null)
                {
                    servicerequestVo.ServiceRequestWarping = data.ServiceRequestWarpings.Count != 0 ? data.ServiceRequestWarpings.FirstOrDefault().MapToDto() : null;
                }
                if (data.ServiceRequestDocuments != null)
                {
                    servicerequestVo.ServiceRequestDocuments = data.ServiceRequestDocuments.Count != 0 ? data.ServiceRequestDocuments.MapToDto() : null;
                }

                if (data.ArrivalNotification != null)
                {
                    servicerequestVo.ArrivalNotification = data.ArrivalNotification != null ? data.ArrivalNotification.ServicerequestMapToDTO() : null;
                    if (data.ArrivalNotification.LastPort != null)
                    {
                        servicerequestVo.LastPortOfCall = data.ArrivalNotification.LastPort.PortName;
                        servicerequestVo.NextPortOfCall = data.ArrivalNotification.NextPort.PortName;
                    }
                }


                if (data.ArrivalNotification != null)
                {
                    if (data.ArrivalNotification.VesselETAChanges != null)
                    {

                        int i = 0;
                        var vesseletachange = data.ArrivalNotification.VesselETAChanges.OrderByDescending(t => t.VesselETAChangeID).ToList();

                        foreach (var vesseleta in vesseletachange)
                        {
                            if (vesseleta.VoyageIn != "" && i == 0)
                            {
                                i = 1;
                                servicerequestVo.ArrivalNotification.VoyageIn = vesseleta.VoyageIn;
                            }
                        }

                    }
                }
                
                if (data.VesselCallMovements.Count != 0)
                {
                    servicerequestVo.VesselCallMovement = data.VesselCallMovements.Count != 0 ? data.VesselCallMovements.FirstOrDefault().MapToDto() : null;
                }
                if (data.SubCategory != null)
                {
                    servicerequestVo.Subcategory = data.SubCategory != null ? data.SubCategory.MapToDto() : null;
                }
                if (data.SubCategory1 != null)
                {
                    servicerequestVo.Subcategory1 = data.SubCategory1 != null ? data.SubCategory1.MapToDto() : null;
                }

                if (data.ArrivalNotification != null)
                {
                    if (data.ArrivalNotification.Agent != null)
                    {
                        servicerequestVo.Agent = data.ArrivalNotification.Agent != null ? data.ArrivalNotification.Agent.MapToDTO() : null;
                    }
                }

                if (data.ArrivalNotification != null)
                {
                    if (data.ArrivalNotification.Agent != null)
                    {
                        servicerequestVo.AuthorizedContactPerson = data.ArrivalNotification.Agent.AuthorizedContactPerson != null ? data.ArrivalNotification.Agent.AuthorizedContactPerson.MapToDTO() : null;
                    }
                }
                if (data.ArrivalNotification != null)
                {
                    if (data.ArrivalNotification.Vessel != null)
                    {
                        servicerequestVo.VesselType = data.ArrivalNotification.Vessel.SubCategory3.SubCatName;
                        servicerequestVo.VesselNationality = data.ArrivalNotification.Vessel.SubCategory2.SubCatName;
                    }
                }
                servicerequestVo.DraftFWD = data.DraftFWD;
                servicerequestVo.DraftAFT = data.DraftAFT;
                servicerequestVo.PassengersEmbarking = data.PassengersEmbarking;
                servicerequestVo.PassengersDisembarking = data.PassengersDisembarking;
                
            }
            return servicerequestVo;
        }
        public static ServiceRequest MapToEntity(this ServiceRequest_VO vo)
        {
            ServiceRequest servicerequest = new ServiceRequest();
            if (vo != null)
            {
                servicerequest.ServiceRequestID = vo.ServiceRequestID;
                servicerequest.VCN = vo.VCN;
                servicerequest.MovementType = vo.MovementType;
                servicerequest.MovementDateTime = DateTime.Parse(vo.MovementDateTime, CultureInfo.InvariantCulture);
                servicerequest.SubmittedDateTime = vo.CreatedDate;
                servicerequest.SideAlongSideCode = vo.SideAlongSideCode;
                servicerequest.IsTidal = vo.IsTidal.ToString(CultureInfo.InvariantCulture);
                servicerequest.OwnSteam = vo.OwnSteam.ToString(CultureInfo.InvariantCulture);
                servicerequest.NoMainEngine = vo.NoMainEngine.ToString(CultureInfo.InvariantCulture);
                servicerequest.Comments = vo.Comments;
                servicerequest.RecordStatus = vo.RecordStatus;
                servicerequest.CreatedBy = vo.CreatedBy;
                servicerequest.CreatedDate = vo.CreatedDate;
                servicerequest.ModifiedBy = vo.ModifiedBy;
                servicerequest.ModifiedDate = vo.ModifiedDate;
                servicerequest.WorkflowInstanceId = (vo.WorkflowInstanceId == 0 ? null : vo.WorkflowInstanceId);
                servicerequest.IsFinal = (string.IsNullOrEmpty(vo.IsFinal) ? null : vo.IsFinal);
                servicerequest.MovementName = vo.MovementName;
                servicerequest.VesselName = vo.VesselName;
                servicerequest.BPWorkflowInstanceId = (vo.BPWorkflowInstanceId == 0 ? null : vo.BPWorkflowInstanceId);                
                if (vo.PreferredDateTime != null)
                {
                    servicerequest.PreferredDateTime = DateTime.Parse(vo.PreferredDateTime, CultureInfo.InvariantCulture);
                }
                servicerequest.SlotPeriod = vo.SlotPeriod;
                servicerequest.MovementSlot = vo.MovementSlot;

                if (vo.ServiceRequestShifting != null)
                {
                    servicerequest.ServiceRequestShiftings = new List<ServiceRequestShifting>();
                    servicerequest.ServiceRequestShiftings.Add(vo.ServiceRequestShifting.MapToEntity());
                }
                if (vo.ServiceRequestSailing != null)
                {
                    servicerequest.ServiceRequestSailings = new List<ServiceRequestSailing>();
                    servicerequest.ServiceRequestSailings.Add(vo.ServiceRequestSailing.MapToEntity());
                }
                if (vo.ServiceRequestWarping != null)
                {
                    servicerequest.ServiceRequestWarpings = new List<ServiceRequestWarping>();
                    servicerequest.ServiceRequestWarpings.Add(vo.ServiceRequestWarping.MapToEntity());
                }
                if (vo.ServiceRequestDocuments != null)
                {                   
                    servicerequest.ServiceRequestDocuments = vo.ServiceRequestDocuments.MapToEntity();
                }


                servicerequest.DraftFWD = vo.DraftFWD;
                servicerequest.DraftAFT = vo.DraftAFT;
                servicerequest.PassengersEmbarking = vo.PassengersEmbarking;
                servicerequest.PassengersDisembarking = vo.PassengersDisembarking;
                servicerequest.IsUpdateMovement = vo.IsUpdateMovement;
            }

            return servicerequest;
        }




    }
}
