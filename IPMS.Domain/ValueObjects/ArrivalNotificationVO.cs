using Core.Repository.Providers.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace IPMS.Domain.ValueObjects
{
    /// <summary>
    /// Data Transfer Object for Arrival Notification
    /// </summary>
    public class ArrivalNotificationVO
    {

        public AddressVO PostalAddress { get; set; }
        public SubCategoryVO SubCategory { get; set; }
        public string VCN { get; set; }
        public List<ArrivalCommodityVo> ArrivalCommodities { get; set; }
        public List<ArrivalIMDGTankerVo> ArrivalIMDGTankers { get; set; }
        public List<ArrivalDocumentVo> ArrivalDocuments { get; set; }
        public List<ArrivalReasonVO> ArrivalReasons { get; set; }
        public List<IMDGInformationVO> IMDGInformations { get; set; }        
        public VesselVO Vessel { get; set; }
        public List<ArrivalAgentVO> ArrivalAgents { get; set; }
        public List<VesselCallVO> VesselCalls { get; set; }
        public string PortCode { get; set; }
        public string VoyageIn { get; set; }
        public string VoyageOut { get; set; }
        public string ETA { get; set; }
        public string ETD { get; set; }
        public string ArrDraft { get; set; }
        public string DepDraft { get; set; }      
        public string ReasonForVisit { get; set; }
        public string IsTerminalOperator { get; set; }
        public Nullable<int> TerminalOperatorID { get; set; }
        public string LastPortOfCall { get; set; }
        public string NextPortOfCall { get; set; }
        public Nullable<System.DateTime> NominationDate { get; set; }
        public string AppliedForISPS { get; set; }
        public Nullable<System.DateTime> AppliedDate { get; set; } // ISPSAppliedDate
        public string Clearance { get; set; }  // ISPSClearance
        public string ISPSReferenceNo { get; set; }
        public string PilotExemption { get; set; }
        public Nullable<int> ExemptionPilotID { get; set; }
        public string PreferredBerth { get; set; }
        public string AlternativeBerth { get; set; }
        public string PreferredPortCode { get; set; }
        public string PreferredQuayCode { get; set; }
        public string PreferredBerthCode { get; set; }
        public string AlternatePortCode { get; set; }
        public string AlternateQuayCode { get; set; }
        public string AlternateBerthCode { get; set; }
        public string PreferredSideDock { get; set; }
        public string PreferredSideAlternateBirth { get; set; }
        public string ReasonAlternateBirth { get; set; }
        public string Tidal { get; set; }
        public string BallastWater { get; set; }
        public string WasteDeclaration { get; set; }
        public string DaylightRestriction { get; set; }
        public string DaylightSpecifyReason { get; set; } //
        public string ExceedSpecifyReason { get; set; }
        public string ExceedPortLimitations { get; set; }
        public string AnyAdditionalInfo { get; set; }
        public Nullable<System.DateTime> PlanDateTimeOfBerth { get; set; }
        public Nullable<System.DateTime> PlanDateTimeToVacateBerth { get; set; }
        public Nullable<System.DateTime> PlanDateTimeToStartCargo { get; set; }
        public Nullable<System.DateTime> PlanDateTimeToCompleteCargo { get; set; }
        public string AnyDangerousGoodsonBoard { get; set; }
        public string DangerousGoodsClass { get; set; }
        public string UNNo { get; set; }
        public Nullable<System.DateTime> LoadDischargeDate { get; set; }
        public Nullable<System.DateTime> DischargeDate { get; set; }


        public Nullable<decimal> IMDGNetQty { get; set; }
        public string CellNo { get; set; }
        public Nullable<System.DateTime> PlannedDurationDate { get; set; }
        public Nullable<System.DateTime> PlannedDurationToDate { get; set; }
        public string ReasonForLayup { get; set; }
        public string BunkersRequired { get; set; }
        public string BunkersMethod { get; set; }
        public Nullable<int> BunkerService { get; set; }
        public Nullable<decimal> DistanceFromStern { get; set; }
        public Nullable<decimal> TonsMT { get; set; }
        public string AnyImpInfo { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public Nullable<int> WorkflowInstanceId { get; set; }

        public string PreferedBerthKey { get; set; }
        public string DraftKey { get; set; }
        public string DryDockBerthKey { get; set; }
        public string AlternateBerthKey { get; set; }
        public string VCN_VesselName { get; set; }
        public string WFStatus { get; set; }
        public string WFCode { get; set; }
        public string SpecifyReason { get; set; }

        public int SecondaryAgentID1 { get; set; }
        public int SecondaryAgentID2 { get; set; }

        public string SecondaryAgent1Name { get; set; }
        public string SecondaryAgent2Name { get; set; }
        public string CancelRemarks { get; set; }

        
        public int AgentID { get; set; }
        public int VesselID { get; set; }
        public string CargoDescription { get; set; }
        public string Daycnt { get; set; }
        public string Isdraft { get; set; }


        public string ReasonforvisitName { get; set; }
        public string VesselType { get; set; }


        public string IsANFinal { get; set; }
        public string IsISPSANFinal { get; set; }
        public string IsPHANFinal { get; set; }
        public string IsIMDGANFinal { get; set; }
        public string UserType { get; set; }
        public string IsSpecialNature { get; set; }
        public string SpecialNatureReason { get; set; }
        public bool PilotExemptionChecked { get; set; }
        public string TerminalOperatorid { get; set; }
        public string RegisteredName { get; set; }

        
        public string LastPortWasteDelivered { get; set; }        
        public string NextPortWasteDelivery { get; set; }        
        public Nullable<System.DateTime> DateLastWasteDelivered { get; set; }


        public string UserName { get; set; }
        public string ContactNo { get; set; }

        public string workflowRemarks { get; set; }
        public bool IsSamsaArrested { get; set; }    

        // -- Added by sandeep on 25-12-2014
        public string CurrentBerth { get; set; }
        public string ETB { get; set; }
        public string ETUB { get; set; }


        public System.DateTime ETADate { get; set; }
        public System.DateTime ETDDate { get; set; }

        public Nullable<decimal> GRT { get; set; }


        // -- end
        
        //-- Added by sandeep on 15-09-2015
        public string ArrivalCreatedAgent { get; set; }
        //-- end

        //mahesh: workfllow flag.
        public string WokflowFlag { get; set; }
        public List<string> ArrivaReasonArray { get; set; }
        public SAPArrivalVO SAPArrival { get; set; }
        public List<WasteDeclarationVO> WasteDeclarations { get; set; }


        [NotMapped]
        [StoredProcedure("usp_ArrivalNotificationCancel")]
        public class ArrivalNotificationCancel_proc
        {
            private ArrivalNotificationVO _objArrivalNotificationVO;
            public ArrivalNotificationCancel_proc(ArrivalNotificationVO p_ArrivalNotificationVO)
            {
                _objArrivalNotificationVO = p_ArrivalNotificationVO;
            }

            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string p_VCN
            {
                get
                {
                    return _objArrivalNotificationVO.VCN;
                }
            }

            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string p_Remarks
            {
                get
                {
                    return _objArrivalNotificationVO.workflowRemarks;
                }
            }

            [StoredProcedureParameter(SqlDbType.Int, Direction = ParameterDirection.Input)]
            public int p_UserId
            {
                get
                {
                    return _objArrivalNotificationVO.CreatedBy;
                }
            }

        }

    }

    public class ArrivalNotificationDraftVO
    {
        public string VCN { get; set; }
        public string VCNdraftDisplay { get; set; }
    }

    public class ArrivalNotificationGridVO
    {
        public string VCN { get; set; }
        public string VesselName { get; set; }
        public string VesselType { get; set; }
        public string ReasonforvisitName { get; set; }
        public Nullable<System.DateTime> ETA { get; set; }
        public Nullable<System.DateTime> ETD { get; set; }
        public Nullable<System.DateTime> NominationDate { get; set; }
        public string wfStatus { get; set; }
        public string isEditVisible { get; set; }
        public string isViewVisible { get; set; }


        public string ReasonForVisit { get; set; }

        public string IsANFinal { get; set; }
        public string IsPHANFinal { get; set; }
        public string IsIMDGANFinal { get; set; }
        public string IsISPSANFinal { get; set; }
        public string AnyDangerousGoodsonBoard { get; set; }

        public string IsSamsaArrested { get; set; }

        public string IsArrivaStatus { get; set; }
        public string IsPHANStatus { get; set; }
        public string IsISPSANStatus { get; set; }
        public string IsIMDGANStatus { get; set; }
        public string RecordStatus { get; set; }


        public string ArrvwfRemarks { get; set; }
        public string PHOwfRemarks { get; set; }
        public string ISPSwfRemarks { get; set; }
        public string IMDGwfRemarks { get; set; }
        public string ArvStatus { get; set; }
        public string IsPrimary { get; set; }
        public string CancelRemarks { get; set; }


    }

    public class ArrvWorkflowStatusVo
    {
        public string WEntityCode { get; set; }
        public string WEntityStatus { get; set; }
    }

    public class ArrvNotfMobileAppVo
    {

        public string VCN { get; set; }
        public string VesselName { get; set; }
        public string AgencyName { get; set; }
        public System.DateTime ETA { get; set; }
        public System.DateTime ETD { get; set; }
        public Nullable<System.DateTime> ATA { get; set; }
        public Nullable<System.DateTime> ATD { get; set; }
        public string Status { get; set; }

        public string BerthPlannerStatus { get; set; }
        public string PHOStatus { get; set; }
        public string ISPSStatus { get; set; }
        public string IMDGStatus { get; set; }
        public string DHMStatus { get; set; }

        //For Future use
        public Nullable<System.DateTime> Prop1 { get; set; }
        public int Prop2 { get; set; }
        public string Prop3 { get; set; }
        public string Prop4 { get; set; }
        public System.DateTime Prop5 { get; set; }
        public string Prop6 { get; set; }
        public string Prop7 { get; set; }
    }

    public class ArrvNotForSuppServiceViewVO
    {

        public string VCN { get; set; }
        public string VesselName { get; set; }       
        public System.DateTime ETA { get; set; }
        public System.DateTime ETD { get; set; }
        public string VoyageIn { get; set; }
        public string VoyageOut { get; set; }       
        public Nullable<decimal> BeamInM { get; set; }       
        public Nullable<decimal> GrossRegisteredTonnageInMT { get; set; }        
        public Nullable<decimal> LengthOverallInM { get; set; }
        public string IMONo { get; set; }
        public Nullable<decimal> DeadWeightTonnageInMT { get; set; }
        public string Tidal { get; set; }
        public string ArrDraft { get; set; }       
        public string ReasonForVisit { get; set; }       
        public string LastPortOfCall { get; set; }
        public string NextPortOfCall { get; set; }
        public string VesselNationality { get; set; }
        public string DaylightRestriction { get; set; }
        public string CargoType { get; set; }
        public string Package { get; set; }
        public string UOM { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public string CallSign { get; set; }
        public List<ArrivalCommodityVo> ArrivalCommodities { get; set; }       
        public List<ArrivalReasonVO> ArrivalReasons { get; set; }
        public string AnyDangerousGoodsonBoard { get; set; }
        

        
    }

}
