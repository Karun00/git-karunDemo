using Core.Repository;
using IPMS.Domain;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace IPMS.Repository
{
    public class VesselSAPPostingRepository : IVesselSAPPostingRepository
    {
        private IUnitOfWork _unitOfWork;
    //    private readonly ILog log;

        public VesselSAPPostingRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            XmlConfigurator.Configure();
           // log = LogManager.GetLogger(typeof(VesselSAPPostingRepository));
        }

        public List<SAPPostingVO> GetSAPVesselPostGrid(string PortCode)
        {
            var _PortCode = new SqlParameter("@p_PortCode", PortCode);

            var sapResult = _unitOfWork.SqlQuery<SAPPostingVO>("usp_VesselSAPPostingGrid @p_PortCode", _PortCode).ToList();

            return sapResult;
        }


        /// <summary>
        /// To Get vessels list
        /// </summary>
        /// <returns></returns>
        public List<VesselSAPPostingVO> GetVesselsList(string SearchColumn, string searchValue, string PortCode)
        {
            var _SerchColumn = new SqlParameter("@p_SrchOn", SearchColumn);
            var _searchValue = new SqlParameter("@p_SearchText", searchValue);
            var _PortCode = new SqlParameter("@p_PortCode", PortCode);

            var vessels = _unitOfWork.SqlQuery<VesselSAPPostingVO>("dbo.usp_GetSapPostVessel @p_SearchText, @p_PortCode, @p_SrchOn",
                _searchValue, _PortCode, _SerchColumn).ToList();

            foreach (var vessel in vessels)
            {                           
                vessel.TransmitData = PostToSAP.VesselCreation;
                vessel.TransmitData = vessel.TransmitData
                            .Replace("#ICALLSIGN#", vessel.CallSign != null ? vessel.CallSign.ToString(CultureInfo.InvariantCulture) : "")
                            .Replace("#ICITY#", vessel.PortOfRegistry != null ? vessel.PortOfRegistry.ToString(CultureInfo.InvariantCulture) : "")
                            .Replace("#ICOUNTRY#", vessel.NationalityCode != null ? vessel.NationalityCode.ToString(CultureInfo.InvariantCulture) : "")
                            .Replace("#IDATE#", vessel.IDATE != null ? vessel.IDATE.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) : "")
                            .Replace("#IIMO#", vessel.IMONo != null ? vessel.IMONo.ToString() : "")
                            .Replace("#ILENGTH#", vessel.LengthOverallInM != null ? vessel.LengthOverallInM.ToString().Replace(',', '.') : "")
                            .Replace("#IPOSTAL#", "")// 0157
                            .Replace("#ITONNAGE#", vessel.GrossRegisteredTonnageInMT != null ? vessel.GrossRegisteredTonnageInMT.ToString().Replace(',', '.') : "")
                            .Replace("#IVESIND#", "02") // 01/02
                            .Replace("#IVESNAME#", vessel.VSNAME != null ? vessel.VSNAME.ToString(CultureInfo.InvariantCulture) : "")
                            .Replace("#IVESTYPE#", vessel.VesselType != null ? vessel.VesselType.ToString(CultureInfo.InvariantCulture) : "")
                            .Replace("#VKORG#", vessel.VKORG != null ? vessel.VKORG.ToString(CultureInfo.InvariantCulture) : "");
            }

            return vessels;
        }

        /// <summary>
        /// To Post Vessel SAP Data
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public VesselSAPPostingVO PostVesselSAP(VesselSAPPostingVO value, string PortCode)
        {


            return value;
        }
    }
}













