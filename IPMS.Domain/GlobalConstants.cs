using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain
{
    public static class GlobalConstants
    {
        public const string SupCat_BerthType = "BETY";
        public const string AGENT = "AGNT";
        public const string DateFormat = "dd-MM-yyyy";
        public const string IPMSDateFormat = "yyyy-MM-dd";
        public const string IPMSTimeFormat = "HH:mm";

        public const string DateTimeFormatforToken = "M/d/yyyy h:m:s tt";
        public const string DateTimeFormatWith24Hour = "yyyy-MM-dd HH:mm";
        public const string IPMSDateTimeFormat = "yyyy/MM/dd HH:mm";

        
    }

    public static class SuperCategoryConstants
    {

        public const string BUNKERREQUIREDDTLS = "BNRQ";
        public const string BUNKERMETHODDTLS = "BNMT";
        public const string CARGOUOMS = "CUOM";

        public const string REASON_FOR_DELAY = "RFDL";
        public const string TPT_DOCUMENT_UPLOADS = "TPTD";

        public const string WASTEDISPOSAL = "WST";
        public const string BUNKERINGLICENE = "BNK";
        public const string WRKFLOWAPPROVALS = "WFSA";
        public const string USER_TYPE = "USTY";
        public const string DOCK_TYPE = "DOCK";
        public const string CARGO_TYPE = "CRTY";
        public const string PURPOSE_TYPE = "PURP";
        public const string UOM_TYPE = "UOM";
        public const string TPTUOM_TYPE = "TUOM";
        public const string REASON_FOR_VISIT_TYPE = "RSV";
        public const string DANGEROUSGOODS_TYPE = "DGDS";
        public const string DOCUMENT_CATEGORY = "DOCT";
        public const string MOMENT_TYPE = "MVTY";
        public const string TANKER_TYPE = "";
        public const string BUNKERSERVICE_TYPE = "BSP";
        public const string SIDE_ALONG_SIDE_TYPE = "SASD";
        public const string WORK_FLOW_STATUS = "WFST";
        public const string COMMODITY_TYPE = "CMDT";
        public const string TO_LICENSED_STATUS = "TOLF";
        public const string LICENSEREQUEST_DOCTYPE = "LDCT";
        public const string PILOTEXEMPTION_DOCTYPE = "PDCT";
        //CRAFTMASTER
        public const string CRAFT_TYPE = "CRFY";
        public const string CRAFT_NATIONALITY = "CRNT";
        public const string CLASSIFICATION_SOCIETY = "CLSF";
        public const string FUEL_TYPE = "FUEL";
        public const string PORT_OF_REGISTRY = "POR";
        public const string ENGINE_TYPE = "ENG";
        public const string PROPULSION_TYPE = "PROP";
        public const string COMMISSION_STATUS = "COMM";
        public const string PRIVILEGE_TYPE = "PRIV";
        public const string LICENSING_REQUEST_TYPE = "LIC";
        public const string NATURE_INCIDENT_TYPE = "NOIN";
        public const string MAINTENANCE_TYPE = "MNTP";
        public const string PILOT_NATIONALITY = "VENA";
        public const string PILOT_ROLECODE = "PIRC";
        public const string REASON_TYPE = "RESN";
        //CRAFT REMAINDER CONFIG 
        public const string CRAFTCONFIG_PART_TYPE = "PART";
        public const string CRAFTCONFIG_CALD_TYPE = "CALD";

        // Vessel Type
        public const string VESSEL_TYPE = "VETY";
        // Movement Status
        public const string MOVEMENT_STATUS = "MVST";
        //CHANGE OF AGENT
        public const string AGENTCHANGE_RSTRCODE = "RSTR";
        //VESSEL REGISTRATION
        public const string VESSELREG_CLSSOCITYCODE = "CLSO";
        public const string VESSELREG_VESSELTYPECODE = "VETY";
        public const string VESSELREG_VESSELNATCODE = "VENA";
        public const string VESSELREG_VESSELCERTCODE = "VCER";
        public const string VESSELREG_VESSELSWLCODE = "SWL";
        //Resource group
        public const string RESGRP_DESIGANTIONCODE = "DESG";
        // Statement Of Fact
        public const string OPERATION_TYPE = "OPER";
        public const string KEYEVENT_TYPE = "KEVT";
        //Pilotcapacity
        public const string PILOT_CAPACITY = "CAPA";
        public const string OIL_TYPE = "OLTY";
        public const string GRADE_TYPE = "GRDE";
        public const string SUPPLYING_MODE = "SPMD";

        public const string NATURE_TYPE = "24HR";
        public const string RECORDING_OF_INCIDENT = "6CRE";
        public const string GENERAL_AGENCIES = "6CGE";
        public const string OCCUPATIONAL_HYGIENE_AGENCIES = "6COC";
        public const string TYPE_OF_CONTACT = "6CTC";
        public const string STANDARD_ACTS = "6CAC";
        public const string SUBSTANDARD_CONDITIONS = "6CSC";
        public const string PERSONAL_FACTORS = "6CPF";
        public const string JOB_FACTORS = "6CJF";
        public const string CONTROL_STEPS_TO_PREVENT_RECURRENCE = "6CPR";
        public const string FIRE_DEPARTMENT = "6DFD";
        public const string INCIDENT_CLASSIFICATION = "6DIC";
        public const string DISCRIPTION_OF_EXPOSED_RISK = "6DER";
        public const string SECTION_RECORD_INCIDENT = "6GRI";
        public const string FINANCIAL_YEAR = "FIYR";
        public const string DEPLOYMENT_TYPE = "DGTP";
        public const string DREDGING_COLOR = "DGCL";
        public const string DREDGING_TYPE = "DRDG";
        public const string DEPARTMENT_TYPE = "DEPT";
        public const string WEATHER_TYPE = "WTTP";


        public const string PERMITDOCUMENT_CATEGORY = "SECR";
        public const string SECURITYDOCUMENT_CATEGORY = "SSPA";
        public const string PERMIT_TYPE = "PERT";
        public const string APPLICATION_CATEGORY = "APCT";
        public const string OPERATED_TERMINALS = "OPRT";
        public const string ADHOC_PERMITS = "ADPR";
        public const string TEMPORARY_PERMITS = "TEMP";
        public const string AREA_OF_CONSTRUCTION = "AOCN";

        public const string PERMIT_REQUEIREMENTS = "PERQ";
        public const string ACCESS_GATES = "PAGT";
        public const string REQUEIREMENT_TYPES = "PRQT";
        public const string REQUEIREMENT_DURATION = "PRTD";
        public const string PERMIT_CODES = "PERC";
        public const string PERMIT_STATUS = "PRST";

        public const string PHASE_TYPE = "PHSE";

        public const string RFV_REPAIR = "REPA";

        // Service request warp sides
        public const string WARP_SIDE = "WARP";


        public const string VACHREQ_DOCTYPE = "VADY";
        public const string REVENUE_ACCOUNT_STATUS = "REVA";

        public const string FUEL_UOM = "FUOM";

        //-- Added by sandeep on 11-03-2015
        public const string Diving_Request_Reason = "DRR";
        //-- end

        public const string Reason_DryDock = "DRYD";

        public const string Reason_Bunkering = "BUNK";


        public const string DELAY_TYPE = "TYDL";

        public const string SERV_DOCTYP = "SRDT";

        public const string MOPS_DELAY = "MOPS";

        public const string MARPOL_TYPE = "MRPL";

        public const string DELAY_REASONS = "DLRS";

        public const string TEMPORARY_PERMITS_IND = "TEMI";

        public const string ACCESS_AREAS_RB = "ARRB";
        public const string ACCESS_AREAS_CT = "ARCT";
        public const string ACCESS_AREAS_DB = "ARDB";
        public const string ACCESS_AREAS_EL = "AREL";
        public const string ACCESS_AREAS_NG = "ARNG";
        public const string ACCESS_AREAS_MB = "ARMB";
        public const string ACCESS_AREAS_SB = "SASB";
        public const string ACCESS_AREAS_PE = "ARPE";

        public const string CONTRACTOR_TEMPORARY_PERMITS = "CRAP";
        public const string CONTRACTOR_PERMANANT_PERMITS = "CRPR";
        public const string PERMANENT_PERMITS_IND = "TEPI";
        public const string REASON_PERMIT = "RSPR";
        public const string CONTRACTOR_REASON_PERMIT = "RSCR";

        
        
    }


    public static class MovementTypes
    {
        //SERVICE REQUEST MOVEMENTTYPES
        public const string ARRIVAL = "ARMV";
        public const string SHIFTING = "SHMV";
        public const string WARPING = "WRMV";
        public const string SAILING = "SGMV";
        public const string Pilotage = "PILT";
    }

    public static class MovementStatus
    {
        //SERVICE REQUEST MOVEMENT STATUS
        public const string BERTHED = "BERT";
        public const string CONFIRMED = "CONF";
        public const string PENDING = "MPEN";
        public const string SCHEDULED = "SCH";
        public const string UNSCHEDULED = "USCH";
        public const string SAILED = "SALD";

        public const string PEND = "PEND";
    }

    public static class EntityCodes
    {

        public const string User_Groups = "UG";
        public const string Modules = "MOD";
        public const string Users_Privileges = "USRPRIV";
        public const string General_Settings = "GENSETT";
        public const string Audit_Trail = "AUDTRA";
        public const string Port = "PORT";
        public const string Quay = "QUAY";
        public const string BERTH = "BERTH";
        public const string Bollard = "BLRD";
        public const string Agent = "AGENT";
        public const string Employee = "EMP";
        public const string User = "USER";
        public const string Terminal_Operator = "TERMOPR";
        public const string Arrival_Notification = "ARVLNOT";
        public const string ISPSAN = "ISPSAN";
        public const string PortHealthAN = "PHAN";
        public const string IMDGAN = "IMDGAN";
        public const string Service_Request = "SERVREQ";
        public const string Electronic_Notifications = "ELECNOT";
        public const string NEWS = "NEWS";
        public const string SUPCAT = "SUPCAT";
        public const string SUBCAT = "SUBCAT";
        public const string VACHREQ = "VACHREQ";
        public const string AGENTREG = "AGENTREG";
        public const string ServiceRequest = "SERVREQ";
        public const string VESLREG = "VESLREG";
        public const string LICENSEREQ = "LICENSEREQ";
        public const string User_Registration = "USERREG";
        public const string BerthMaintenance_Approval = "BERTHMNT";
        public const string BerthMaintenanceCompletion_Approval = "BRTHMTCM";
        public const string Vessel_ETAChange = "CETADVIO";
        public const string Statement_Fact = "STATFACT";

        public const string Hour24Report625 = "HOURS24REPORTLIST";
        public const string PilotExemption = "PILTEXEMP";
        public const string Fuel_Requisition = "FUELREQ";
        public const string Fuel_Receipt = "FUELRECEIPT";

        public const string Berth_PreScheduling = "BPS";

        //Added By Srini, on 05 Aug 2014, constant for DivingRequestOccupation
        public const string DivingRequestOccupation = "DIVREQ";

        //Added By Srini, on 05 Sep 2014, constant for SuppHotWorkInspection
        public const string SupplymentaryServiceRequest = "SSRQ";
        public const string ColdWorkSupplServiceRequest = "SSRC";
        public const string FloatingSuppServiceRequest = "SSRF";
        public const string HotWorkSuppServiceRequest = "SSRH";
        public const string HotColdSuppServiceRequest = "SSRHC";
        public const string WaterSuppServiceRequest = "SSRW";


        public const string CraftReminderConfig = "CRAFTRECONFIG";
        public const string Docking_Plan = "DOCKPLAN";


        //Addes by chandrima on 11.11.2014 for Dredging Volume Workflow
        public const string Dredging_Volume = "DREDGVLM";
        public const string Berth_Occupation = "DREDGOCCP";
        public const string Dredging_Priority = "DREDGPRTY";



        public const string Supp_DryDock = "DDAP";
        //Added By Srini, on 12 Dec 2014, constant for VesselArrests
        public const string VesselArrests = "VAIS";

        //Added By Omprakash On 12th Dec 2014 For Notifications
        public const string AutomatedSlotting = "AUTOSLOT";

        public const string Supp_DryDockUndock = "DUTC";

        //Added By Santosh On 20th Dec 2014 For Departure Notice Request Notifications
        public const string DepartureNotice = "DEPNOTI";

        public const string Supp_DryDockExtension = "DDEX";

        //
        public const string AutomatedResourceAllocation = "AURA";

        // -- Added by sandeep on 26-12-2014
        public const string SupplemetaryResourceAllocation = "SURA";

        // -- end

        //---Portentrypass

        public const string PortEntryPassApplication = "PORTENTRYPASS";
        public const string SSAverification = "PORTENTRYPASSSSACheck";
        public const string SAPSverification = "PORTENTRYPASSSAPSCheck";


        public const string ExternalDivingRegister = "EXDREG";
        //Added By Omprakash, on 16 Feb 2015, constant for DivingRequest on Completion
        public const string DivingRequestOnCompletion = "DVREQCMP";

        //Added By Omprakash, on 16 Feb 2015, constant for DivingRequest on Completion
        public const string ExternalDivingRequestOnCompletion = "EXDVCMP";

        public const string MobileTask_Code = "MOBTASK";
        public const string MobileScheduleTaskExecution = "ScheduledTask";

        public const string CraftOut_Commision = "COMM";
        public const string CraftIn_Commision = "CIC";

        public const string DHMAN = "DHMAN";

        public const string ServiceRequest_Shifting = "SERVSHFT";
        public const string Capture_ArrDeparture = "VSLCALLANC";
        public const string ServiceRecording = "SERVICERECORDING";
        public const string EntryORExit = "ENTRY";

        public const string WasteDeclarationAN = "WDAN";

        //anusha 04/04/2024
        public const string InternalEmployeePermit = "EMPPERMIT";

        public const string IncidentReporting = "INR";
        public const string PortInformation = "PORTI";

    }

    public static class UserType
    {
        //below codes from subcategory table with SupCatCode USTY
        public const string TerminalOperator = "TO";
        public const string Employee = "EMP";
        public const string Agent = "AGNT";
        public const string ADMIN = "ADMN";
        public const string ExternalUser = "EXTU";
    }

    public static class ExportTypes
    {
        public const string Excel = "application/vnd.ms-excel";
        public const string PDF = "application/pdf";
        public const string Word = "application/msword";
        public const string Image = "image/jpeg";
        public const string Zip = "application/zip";
    }

    public static class AutomatedSlotStatus
    {
        public const string Completed = "CMPL";
        public const string Confirmed = "CNFR";
        public const string OnGoing = "ONG";
        public const string Overridden = "OVRD";
        public const string Planned = "PLND";
        public const string Pending = "PEND";
        public const string Scheduled = "SCHD";
    }

    public static class ResourceAllcationWorkFlowStatus
    {
        public const string Accepted = "ACCP";
        public const string Closed = "CLSD";
        public const string Completed = "COMP";
        public const string Confirmed = "CFRI";
        public const string Pending = "PNDG";
        public const string Rejected = "REJT";
        public const string Scheduled = "SCHD";
        public const string Verified = "VERF";
        public const string Started = "STRD";
        public const string Overridden = "OVRD";
    }

    public static class PilotCapacity
    {
        public const string DeadweightTonnage = "DEDW";
        public const string GrossTonnage = "GROS";
    }

    public static class ServiceReferenceType
    {
        public const string SupplementoryService = "SUPP";
        public const string VeselTraficServices = "VTSR";
    }

    public static class ServiceTypeCode
    {
        public const string ColdWorkPermitService = "CWST";
        public const string DryDock = "DDST";
        public const string FloatingCrane = "FCST";
        public const string HotandColdWorkPermitService = "HCST";
        public const string HotWorkPermitService = "HWST";
        public const string WaterService = "WTST";

        public const string Pilotage = "PILT";
        public const string TugorworkBoatService = "TGWR";
        public const string BerthMaster = "BRTH";
        public const string FlootingCrane = "FCST";
        public const string PilotBoatorHelicopterService = "PIHE";
        public const string Shifting = "SHFT";
    }

    public static class ResourceType
    {
        public const string Floating = "FCMR";
        public const string Water = "WMUR";

    }

    public static class RecordStatus
    {
        public const string Active = "A";
        public const string InActive = "I";
    }

    public static class CraftStatus
    {
        public const string InCommission = "IC";
        public const string OutCommission = "OC";
    }

    public static class DryDockStatus
    {
        public const string DryDockType = "DRDK";
        public const string Confirmed = "CNFR";
        public const string Docking = "DOCK";
        public const string UnDocking = "UNDK";
    }

    public static class DivingRequestType
    {
        public const string DivingType = "DIV";
        
    }

    /////DashBoard From date and To date Duration
    public static class DatesReportPeriod
    {
        public const string ReportPeriod = "ReportPeriod";
    }

    public static class SubCategoryConstants
    {
        public const string Automotive = "CTY2";
        public const string Breakbulk = "CTY3";
        public const string Bulk = "BULK";
        public const string Container = "CTY1";
        public const string LiquidBulk = "CTY4";
        public const string NonCargoWorking = "CTNC";
        public const string Bunkers = "CTBK";
        public const string Passengers = "DG";
    }

    public static class ErrorMessages
    {
        public const string UnscheduleAllResources = "Automated Resource Allocation can’t save without resources";
        public const string DelayReason = "Please enter Delay Reason";
        public const string MOPSDelayReason = "Please enter MOPS Delay Reason";

    }
    public static class IndividualApplication
{
      public const string IndividualTemporary = "Inditemp";
      public const string IndividualPermanent = "Indiper";
}

    public static class ContractorApplication
    {
        public const string ContractorTemporary = "contemp";
        public const string ContractorPermanent = "conper";
    }

    #region Workflows & Notifications related

    public static class WFStatus
    {
        public const string Approved = "WFSA";
        public const string Reject = "WFRE";
        public const string New = "WFSN";
        public const string Verified = "WFVE";
        public const string ViewRequest = "WFVI";
        public const string Confirmed = "WFCO";
        public const string Cancel = "WFCA";
        public const string Applied = "WFAP";
        public const string NewRequest = "NEW";
        public const string RequestApproved = "APPR";
        public const string Update = "UPDT";
        public const string Acknowledge = "WFAK";
        public const string An72 = "SBRS";
        public const string RequestReject = "REJ";
        public const string ResubmitUpdate = "VUPD";
         public const string Resubmit = "VRES";
         public const string CancelReject = "WSRE";
        

        // added by srini - VesselArrests
        public const string VesselArrested = "ARST";
        public const string VesselReleased = "RELS";
        public const string VesselSAMSAStopped = "SAMS";
        public const string VesselSAMSACleared = "SAMC";
        public const string Immobilization = "IMMB";

        // -- Added by sandeep - Supplementary Resource Allocation
        public const string ResourceAssigned = "RARA";
        public const string ResourceRemoved = "RARR";
        public const string ResourceUpdated = "RARU";

        public const string ScheduleTaskSailing = "SDTS";
        public const string ScheduleTaskShifting = "SDSS";
        // -- end

        ///-- Added by Venkataramana - SecurityModule
        public const string PortEnteyPassNew = "PPSN";
        public const string NotificationTemplateBase = "M";
        public const string PortEnteyPassManual = "PPAN";
        public const string Reverted_Resubmission = "PPNE";
        public const string PortEnteyPassResubmission = "PERS";
        public const string SAPSSecuriryCheck_Verification = "SSVE"; //PPVE
        public const string SSASecuriryCheck_Verification = "SPVE"; //PPVU
        public const string PortEnteyPassSSA_Verified = "SSVU";
        public const string PortEnteyPassSAPS_Verified = "SPVU";
        public const string PortEnteyPass_Approve = "PPAP";
        public const string PortEnteyPass_Rejected = "PPRJ";
        public const string PortEnteyPass_Appealed = "PEAL";
        public const string PortEnteyPassAppeal_Upheld = "PEUP";
        public const string PortEnteyPassAppeal_Approve = "APAP";
        public const string PortEnteyPass_Issue = "PPIS";
        ///-- Added by Venkataramana - 24hoursreport
        public const string New24HoursincidentReport = "NHIR";

        public const string CaptureArrival = "CADT";

        //-- Added by sandeep on 12-05-2015
        public const string ExtendSlot = "WFES";
        //-- end
        ///

        //
    }

    public static class ConfigName
    {
        public const string IncorrectPWDCount = "IncorrectPWDCount";
        public const string ApprovedCode = "ApproveCode";
        public const string CancelCode = "CancelCode";
        public const string ServiceRequestCondition1 = "SERVREQPRECOND1";
        public const string DepartureNoticeCondition1 = "DEPNOTIPRECOND1";
        public const string WorkflowInitialStatus = "WorkFlowInitialStatus";
        public const string DateFormat = "DateFormat";
        public const string NotificationLifeSpan = "NotificationLifeSpan";
        public const string FileUploadMaxSize = "FileUploadMaxSize";
        public const string PasswordExpiryDays = "PASSWORDEXPIRYDAYS";
        public const string PreviousPasswordsCount = "PREVIOUSPASSWORDSCOUNT";
        public const string ServiceRequestBunkersCondition = "SERVREQPRECOND2";
    }

    public static class Roles
    {
        public const string Annuser = "Anuuser";
        public const string Admin = "ADMN";
        public const string Agent = "AGNT";
        public const string BerthPlanner = "BP";
        public const string TerminalOperator = "TO";
        public const string VesselTrafficController = "VTC";
        public const string PortSecurityOfficer = "ISPS";
        public const string PortHealthOfficer = "PHO";
        public const string MarineSafetyOfficer = "IMDG";
        public const string HarbourMaster = "HM";
        public const string DeputyHarbourMaster = "DHM";
        public const string MarineRevenueManager = "MRM";
        public const string LegalVerifyingAuthority = "LVA";
        public const string LegalApprovingAuthority = "LAA";
        public const string DivingSupervisor = "DSUP";
        public const string PortEngineer = "PE";
        public const string SupplementaryServiceRequestApprovingAuthority = "SSRAA";
        public const string SeniorVesselTrafficController = "SVTC";
        public const string SeniorBerthingMaster = "SBM";
        public const string DredgingProjectManager = "PMDG";
        public const string LicenseManager = "LM";
        public const string ResourceAllocationApprovingAuthority = "RAAA";
        public const string ChiefEngineer = "CE";
        public const string MarineTechnicalManager = "MTM";
        public const string MarineEngineerOfficer = "MEO";
        public const string MarineOperationsManagers = "MOM";
        public const string PettyOfficer = "PO";
        public const string DockMaster = "DM";
        public const string Portvisitors = "PV";
        public const string Supervisor = "SV";
        public const string FireOfficer = "FO";
        public const string LineManager = "LNMR";
        public const string LlyodsRegisterApprovingAtuthority = "LRAA";
        public const string CraftMaster = "CM";
        public const string General = "GEN";
        public const string DeckOfficer = "DO";
        //port entey pass
        public const string StateSecurityAgency = "SSA";
        public const string Southafricanpoliceservice = "SAPS";
        public const string WaterMan = "WTM";
        public const string FloatingCraneMaster = "FLTC";
        //end
    }


    // Berth Planning Configurations
    public static class BerthPlanningConstants
    {
        public const string ScheduleColor = "SCHEDULED";
        public const string ConfirmColor = "CONFIRMED";
        public const string BerthedColor = "BERTHED";
        public const string SailedColor = "SAILED";
        public const string ArrestedColor = "ARRESTED";
        public const string CaptureArrivalDepature = "ATA/ATD Configuration";
        public const string RepairCode = "REPA";
        public const string UnderKeelClearance = "UNDER KEEL CLEARANCE";

    }

    public static class ScheduleStatus
    {
        public const string New = "NEW";
    }
    #endregion


    #region SAP Integration Related

    public static class PostToSAP
    {
        public const string SAPReason = "PSC";

        public const string AddArrival = " <ArrivalCreate> "
                                           + " <AED>#AED#</AED>"
                                           + " <AET>#AET#</AET>"
                                           + " <DED>#DED#</DED>"
                                           + " <DET>#DET#</DET>"
                                           + " <ARRNO>0</ARRNO> "
                                           + " <CODE>#CODE#</CODE>"                                 
                                           + " <EDA>#EDA#</EDA>"
                                           + " <KUNNR>#KUNNR#</KUNNR>"
                                           + " <PORTCALL>#PORTCALL#</PORTCALL>"
                                           + " <PORTORIGIN>#PORTORIGIN#</PORTORIGIN>"
                                           + " <VKORG>#VKORG#</VKORG>"
                                           + " <VOYIN>#VOYIN#</VOYIN>"
                                           + " <VOYOUT>#VOYOUT#</VOYOUT>"
                                           + " <ZZBERTH>#ZZBERTH#</ZZBERTH>"
                                           + " </ArrivalCreate>";

        public const string UpdateArrival = " <ArrivalUpdate> "
                                           + " <AED>#AED#</AED>"
                                           + " <AET>#AET#</AET>"
                                           + " <DED>#DED#</DED>"
                                           + " <DET>#DET#</DET>"
                                           + "  <ARRNO>#ARRNO#</ARRNO>"
                                           + "  <CODE>#CODE#</CODE>"
                                           + "  <EDA>#EDA#</EDA>"
                                           + "  <EDD>#EDD#</EDD>"
                                           + "  <KUNNR>#KUNNR#</KUNNR>"
                                           + "  <PORTCALL>#PORTCALL#</PORTCALL>"
                                           + "  <PORTORIGIN>#PORTORIGIN#</PORTORIGIN>"
                                           + "  <VKORG>#VKORG#</VKORG>"
                                           + "  <VOYIN>#VOYIN#</VOYIN>"
                                           + "  <VOYOUT>#VOYOUT#</VOYOUT>"
                                           + " <ZZBERTH>#ZZBERTH#</ZZBERTH>"
                                           + "  </ArrivalUpdate> ";



        public const string MarineCreateHdr = "<MarineOrderCreate>"
                                             + "<DISTRIBUTIONCHANNEL>01</DISTRIBUTIONCHANNEL>"//Fixed
                                             + "<DIVISION>01</DIVISION>"// Fixed
                                             + "<ITEMNO></ITEMNO>" // Empty
                                             + "<ORDERITEMIN>"

                                             + "#@@@@@@@@@@@#"

                                             + "</ORDERITEMIN>"
                                             + "<ORDERTYPE>ZM</ORDERTYPE>"   //ZM
                                             + "<PONO>#PONO#</PONO>" // ARRNO
                                             + "<RETURN>"
                                             + "<Item>"
                                             + "<TYPE></TYPE>"
                                           + "<ID></ID>"
                                           + "<NUMBER></NUMBER>"
                                           + "<MESSAGE></MESSAGE>"
                                           + "<LOGNO></LOGNO>"
                                           + "<LOGMSGNO></LOGMSGNO>"
                                           + "<MESSAGEV1>aa</MESSAGEV1>"
                                           + "<MESSAGEV2>aa</MESSAGEV2>"
                                           + "<MESSAGEV3>aa</MESSAGEV3>"
                                           + "<MESSAGEV4>aa</MESSAGEV4>"
                                           + "<PARAMETER>aa</PARAMETER>"
                                           + "<ROW>0</ROW>"
                                           + "<FIELD>aaa</FIELD>"
                                           + "<SYSTEM>aaa</SYSTEM>"
                                           + "</Item>"
                                           + "</RETURN>"
                                           + "<SALESORGANIZATION>#SALESORGANIZATION#</SALESORGANIZATION>" // SALESORGANIZATIONTable
                                           + "<SCHEDULELINES>"

                                           + "###@@@@@###"



                                           + "</SCHEDULELINES>"
                                           + "<SCHEDULELINESX>"
                                           + "<Item>"
                                           + "<ITMNUMBER></ITMNUMBER>"
                                           + "<SCHEDLINE></SCHEDLINE>"
                                           + "<UPDATEFLAG></UPDATEFLAG>"
                                           + "<REQDATE></REQDATE>"
                                           + "<DATETYPE></DATETYPE>"
                                           + "<REQTIME></REQTIME>"
                                           + "<REQQTY></REQQTY>"
                                           + "<REQDLVBL></REQDLVBL>"
                                           + "<SCHEDTYPE></SCHEDTYPE>"
                                           + "<TPDATE></TPDATE>"
                                           + "<MSDATE></MSDATE>"
                                           + "<LOADDATE></LOADDATE>"
                                           + "<GIDATE></GIDATE>"
                                           + "<MSTIME></MSTIME>"
                                           + "<LOADTIME></LOADTIME>"
                                           + "<GITIME></GITIME>"
                                           + "<REFOBJTYPE></REFOBJTYPE>"
                                           + "<REFOBJKEY></REFOBJKEY>"
                                           + "<DLVDATE></DLVDATE>"
                                           + "<DLVTIME></DLVTIME>"
                                           + "<RELTYPE></RELTYPE>"
                                           + "<PLANSCHEDTYPE></PLANSCHEDTYPE>"
                                           + "</Item>"
                                           + "</SCHEDULELINESX>"
                                        + "<CREATEDDATE></CREATEDDATE>"
                                        + "<REVENUEPOSTINGID></REVENUEPOSTINGID>"
                                        + "<MESSAGETYPE></MESSAGETYPE>"
                                        + "<SAPPOSTINGID></SAPPOSTINGID>"
                                        + "<SALESDOCUMENT>0</SALESDOCUMENT>"
                                        + "<SHIPTOPARTY>#SHIPTOPARTY#</SHIPTOPARTY>"  //  Marine Posting Account No
                                        + "<SOLDTOPARTY>#SOLDTOPARTY#</SOLDTOPARTY>"//  Vesel  SapAccount Table
                                        + "<ZZDATEFROM>#ZZDATEFROM#</ZZDATEFROM>" // Breakin/out
                                        + "<ZZTIMEFROM>#ZZTIMEFROM#</ZZTIMEFROM>"// Breakin/out
                                        + "<ZZTIMETO>#ZZTIMETO#</ZZTIMETO>" //Breakin/out
                                        + "<ZZDATETO>#ZZDATETO#</ZZDATETO>"// Breakin/out
                                        + "<ZZDOCKTIME>0</ZZDOCKTIME>";







        public const string SCHEDULELINESdtl = "<Item>"
                                           + "<ITMNUMBER>#ITMNUMBER#</ITMNUMBER>"  // itemNo
                                           + "<SCHEDLINE></SCHEDLINE>"  // Empty
                                           + "<REQQTY>#REQQTY#</REQQTY>"        // Units
                                           + "</Item>";


        public const string MarineCreatedtl = ""
                                             + "<Item>"
            //+ "<TARGETQTY>#TARGETQTY#</TARGETQTY>" // Units
                                             + "<MATERIAL>#MATERIAL#</MATERIAL>" // Matrcode
                                             + "<ITMNUMBER>#ITMNUMBER#</ITMNUMBER>" // SeqNo
            //+ "<HGLVITEM></HGLVITEM>"
                                             + "</Item>";




        public const string VesselCreation = "<VESSELDETAILS>"
                                                + "<ICALLSIGN>#ICALLSIGN#</ICALLSIGN>" // callsign
                                                + "<ICITY>#ICITY#</ICITY>"
                                                + "<ICOUNTRY>#ICOUNTRY#</ICOUNTRY>"
                                                + "<IDATE>#IDATE#</IDATE>"
                                                + "<IIMO>#IIMO#</IIMO>"
                                                + "<ILENGTH>#ILENGTH#</ILENGTH>"
                                                + "<IPOSTAL>#IPOSTAL#</IPOSTAL>"
                                                + "<ITONNAGE>#ITONNAGE#</ITONNAGE>"
                                                + "<IVESIND>#IVESIND#</IVESIND>"
                                                + "<IVESNAME>#IVESNAME#</IVESNAME>"
                                                + "<IVESTYPE>#IVESTYPE#</IVESTYPE>"
                                                + "<VKORG>#VKORG#</VKORG>"
                                                + "<MESSTAB>"
                                                + "<Item>"
                                                + "<TCODE></TCODE>"
                                                + "<DYNAME></DYNAME>"
                                                + "<DYNUMB></DYNUMB>"
                                                + "<MSGTYP></MSGTYP>"
                                                + "<MSGSPRA></MSGSPRA>"
                                                + "<MSGID></MSGID>"
                                                + "<MSGNR></MSGNR>"
                                                + "<MSGV1></MSGV1>"
                                                + "<MSGV2></MSGV2>"
                                                + "<MSGV3></MSGV3>"
                                                + "<MSGV4></MSGV4>"
                                                + "<ENV></ENV>"
                                                + "<FLDNAME></FLDNAME>"
                                                + "</Item>"
                                                + "</MESSTAB>"
                                                + "</VESSELDETAILS>";

        public const string MarineCreateHdrClose = "</MarineOrderCreate>";

        public const string SapInvoice = "<INVOICE>"
                                   + "<EINVOICE>"
                                   + "<Item>"
                                   + "<ORDERNUMBER>#ORDERNUMBER#</ORDERNUMBER>"
                                   + "</Item>"
                                   + "</EINVOICE>"
                                   + "<VBELN>#VBELN#</VBELN>"
                                   + "</INVOICE>";



        public const string MarineUpdateHdrXML = "<MarineOrderUpdate>"
                                   + "<ORDER>#ORDER#</ORDER>"
                                   + "<ORDERHEADERINX>"
                                   + "<Item>"
                                   + "<UPDATEFLAG>U</UPDATEFLAG>"
                                   + "</Item>"
                                   + "</ORDERHEADERINX>"
                                   + "<ORDERITEMSIN>"
                                   + "@@@@@"
                                   + "</ORDERITEMSIN>"
                                   + "<ORDERITEMSINX>"
                                   + "@#@@@@"
                                      + "</ORDERITEMSINX>"
                                      + "<SCHEDULELINES>"
                                      + "@@#@@@"
                                      + "</SCHEDULELINES>"
                                      + "<SCHEDULELINESX>"
                                      + "@@@#@@"
                                      + "</SCHEDULELINESX>"
                                      + "<ZZDATEFROM>#ZZDATEFROM#</ZZDATEFROM>"
                                      + "<ZZDATETO>#ZZDATETO#</ZZDATETO>"
                                      + "<ZZTIMEFROM>#ZZTIMEFROM#</ZZTIMEFROM>"
                                      + "<ZZTIMETO>#ZZTIMETO#</ZZTIMETO>"
                                      + "</MarineOrderUpdate>";

        public const string ORDERITEMSINSTR = "<Item>"  //"@@@@@"
                                                   + "<ITMNUMBER>#ITMNUMBER#</ITMNUMBER>"
                                                   + "<MATERIAL>#MATERIAL#</MATERIAL>"
                                                   + "</Item>";
        public const string ORDERITEMSINXSTR = "<Item>" // "@#@@@@"
                                               + "<ITMNUMBER>#ITMNUMBER#</ITMNUMBER>"
                                               + "<UPDATEFLAG>I</UPDATEFLAG>"
                                               + "<MATERIAL>X</MATERIAL>"
                                               + "</Item>";

        public const string SCHEDULELINESSTR = "<Item>" // "@@#@@@"
                                                + "<ITMNUMBER>#ITMNUMBER#</ITMNUMBER>"
                                                + "<REQQTY>#REQQTY#</REQQTY>"
                                                + "</Item>";

        public const string SCHEDULELINESXSTR = "<Item>"   //"@@@#@@"
                                               + "<ITMNUMBER>#ITMNUMBER#</ITMNUMBER>"
                                               //+ "<UPDATEFLAG>I</UPDATEFLAG>"
                                               + "<REQQTY>X</REQQTY>"
                                               + "</Item>";

        

    }

    public static class SAPPostingStatus
    {

        public const string New = "SNEW";
        public const string Posted = "SPOS";
        public const string Completed = "SCMP";
        public const string Error = "SERR";

    }

    public static class SAPMessageTypes
    {

        public const string ArrivalCreate = "CRAR";
        public const string ArrivalUpdate = "UPAR";

        public const string MarineCreate = "CRMO";
        public const string MarineUpdate = "UPMO";

        public const string VesselCreate = "CRVS";
        public const string InvoiceCreate = "CRIN";

    }

    public static class SAPResponseStatus
    {
        //Create Arrival
        public const string Created = "Created";
        public const string Updated = "Amended";

    }

    public static class AISResponseMovements
    {

        public const string BreakwaterMovement = "B_Arrival";
       

    }
    public static class UserLogin
    {
        public const string FirstTimeLogin = "Y";
        public const string LoggedIn = "N";
    }
    #endregion
    

    //Mahesh K added on 29-09-2023
    public static class SlotPeriodTimeStatus
    {
        public const string slotperiod0 = "00:00";
        public const string slotperiod1 = "01:00";
    }


    //Anusha 28-05-2024
    public static class AuditPWDFlag
    {
        public const string ForgotPassword = "F";
        public const string ResetPassword = "R";
    }


}


