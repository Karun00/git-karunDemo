using System;
using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;


namespace IPMS.Repository
{
    public interface IVesselRegistrationRepository
    {
        /// <summary>
        /// To get vessel  data
        /// </summary>
        /// <returns></returns>
        List<VesselVO> GetVesselRegistrationData(string portCode,int userID);
        /// <summary>
        /// To view vessel registration data from pending tasks 
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        List<Vessel> GetzVesselRegistrationData(string vcn);
        /// <summary>
        /// To get vessel registration data by IMO Number
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        Vessel GetVesselRegistrationByIMO(string value);
        List<PortRegistryVO> GetPortRegistry();
        List<VesselVO> GetSearchVesselData(string imoNo, string vesselName, string portOfRegistry, string vesselNationality, string vesselType, string callSign, string portCode, int userId);
    }
}
