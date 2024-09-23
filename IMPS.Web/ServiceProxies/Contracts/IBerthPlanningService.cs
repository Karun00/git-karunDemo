using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Web.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IBerthPlanningService
    {
        /// <summary>
        /// To Get Quays List
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        List<QuayVO> GetQuaysInPort();

        /// <summary>
        /// To Get BerthsBollards List
        /// </summary>
        /// <param name="request"></param>
        ///  /// <param name="quayCode"></param>
        /// <returns></returns>
        [OperationContract]
        List<BerthsData> GetBerthsWithBollard(string quayCode);



        [OperationContract]
        List<QuayBerthBollardData> GetQuayBerthsBollard();


        /// <summary>
        /// To Get VesselCallMovements List
        /// </summary>
        /// <param name="request"></param>
        /// <param name="fromDate"></param>
        ///<param name="toDate"></param>
        /// <returns></returns>
        [OperationContract]
        List<BerthPlanningVO> GetVesselInformation(string fromDate, string toDate);


        [OperationContract]
        Boolean CheckBerthAvailability(string vcn, string vcmId, string quayCode, string fromBerthCode, string fromBollardMeter, string toBerthCode, string toBollardMeter, string fromTime, string toTime);

        /// <summary>
        /// To Update VesselCallMovementInformation
        /// </summary>
        /// <param name="request"></param>
        /// <param name="plannedvessel"></param>
        /// <returns></returns>
        [OperationContract]
        List<BerthPlanningVO> SaveVesselCallMovements(List<BerthPlanningVO> plannedVessels);

        [OperationContract]
        List<BerthMaintenanceData> GetBerthMaintenance(string fromDate, string toDate);

        [OperationContract]
        UserData GetUserDetails();

        [OperationContract]
        List<BerthPlanningConfiguration> GetBerthPlanningConfigurations();



        /// <summary>
        /// To Get Quays List Asynchronously
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //[OperationContract]
        //Task<List<QuayVO>> GetQuaysInPortAsync();

        /// <summary>
        /// To Get BerthsBollards List Asynchronously
        /// </summary>
        /// <param name="request"></param>
        ///  /// <param name="quayCode"></param>
        ///// <returns></returns>
        //[OperationContract]
        //Task<List<BerthsData>> GetBerthsWithBollardAsync(string quayCode);

        /// <summary>
        /// To Get VesselCallMovements List Asynchronously
        /// </summary>
        /// <param name="request"></param>
        /// <param name="fromDate"></param>
        ///<param name="toDate"></param>
        /// <returns></returns>
        //[OperationContract]
        //Task<List<BerthPlanningVO>> GetVesselInformationAsync(string fromDate, string toDate);

        /// <summary>
        /// To Update VesselCallMovementInformation Asynchronously
        /// </summary>
        /// <param name="request"></param>
        /// <param name="plannedvessel"></param>
        /// <returns></returns>
        //[OperationContract]
        //Task<List<BerthPlanningVO>> SaveVesselCallMovementsAsync(List<BerthPlanningVO> plannedVessels);

        /// <summary>
        /// Author  : Sandeep Appana  
        /// Date    : 10th July 2015
        /// Purpose : To Get GIS Map path
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        GISMapPathVo GetGisMapPath(string portcodegis);

        /// <summary>
        /// To Get VesselCallMovements List
        /// </summary>
        /// <param name="request"></param>
        /// <param name="fromDate"></param>
        ///<param name="toDate"></param>
        /// <returns></returns>
        [OperationContract]
        List<BerthPlanningVO> GetVesselInformationGIS(string portcode,string fromDate, string toDate);
        [OperationContract]
        List<BerthedVessels> GetBerthedVesselDetails(string portcode);
        [OperationContract]
        List<SailedVessels> GetSailedVesselDetails(string portcode);
        [OperationContract]
        List<AnchoredVessels> GetAnchoredVesselDetails(string portcode);
        //Anchored code

        [OperationContract]
        List<AnchorVesselInfoGISVO> GetAnchorVesselInformationGIS(string portcode);
    }
}
