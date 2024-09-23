using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Data;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Repository.Providers.EntityFramework;
using Core.Repository;
using System.Globalization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class PortLimitDataVO
    {
        [DataMember]
        public int PortlimitId { get; set; }
        [DataMember]
        public string PortId { get; set; }
        [DataMember]
        public string mmsi { get; set; }
        [DataMember]
        public string area { get; set; }
        [DataMember]
        public string movement { get; set; }
        [DataMember]
        public string datetime { get; set; }
        [DataMember]
        public string IMONo { get; set; }
        [DataMember]
        public string callsign { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string VCN { get; set; }


        [NotMapped]
        [StoredProcedure("usp_PortLimitData_dml")]
        public class PortLimitData_dml_Proc
        {
            private PortLimitDataVO _objPortLimitData;
            public PortLimitData_dml_Proc(PortLimitDataVO p_PortLimitData)
            {
                _objPortLimitData = p_PortLimitData;
            }

            [StoredProcedureParameter(System.Data.SqlDbType.Int, Direction = ParameterDirection.InputOutput)]
            public int PortlimitId
            {
                get
                {
                    return _objPortLimitData.PortlimitId;
                }
                set
                {
                    _objPortLimitData.PortlimitId = value;
                }
            }

            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.InputOutput)]
            public string PortCode
            {
                get
                {
                    return _objPortLimitData.PortCode;
                }
                set
                {
                    _objPortLimitData.PortCode = value;
                }
            }

            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.InputOutput)]
            public string VCN
            {
                get
                {
                    return _objPortLimitData.VCN;
                }
                set
                {
                    _objPortLimitData.VCN = value;
                }
            }

            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string PortId
            {
                get
                {
                    return _objPortLimitData.PortId;
                }
            }

            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string mmsi
            {
                get
                {
                    return _objPortLimitData.mmsi;
                }
            }


            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string area
            {
                get
                {
                    return _objPortLimitData.area;
                }
            }

            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string movement
            {
                get
                {
                    return _objPortLimitData.movement;
                }
            }


            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string datetime
            {
                get
                {
                    return Convert.ToDateTime(_objPortLimitData.datetime, CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture);
                }
            }


            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string IMONo
            {
                get
                {
                    return _objPortLimitData.IMONo;
                }
            }


            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string callsign
            {
                get
                {
                    return _objPortLimitData.callsign;
                }
            }


            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string name
            {
                get
                {
                    return _objPortLimitData.name;
                }
            }

        }
    }



}

