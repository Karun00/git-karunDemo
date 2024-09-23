using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    /// <summary>
    /// Value object for VesselArrestImmobilizationSAMSAStop
    /// </summary>
    public class VesselArrestImmobilizationSAMSAStopVO
    {
        public int VAISID { get; set; }

        public string VCN { get; set; }

        public string VesselArrested { get; set; }

        public string ArrestedDate { get; set; }

        public string ArrestedRemarks { get; set; }

        public string VesselReleased { get; set; }

        public string ReleasedDate { get; set; }

        public string ReleasedRemarks { get; set; }

        public string Immobilization { get; set; }

        public string ImmobilizationStartDate { get; set; }

        public string ImmobilizationEndDate { get; set; }

        public string ExactWorkProposed { get; set; }

        public string PollutionPrecautionTaken { get; set; }

        public string ApprovedDate { get; set; }

        public string SAMSAStop { get; set; }

        public string SAMSAStopDate { get; set; }

        public string SAMSAStopRemarks { get; set; }

        public string SAMSACleared { get; set; }

        public string SAMSAClearedDate { get; set; }

        public string SAMSAClearedRemarks { get; set; }

        public string RecordStatus { get; set; }

        public int CreatedBy { get; set; }

        public System.DateTime CreatedDate { get; set; }

        public int ModifiedBy { get; set; }

        public System.DateTime ModifiedDate { get; set; }

        public List<VesselArrestDocumentVO> VesselArrestDocuments { get; set; }

        public List<VesselSAMSAStopDocumentVO> VesselSAMSAStopDocuments { get; set; }

        public VesselVO Vessel { get; set; }

        public AgentVO Agent { get; set; }

        public bool VesselArrestedStatus { get; set; }

        public bool ImmobilizationStatus { get; set; }

        public bool SAMSAStopStatus { get; set; }

        public bool PollutionPrecautionTakenStatus { get; set; }

        public bool VesselReleasedStatus { get; set; }

        public bool SAMSAClearedStatus { get; set; }

        public DateTime ETA { get; set; }

        public string CurrentBerth { get; set; }

        //-- Added by sandeep on 21-02-2015
        public string AnyDangerousGoods { get; set; }
        //-- end

        public string PortCode { get; set; }

        public string PortName { get; set; }

        public string BerthMaintenanceID { get; set; }

        public string MaintenanceTypeCode { get; set; }

        public string MaintBerthCode { get; set; }

        public string FromBollard { get; set; }

        public string ToBollard { get; set; }

        public string PeriodFrom { get; set; }

        public string PeriodTo { get; set; }

        public string VesselName { get; set; }

        public string PortofRegistry { get; set; }

        public string AgentName { get; set; }
        public int AgentID { get; set; }

        public string AgentContactNo { get; set; }
    }
}
