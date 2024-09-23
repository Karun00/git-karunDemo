using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using IPMSFeedService.Models;
using IPMSFeedService.ValueObjects;
using System.Data.Common;
using System.Globalization;

namespace IPMSFeedService.Repository
{
    public class IpmsFeedRepository : IIpmsFeedRepository
    {

        private readonly IApplicationDbContext _ipmsContext;

        public IpmsFeedRepository()
        {
            _ipmsContext = new ApplicationDbContexts();
        }
        public List<IPMSFeedVO> GetIpmsServiceFeedDetails(string portCode, string movementFromDate, string movementToDate, string vcn, string imono, string vesselname)
        {
            var spParameters = new DbParameter[]
            {
                new SqlParameter {ParameterName = "@PortCode", Value = portCode},
                new SqlParameter {ParameterName = "@VCN", Value = vcn},
                new SqlParameter {ParameterName = "@IMONO", Value = imono},
                new SqlParameter {ParameterName = "@VesselName", Value = vesselname},
                new SqlParameter {ParameterName = "@MovementFrmDt", Value = movementFromDate},
                new SqlParameter {ParameterName = "@MovementToDt", Value = movementToDate}
            };

            #region Input parameters validation

            if (portCode != null && portCode.Length > 2)
                throw new Exception("Business Validation : Invalid portcode");

            if (!string.IsNullOrEmpty(portCode) && string.IsNullOrEmpty(vcn) && string.IsNullOrEmpty(imono) && string.IsNullOrEmpty(vesselname) && string.IsNullOrEmpty(movementFromDate) && string.IsNullOrEmpty(movementToDate))
                throw new Exception("Business Validation : Please provide sufficient parameters.");

            if (string.IsNullOrEmpty(portCode))
            {
                spParameters[0].Value = DBNull.Value;
            }
            else if (portCode.Length != 2)
            {
                throw new Exception("Business Validation : Invalid PortCode");
            }
            if (!string.IsNullOrEmpty(vcn))
            {
                if (vcn.Length != 12 || !vcn.StartsWith("VCN"))
                    throw new Exception("Business Validation : Invalid VCN");
            }
            else
            {
                spParameters[1].Value = DBNull.Value;
            }

            if (string.IsNullOrEmpty(imono))
                spParameters[2].Value = DBNull.Value;

            if (string.IsNullOrEmpty(vesselname))
                spParameters[3].Value = DBNull.Value;

            if (!string.IsNullOrEmpty(movementFromDate))
            {
                if (movementFromDate.Length != 10)
                    throw new Exception("Business Validation : Invalid Fromdate");
            }
            else
            {
                spParameters[4].Value = DBNull.Value;
            }
            if (!string.IsNullOrEmpty(movementToDate))
            {
                if (movementToDate.Length != 10)
                    throw new Exception("Business Validation : Invalid ToDate");
            }
            else
            {
                spParameters[5].Value = DBNull.Value;
            }

            if (string.IsNullOrEmpty(movementFromDate) && !string.IsNullOrEmpty(movementToDate))
                throw new Exception("Business Validation : Movement Fromdate is mandatory");

            if (!string.IsNullOrEmpty(movementFromDate) && string.IsNullOrEmpty(movementToDate))
                throw new Exception("Business Validation : Movement Todate is mandatory");

            if (!string.IsNullOrEmpty(movementFromDate) && !string.IsNullOrEmpty(movementToDate))
            {
                if (string.IsNullOrEmpty(portCode))
                {
                    throw new Exception("Business Validation : Portcode is mandatory");
                }

                DateTime _movementFromDate;
                DateTime _movementToDate;
                try
                {
                    _movementFromDate = Convert.ToDateTime(movementFromDate, CultureInfo.InvariantCulture);
                    _movementToDate = Convert.ToDateTime(movementToDate, CultureInfo.InvariantCulture);

                }
                catch (Exception)
                {
                    throw new Exception("Business Validation : Invalid date format, default format eg: 2018-11-21 (yyyy-mm-dd)");
                }
                if (Math.Abs((_movementToDate - _movementFromDate).TotalDays) > 30)
                {
                    throw new Exception("Business Validation : Movement From and to date duration should be below 30 days");
                }
            }
            #endregion

            string sqlProc = "exec USP_IPMS_FEED_SERVICE  @PortCode, @VCN, @IMONO, @VesselName, @MovementFrmDt, @MovementToDt";
            var ipmsfeedDetails = ((IObjectContextAdapter)_ipmsContext).ObjectContext.ExecuteStoreQuery<IPMSFeedVO>(sqlProc, spParameters).ToList();
            return ipmsfeedDetails;
        }

