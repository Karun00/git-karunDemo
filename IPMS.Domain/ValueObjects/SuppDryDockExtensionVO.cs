using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class SuppDryDockExtensionVO
    {
        [DataMember]
        public int SuppDryDockExtensionID { get; set; }

        [DataMember]
        public int SuppDryDockID { get; set; }
        [DataMember]
        public string VCN { get; set; }
        [DataMember]
        public string VesselName { get; set; }
        [DataMember]
        public string FromDate { get; set; }
        [DataMember]
        public string ToDate { get; set; }
        [DataMember]
        public string BarkeelCode { get; set; }
        [DataMember]
        public Nullable<int> CargoTons { get; set; }
        [DataMember]
        public Nullable<int> Ballast { get; set; }
        [DataMember]
        public Nullable<int> Bunkers { get; set; }
        [DataMember]
        public string ExtensionDateTime { get; set; }
        //public Nullable<System.DateTime> ExtensionDateTime { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public bool TermsandConditions { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public Nullable<int> WorkflowInstanceID { get; set; }
        [DataMember]
        public string WFStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
        [DataMember]
        public virtual ArrivalNotificationVO ArrivalNotification { get; set; }
        [DataMember]
        public virtual AgentVO Agent { get; set; }
        [DataMember]
        public virtual List<SuppDryDockDocumentVO> SuppDryDockDocuments { get; set; }

        [DataMember]
        public SuppDockUnDockTimeVO SuppDockUnDockTimes { get; set; }
        [DataMember]
        public string VesselAgent { get; set; }
        [DataMember]
        public string ApplicationDateTime { get; set; }

        [DataMember]
        public string Chamber { get; set; }
        [DataMember]
        public Nullable<System.DateTime> EnteredDockDateTime { get; set; }
        [DataMember]
        public Nullable<System.DateTime> OnBlocksDateTime { get; set; }
        [DataMember]
        public Nullable<System.DateTime> DryDockDateTime { get; set; }
        [DataMember]
        public Nullable<System.DateTime> FinishedDockDateTime { get; set; }
        [DataMember]
        public Nullable<System.DateTime> OffBlocksDateTime { get; set; }
        [DataMember]
        public Nullable<System.DateTime> LeftDockDateTime { get; set; }

        [DataMember]
        public string TermsandConditionStatus { get; set; }
        [DataMember]
        public string DockPortCode { get; set; }
        [DataMember]
        public string DockQuayCode { get; set; }
        [DataMember]
        public string DockBerthCode { get; set; }
        [DataMember]
        public string ScheduleFromDate { get; set; }
        [DataMember]
        public string ScheduleToDate { get; set; }
        [DataMember]
        public string ScheduleStatus { get; set; }

        [DataMember]
        public string ScheduleStatusText { get; set; }


        [DataMember]
        public Nullable<decimal> LengthOverallInM { get; set; }
        [DataMember]
        public string ArrDraft { get; set; }

        [DataMember]
        public string IMONo { get; set; }
        [DataMember]
        public Nullable<decimal> GrossRegisteredTonnageInMT { get; set; }

        [DataMember]
        public Nullable<decimal> BeamInM { get; set; }

        [DataMember]
        public string RegisteredName { get; set; }

        [DataMember]
        public string TradingName { get; set; }
        [DataMember]
        public string TelephoneNo1 { get; set; }
        [DataMember]
        public string TelephoneNo2 { get; set; }
        [DataMember]
        public string FaxNo { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string PortName { get; set; }

        [DataMember]
        public string PortCode { get; set; }

    }
}
