using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    public class VesselCallVO
    {

        public int VesselCallID { get; set; }

        public string VCN { get; set; }

        public int RecentAgentID { get; set; }

        public System.DateTime ETA { get; set; }

        public System.DateTime ETD { get; set; }

        public System.DateTime ETB { get; set; }

        public System.DateTime ETUB { get; set; }

        public string ATA { get; set; }

        public string ATD { get; set; }

        public string VesselStatus { get; set; }

        public Nullable<System.DateTime> ATB { get; set; }

        public Nullable<System.DateTime> ATUB { get; set; }

        public string BreakWaterIn { get; set; }

        public string BreakWaterOut { get; set; }

        public string PortLimitIn { get; set; }

        public string PortLimitOut { get; set; }

        public System.DateTime AnchorUp { get; set; }

        public System.DateTime AnchorDown { get; set; }

        public string FromPositionPortCode { get; set; }

        public string FromPositionQuayCode { get; set; }

        public string FromPositionBerthCode { get; set; }

        public string FromPositionBollardCode { get; set; }

        public string ToPositionPortCode { get; set; }

        public string ToPositionQuayCode { get; set; }

        public string ToPositionBerthCode { get; set; }

        public string ToPositionBollardCode { get; set; }

        public string RecordStatus { get; set; }

        public int CreatedBy { get; set; }

        public System.DateTime CreatedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public System.DateTime ModifiedDate { get; set; }

        public VesselVO Vessel { get; set; }

        public ArrivalNotificationVO ArrivalNotification { get; set; }
        public ICollection<VesselCallAnchorageVO> VesselCallAnchorages { get; set; }
        public List<VesselCallAnchorageVO> VesselCallAnchorages1 { get; set; }

        public string VesselName { get; set; }
        public string VesselType { get; set; }
        public string ReasonForVisit { get; set; }
        public string VesselTypeName { get; set; }
        public string ReasonForVisitName { get; set; }

    }

    public class VcnCloseVO
    {
        public int VesselCallID { get; set; }

        public string VCN { get; set; }

        public int RecentAgentID { get; set; }

        public System.DateTime ETA { get; set; }

        public System.DateTime ETD { get; set; }

        public Nullable<System.DateTime> ETB { get; set; }

        public Nullable<System.DateTime> ETUB { get; set; }

        public Nullable<System.DateTime> ATA { get; set; }

        public Nullable<System.DateTime> ATD { get; set; }

        public Nullable<System.DateTime> ATB { get; set; }

        public Nullable<System.DateTime> ATUB { get; set; }

        public Nullable<System.DateTime> BreakWaterIn { get; set; }

        public Nullable<System.DateTime> BreakWaterOut { get; set; }

        public Nullable<System.DateTime> PortLimitIn { get; set; }

        public Nullable<System.DateTime> PortLimitOut { get; set; }

        public Nullable<System.DateTime> AnchorUp { get; set; }

        public Nullable<System.DateTime> AnchorDown { get; set; }

        public string FromPositionPortCode { get; set; }

        public string FromPositionQuayCode { get; set; }

        public string FromPositionBerthCode { get; set; }

        public string FromPositionBollardCode { get; set; }

        public string ToPositionPortCode { get; set; }

        public string ToPositionQuayCode { get; set; }

        public string ToPositionBerthCode { get; set; }

        public string ToPositionBollardCode { get; set; }

        public string RecordStatus { get; set; }

        public int CreatedBy { get; set; }

        public System.DateTime CreatedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public System.DateTime ModifiedDate { get; set; }

        public int NoofTimesETAChanged { get; set; }

        public string VesselStatus { get; set; }


    }
}