        public List<IPMSANFeedVO> GetIpmsANFeedDetails(string portCode, string etaFromDate, string etaToDate, string vcn, string imono, string vesselname)
        {
            var spParameters = new DbParameter[]
            {
                new SqlParameter {ParameterName = "@PortCode", Value = portCode},
                new SqlParameter {ParameterName = "@VCN", Value = vcn},
                new SqlParameter {ParameterName = "@IMONO", Value = imono},
                new SqlParameter {ParameterName = "@VesselName", Value = vesselname},
                new SqlParameter {ParameterName = "@ETAFrmDt", Value = etaFromDate},
                new SqlParameter {ParameterName = "@ETAToDt", Value = etaToDate}
            };

            #region Input parameters validation
            if (portCode != null && portCode.Length > 2)
                throw new Exception("Business Validation : Invalid portcode");

            if (!string.IsNullOrEmpty(portCode) && string.IsNullOrEmpty(vcn) && string.IsNullOrEmpty(imono) && string.IsNullOrEmpty(vesselname) && string.IsNullOrEmpty(etaFromDate) && string.IsNullOrEmpty(etaToDate))
                throw new Exception("Business Validation : Please provide sufficient parameters.");


            if (string.IsNullOrEmpty(portCode))
            {
                spParameters[0].Value = DBNull.Value;
            }
            else if (portCode.Length != 2)
            {
                throw new Exception("Business Validation : Invalid PortCode");
            }


            if (!string.IsNullOrEmpty(vcn))
            {
                if (vcn.Length != 12 || !vcn.StartsWith("VCN"))
                    throw new Exception("Business Validation : Invalid VCN");
            }
            else
            {
                spParameters[1].Value = DBNull.Value;
            }

            if (string.IsNullOrEmpty(imono))
                spParameters[2].Value = DBNull.Value;

            if (string.IsNullOrEmpty(vesselname))
                spParameters[3].Value = DBNull.Value;

            if (!string.IsNullOrEmpty(etaFromDate))
            {
                if (etaFromDate.Length != 10)
                    throw new Exception("Business Validation : Invalid Fromdate");
            }
            else
            {
                spParameters[4].Value = DBNull.Value;
            }
            if (!string.IsNullOrEmpty(etaToDate))
            {
                if (etaToDate.Length != 10)
                    throw new Exception("Business Validation : Invalid ToDate");
            }
            else
            {
                spParameters[5].Value = DBNull.Value;
            }

            if (string.IsNullOrEmpty(etaFromDate) && !string.IsNullOrEmpty(etaToDate))
                throw new Exception("Business Validation : ETA Fromdate is mandatory");

            if (!string.IsNullOrEmpty(etaFromDate) && string.IsNullOrEmpty(etaToDate))
                throw new Exception("Business Validation : ETA Todate is mandatory");

            if (!string.IsNullOrEmpty(etaFromDate) && !string.IsNullOrEmpty(etaToDate))
            {
                if (string.IsNullOrEmpty(portCode))
                {
                    throw new Exception("Business Validation : Portcode is mandatory");
                }

                DateTime _etaFromDate;
                DateTime _etaToDate;
                try
                {
                    _etaFromDate = Convert.ToDateTime(etaFromDate, CultureInfo.InvariantCulture);
                    _etaToDate = Convert.ToDateTime(etaToDate, CultureInfo.InvariantCulture);

                }
                catch (Exception)
                {
                    throw new Exception("Business Validation : Invalid date format, default format eg: 2018-11-21 (yyyy-mm-dd)");
                }

                if (Math.Abs((_etaToDate - _etaFromDate).TotalDays) > 30)
                {
                    throw new Exception("Business Validation : ETA From and to date duration should be below 30 days");

                }
            }
            #endregion

            string sqlProc = "exec USP_IPMS_ANFEEDSERVICE @PortCode, @VCN, @IMONO, @VesselName, @ETAFrmDt, @ETAToDt";

            var ipmsAnDetails = ((IObjectContextAdapter)_ipmsContext).ObjectContext.ExecuteStoreQuery<IPMSANFeedVO>(sqlProc, spParameters).ToList();
            return ipmsAnDetails;
        }

