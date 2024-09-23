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
    public class SuppDryDockVO
    {
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
        public Nullable<System.DateTime> ExtensionDateTime { get; set; }
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
        public string EnteredDockDateTime { get; set; }
        [DataMember]
        public string OnBlocksDateTime { get; set; }
        [DataMember]
        public string DryDockDateTime { get; set; }
        [DataMember]
        public string FinishedDockDateTime { get; set; }
        [DataMember]
        public string OffBlocksDateTime { get; set; }
        [DataMember]
        public string LeftDockDateTime { get; set; }

        [DataMember]
        public string TermsandConditionStatus { get; set; }
        [DataMember]
        public string DockPortCode { get; set; }
        [DataMember]
        public string DockQuayCode { get; set; }
        [DataMember]
        public string DockBerthCode { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ScheduleFromDate { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ScheduleToDate { get; set; }
        [DataMember]
        public string ScheduleStatus { get; set; }
        [DataMember]
        public Nullable<decimal> LengthOverallInM { get; set; }
        [DataMember]
        public string ArrDraft { get; set; }

        
        [DataMember]
        public int SuppMiscServiceID { get; set; }     
        [DataMember]
        public int ServiceTypeID { get; set; }
        [DataMember]
        public string Phase { get; set; }
        [DataMember]
        public string FromDateTime { get; set; }
        [DataMember]
        public string ToDateTime { get; set; }
        [DataMember]
        public long Quantity { get; set; }
        [DataMember]
        public long StartMeterReading { get; set; }//added by divya on 1Nov2017
        [DataMember]
        public long EndMeterReading { get; set; }//added by divya on 1Nov2017
        [DataMember]
        public string UOMCode { get; set; }
        //[DataMember]
        //public string MiscRemarks { get; set; }    
        [DataMember]
        public List<SuppMiscServiceVO> SuppMiscServices { get; set; }
        
       
        [DataMember]
        public int SuppDryDockExtensionID { get; set; }
        [DataMember]
        public string BerthName { get; set; }
        //-- Added by sandeep on 21-02-2015
        [DataMember]
        public string AnyDangerousGoods { get; set; }
        //-- end

        [DataMember]
        public string SuppWFStatus { get; set; }
        [DataMember]
        public Nullable<System.DateTime> LeftDockDateTime1 { get; set; }
        [DataMember]
        public string WorkflowTaskCode { get; set; }
        [DataMember]
        public string IsConfirmCancel { get; set; }
        
    }  
}
