using System;
using System.Collections.Generic;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;
using System.Linq;
using System.Globalization;

namespace IPMS.Domain.DTOS
{
    public static class SuppServiceRequestMapExtension
    {
        public static List<SuppServiceRequestVO> MapToDto(this List<SuppServiceRequest> suppServiceRequests)
        {
            List<SuppServiceRequestVO> lstSuppServiceRequest = new List<SuppServiceRequestVO>();

            if (suppServiceRequests != null)
            {
                foreach (SuppServiceRequest suppServiceRequest in suppServiceRequests)
                {
                    lstSuppServiceRequest.Add(suppServiceRequest.MapToDto());
                }
            }

            return lstSuppServiceRequest;
        }

        public static SuppServiceRequestVO MapToDto(this SuppServiceRequest data)
        {
            SuppServiceRequestVO suppServiceRequestVO = new SuppServiceRequestVO();
            if (data != null)
            {
                suppServiceRequestVO.SuppServiceRequestID = data.SuppServiceRequestID;
                suppServiceRequestVO.VCN = data.VCN;
                //suppServiceRequestVO.VesselName = data.Vessel != null ? data.Vessel.VesselName : "";
                suppServiceRequestVO.ServiceType = data.ServiceType;
                suppServiceRequestVO.PortCode = data.PortCode;
                //suppServiceRequestVO.QuayCode = data.QuayCode;
                //suppServiceRequestVO.BerthCode = data.BerthCode;
                suppServiceRequestVO.BerthName = data.Berth.BerthName;
                suppServiceRequestVO.BerthKey = data.BerthCode != null ? data.PortCode + "." + data.QuayCode + "." + data.BerthCode : null;
                suppServiceRequestVO.FromDate = Convert.ToString(data.FromDate, CultureInfo.InvariantCulture);
                if (data.ToDate != null)
                {
                    suppServiceRequestVO.ToDate = Convert.ToString(data.ToDate, CultureInfo.InvariantCulture);
                }
                suppServiceRequestVO.Remarks = data.Remarks;
                suppServiceRequestVO.Quantity = data.Quantity;
                suppServiceRequestVO.TermsandConditions = data.TermsandConditions == "Y" ? true : false;
                suppServiceRequestVO.WorkflowInstanceID = data.WorkflowInstanceID;
                suppServiceRequestVO.RecordStatus = data.RecordStatus;
                suppServiceRequestVO.CreatedBy = data.CreatedBy;
                suppServiceRequestVO.CreatedDate = data.CreatedDate;
                suppServiceRequestVO.ModifiedBy = data.ModifiedBy;
                suppServiceRequestVO.ModifiedDate = data.ModifiedDate;
                suppServiceRequestVO.ServiceTypeName = data.SubCategory.SubCatName;
                suppServiceRequestVO.ArrivalNotification = data.ArrivalNotification.MapToDto();
                //suppServiceRequestVO.ArrivalNotification.Vessel = data.ArrivalNotification.Vessel.MapToDTO();                
                suppServiceRequestVO.VesselName = data.ArrivalNotification.Vessel != null ? data.ArrivalNotification.Vessel.VesselName : "";
                suppServiceRequestVO.ArrivalNotification.CurrentBerth = data.ArrivalNotification.VesselCalls.Count > 0 ? (data.ArrivalNotification.VesselCalls.FirstOrDefault().ToPositionBerthCode != null ? data.ArrivalNotification.VesselCalls.FirstOrDefault().Bollard1.Berth.BerthName : "NA") : "NA";

                if (data.ArrivalNotification.ArrivalReasons.Count > 0)
                {
                    if (data.ArrivalNotification.ArrivalReasons.FirstOrDefault().SubCategory != null)
                    {
                        var lstArrivalReasons = (data.ArrivalNotification.ArrivalReasons.Select(k => k.SubCategory.SubCatName)).ToList<String>();
                        suppServiceRequestVO.ArrivalNotification.ReasonForVisit = lstArrivalReasons.Count > 0 ? String.Join(", ", lstArrivalReasons) : "NA";
                    }
                }

                if (data.ArrivalNotification != null)
                {
                    if (data.ArrivalNotification.VesselETAChanges != null)
                    {

                        int i = 0;
                        int j = 0;
                        var vesseletachange = data.ArrivalNotification.VesselETAChanges.OrderByDescending(t => t.VesselETAChangeID).ToList();

                        foreach (var vesseleta in vesseletachange)
                        {
                            if (vesseleta.VoyageIn != "" && i == 0)
                            {
                                i = 1;
                                suppServiceRequestVO.ArrivalNotification.VoyageIn = vesseleta.VoyageIn;
                            }
                            if (vesseleta.VoyageOut != "" && j == 0)
                            {
                                j = 1;
                                suppServiceRequestVO.ArrivalNotification.VoyageOut = vesseleta.VoyageOut;
                            }
                        }

                    }
                }

                suppServiceRequestVO.VesselName = data.ArrivalNotification.Vessel.VesselName;
                suppServiceRequestVO.WFStatus = data.WorkflowInstance.SubCategory.SubCatName;

                //-- Added by sandeep on 21-02-2015
                suppServiceRequestVO.AnyDangerousGoods = data.ArrivalNotification.AnyDangerousGoodsonBoard;
                //-- end

                if (data.SuppFloatingCranes.Count > 0)
                {
                    List<SuppFloatingCraneVO> lstSuppFloatingCraneVO = new List<SuppFloatingCraneVO>();

                    foreach (SuppFloatingCrane floatingcrane in data.SuppFloatingCranes)
                    {
                        lstSuppFloatingCraneVO.Add(floatingcrane.MapToDTO());
                    }

                    suppServiceRequestVO.SuppFloatingCranesVO = lstSuppFloatingCraneVO;
                }

                if (data.SuppHotColdWorkPermits.Count > 0)
                {
                    foreach (SuppHotColdWorkPermit suppHotColdWorkPermit in data.SuppHotColdWorkPermits)
                    {
                        suppServiceRequestVO.SuppHotColdWorkPermitsVO = suppHotColdWorkPermit.MapToDTO();
                    }
                }

                if (data.SuppHotWorkInspections != null)
                {
                    if (data.SuppHotWorkInspections.Count > 0)
                    {
                        foreach (SuppHotWorkInspection suppHotWorkInspection in data.SuppHotWorkInspections)
                        {
                            suppServiceRequestVO.SuppHotWorkInspectionVO = suppHotWorkInspection.MapToDTO();
                        }
                    }
                }

                suppServiceRequestVO.AgentId = data.AgentId;
                suppServiceRequestVO.IsPrimaryAgent = data.IsPrimaryAgent;
                suppServiceRequestVO.IsStartTime = ((data.WorkflowInstance.WorkflowTaskCode == "WFSA" && data.IsStartTime == "Y") || (data.WorkflowInstance.WorkflowTaskCode == "WFRE")) ? false : true;

            }

            return suppServiceRequestVO;
        }