        public List<IPMSLocationVO> GetIpmsFeedLocationDetails(string portCode, string portLimitInFromDate, string portLimitInToDate, string vcn, string imono, string vesselname)
        {
            var spParameters = new DbParameter[]
            {
                new SqlParameter {ParameterName = "@PortCode", Value = portCode},
                new SqlParameter {ParameterName = "@VCN", Value = vcn},
                new SqlParameter {ParameterName = "@IMONO", Value = imono},
                new SqlParameter {ParameterName = "@VesselName", Value = vesselname},
                new SqlParameter {ParameterName = "@portLimitInFromDate", Value = portLimitInFromDate},
                new SqlParameter {ParameterName = "@portLimitInToDate", Value = portLimitInToDate}
            };

            #region Input parameters validation

            if (portCode != null && portCode.Length > 2)
                throw new Exception("Business Validation : Invalid portcode");

            if (!string.IsNullOrEmpty(portCode) && string.IsNullOrEmpty(vcn) && string.IsNullOrEmpty(imono) && string.IsNullOrEmpty(vesselname) && string.IsNullOrEmpty(portLimitInFromDate) && string.IsNullOrEmpty(portLimitInFromDate))
                throw new Exception("Business Validation : Please provide sufficient parameters.");

            if (string.IsNullOrEmpty(portCode))
            {
                spParameters[0].Value = DBNull.Value;
            }
            else if (portCode.Length != 2)
            {
                throw new Exception("Business Validation : Invalid PortCode");
            }
            if (!string.IsNullOrEmpty(vcn))
            {
                if (vcn.Length != 12 || !vcn.StartsWith("VCN"))
                    throw new Exception("Business Validation : Invalid VCN");
            }
            else
            {
                spParameters[1].Value = DBNull.Value;
            }

            if (string.IsNullOrEmpty(imono))
                spParameters[2].Value = DBNull.Value;

            if (string.IsNullOrEmpty(vesselname))
                spParameters[3].Value = DBNull.Value;

            if (!string.IsNullOrEmpty(portLimitInFromDate))
            {
                if (portLimitInFromDate.Length != 10)
                    throw new Exception("Business Validation : Invalid Fromdate");
            }
            else
            {
                spParameters[4].Value = DBNull.Value;
            }
            if (!string.IsNullOrEmpty(portLimitInFromDate))
            {
                if (portLimitInFromDate.Length != 10)
                    throw new Exception("Business Validation : Invalid ToDate");
            }
            else
            {
                spParameters[5].Value = DBNull.Value;
            }

            if (string.IsNullOrEmpty(portLimitInFromDate) && !string.IsNullOrEmpty(portLimitInToDate))
                throw new Exception("Business Validation : Movement Fromdate is mandatory");

            if (!string.IsNullOrEmpty(portLimitInFromDate) && string.IsNullOrEmpty(portLimitInToDate))
                throw new Exception("Business Validation : Movement Todate is mandatory");

            if (!string.IsNullOrEmpty(portLimitInFromDate) && !string.IsNullOrEmpty(portLimitInToDate))
            {
                if (string.IsNullOrEmpty(portCode))
                {
                    throw new Exception("Business Validation : Portcode is mandatory");
                }

                DateTime _portLimitInFromDate;
                DateTime _portLimitInToDate;
                try
                {
                    _portLimitInFromDate = Convert.ToDateTime(portLimitInFromDate, CultureInfo.InvariantCulture);
                    _portLimitInToDate = Convert.ToDateTime(portLimitInToDate, CultureInfo.InvariantCulture);

                }
                catch (Exception)
                {
                    throw new Exception("Business Validation : Invalid date format, default format eg: 2018-11-21 (yyyy-mm-dd)");
                }
                if (Math.Abs((_portLimitInToDate - _portLimitInFromDate).TotalDays) > 30)
                {
                    throw new Exception("Business Validation : Movement From and to date duration should be below 30 days");
                }
            }
            #endregion

            string sqlProc = "exec usp_IPMS_FeedLocationService  @PortCode, @VCN, @IMONO, @VesselName, @portLimitInFromDate, @portLimitInToDate";
            var ipmsfeedLocationDetails = ((IObjectContextAdapter)_ipmsContext).ObjectContext.ExecuteStoreQuery<IPMSLocationVO>(sqlProc, spParameters).ToList();
            return ipmsfeedLocationDetails;
        }


    }
}