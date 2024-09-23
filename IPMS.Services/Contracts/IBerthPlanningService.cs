using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Services
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
        [FaultContract(typeof(Exception))]
        List<QuayVO> GetQuaysInPort();

        /// <summary>
        /// To Get BerthsBollards List
        /// </summary>
        /// <param name="request"></param>
        ///  /// <param name="quayCode"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<BerthsData> GetBerthsWithBollard(string quayCode);



        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<QuayBerthBollardData> GetQuayBerthsBollard();



        /// <summary>
        /// To Get VesselCallMovements List
        /// </summary>
        /// <param name="request"></param>
        /// <param name="fromDate"></param>
        ///<param name="toDate"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<BerthPlanningVO> GetVesselInformation(string fromDate, string toDate);


        [OperationContract]
        [FaultContract(typeof(Exception))]
        Boolean CheckBerthAvailability(string vcn, string vcmId, string quayCode, string fromBerthCode, string fromBollardMeter, string toBerthCode, string toBollardMeter, string fromTime, string toTime);



        /// <summary>
        /// To Get VesselCallMovements List
        /// </summary>
        /// <param name="request"></param>
        /// <param name="fromDate"></param>
        ///<param name="toDate"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<BerthMaintenanceData> GetBerthMaintenance(string fromDate, string toDate);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        UserData GetUserDetails();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<BerthPlanningConfiguration> GetBerthPlanningConfigurations();



        /// <summary>
        /// To Update VesselCallMovementInformation
        /// </summary>
        /// <param name="request"></param>
        /// <param name="plannedvessel"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<BerthPlanningVO> SaveVesselCallMovements(List<BerthPlanningVO> plannedVessels);

        /// <summary>
        /// Author  : Sandeep Appana  
        /// Date    : 10th July 2015
        /// Purpose : To Get GIS Map path
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        GISMapPathVo GetGisMapPath(string portcodegis);

        /// <summary>
        /// To Get VesselCallMovements List
        /// </summary>
        /// <param name="request"></param>
        /// <param name="fromDate"></param>
        ///<param name="toDate"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<BerthPlanningVO> GetVesselInformationGIS( string portcode,string fromDate, string toDate);
        //anchored code
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<AnchorVesselInfoGISVO> GetAnchorVesselInformationGIS(string portcode);
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<BerthedVessels> GetBerthedVesselDetails(string portcode);
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SailedVessels> GetSailedVesselDetails(string portcode);
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<AnchoredVessels> GetAnchoredVesselDetails(string portcode);
    }
}
