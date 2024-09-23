using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public partial class ServiceRequestVCNsForDryDockExts : EntityBase
    {
        [DataMember]
        public string VCN { get; set; }
        [DataMember]
        public int VesselID { get; set; }
        [DataMember]
        public string VesselName { get; set; }
        [DataMember]
        public string ReasonForVisit { get; set; }
        [DataMember]
        public string VesselType { get; set; }
        [DataMember]
        public string CallSign { get; set; }
        [DataMember]
        public System.DateTime ETA { get; set; }
        [DataMember]
        public System.DateTime ETD { get; set; }
        [DataMember]
        public string IMONo { get; set; }
        [DataMember]
        //public Nullable<long> LengthOverallInM { get; set; } 
        public Nullable<decimal> LengthOverallInM { get; set; }
        //[DataMember]
        //public Nullable<long> BeamInM { get; set; } 
        [DataMember]
        public Nullable<decimal> BeamInM { get; set; }
        //[DataMember]
        //public Nullable<long> GrossRegisteredTonnageInMT { get; set; }
        [DataMember]
        public Nullable<decimal> GrossRegisteredTonnageInMT { get; set; }
        //[DataMember]
        //public Nullable<long> DeadWeightTonnageInMT { get; set; } 
        [DataMember]
        public Nullable<decimal> DeadWeightTonnageInMT { get; set; }
        [DataMember]
        public string LastPortOfCall { get; set; }
        [DataMember]
        public string NextPortOfCall { get; set; }
        [DataMember]
        public string Tidal { get; set; }
        [DataMember]
        public string DaylightRestriction { get; set; }
        [DataMember]
        public string VesselNationality { get; set; }
        [DataMember]
        public string ArrDraft { get; set; }
        [DataMember]
        public string VoyageIn { get; set; }
        [DataMember]
        public string VoyageOut { get; set; }
        //////////////////////////////////////////

        [DataMember]
        public string RegisteredName { get; set; }
        [DataMember]
        public string TelephoneNo1 { get; set; }
        [DataMember]
        public string FaxNo { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string SurName { get; set; }
        [DataMember]
        public string CellularNo { get; set; }
        [DataMember]
        public string EmailID { get; set; }



        // -- Added by sandeep on 24-8-2014
        [DataMember]
        public string AnyDangerousGoodsonBoard { get; set; }
        [DataMember]
        public string DangerousGoodsClass { get; set; }
        [DataMember]
        public string UNNo { get; set; }

        // -- end

        [DataMember]
        public string CurrentBerth { get; set; }
        [DataMember]
        public string CurrentFromBollardName { get; set; }
        [DataMember]
        public string CurrentToBollardName { get; set; }
        [DataMember]
        public string CurrentBerthCode { get; set; }

        [DataMember]
        public int SuppDryDockID { get; set; }

        [DataMember]
        public virtual List<SuppDryDockDocumentVO> SuppDryDockDocuments { get; set; }

        [DataMember]
        public string ScheduleStatusText { get; set; }

        [DataMember]
        public int WorkflowInstanceID { get; set; }

        [DataMember]
        public int SuppDryDockExtensionID { get; set; }

        [DataMember]
        public string TradingName { get; set; }

        [DataMember]
        public string TelephoneNo2 { get; set; }

        [DataMember]
        public int AgentID { get; set; }

        [DataMember]
        public Nullable<DateTime> ExtensionDateTime { get; set; }

        [DataMember]
        public string ScheduleStatus { get; set; }

        [DataMember]
        public string Chamber { get; set; }

        [DataMember]
        public Nullable<DateTime> ScheduleToDate { get; set; }

        [DataMember]
        public Nullable<DateTime> ScheduleFromDate { get; set; }

        [DataMember]
        public string DockBerthCode { get; set; }

        [DataMember]
        public string DockQuayCode { get; set; }


        [DataMember]
        public string DockPortCode { get; set; }

        [DataMember]
        public System.DateTime ModifiedDate { get; set; }

        [DataMember]
        public int ModifiedBy { get; set; }

        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }

        [DataMember]
        public string RecordStatus { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public Nullable<int> Bunkers { get; set; }

        [DataMember]
        public Nullable<int> Ballast { get; set; }

        [DataMember]
        public Nullable<int> CargoTons { get; set; }

        [DataMember]
        public string BarkeelCode { get; set; }
        [DataMember]
        public System.DateTime FromDate { get; set; }
        [DataMember]
        public System.DateTime ToDate { get; set; }

        [DataMember]
        public Nullable<DateTime> EnteredDockDateTime { get; set; }
        
        [DataMember]
        public Nullable<DateTime> OnBlocksDateTime { get; set; }

        [DataMember]
        public Nullable<DateTime> DryDockDateTime { get; set; }

        [DataMember]
        public Nullable<DateTime> FinishedDockDateTime { get; set; }

        [DataMember]
        public Nullable<DateTime> OffBlocksDateTime { get; set; }

        [DataMember]
        public Nullable<DateTime> LeftDockDateTime { get; set; }

       
    }
}