        public static SuppServiceRequest MapToEntity(this SuppServiceRequestVO vo)
        {
            SuppServiceRequest suppServiceRequest = new SuppServiceRequest();
            if (vo != null)
            {
                suppServiceRequest.SuppServiceRequestID = vo.SuppServiceRequestID;
                suppServiceRequest.VCN = vo.VCN;
                suppServiceRequest.ServiceType = vo.ServiceType;
                suppServiceRequest.VesselName = vo.VesselName;

                if (vo.BerthKey != "")
                {
                    string[] key = vo.BerthKey.Split('.');

                    suppServiceRequest.PortCode = key[0];
                    suppServiceRequest.QuayCode = key[1];
                    suppServiceRequest.BerthCode = key[2];
                }
                else
                {
                    suppServiceRequest.PortCode = vo.PortCode;
                    //suppServiceRequest.QuayCode = vo.QuayCode;
                    //suppServiceRequest.BerthCode = vo.BerthCode;  Invalid date              
                }

                suppServiceRequest.FromDate = DateTime.Parse(vo.FromDate, CultureInfo.InvariantCulture);
                if (vo.ServiceType != "WTST")
                {
                    if (vo.ToDate != "" && vo.ToDate != null)
                    {
                        suppServiceRequest.ToDate = DateTime.Parse(vo.ToDate, CultureInfo.InvariantCulture);
                    }
                }
                suppServiceRequest.Remarks = vo.Remarks;
                suppServiceRequest.Quantity = vo.Quantity;
                suppServiceRequest.TermsandConditions = vo.TermsandConditions == true ? "Y" : "N";
                suppServiceRequest.WorkflowInstanceID = vo.WorkflowInstanceID;
                suppServiceRequest.RecordStatus = vo.RecordStatus;
                suppServiceRequest.CreatedBy = vo.CreatedBy;
                suppServiceRequest.CreatedDate = vo.CreatedDate;
                suppServiceRequest.SubmittedDateTime = vo.CreatedDate;
                suppServiceRequest.ModifiedBy = vo.ModifiedBy;
                suppServiceRequest.ModifiedDate = vo.ModifiedDate;
                suppServiceRequest.AgentId = vo.AgentId;
            }
            return suppServiceRequest;
        }
    }
}
