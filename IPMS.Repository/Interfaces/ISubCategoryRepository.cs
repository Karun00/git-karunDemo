using System;
using IPMS.Domain.Models;
using System.Collections.Generic;
using IPMS.Domain.ValueObjects;

namespace IPMS.Repository
{
    public interface ISubCategoryRepository
    {
        List<SubCategory> UserTypes();
        List<SubCategory> CargoTypes();
        //By mahesh
        List<SubCategory> WarpSides();

        List<SubCategory> DangerousGoods();
        List<SubCategory> DockTypes();
        List<SubCategory> DocumentsTypes();
        List<SubCategory> LicenseRequestDocumentsTypes();
        List<SubCategory> PilotExemptionDocumentsTypes();
        List<SubCategory> Purpose();
        List<SubCategory> ReasonForVisit();
        List<SubCategory> Uoms();
        List<SubCategory> CargoUoms();
        List<SubCategory> BunkersRequiredType();
        List<SubCategory> BunkersMethod();
        List<SubCategory> SideAlongSideTypes();
        List<SubCategory> MomentTypes();
        List<SubCategory> TankerTypes();
        List<SubCategory> BunkerService();
        List<SubCategory> Commoditys();
        List<SubCategory> CraftType();
        List<SubCategory> EngineType();
        List<SubCategory> CraftNationality();
        List<SubCategory> FuelType();
        List<SubCategory> Pilot_Nationality();
        List<SubCategory> CraftCommissionStatus();
        List<SubCategory> ClassificationSociety();
        List<SubCategory> PortOfRegistry();
        List<SubCategory> LicensingRequestTypes();
        List<SubCategory> PilotRoleCode();

        List<SubCategory> PropulsionType();

        List<SubCategory> PrivilegeTypes();

        List<SubCategory> ParticularsTypes();
        List<SubCategory> CalenderTypes();
        List<SubCategory> ReasonTypes();        


        // Nivedita - Berth Pre Scheduling
        List<SubCategoryCodeNameVO> GetCargoTypes();
        List<SubCategoryCodeNameVO> GetMovementsStatus();
        List<SubCategoryCodeNameVO> GetVesselTypes();
        List<SubCategoryCodeNameVO> GetReasonsForVisit();
        //Statement Of Facts
        List<SubCategoryCodeNameVO> GetOperations();
        List<SubCategoryCodeNameVO> GetDelayTypes();
    
        List<SubCategoryCodeNameVO> GetPilotCapacity();
        List<SubCategoryCodeNameVO> GetNaturetypes();
        List<SubCategory> GetKeyEvents();

        List<SubCategoryCodeNameVO> GetRecordingofIncident();
        List<SubCategoryCodeNameVO> GetGeneralAgencies();
        List<SubCategoryCodeNameVO> GetOccupationalHygieneAgencies();
        List<SubCategoryCodeNameVO> GetTypeofContact();
        List<SubCategoryCodeNameVO> GetStandardActs();
        List<SubCategoryCodeNameVO> GetSubstandardConditions();
        List<SubCategoryCodeNameVO> GetPersonalFactors();
        List<SubCategoryCodeNameVO> GetJobFactors();
        List<SubCategoryCodeNameVO> GetControlStepsToPreventRecurrence();
        List<SubCategoryCodeNameVO> GetFireDepartment();
        List<SubCategoryCodeNameVO> GetIncidentClassification();
        List<SubCategoryCodeNameVO> GetDiscriptionofExposedRisk();
        List<SubCategoryCodeNameVO> GetSection_Record_Incident();
        List<SubCategoryCodeNameVO> GetWeatherTypes();


