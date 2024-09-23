using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Domain;
using IPMS.Domain.ValueObjects;

namespace IPMS.Repository
{
    public class SubCategoryRepository : IPMS.Repository.ISubCategoryRepository
    {
        private IUnitOfWork _unitOfWork;

        public SubCategoryRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //This you can use it in UserService directly. instead of writing LINQ query there..
        public List<SubCategory> UserTypes()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.USER_TYPE);  //but donot hard code it like this. 
        }
        private List<SubCategory> GetSubCategoriesFor(string superCategoryCode)
        {
            var UserType = _unitOfWork.Repository<SubCategory>().Queryable().Where(x => x.SupCatCode == superCategoryCode && x.RecordStatus=="A").OrderBy(x => x.SubCatName).ToList();
            return UserType;
        }

        private List<SubCategoryCodeNameVO> GetSubCategoriesCodeNameFor(string superCategoryCode)
        {
            var SubCategoryCodeName = (from _subcategory in _unitOfWork.Repository<SubCategory>().Queryable().Where(s => s.SupCatCode == superCategoryCode && s.RecordStatus == "A")                                      
                                       select new SubCategoryCodeNameVO
                                       {
                                           SubCatCode = _subcategory.SubCatCode,
                                           SubCatName = _subcategory.SubCatName

                                       });
            return SubCategoryCodeName.OrderBy(x=>x.SubCatName).ToList();
        }

        private List<SubCategoryCodeNameVO> GetSubCategoriesCodeNameForCodeSort(string superCategoryCode)
        {
            var SubCategoryCodeName = (from _subcategory in _unitOfWork.Repository<SubCategory>().Query().Select()
                                       where _subcategory.SupCatCode == superCategoryCode && _subcategory.RecordStatus == "A"
                                       orderby _subcategory.SubCatCode ascending
                                       select new SubCategoryCodeNameVO
                                       {
                                           SubCatCode = _subcategory.SubCatCode,
                                           SubCatName = _subcategory.SubCatName

                                       });
            return SubCategoryCodeName.ToList();
        }
        private List<SubCategoryCodeNameWithSupCatCodeVO> GetSubCategoriesCodeNameForCodeSortWithSupCatCode(string superCategoryCode)
        {
            var SubCategoryCodeNameWithSupCatCode = (from _subcategory in _unitOfWork.Repository<SubCategory>().Query().Select()
                                       where _subcategory.SupCatCode == superCategoryCode && _subcategory.RecordStatus == "A"
                                       orderby _subcategory.SubCatCode ascending
                                       select new SubCategoryCodeNameWithSupCatCodeVO
                                       {
                                           SubCatCode = _subcategory.SubCatCode,
                                           SubCatName = _subcategory.SubCatName ,
                                           SupCatCode=_subcategory.SupCatCode
                                       });
            return SubCategoryCodeNameWithSupCatCode.ToList();
        }

        private List<SubCategory> GetSubCategoriesForActive(string superCategoryCode)
        {
            var UserType = (from ad in _unitOfWork.Repository<SubCategory>().Queryable()

                            where ad.SupCatCode == superCategoryCode && ad.RecordStatus == "A"
                            select ad);
            return UserType.ToList();
        }

        public List<SubCategory> WarpSides()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.WARP_SIDE);
        }

        public List<SubCategory> DockTypes()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.DOCK_TYPE);
        }
        public List<SubCategory> CargoTypes()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.CARGO_TYPE);
        }
        public List<SubCategory> Purpose()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.PURPOSE_TYPE);
        }
        public List<SubCategory> Uoms()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.UOM_TYPE);
        }

        public List<SubCategory> CargoUoms()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.CARGOUOMS);
        }
        public List<SubCategory> BunkersRequiredType()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.BUNKERREQUIREDDTLS);
        }
        public List<SubCategory> BunkersMethod()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.BUNKERMETHODDTLS);
        }

        public List<SubCategory> Commoditys()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.COMMODITY_TYPE);
        }
        public List<SubCategory> ReasonForVisit()
        {

            return GetSubCategoriesForActive(SuperCategoryConstants.REASON_FOR_VISIT_TYPE);
        }
        public List<SubCategory> DangerousGoods()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.DANGEROUSGOODS_TYPE);
        }
        public List<SubCategory> DocumentsTypes()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.DOCUMENT_CATEGORY);
        }
        public List<SubCategory> PilotExemptionDocumentsTypes()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.PILOTEXEMPTION_DOCTYPE);
        }
        public List<SubCategory> LicenseRequestDocumentsTypes()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.LICENSEREQUEST_DOCTYPE);
        }
        public List<SubCategory> MomentTypes()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.MOMENT_TYPE);
        }
        public List<SubCategory> SideAlongSideTypes()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.SIDE_ALONG_SIDE_TYPE);
        }
        public List<SubCategory> TankerTypes()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.TANKER_TYPE);
        }
        public List<SubCategory> BunkerService()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.BUNKERSERVICE_TYPE);
        }
        public List<SubCategory> GetWorkflowStatus()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.WORK_FLOW_STATUS);
        }
        public List<SubCategory> PortOfRegistry()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.PORT_OF_REGISTRY);
        }
        public List<SubCategory> CraftType()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.CRAFT_TYPE);
        }
        public List<SubCategory> CraftNationality()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.CRAFT_NATIONALITY);
        }
        public List<SubCategory> ClassificationSociety()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.CLASSIFICATION_SOCIETY);
        }
        public List<SubCategory> FuelType()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.FUEL_TYPE);
        }
        public List<SubCategory> PropulsionType()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.PROPULSION_TYPE);
        }
        public List<SubCategory> EngineType()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.ENGINE_TYPE);
        }
        public List<SubCategory> CraftCommissionStatus()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.COMMISSION_STATUS);
        }
        public List<SubCategory> PrivilegeTypes()
        {
            return GetSubCategoriesForActive(SuperCategoryConstants.PRIVILEGE_TYPE);
        }
        public List<SubCategory> LicensingRequestTypes()
        {
            return GetSubCategoriesForActive(SuperCategoryConstants.LICENSING_REQUEST_TYPE);
        }
        public List<SubCategory> MaintenanceType()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.MAINTENANCE_TYPE);
        }
        public List<SubCategory> ReasonTypes()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.REASON_TYPE);
        }

        #region Pilot_Nationality
        /// <summary>
        /// Get List Of Nations/Countries
        /// </summary>
        /// <returns></returns>
        public List<SubCategory> Pilot_Nationality()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.PILOT_NATIONALITY);
        }
        #endregion

        public List<SubCategory> PilotRoleCode()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.PILOT_ROLECODE);
        }

        /// <summary>
        ///BY MAHESH : GET PARTICULARS AND CALENDERS TYPES FOR CRAFT REMAINDER CONFIG
        /// </summary>
        /// <returns></returns>
        public List<SubCategory> ParticularsTypes()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.CRAFTCONFIG_PART_TYPE);
        }
        public List<SubCategory> CalenderTypes()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.CRAFTCONFIG_CALD_TYPE);
        }

        #region Berth-PreScehduling - Nivedita

        public List<SubCategoryCodeNameVO> GetMovementsStatus()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.MOVEMENT_STATUS);
        }
        public List<SubCategoryCodeNameVO> GetVesselTypes()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.VESSEL_TYPE);
        }

        public List<SubCategoryCodeNameVO> GetCargoTypes()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.CARGO_TYPE);
        }

        public List<SubCategoryCodeNameVO> GetReasonsForVisit()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.REASON_FOR_VISIT_TYPE);
        }

        #endregion
        public List<SubCategoryCodeNameVO> GetReasonsForDelay()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.REASON_FOR_DELAY);
        } 

        public List<SubCategory> IncidentTypes()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.NATURE_INCIDENT_TYPE);
        }

        //////By mahesh : get workflow entities.......
        public List<SubCategory> WorkFlowEvents()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.WORK_FLOW_STATUS);
        }

        //By Suresh : Cahnge Of Agent
        public List<SubCategory> ReasonForTransfer()
        {
            return GetSubCategoriesForActive(SuperCategoryConstants.AGENTCHANGE_RSTRCODE);
        }

        //By Suresh : Vessel Registration
        public List<SubCategory> ClassificationSocity()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.VESSELREG_CLSSOCITYCODE);
        }

        public List<SubCategory> VesselTypes()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.VESSELREG_VESSELTYPECODE);
        }

        public List<SubCategory> VesselNationality()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.VESSELREG_VESSELNATCODE);
        }

        public List<SubCategory> VesselCertificateNames()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.VESSELREG_VESSELCERTCODE);
        }

        public List<SubCategory> VesselSafeWorkingLoad()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.VESSELREG_VESSELSWLCODE);
        }

        public List<SubCategory> VesselMovementTypes()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.MOMENT_TYPE);
        }
        public List<SubCategoryCodeNameVO> GetOperations()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.OPERATION_TYPE);
        }
        public List<SubCategory> GetKeyEvents()
        {
            return GetSubCategoriesForActive(SuperCategoryConstants.KEYEVENT_TYPE);
        }
        public List<SubCategoryCodeNameVO> GetPilotCapacity()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.PILOT_CAPACITY);
        }
        public List<SubCategoryCodeNameVO> GetNaturetypes()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.NATURE_TYPE);
        }
        public List<SubCategoryCodeNameVO> GetRecordingofIncident()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.RECORDING_OF_INCIDENT);
        }
        public List<SubCategoryCodeNameVO> GetGeneralAgencies()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.GENERAL_AGENCIES);
        }
        public List<SubCategoryCodeNameVO> GetOccupationalHygieneAgencies()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.OCCUPATIONAL_HYGIENE_AGENCIES);
        }
        public List<SubCategoryCodeNameVO> GetTypeofContact()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.TYPE_OF_CONTACT);
        }
        public List<SubCategoryCodeNameVO> GetStandardActs()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.STANDARD_ACTS);
        }
        public List<SubCategoryCodeNameVO> GetSubstandardConditions()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.SUBSTANDARD_CONDITIONS);
        }
        public List<SubCategoryCodeNameVO> GetPersonalFactors()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.PERSONAL_FACTORS);
        }
        public List<SubCategoryCodeNameVO> GetJobFactors()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.JOB_FACTORS);
        }
        public List<SubCategoryCodeNameVO> GetControlStepsToPreventRecurrence()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.CONTROL_STEPS_TO_PREVENT_RECURRENCE);
        }
        public List<SubCategoryCodeNameVO> GetFireDepartment()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.FIRE_DEPARTMENT);
        }
        public List<SubCategoryCodeNameVO> GetIncidentClassification()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.INCIDENT_CLASSIFICATION);
        }
        public List<SubCategoryCodeNameVO> GetDiscriptionofExposedRisk()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.DISCRIPTION_OF_EXPOSED_RISK);
        }
        public List<SubCategoryCodeNameVO> GetSection_Record_Incident()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.SECTION_RECORD_INCIDENT);
        }
        public List<SubCategoryCodeNameVO> GetDocumentTypes()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.DOCUMENT_CATEGORY);
        }
        public List<SubCategoryCodeNameVO> GetWeatherTypes()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.WEATHER_TYPE);
        }


        public List<SubCategory> GetOilTypes()
        {
            return GetSubCategoriesForActive(SuperCategoryConstants.OIL_TYPE);
        }

        public List<SubCategory> GetGradeTypes()
        {
            return GetSubCategoriesForActive(SuperCategoryConstants.GRADE_TYPE);
        }

        public List<SubCategory> GetUOMTypes()
        {
            return GetSubCategoriesForActive(SuperCategoryConstants.UOM_TYPE);
        }

        public List<SubCategory> GetTptUOMTypes()
        {
            return GetSubCategoriesForActive(SuperCategoryConstants.TPTUOM_TYPE);
        }

        public List<SubCategory> GetSupplyingModes()
        {
            return GetSubCategoriesForActive(SuperCategoryConstants.SUPPLYING_MODE);
        }

        public List<SubCategory> GetFinancialYears()
        {
            return GetSubCategoriesForActive(SuperCategoryConstants.FINANCIAL_YEAR);
        }

        public List<SubCategory> GetDeploymentTypes()
        {
            return GetSubCategoriesForActive(SuperCategoryConstants.DEPLOYMENT_TYPE);
        }

        public List<SubCategory> GetDredgingColors()
        {
            return GetSubCategoriesForActive(SuperCategoryConstants.DREDGING_COLOR);
        }

        public List<SubCategory> GetDredgingTypes()
        {
            return GetSubCategoriesForActive(SuperCategoryConstants.DREDGING_TYPE);
        }
        public List<SubCategory> GetDepartments()
        {
            return GetSubCategoriesForActive(SuperCategoryConstants.DEPARTMENT_TYPE);
        }

        public List<SubCategoryCodeNameVO> Getapplicationcategory()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.APPLICATION_CATEGORY);
        }
        public List<SubCategoryCodeNameVO> GetPermittypes()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.PERMIT_TYPE);
        }
        public List<SubCategoryCodeNameVO> GetAreaOfConstruction()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.AREA_OF_CONSTRUCTION);
        }
        public List<SubCategoryCodeNameVO> GetTemporaryPermits()
        {
            return GetSubCategoriesCodeNameForCodeSort(SuperCategoryConstants.TEMPORARY_PERMITS);
        }
        public List<SubCategoryCodeNameVO> GetAdhocPermits()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.ADHOC_PERMITS);
        }
        public List<SubCategoryCodeNameVO> GetOperatedTerminals()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.OPERATED_TERMINALS);
        }

        public List<SubCategoryCodeNameVO> GetPermitRequeirements()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.PERMIT_REQUEIREMENTS);
        }
        public List<SubCategoryCodeNameVO> GetAccessGates()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.ACCESS_GATES);
        }
        public List<SubCategoryCodeNameVO> GetPermitRequeirementstypes()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.REQUEIREMENT_TYPES);
        }
        public List<SubCategoryCodeNameVO> GetPermitRequeirementsDuration()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.REQUEIREMENT_DURATION);
        }
        public List<SubCategoryCodeNameVO> GetPermitCodes()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.PERMIT_CODES);
        }

        public List<SubCategoryCodeNameVO> GetPermitDocumentTypes()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.PERMITDOCUMENT_CATEGORY);
        }
        public List<SubCategoryCodeNameVO> GetSecurityDocumentTypes()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.SECURITYDOCUMENT_CATEGORY);
        }

        public List<SubCategoryCodeNameVO> GetRevenueAccountStatus()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.REVENUE_ACCOUNT_STATUS);
        }

        public List<SubCategory> GetPhaseTypes()
        {
            return GetSubCategoriesForActive(SuperCategoryConstants.PHASE_TYPE);
        }

        public List<SubCategoryCodeNameVO> GetPermitStatus()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.PERMIT_STATUS);
        }
        public List<SubCategory> GetVesselagentChangeDOCTypes()
        {
            return GetSubCategoriesForActive(SuperCategoryConstants.VACHREQ_DOCTYPE);
        }
        public List<SubCategory> GetFuelUOMTypes()
        {
            return GetSubCategoriesForActive(SuperCategoryConstants.FUEL_UOM);
        }

        public List<SubCategoryCodeNameVO> GetSubCategoryDetailsBySupCatCode(string supcatcode)
        {
            return GetSubCategoriesCodeNameFor(supcatcode);
        }

        public List<SubCategory> GetReasons()
        {
            return GetSubCategoriesForActive(PostToSAP.SAPReason);
        }
        public List<SubCategoryCodeNameVO> GetDelayTypes()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.DELAY_TYPE);
        }
        public List<SubCategory> GetServiceDocumentTypes()
        {
            return GetSubCategoriesFor(SuperCategoryConstants.SERV_DOCTYP);
        }
        public List<SubCategoryCodeNameVO> GetMopsDelays()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.MOPS_DELAY);
        }
        // Delay Reasons Code

        public List<SubCategoryCodeNameVO> GetDelayReasons()
        {
            return GetSubCategoriesCodeNameFor(SuperCategoryConstants.DELAY_REASONS);
        }

        public List<SubCategory> MarpolTypes()
        {
            return GetSubCategoriesForActive(SuperCategoryConstants.MARPOL_TYPE);
        }
        public List<SubCategoryCodeNameVO> GetTemporaryPermitsForIndividual()
        {
            return GetSubCategoriesCodeNameForCodeSort(SuperCategoryConstants.TEMPORARY_PERMITS_IND);
        }
        public List<SubCategoryCodeNameVO> GetAccessAreasForRB()
        {
            return GetSubCategoriesCodeNameForCodeSort(SuperCategoryConstants.ACCESS_AREAS_RB);
        }

        public List<SubCategoryCodeNameVO> GetAccessAreasForCT()
        {
            return GetSubCategoriesCodeNameForCodeSort(SuperCategoryConstants.ACCESS_AREAS_CT);
        }
        public List<SubCategoryCodeNameVO> GetAccessAreasForDB()
        {
            return GetSubCategoriesCodeNameForCodeSort(SuperCategoryConstants.ACCESS_AREAS_DB);
        }
        public List<SubCategoryCodeNameVO> GetAccessAreasForEL()
        {
            return GetSubCategoriesCodeNameForCodeSort(SuperCategoryConstants.ACCESS_AREAS_EL);
        }
        public List<SubCategoryCodeNameVO> GetAccessAreasForNG()
        {
            return GetSubCategoriesCodeNameForCodeSort(SuperCategoryConstants.ACCESS_AREAS_NG);
        }
        public List<SubCategoryCodeNameVO> GetAccessAreasForMB()
        {
            return GetSubCategoriesCodeNameForCodeSort(SuperCategoryConstants.ACCESS_AREAS_MB);
        }
        public List<SubCategoryCodeNameVO> GetAccessAreasForPE()
        {
            return GetSubCategoriesCodeNameForCodeSort(SuperCategoryConstants.ACCESS_AREAS_PE);
        }
        public List<SubCategoryCodeNameVO> GetAccessAreasForSB()
        {
            return GetSubCategoriesCodeNameForCodeSort(SuperCategoryConstants.ACCESS_AREAS_SB);
        }

        public List<SubCategoryCodeNameVO> GetContractorTemporaryPermits()
        {
            return GetSubCategoriesCodeNameForCodeSort(SuperCategoryConstants.CONTRACTOR_TEMPORARY_PERMITS);
        }
        public List<SubCategoryCodeNameVO> GetContractorPermanentPermits()
        {
            return GetSubCategoriesCodeNameForCodeSort(SuperCategoryConstants.CONTRACTOR_PERMANANT_PERMITS);
        }
        public List<SubCategoryCodeNameVO> GetPermanentPermitsForIndividual()
        {
            return GetSubCategoriesCodeNameForCodeSort(SuperCategoryConstants.PERMANENT_PERMITS_IND);
        }
        public List<SubCategoryCodeNameVO> GetReasonsForPermit()
        {
            return GetSubCategoriesCodeNameForCodeSort(SuperCategoryConstants.REASON_PERMIT);
        }
        public List<SubCategoryCodeNameWithSupCatCodeVO> GetSubAccessAreasForRB(string supCatCode)
        {
            return GetSubCategoriesCodeNameForCodeSortWithSupCatCode(supCatCode);
        }

        public List<SubCategoryCodeNameWithSupCatCodeVO> GetAreas(string supCatCode)
        {
            return GetSubCategoriesCodeNameForCodeSortWithSupCatCode(supCatCode);
        }
        public List<SubCategoryCodeNameVO> GetContractorReasonForPermit()
        {
            return GetSubCategoriesCodeNameForCodeSort(SuperCategoryConstants.CONTRACTOR_REASON_PERMIT);
        }
        
        public string GetSubCatName(string code)
        {
            var result  = (from _subcategory  in _unitOfWork.Repository<SubCategory>().Queryable().Where(s => s.SubCatCode == code && s.RecordStatus == "A")
                                     
                                     select(_subcategory)).FirstOrDefault();
                                
            return result.SubCatName.ToString();
        }
    }
}
