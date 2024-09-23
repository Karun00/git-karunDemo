using Core.Repository.Providers.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class VesselETAChangeVO
    {
        [DataMember]
        public int VesselETAChangeID { get; set; }
        [DataMember]
        public string VCN { get; set; }
        [DataMember]
        public string VesselName { get; set; }
        [DataMember]
        public string TelephoneNo1 { get; set; }
        [DataMember]
        public string PortOfRegistry { get; set; }
        [DataMember]
        public string VCN_VesselName { get; set; }
        [DataMember]
        public System.DateTime Date { get; set; }
        [DataMember]
        public string VesselAgent { get; set; }
        [DataMember]
        public string ReportingTo { get; set; }
        [DataMember]
        public string AgentName { get; set; }
        [DataMember]
        public decimal? GRT { get; set; }
        [DataMember]
        public decimal? LOA { get; set; }
        [DataMember]
        public string Draft { get; set; }
        [DataMember]
        public string VoyageIn { get; set; }
        [DataMember]
        public string VoyageOut { get; set; }

        [DataMember]
        public string ETA { get; set; }

        [DataMember]
        public string ETD { get; set; }

        [DataMember]
        public string NewETA { get; set; }

        [DataMember]
        public string NewETD { get; set; }

        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public int NoofTimesETAChanged { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public string CreatedDateAndTime { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
        [DataMember]
        public int VesselCallMovementID { get; set; }
        [DataMember]
        public string OldETA { get; set; }
        [DataMember]
        public string OldETD { get; set; }
        [DataMember]
        public string AnyDangerousGoodsonBoard { get; set; }

        [DataMember]
        public string ATB { get; set; }
        [DataMember]
        public string ATUB { get; set; }
        [DataMember]
        public string ATA { get; set; }
        [DataMember]
        public string ATD { get; set; }

        [NotMapped]
        public string PortCode { get; set; }
        [NotMapped]
        public string PortName { get; set; }

        [DataMember]
        public int SuppServiceRequestID { get; set; }
        [DataMember]
        public string CancelRemarks { get; set; }
        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public DateTime ETADateTime { get; set; }

        [DataMember]
        public DateTime ETDDateTime { get; set; }

        [DataMember]
        public DateTime NewETADateTime { get; set; }

        [DataMember]
        public DateTime NewETDDateTime { get; set; }

        [DataMember]
        public DateTime OldETADateTime { get; set; }

        [DataMember]
        public DateTime OldETDDateTime { get; set; }

        [DataMember]
        public DateTime? ATBDateTime { get; set; }

        [DataMember]
        public DateTime? ATUBDateTime { get; set; }

        [DataMember]
        public DateTime? ATADateTime { get; set; }

        [DataMember]
        public DateTime? ATDDateTime { get; set; }

        [DataMember]
        public string PlanDateTimeOfBerth { get; set; }

        [DataMember]
        public string PlanDateTimeToStartCargo { get; set; }

        [DataMember]
        public string PlanDateTimeToCompleteCargo { get; set; }

        [DataMember]
        public string PlanDateTimeToVacateBerth { get; set; }

        [DataMember]
        public Nullable<System.DateTime> PlanDateTimeOfBerthDT { get; set; }

        [DataMember]
        public Nullable<System.DateTime> PlanDateTimeToStartCargoDT { get; set; }

        [DataMember]
        public Nullable<System.DateTime> PlanDateTimeToCompleteCargoDT { get; set; }

        [DataMember]
        public Nullable<System.DateTime> PlanDateTimeToVacateBerthDT { get; set; }



        [DataMember]
        public Nullable<System.DateTime> OldPlanDateTimeOfBerth { get; set; }

        [DataMember]
        public Nullable<System.DateTime> OldPlanDateTimeToStartCargo { get; set; }

        [DataMember]
        public Nullable<System.DateTime> OldPlanDateTimeToCompleteCargo { get; set; }

        [DataMember]
        public Nullable<System.DateTime> OldPlanDateTimeToVacateBerth { get; set; }
        [DataMember]
        public List<ArrivalReason> ArrivalReasons { get; set; }

        [DataMember]
        public string ArrivalReasonforVisit { get; set; }

        [NotMapped]
        [StoredProcedure("usp_SuppServRequestCancel")]
        public class SuppServRequestCancel_proc
        {
            private VesselETAChangeVO objVesselETAChangeVO;
            public SuppServRequestCancel_proc(VesselETAChangeVO pVesselETAChangeVO)
            {
                objVesselETAChangeVO = pVesselETAChangeVO;
            }

            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string p_VCN
            {
                get
                {
                    return objVesselETAChangeVO.VCN;
                }
            }

            [StoredProcedureParameter(SqlDbType.Int, Direction = ParameterDirection.Input)]
            public int p_SuppServiceRequestID
            {
                get
                {
                    return objVesselETAChangeVO.SuppServiceRequestID;
                }
            }

            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string p_Remarks
            {
                get
                {
                    return objVesselETAChangeVO.CancelRemarks;
                }
            }

            [StoredProcedureParameter(SqlDbType.Int, Direction = ParameterDirection.Input)]
            public int p_UserId
            {
                get
                {
                    return objVesselETAChangeVO.CreatedBy;
                }
            }
        }
    }
}
