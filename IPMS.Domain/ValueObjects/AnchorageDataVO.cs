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
    public partial class AnchorageDataVO
    {
        [DataMember]
        public int AnchorageId { get; set; }
        [DataMember]
        public string PortId { get; set; }
        [DataMember]
        public string MMSI { get; set; }
        [DataMember]
        public string Area { get; set; }
        [DataMember]
        public string Assignment { get; set; }
        [DataMember]
        public string DateTime { get; set; }
        [DataMember]
        public string IMONo { get; set; }
        [DataMember]
        public string CallSign { get; set; }
        [DataMember]
        public string VesselName { get; set; }
        [DataMember]
        public string PortCode { get; set; }

        [NotMapped]
        [StoredProcedure("usp_AnchorageData_dml")]
        public class AnchorageData_dml_Proc
        {
            private AnchorageDataVO _objAnchorageData;
            public AnchorageData_dml_Proc(AnchorageDataVO p_AnchorageData)
            {
                _objAnchorageData = p_AnchorageData;
            }

            [StoredProcedureParameter(System.Data.SqlDbType.Int, Direction = ParameterDirection.InputOutput)]
            public int AnchorageId
            {
                get
                {
                    return _objAnchorageData.AnchorageId;
                }
                set
                {
                    _objAnchorageData.AnchorageId = value;
                }
            }

            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.InputOutput)]
            public string PortCode
            {
                get
                {
                    return _objAnchorageData.PortCode;
                }
                set
                {
                    _objAnchorageData.PortCode = value;
                }
            }

            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string PortId
            {
                get
                {
                    return _objAnchorageData.PortId;
                }
            }

            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string MMSI
            {
                get
                {
                    return _objAnchorageData.MMSI;
                }
            }


            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string Area
            {
                get
                {
                    return _objAnchorageData.Area;
                }
            }

            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string Assignment
            {
                get
                {
                    return _objAnchorageData.Assignment;
                }
            }


            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string DateTime
            {
                get
                {
                    return Convert.ToDateTime(_objAnchorageData.DateTime, CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture);
                }
            }


            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string IMONo
            {
                get
                {
                    return _objAnchorageData.IMONo;
                }
            }


            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string CallSign
            {
                get
                {
                    return _objAnchorageData.CallSign;
                }
            }


            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string VesselName
            {
                get
                {
                    return _objAnchorageData.VesselName;
                }
            }

        }

    }

}