        List<SubCategoryCodeNameVO> GetPermittypes();
        List<SubCategoryCodeNameVO> Getapplicationcategory();
        List<SubCategoryCodeNameVO> GetAreaOfConstruction();
        List<SubCategoryCodeNameVO> GetTemporaryPermits();
        List<SubCategoryCodeNameVO> GetAdhocPermits();
        List<SubCategoryCodeNameVO> GetOperatedTerminals();
        List<SubCategoryCodeNameVO> GetPermitDocumentTypes();
        List<SubCategoryCodeNameVO> GetSecurityDocumentTypes();
        List<SubCategoryCodeNameVO> GetPermitRequeirements();
        List<SubCategoryCodeNameVO> GetAccessGates();
        List<SubCategoryCodeNameVO> GetPermitRequeirementstypes();
        List<SubCategoryCodeNameVO> GetPermitRequeirementsDuration();
        List<SubCategoryCodeNameVO> GetPermitCodes();
        List<SubCategoryCodeNameVO> GetPermitStatus();

        List<SubCategoryCodeNameVO> GetTemporaryPermitsForIndividual();
        List<SubCategoryCodeNameVO> GetAccessAreasForRB();
        List<SubCategoryCodeNameVO> GetAccessAreasForCT();
        List<SubCategoryCodeNameVO> GetAccessAreasForDB();
        List<SubCategoryCodeNameVO> GetAccessAreasForEL();
        List<SubCategoryCodeNameVO> GetAccessAreasForNG();
        List<SubCategoryCodeNameVO> GetAccessAreasForMB();
        List<SubCategoryCodeNameVO> GetAccessAreasForSB();
        List<SubCategoryCodeNameVO> GetAccessAreasForPE();
        List<SubCategoryCodeNameVO> GetContractorTemporaryPermits();
        List<SubCategoryCodeNameVO> GetContractorPermanentPermits();
        List<SubCategoryCodeNameVO> GetPermanentPermitsForIndividual();
        List<SubCategoryCodeNameVO> GetReasonsForPermit();
        List<SubCategoryCodeNameVO> GetContractorReasonForPermit();
        List<SubCategoryCodeNameWithSupCatCodeVO> GetSubAccessAreasForRB(string supCatCode);

        List<SubCategoryCodeNameWithSupCatCodeVO> GetAreas(string supCatCode);
        List<SubCategoryCodeNameVO> GetRevenueAccountStatus();


        //  object LicensingRequestTypes();
        List<SubCategory> MaintenanceType();


        ////By Mahesh :Get WorkFlowEvents.....
        List<SubCategory> WorkFlowEvents();

        //By Suresh : Cahnge Of Agent
        List<SubCategory> ReasonForTransfer();

        //By Suresh : Vessel Registration
        List<SubCategory> ClassificationSocity();
        List<SubCategory> VesselTypes();
        List<SubCategory> VesselNationality();
        List<SubCategory> VesselCertificateNames();
        List<SubCategory> VesselSafeWorkingLoad();

        //By Suresh:Automated Slotting 
        List<SubCategory> VesselMovementTypes();

        List<SubCategory> GetOilTypes();
        List<SubCategory> GetGradeTypes();
        List<SubCategory> GetUOMTypes();
        List<SubCategory> GetTptUOMTypes();
        List<SubCategory> GetSupplyingModes();

        List<SubCategory> GetFinancialYears();
        List<SubCategory> GetDeploymentTypes();
        List<SubCategory> GetDredgingColors();
        List<SubCategory> GetDredgingTypes();

        List<SubCategory> GetDepartments();

        List<SubCategory> GetPhaseTypes();

        List<SubCategory> GetVesselagentChangeDOCTypes();

        List<SubCategory> GetFuelUOMTypes();

        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 11th Mar 2015
        /// Purpose : To get Diving Request Reasons details in subcategory
        /// </summary>
        /// <param name="supcatcode"></param>
        /// <returns></returns>
        List<SubCategoryCodeNameVO> GetSubCategoryDetailsBySupCatCode(string supcatcode);

        List<SubCategory> GetReasons();

        List<SubCategory> GetServiceDocumentTypes();

        List<SubCategoryCodeNameVO> GetMopsDelays();
        // Delay Reasons Code
        List<SubCategoryCodeNameVO> GetDelayReasons();

        List<SubCategory> MarpolTypes();

        string GetSubCatName(string code);
    }
}
