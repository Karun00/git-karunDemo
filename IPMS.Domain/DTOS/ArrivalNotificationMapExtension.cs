using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace IPMS.Domain.DTOS
{
    public static class ArrivalNotificationMapExtension
    {
        /// <summary>
        /// Data List Transfer from Entity to DTO
        /// </summary>
        /// <param name="arrivalNotifications"></param>
        /// <returns></returns>
        /// 
        public static List<ArrivalNotificationVO> MapToDto(this List<ArrivalNotification> arrivalNotifications)
        {
            List<ArrivalNotificationVO> arrivalNotificationrVos = new List<ArrivalNotificationVO>();
            if (arrivalNotifications != null)
            {
                foreach (var arrivalNotification in arrivalNotifications)
                {
                    arrivalNotificationrVos.Add(arrivalNotification.MapToDto());
                }
            }
            return arrivalNotificationrVos;
        }

        public static List<ArrivalNotificationVO> MapToDto(this List<ArrivalNotification> arrivalNotifications, string userType)
        {
            List<ArrivalNotificationVO> arrivalNotificationrVos = new List<ArrivalNotificationVO>();
            if (arrivalNotifications != null)
            {
                foreach (var arrivalNotification in arrivalNotifications)
                {
                    arrivalNotificationrVos.Add(arrivalNotification.MapToDto(userType));
                }
            }
            return arrivalNotificationrVos;
        }

        /// <summary>
        /// Data List Transfer from Entity to DTO For Draft
        /// </summary>
        /// <param name="arrivalNotifications"></param>
        /// <returns></returns>
        public static List<ArrivalNotificationDraftVO> DraftMapToDto(this List<ArrivalNotification> arrivalNotifications)
        {
            List<ArrivalNotificationDraftVO> arrivalNotificationrVos = new List<ArrivalNotificationDraftVO>();
            if (arrivalNotifications != null)
            {
                foreach (var arrivalNotification in arrivalNotifications)
                {
                    arrivalNotificationrVos.Add(arrivalNotification.DraftMapToDto());
                }
            }
            return arrivalNotificationrVos;
        }

        /// <summary>
        /// Data Transfer from Entity to DTO  For Draft
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ArrivalNotificationDraftVO DraftMapToDto(this ArrivalNotification data)
        {
            ArrivalNotificationDraftVO arrivalnotificationVO = new ArrivalNotificationDraftVO();
            if (data != null)
            {
                arrivalnotificationVO.VCN = data.VCN;
                arrivalnotificationVO.VCNdraftDisplay = data.VCN + " - " + data.Vessel.IMONo + " - " + data.Vessel.VesselName;
            }
            return arrivalnotificationVO;
        }



        /// <summary>
        /// Data Transfer from Entity to DTO
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// 
        public static ArrivalNotificationVO MapToDto(this ArrivalNotification data)
        {
            ArrivalNotificationVO arrivalnotificationVO = new ArrivalNotificationVO();
            if (data != null)
            {
                arrivalnotificationVO.VCN = data.VCN;
                arrivalnotificationVO.PortCode = data.PortCode;
                arrivalnotificationVO.AgentID = data.AgentID;
                arrivalnotificationVO.VesselID = data.VesselID;
                arrivalnotificationVO.VoyageIn = data.VoyageIn;
                arrivalnotificationVO.VoyageOut = data.VoyageOut;
                arrivalnotificationVO.ArrDraft = data.ArrDraft;
                arrivalnotificationVO.DepDraft = data.DepDraft;         

                arrivalnotificationVO.ETA = data.ETA.ToString();
                arrivalnotificationVO.ETD = data.ETD.ToString();

                arrivalnotificationVO.IsTerminalOperator = data.IsTerminalOperator;
                arrivalnotificationVO.TerminalOperatorID = data.TerminalOperatorID;
                arrivalnotificationVO.LastPortOfCall = data.LastPortOfCall;
                arrivalnotificationVO.NextPortOfCall = data.NextPortOfCall;
                arrivalnotificationVO.NominationDate = data.NominationDate;
                arrivalnotificationVO.AppliedForISPS = data.AppliedForISPS;
                arrivalnotificationVO.AppliedDate = data.AppliedDate;
                arrivalnotificationVO.Clearance = data.Clearance;
                arrivalnotificationVO.ISPSReferenceNo = data.ISPSReferenceNo;
                arrivalnotificationVO.PilotExemption = data.PilotExemption;
                arrivalnotificationVO.ExemptionPilotID = data.ExemptionPilotID;
                arrivalnotificationVO.VesselType = data.Vessel.SubCategory3 == null ? string.Empty : data.Vessel.SubCategory3.SubCatName;

                if (data.ExemptionPilotID > 0)
                {
                    arrivalnotificationVO.PilotExemptionChecked = true;
                }
                else
                {
                    arrivalnotificationVO.PilotExemptionChecked = false;
                }

                arrivalnotificationVO.DaylightSpecifyReason = data.DaylightSpecifyReason;
                arrivalnotificationVO.AnyDangerousGoodsonBoard = data.AnyDangerousGoodsonBoard;

                arrivalnotificationVO.PreferedBerthKey = data.PreferredPortCode + "." + data.PreferredQuayCode + "." + data.PreferredBerthCode;
                arrivalnotificationVO.AlternateBerthKey = data.AlternatePortCode + "." + data.AlternateQuayCode + "." + data.AlternateBerthCode;
                arrivalnotificationVO.DryDockBerthKey = data.DryDockBerthPortCode + "." + data.DryDockBerthQuayCode + "." + data.DryDockBerthCode;


                arrivalnotificationVO.PreferredSideDock = data.PreferredSideDock;
                arrivalnotificationVO.PreferredSideAlternateBirth = data.PreferredSideAlternateBirth;
                arrivalnotificationVO.ReasonAlternateBirth = data.ReasonAlternateBirth;
                arrivalnotificationVO.Tidal = data.Tidal;
                arrivalnotificationVO.CellNo = data.CellNo;
                arrivalnotificationVO.UNNo = data.UNNo;
                arrivalnotificationVO.BallastWater = data.BallastWater;
                arrivalnotificationVO.WasteDeclaration = data.WasteDeclaration;
                arrivalnotificationVO.DaylightRestriction = data.DaylightRestriction;
                arrivalnotificationVO.ExceedPortLimitations = data.ExceedPortLimitations;
                arrivalnotificationVO.ExceedSpecifyReason = data.ExceedSpecifyReason;
                arrivalnotificationVO.AnyAdditionalInfo = data.AnyAdditionalInfo;
                arrivalnotificationVO.IMDGNetQty = data.IMDGNetQty;
                arrivalnotificationVO.CargoDescription = data.CargoDescription;
                arrivalnotificationVO.ReasonForLayup = data.ReasonForLayup;
                arrivalnotificationVO.BunkersRequired = data.BunkersRequired;
                arrivalnotificationVO.BunkersMethod = data.BunkersMethod;
                arrivalnotificationVO.BunkerService = data.BunkerService;
                arrivalnotificationVO.DistanceFromStern = data.DistanceFromStern;
                arrivalnotificationVO.TonsMT = data.TonsMT;
                arrivalnotificationVO.AnyImpInfo = data.AnyImpInfo;
                arrivalnotificationVO.PlannedDurationDate = data.PlannedDurationDate;
                arrivalnotificationVO.PlannedDurationToDate = data.PlannedDurationToDate;

                arrivalnotificationVO.ReasonForVisit = data.ReasonForVisit;

                arrivalnotificationVO.IsANFinal = data.IsANFinal;
                arrivalnotificationVO.IsISPSANFinal = data.IsISPSANFinal;
                arrivalnotificationVO.IsPHANFinal = data.IsPHANFinal;
                arrivalnotificationVO.IsIMDGANFinal = data.IsIMDGANFinal;

                arrivalnotificationVO.IsSpecialNature = data.IsSpecialNature;
                arrivalnotificationVO.SpecialNatureReason = data.SpecialNatureReason;

                if (data.ArrivalReasons.Count > 0)
                {
                    foreach (ArrivalReason ar in data.ArrivalReasons)
                    {

                        if (arrivalnotificationVO.ReasonforvisitName == null)
                            arrivalnotificationVO.ReasonforvisitName = ar.SubCategory.SubCatName;
                        else
                            arrivalnotificationVO.ReasonforvisitName = arrivalnotificationVO.ReasonforvisitName + ',' + ar.SubCategory.SubCatName;
                    }
                }

                if (data.ArrivalAgents.Count > 0)
                {
                    foreach (ArrivalAgent arag in data.ArrivalAgents)
                    {
                        if (arag.IsPrimary == "F")
                        {
                            arrivalnotificationVO.SecondaryAgentID1 = arag.AgentID;
                            if (arag.Agent != null)
                            {
                                arrivalnotificationVO.SecondaryAgent1Name = arag.Agent.RegisteredName + " - " + arag.Agent.RegistrationNumber;
                            }
                        }
                        else if (arag.IsPrimary == "S")
                        {
                            arrivalnotificationVO.SecondaryAgentID2 = arag.AgentID;
                            if (arag.Agent != null)
                            {
                                arrivalnotificationVO.SecondaryAgent2Name = arag.Agent.RegisteredName + " - " + arag.Agent.RegistrationNumber;
                            }
                        }

                    }
                }
                

                DateTime? a = data.PlannedDurationToDate;
                DateTime? b = data.PlannedDurationDate;
                string Daycnt = string.Empty;

                if (a.HasValue && b.HasValue)
                    Daycnt = Convert.ToString((a.Value - b.Value).Days,CultureInfo.InvariantCulture);

                arrivalnotificationVO.Daycnt = Daycnt;



                arrivalnotificationVO.LoadDischargeDate = data.LoadDischargeDate;
                arrivalnotificationVO.DischargeDate = data.DischargeDate;
                arrivalnotificationVO.PlanDateTimeOfBerth = data.PlanDateTimeOfBerth;
                arrivalnotificationVO.PlanDateTimeToVacateBerth = data.PlanDateTimeToVacateBerth;
                arrivalnotificationVO.PlanDateTimeToStartCargo = data.PlanDateTimeToStartCargo;
                arrivalnotificationVO.PlanDateTimeToCompleteCargo = data.PlanDateTimeToCompleteCargo;
                arrivalnotificationVO.DangerousGoodsClass = data.DangerousGoodsClass;
                arrivalnotificationVO.RecordStatus = data.RecordStatus;
                arrivalnotificationVO.CreatedBy = data.CreatedBy;
                arrivalnotificationVO.CreatedDate = data.CreatedDate;
                arrivalnotificationVO.WorkflowInstanceId = data.WorkflowInstanceId;
                arrivalnotificationVO.ModifiedBy = data.ModifiedBy;
                arrivalnotificationVO.ModifiedDate = data.ModifiedDate;
                arrivalnotificationVO.SpecifyReason = data.SpecifyReason;

                arrivalnotificationVO.LastPortWasteDelivered = data.LastPortWasteDelivered;
                arrivalnotificationVO.NextPortWasteDelivery = data.NextPortWasteDelivery;
                arrivalnotificationVO.DateLastWasteDelivered = data.DateLastWasteDelivered;

                if (data.WasteDeclarations != null)
                {
                    arrivalnotificationVO.WasteDeclarations = data.WasteDeclarations.MapToDto();
                }


                if (data.IMDGInformations != null)
                {
                    arrivalnotificationVO.IMDGInformations = data.IMDGInformations.MapToDto();
                }

                if (data.WasteDeclarations != null)
                {
                    arrivalnotificationVO.WasteDeclarations = data.WasteDeclarations.MapToDto();
                }

                if (data.Vessel != null)
                {
                    arrivalnotificationVO.Vessel = data.Vessel.MapToDto();
                }
                if (data.ArrivalCommodities != null)
                {
                    arrivalnotificationVO.ArrivalCommodities = data.ArrivalCommodities.MapToDto();
                }
                if (data.SubCategory3 != null)
                {
                    arrivalnotificationVO.SubCategory = data.SubCategory3.MapToDto();
                }
                if (data.ArrivalIMDGTankers != null)
                {
                    arrivalnotificationVO.ArrivalIMDGTankers = data.ArrivalIMDGTankers.MapToDto();
                }
                if (data.ArrivalDocuments != null)
                {
                    arrivalnotificationVO.ArrivalDocuments = data.ArrivalDocuments.MapToDto();
                }

                arrivalnotificationVO.IsSamsaArrested = false;
                if (data.VesselArrestImmobilizationSAMSAs != null)
                {
                    foreach (var item in data.VesselArrestImmobilizationSAMSAs)
                    {
                        if (item.VesselArrested == "Y" && item.VesselReleased == "N")
                        {
                            arrivalnotificationVO.IsSamsaArrested = true;
                        }
                    }
                }

                arrivalnotificationVO.VCN_VesselName = data.VCN + "_" + data.Vessel.VesselName;

                // -- Added by sandeep on 25-12-2014

                List<VesselCall> lstVesselCalls = data.VesselCalls as List<VesselCall>;
                arrivalnotificationVO.CurrentBerth = lstVesselCalls.Count > 0 ? (lstVesselCalls[0].ToPositionBerthCode != null ? lstVesselCalls[0].ToPositionBerthCode : "NA") : "NA";
                arrivalnotificationVO.ETB = lstVesselCalls.Count > 0 ? (lstVesselCalls[0].ETB != null ? lstVesselCalls[0].ETB.ToString() : "NA") : "NA";
                arrivalnotificationVO.ETUB = lstVesselCalls.Count > 0 ? (lstVesselCalls[0].ETUB != null ? lstVesselCalls[0].ETUB.ToString() : "NA") : "NA";



                // -- end
                
               
                //Changed by Prasad coz getting error whnvr subcategory is null
                if (data.WorkflowInstance != null)
                {
                    if (data.WorkflowInstance.SubCategory != null)
                    {
                        if (data.WorkflowInstance.SubCategory.SubCatCode == "WFSA")
                        {
                            arrivalnotificationVO.WFStatus = "Approved";
                        }
                        else
                        {
                            arrivalnotificationVO.WFStatus = data.WorkflowInstance.SubCategory.SubCatName;
                        }
                        arrivalnotificationVO.WFCode = data.WorkflowInstance.SubCategory.SubCatCode;
                    }
                }

                List<string> TempArray = new List<string>();
                foreach (var item in data.ArrivalReasons)
                {
                    TempArray.Add(item.Reason);
                }
                arrivalnotificationVO.ArrivaReasonArray = TempArray;
            }
            return arrivalnotificationVO;
        }

        public static ArrivalNotificationVO MapToDto(this ArrivalNotification data, string userType)
        {
            ArrivalNotificationVO arrivalnotificationVO = new ArrivalNotificationVO();
            if (userType != null)
            {
            arrivalnotificationVO.VCN = data.VCN;
            arrivalnotificationVO.PortCode = data.PortCode;
            arrivalnotificationVO.AgentID = data.AgentID;
            arrivalnotificationVO.VesselID = data.VesselID;
            arrivalnotificationVO.VoyageIn = data.VoyageIn;
            arrivalnotificationVO.VoyageOut = data.VoyageOut;
            arrivalnotificationVO.ArrDraft = data.ArrDraft;
            arrivalnotificationVO.DepDraft = data.DepDraft;          

            arrivalnotificationVO.ETA = data.ETA.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
            arrivalnotificationVO.ETD = data.ETD.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
            arrivalnotificationVO.SpecifyReason = data.SpecifyReason;


            arrivalnotificationVO.IsTerminalOperator = data.IsTerminalOperator;
            arrivalnotificationVO.TerminalOperatorID = data.TerminalOperatorID;
            arrivalnotificationVO.LastPortOfCall = data.LastPortOfCall;
            arrivalnotificationVO.NextPortOfCall = data.NextPortOfCall;
            arrivalnotificationVO.NominationDate = data.NominationDate;
            arrivalnotificationVO.AppliedForISPS = data.AppliedForISPS;
            arrivalnotificationVO.AppliedDate = data.AppliedDate;
            arrivalnotificationVO.Clearance = data.Clearance;
            arrivalnotificationVO.ISPSReferenceNo = data.ISPSReferenceNo;
            arrivalnotificationVO.PilotExemption = data.PilotExemption;
            arrivalnotificationVO.ExemptionPilotID = data.PilotExemption == "A" ? data.ExemptionPilotID : null;
            arrivalnotificationVO.UserType = userType;
            arrivalnotificationVO.UserName = data.User != null ? data.User.FirstName + ' ' + data.User.LastName : "";
                arrivalnotificationVO.ContactNo = data.User != null ? data.User.ContactNo : "";
                arrivalnotificationVO.VesselType = data.Vessel.SubCategory3 == null
                    ? string.Empty
                    : data.Vessel.SubCategory3.SubCatName;
            arrivalnotificationVO.PilotExemptionChecked = data.PilotExemption == "A" ? true : false;            

            arrivalnotificationVO.DaylightSpecifyReason = data.DaylightSpecifyReason;
            arrivalnotificationVO.AnyDangerousGoodsonBoard = data.AnyDangerousGoodsonBoard;

                arrivalnotificationVO.PreferedBerthKey = data.PreferredPortCode + "." + data.PreferredQuayCode + "." +
                                                         data.PreferredBerthCode;
                arrivalnotificationVO.AlternateBerthKey = data.AlternatePortCode + "." + data.AlternateQuayCode + "." +
                                                          data.AlternateBerthCode;
                arrivalnotificationVO.DryDockBerthKey = data.DryDockBerthPortCode + "." + data.DryDockBerthQuayCode +
                                                        "." + data.DryDockBerthCode;

            arrivalnotificationVO.PreferredSideDock = data.PreferredSideDock;
            arrivalnotificationVO.PreferredSideAlternateBirth = data.PreferredSideAlternateBirth;
            arrivalnotificationVO.ReasonAlternateBirth = data.ReasonAlternateBirth;
            arrivalnotificationVO.Tidal = data.Tidal;
            arrivalnotificationVO.CellNo = data.CellNo;
            arrivalnotificationVO.UNNo = data.UNNo;
            arrivalnotificationVO.BallastWater = data.BallastWater;
            arrivalnotificationVO.WasteDeclaration = data.WasteDeclaration;
            arrivalnotificationVO.DaylightRestriction = data.DaylightRestriction;
            arrivalnotificationVO.ExceedPortLimitations = data.ExceedPortLimitations;
            arrivalnotificationVO.ExceedSpecifyReason = data.ExceedSpecifyReason;
            arrivalnotificationVO.AnyAdditionalInfo = data.AnyAdditionalInfo;
            arrivalnotificationVO.IMDGNetQty = data.IMDGNetQty;
            arrivalnotificationVO.CargoDescription = data.CargoDescription;
            arrivalnotificationVO.ReasonForLayup = data.ReasonForLayup;
            arrivalnotificationVO.BunkersRequired = data.BunkersRequired;
            arrivalnotificationVO.BunkersMethod = data.BunkersMethod;
            arrivalnotificationVO.BunkerService = data.BunkerService;
            arrivalnotificationVO.DistanceFromStern = data.DistanceFromStern;
            arrivalnotificationVO.TonsMT = data.TonsMT;
            arrivalnotificationVO.AnyImpInfo = data.AnyImpInfo;
            arrivalnotificationVO.PlannedDurationDate = data.PlannedDurationDate;
            arrivalnotificationVO.PlannedDurationToDate = data.PlannedDurationToDate;

            arrivalnotificationVO.ReasonForVisit = data.ReasonForVisit;

            arrivalnotificationVO.IsANFinal = data.IsANFinal;
            arrivalnotificationVO.IsISPSANFinal = data.IsISPSANFinal;
            arrivalnotificationVO.IsPHANFinal = data.IsPHANFinal;
            arrivalnotificationVO.IsIMDGANFinal = data.IsIMDGANFinal;

            arrivalnotificationVO.IsSpecialNature = data.IsSpecialNature;
            arrivalnotificationVO.SpecialNatureReason = data.SpecialNatureReason;
            arrivalnotificationVO.LastPortWasteDelivered = data.LastPortWasteDelivered;
            arrivalnotificationVO.NextPortWasteDelivery = data.NextPortWasteDelivery;
            arrivalnotificationVO.DateLastWasteDelivered = data.DateLastWasteDelivered;

            if (data.ArrivalAgents.Count > 0)
            {
                foreach (ArrivalAgent arag in data.ArrivalAgents)
                {
                    if (arag.IsPrimary == "Y")
                    {
                        arrivalnotificationVO.ArrivalCreatedAgent = arag.Agent.RegisteredName + " - " + arag.Agent.RegistrationNumber;
                    }
                    if (arag.IsPrimary == "F")
                    {
                        arrivalnotificationVO.SecondaryAgentID1 = arag.AgentID;
                        if (arag.Agent != null)
                        {
                                arrivalnotificationVO.SecondaryAgent1Name = arag.Agent.RegisteredName + " - " +
                                                                            arag.Agent.RegistrationNumber;
                        }
                    }
                    else if (arag.IsPrimary == "S")
                    {
                        arrivalnotificationVO.SecondaryAgentID2 = arag.AgentID;
                        if (arag.Agent != null)
                        {
                                arrivalnotificationVO.SecondaryAgent2Name = arag.Agent.RegisteredName + " - " +
                                                                            arag.Agent.RegistrationNumber;
                        }
                    }

                }
            }

            if (data.ArrivalReasons.Count > 0)
            {
                foreach (ArrivalReason ar in data.ArrivalReasons)
                {

                    if (arrivalnotificationVO.ReasonforvisitName == null)
                    {
                        if (ar.SubCategory.SubCatName != null)
                        {
                            arrivalnotificationVO.ReasonforvisitName = ar.SubCategory.SubCatName;
                        }

                    }
                    else
                    {
                        if (ar.SubCategory.SubCatName != null)
                        {
                                arrivalnotificationVO.ReasonforvisitName = arrivalnotificationVO.ReasonforvisitName +
                                                                           ',' + ar.SubCategory.SubCatName;
                        }
                    }
                }
            }
            
            DateTime? a = data.PlannedDurationToDate;
            DateTime? b = data.PlannedDurationDate;
            string Daycnt = string.Empty;

            if (a.HasValue && b.HasValue)
                Daycnt = Convert.ToString((a.Value - b.Value).Days, CultureInfo.InvariantCulture);

            arrivalnotificationVO.Daycnt = Daycnt;
            arrivalnotificationVO.LoadDischargeDate = data.LoadDischargeDate;
            arrivalnotificationVO.DischargeDate = data.DischargeDate;
            arrivalnotificationVO.PlanDateTimeOfBerth = data.PlanDateTimeOfBerth;
            arrivalnotificationVO.PlanDateTimeToVacateBerth = data.PlanDateTimeToVacateBerth;
            arrivalnotificationVO.PlanDateTimeToStartCargo = data.PlanDateTimeToStartCargo;
            arrivalnotificationVO.PlanDateTimeToCompleteCargo = data.PlanDateTimeToCompleteCargo;
            arrivalnotificationVO.DangerousGoodsClass = data.DangerousGoodsClass;
            arrivalnotificationVO.RecordStatus = data.RecordStatus;
            arrivalnotificationVO.CreatedBy = data.CreatedBy;
            arrivalnotificationVO.CreatedDate = data.CreatedDate;
            arrivalnotificationVO.WorkflowInstanceId = data.WorkflowInstanceId;
            arrivalnotificationVO.ModifiedBy = data.ModifiedBy;
            arrivalnotificationVO.ModifiedDate = data.ModifiedDate;

            if (data.IMDGInformations != null)
            {
                arrivalnotificationVO.IMDGInformations = data.IMDGInformations.MapToDto();
            }

            if (data.WasteDeclarations != null)
            {
                arrivalnotificationVO.WasteDeclarations = data.WasteDeclarations.MapToDto();
            }

            if (data.Vessel != null)
            {
                arrivalnotificationVO.Vessel = data.Vessel.MapToDtoWithDrydockDtls(arrivalnotificationVO.PortCode);
            }

            if (data.TerminalOperator != null)
            {
                arrivalnotificationVO.TerminalOperatorID = data.TerminalOperator.TerminalOperatorID;
                arrivalnotificationVO.RegisteredName = data.TerminalOperator.RegisteredName;
            }



            if (data.ArrivalCommodities != null)
            {
                arrivalnotificationVO.ArrivalCommodities = data.ArrivalCommodities.MapToDto();
            }
            if (data.SubCategory3 != null)
            {
                arrivalnotificationVO.SubCategory = data.SubCategory3.MapToDto();
            }
            if (data.ArrivalIMDGTankers != null)
            {
                arrivalnotificationVO.ArrivalIMDGTankers = data.ArrivalIMDGTankers.MapToDto();
            }
            if (data.ArrivalDocuments != null)
            {
                arrivalnotificationVO.ArrivalDocuments = data.ArrivalDocuments.MapToDto();
            }

            arrivalnotificationVO.IsSamsaArrested = false;
            if (data.VesselArrestImmobilizationSAMSAs != null)
            {
                foreach (var item in data.VesselArrestImmobilizationSAMSAs)
                {
                    if (item.VesselArrested == "Y" && item.VesselReleased == "N")
                    {
                        arrivalnotificationVO.IsSamsaArrested = true;
                    }
                }
            }

            arrivalnotificationVO.VCN_VesselName = data.VCN + "_" + data.Vessel.VesselName;
           
            //Changed by Prasad coz getting error whnvr subcategory is null

            if (data.WorkflowInstance != null)
            {
                if (data.WorkflowInstance.SubCategory != null)
                {
                    if (data.WorkflowInstance.SubCategory.SubCatCode == "WFSA")
                    {
                        arrivalnotificationVO.WFStatus = "Approved";
                    }
                    else
                    {
                        arrivalnotificationVO.WFStatus = data.WorkflowInstance.SubCategory.SubCatName;
                    }
                    arrivalnotificationVO.WFCode = data.WorkflowInstance.SubCategory.SubCatCode;
                }
            }

            List<string> TempArray = new List<string>();
            foreach (var item in data.ArrivalReasons)
            {
                TempArray.Add(item.Reason);
            }
            arrivalnotificationVO.ArrivaReasonArray = TempArray;
            }

            return arrivalnotificationVO;
        }

        /// <summary>
        /// Data Transfer from DTO to Entity 
        /// </summary>
        /// <param name="Vo"></param>
        /// <returns></returns>
        public static ArrivalNotification MapToEntity(this ArrivalNotificationVO Vo)
        {

            ArrivalNotification arrivalNotification = new ArrivalNotification();
            if (Vo != null)
            {

            arrivalNotification.VCN = Vo.VCN;
            arrivalNotification.PortCode = Vo.PortCode;
            arrivalNotification.AgentID = Vo.AgentID;
            arrivalNotification.VesselID = Vo.VesselID;
            arrivalNotification.VoyageIn = Vo.VoyageIn;
            arrivalNotification.VoyageOut = Vo.VoyageOut;
            arrivalNotification.ArrDraft = Vo.ArrDraft;
            arrivalNotification.DepDraft = Vo.DepDraft;            

            arrivalNotification.ETA = DateTime.Parse(Vo.ETA, CultureInfo.InvariantCulture);
            arrivalNotification.ETD = DateTime.Parse(Vo.ETD, CultureInfo.InvariantCulture);
            arrivalNotification.ReasonForVisit = Vo.ReasonForVisit;
            arrivalNotification.IsTerminalOperator = Vo.IsTerminalOperator;
            arrivalNotification.TerminalOperatorID = Vo.TerminalOperatorID;
            arrivalNotification.LastPortOfCall = Vo.LastPortOfCall;
            arrivalNotification.NextPortOfCall = Vo.NextPortOfCall;
            arrivalNotification.NominationDate = Vo.NominationDate;
            arrivalNotification.AppliedForISPS = Vo.AppliedForISPS;
            arrivalNotification.AppliedDate = Vo.AppliedDate;
            arrivalNotification.Clearance = Vo.Clearance;
            arrivalNotification.ISPSReferenceNo = Vo.ISPSReferenceNo;
            arrivalNotification.PilotExemption = Vo.PilotExemptionChecked == true ? "A" : "I";
            arrivalNotification.ExemptionPilotID = Vo.ExemptionPilotID;
            arrivalNotification.DaylightSpecifyReason = Vo.DaylightSpecifyReason;
            arrivalNotification.SpecialNatureReason = Vo.SpecialNatureReason;
            arrivalNotification.SpecifyReason = Vo.SpecifyReason;


            arrivalNotification.AnyDangerousGoodsonBoard = Vo.AnyDangerousGoodsonBoard;
            arrivalNotification.PreferredSideDock = Vo.PreferredSideDock;
            arrivalNotification.PreferredSideAlternateBirth = Vo.PreferredSideAlternateBirth;
            arrivalNotification.ReasonAlternateBirth = Vo.ReasonAlternateBirth;
            arrivalNotification.Tidal = Vo.Tidal;
            arrivalNotification.CellNo = Vo.CellNo;
            arrivalNotification.UNNo = Vo.UNNo;
            arrivalNotification.BallastWater = Vo.BallastWater;
            arrivalNotification.WasteDeclaration = Vo.WasteDeclaration;
            arrivalNotification.DaylightRestriction = Vo.DaylightRestriction;
            arrivalNotification.IsSpecialNature = Vo.IsSpecialNature;
            arrivalNotification.ExceedPortLimitations = Vo.ExceedPortLimitations;
            arrivalNotification.ExceedSpecifyReason = Vo.ExceedSpecifyReason;
            arrivalNotification.AnyAdditionalInfo = Vo.AnyAdditionalInfo;
            arrivalNotification.IMDGNetQty = Vo.IMDGNetQty;
            arrivalNotification.CargoDescription = Vo.CargoDescription;
            arrivalNotification.ReasonForLayup = Vo.ReasonForLayup;
            arrivalNotification.BunkersRequired = Vo.BunkersRequired;
            arrivalNotification.BunkersMethod = Vo.BunkersMethod;
            arrivalNotification.BunkerService = Vo.BunkerService;
            arrivalNotification.DistanceFromStern = Vo.DistanceFromStern;
            arrivalNotification.TonsMT = Vo.TonsMT;
            arrivalNotification.AnyImpInfo = Vo.AnyImpInfo;
            arrivalNotification.PlannedDurationDate = Vo.PlannedDurationDate;
            arrivalNotification.PlannedDurationToDate = Vo.PlannedDurationToDate;
            arrivalNotification.LoadDischargeDate = Vo.LoadDischargeDate;
            arrivalNotification.DischargeDate = Vo.DischargeDate;
            arrivalNotification.PlanDateTimeOfBerth = Vo.PlanDateTimeOfBerth;
            arrivalNotification.PlanDateTimeToVacateBerth = Vo.PlanDateTimeToVacateBerth;
            arrivalNotification.PlanDateTimeToStartCargo = Vo.PlanDateTimeToStartCargo;
            arrivalNotification.PlanDateTimeToCompleteCargo = Vo.PlanDateTimeToCompleteCargo;
            arrivalNotification.DangerousGoodsClass = Vo.DangerousGoodsClass;
            arrivalNotification.RecordStatus = Vo.RecordStatus;
            arrivalNotification.CreatedBy = Vo.CreatedBy;
            arrivalNotification.CreatedDate = Vo.CreatedDate;
            arrivalNotification.ModifiedBy = Vo.ModifiedBy;
            arrivalNotification.ModifiedDate = Vo.ModifiedDate;
            arrivalNotification.WorkflowInstanceId = Vo.WorkflowInstanceId;

            arrivalNotification.IsANFinal = Vo.IsANFinal;
            arrivalNotification.IsISPSANFinal = Vo.IsISPSANFinal;
            arrivalNotification.IsPHANFinal = Vo.IsPHANFinal;
            arrivalNotification.IsIMDGANFinal = Vo.IsIMDGANFinal;
            arrivalNotification.CancelRemarks = Vo.CancelRemarks;

            arrivalNotification.LastPortWasteDelivered = Vo.LastPortWasteDelivered;
            arrivalNotification.NextPortWasteDelivery = Vo.NextPortWasteDelivery;
            arrivalNotification.DateLastWasteDelivered = Vo.DateLastWasteDelivered;

            if (!string.IsNullOrEmpty(Vo.DryDockBerthKey))
            {
                string[] fieldsD = Vo.DryDockBerthKey.Split('.');
                string portCodeD = fieldsD[0];
                string quayCodeD = fieldsD[1];
                string berthCodeD = fieldsD[2];
                arrivalNotification.DryDockBerthPortCode = portCodeD;
                arrivalNotification.DryDockBerthQuayCode = quayCodeD;
                arrivalNotification.DryDockBerthCode = berthCodeD;
            }


            if (!string.IsNullOrEmpty(Vo.AlternateBerthKey))
            {
                string[] fields = Vo.AlternateBerthKey.Split('.');
                string portCode = fields[0];
                string quayCode = fields[1];
                string berthCode = fields[2];
                arrivalNotification.AlternatePortCode = portCode;
                arrivalNotification.AlternateQuayCode = quayCode;
                arrivalNotification.AlternateBerthCode = berthCode;
            }

            string[] Prefields = Vo.PreferedBerthKey.Split('.');
            string PreportCode = Prefields[0];
            string PrequayCode = Prefields[1];
            string PreberthCode = Prefields[2];
            arrivalNotification.PreferredPortCode = PreportCode;
            arrivalNotification.PreferredQuayCode = PrequayCode;
            arrivalNotification.PreferredBerthCode = PreberthCode;


            if (Vo.SubCategory != null)
            {
                arrivalNotification.SubCategory3 = Vo.SubCategory.MapToEntity();
            }
            if (Vo.IMDGInformations != null)
            {
                arrivalNotification.IMDGInformations = Vo.IMDGInformations.MapToEntity();
            }
            arrivalNotification.ArrivalCommodities = Vo.ArrivalCommodities.MapToEntity();
            arrivalNotification.ArrivalIMDGTankers = Vo.ArrivalIMDGTankers.MapToEntity();
            arrivalNotification.ArrivalDocuments = Vo.ArrivalDocuments.MapToEntity();

            arrivalNotification.ArrivalReasons = Vo.ArrivaReasonArray.MapToEntityArray();
            
            if (Vo.WasteDeclarations != null)
            {
                arrivalNotification.WasteDeclarations = Vo.WasteDeclarations.MapToEntity();
            }

            }
            return arrivalNotification;
        }
        //By mahesh: for service request
        public static ArrivalNotificationVO ServicerequestMapToDTO(this ArrivalNotification data)
        {
            ArrivalNotificationVO arrivalnotificationVO = new ArrivalNotificationVO();
            if (data != null)
            {
            arrivalnotificationVO.VCN = data.VCN;
            arrivalnotificationVO.PortCode = data.PortCode;
            arrivalnotificationVO.AgentID = data.AgentID;
            arrivalnotificationVO.VesselID = data.VesselID;
            arrivalnotificationVO.VoyageIn = data.VoyageIn;
            arrivalnotificationVO.VoyageOut = data.VoyageOut;
            arrivalnotificationVO.ArrDraft = data.ArrDraft;
            arrivalnotificationVO.DepDraft = data.DepDraft;           

            arrivalnotificationVO.IsTerminalOperator = data.IsTerminalOperator;
            arrivalnotificationVO.TerminalOperatorID = data.TerminalOperatorID;
            arrivalnotificationVO.LastPortOfCall = data.LastPortOfCall;
            arrivalnotificationVO.NextPortOfCall = data.NextPortOfCall;
            arrivalnotificationVO.NominationDate = data.NominationDate;
            arrivalnotificationVO.AppliedForISPS = data.AppliedForISPS;
            arrivalnotificationVO.AppliedDate = data.AppliedDate;
            arrivalnotificationVO.Clearance = data.Clearance;
            arrivalnotificationVO.ISPSReferenceNo = data.ISPSReferenceNo;
            arrivalnotificationVO.PilotExemption = data.PilotExemption;
            arrivalnotificationVO.ExemptionPilotID = data.ExemptionPilotID;
            if (data.Vessel != null)
            {
                if (data.Vessel.SubCategory3 != null)
                {
                        arrivalnotificationVO.VesselType = data.Vessel.SubCategory3 == null
                            ? string.Empty
                            : data.Vessel.SubCategory3.SubCatName;
                }
            }

            arrivalnotificationVO.DaylightSpecifyReason = data.DaylightSpecifyReason;
            arrivalnotificationVO.AnyDangerousGoodsonBoard = data.AnyDangerousGoodsonBoard;


            arrivalnotificationVO.PreferredSideDock = data.PreferredSideDock;
            arrivalnotificationVO.PreferredSideAlternateBirth = data.PreferredSideAlternateBirth;
            arrivalnotificationVO.ReasonAlternateBirth = data.ReasonAlternateBirth;
            arrivalnotificationVO.Tidal = data.Tidal;
            arrivalnotificationVO.IsSpecialNature = data.IsSpecialNature;
            arrivalnotificationVO.CellNo = data.CellNo;
            arrivalnotificationVO.UNNo = data.UNNo;
            arrivalnotificationVO.BallastWater = data.BallastWater;
            arrivalnotificationVO.WasteDeclaration = data.WasteDeclaration;
            arrivalnotificationVO.DaylightRestriction = data.DaylightRestriction;
            arrivalnotificationVO.ExceedPortLimitations = data.ExceedPortLimitations;
            arrivalnotificationVO.ExceedSpecifyReason = data.ExceedSpecifyReason;
            arrivalnotificationVO.AnyAdditionalInfo = data.AnyAdditionalInfo;
            arrivalnotificationVO.IMDGNetQty = data.IMDGNetQty;
            arrivalnotificationVO.CargoDescription = data.CargoDescription;
            arrivalnotificationVO.ReasonForLayup = data.ReasonForLayup;
            arrivalnotificationVO.BunkersRequired = data.BunkersRequired;
            arrivalnotificationVO.BunkersMethod = data.BunkersMethod;
            arrivalnotificationVO.BunkerService = data.BunkerService;
            arrivalnotificationVO.DistanceFromStern = data.DistanceFromStern;
            arrivalnotificationVO.TonsMT = data.TonsMT;
            arrivalnotificationVO.AnyImpInfo = data.AnyImpInfo;
            arrivalnotificationVO.PlannedDurationDate = data.PlannedDurationDate;
            arrivalnotificationVO.PlannedDurationToDate = data.PlannedDurationToDate;

            arrivalnotificationVO.ReasonForVisit = data.ReasonForVisit;

            arrivalnotificationVO.IsANFinal = data.IsANFinal;
            arrivalnotificationVO.IsISPSANFinal = data.IsISPSANFinal;
            arrivalnotificationVO.IsPHANFinal = data.IsPHANFinal;
            arrivalnotificationVO.IsIMDGANFinal = data.IsIMDGANFinal;

            arrivalnotificationVO.LastPortWasteDelivered = data.LastPortWasteDelivered;
            arrivalnotificationVO.NextPortWasteDelivery = data.NextPortWasteDelivery;
            arrivalnotificationVO.DateLastWasteDelivered = data.DateLastWasteDelivered;



            if (data.SubCategory3 != null)
            {
                arrivalnotificationVO.ReasonforvisitName = data.SubCategory3.SubCatName;
            }           

            if (data.Vessel != null)
            {
                arrivalnotificationVO.Vessel = data.Vessel.MapToDto();
            }
            if (data.SubCategory3 != null)
            {
                arrivalnotificationVO.SubCategory = data.SubCategory3.MapToDto();
            }

            List<string> TempArray = new List<string>();
            foreach (var item in data.ArrivalReasons)
            {
                TempArray.Add(item.Reason);
            }
            arrivalnotificationVO.ArrivaReasonArray = TempArray;
            }
            return arrivalnotificationVO;
        }
    }
}
