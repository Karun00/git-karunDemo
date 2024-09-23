using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class StatementFactVO
    {
        [DataMember]
        public int StatementFactID { get; set; }
        [DataMember]
        public string VCN { get; set; }
        [DataMember]
        public string OperationCode { get; set; }
        [DataMember]
        public string MasterName { get; set; }
        [DataMember]
        public decimal ArrivalFuel { get; set; }
        [DataMember]
        public decimal ArrivalDiesel { get; set; }
        [DataMember]
        public decimal SailingFuel { get; set; }
        [DataMember]
        public decimal SailingDiesel { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }

        public List<StatementVCNVO> StatementFactEvents { get; set; }

        //[DataMember]
        //public virtual ICollection<StatementFactBunker> StatementFactBunkers { get; set; }
        //[DataMember]
        //public virtual ICollection<StatementFactEvent> StatementFactEvents { get; set; }

    }

    public class StatementFactReferenceDataVO
    {

        public List<PortVO> Ports { get; set; }
        public List<SubCategoryCodeNameVO> Operations { get; set; }
        public List<SubCategoryCodeNameVO> KeyEvents { get; set; }
        public List<SubCategoryCodeNameVO> DelayTypes { get; set; }

        public ICollection<BerthVO> Berths { get; set; }     
        public ICollection<SubCategoryCodeNameVO> CargoTypes { get; set; }
        public ICollection<SubCategoryCodeNameVO> Purpose { get; set; }
        public ICollection<SubCategoryCodeNameVO> Uoms { get; set; }
        public ICollection<SubCategoryCodeNameVO> Commodities { get; set; }
        public ICollection<TerminalOperatorVO> TerminalOperators { get; set; }

    }

    public class StatementVCNVO
    {

        [DataMember]
        public int VesselETAChangeID { get; set; }
        //[DataMember]
        //public string VCN { get; set; }
        [DataMember]
        public string VesselName { get; set; }
        [DataMember]
        public string PortOfRegistry { get; set; }
        [DataMember]
        public string VCN_VesselName { get; set; }
        [DataMember]
        public System.DateTime Date { get; set; }
        [DataMember]
        public string VesselAgent { get; set; }
        [DataMember]
        public string AgentName { get; set; }
        [DataMember]
        public long? GRT { get; set; }
        [DataMember]
        public long? LOA { get; set; }
        [DataMember]
        public string Draft { get; set; }
        [DataMember]
        public string VoyageIn { get; set; }
        [DataMember]
        public string VoyageOut { get; set; }

        // For Statement Fact Table

        [DataMember]
        public int StatementFactID { get; set; }
        [DataMember]
        public string VCN { get; set; }
        [DataMember]
        public string OperationCode { get; set; }
        [DataMember]
        public string OperationName { get; set; }
        [DataMember]
        public string MasterName { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }



        [DataMember]
        public virtual ICollection<StatementFactBunker> StatementFactBunkers { get; set; }
    //  [DataMember]
    //  public virtual ICollection<StatementFactEvent> StatementFactEvents { get; set; }

        public List<StatementVCNVO> StatementFactEvents { get; set; }

        public List<StatementCommodityVO> StatementCommodities { get; set; }

        // For Key Events

        [DataMember]
        public string EOSPDateTime { get; set; }
        [DataMember]
        public string GangwayDown { get; set; }
        [DataMember]
        public string NORTendered { get; set; }
        [DataMember]
        public string NORAccepted { get; set; }
        [DataMember]
        public string StevedoreOnBoard { get; set; }
        [DataMember]
        public string StevedoreStart { get; set; }
        [DataMember]
        public string StevedoreEnd { get; set; }
        [DataMember]
        public string StevedoreOff { get; set; }
        [DataMember]
        public Nullable<int> CranesDeployed { get; set; }
        [DataMember]
        public string StartCargo { get; set; }
        [DataMember]
        public string EndCargo { get; set; }


        // For Statement Fact Event Table


        [DataMember]
        public int StatementFactEventID { get; set; }
        [DataMember]
        public string SubCatCode { get; set; }
        [DataMember]
        public string SubCatName { get; set; }
        [DataMember]
        public string DelayType { get; set; }
        [DataMember]
        public string StartOperational { get; set; }
        [DataMember]
        public string EndOperational { get; set; }
        //Added by Srinivas
        [DataMember]
        public decimal? Duration { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        //
        //[DataMember]
        //public string KeyEventCode { get; set; }
        //[DataMember]
        //public System.DateTime KeyEventDateTime { get; set; }
        //[DataMember]
        //public string OperationsDelay { get; set; }

        // For Statement Fact Bunker Table

        [DataMember]
        public decimal ArrivalFuel { get; set; }
        [DataMember]
        public decimal ArrivalDiesel { get; set; }
        [DataMember]
        public decimal SailingFuel { get; set; }
        [DataMember]
        public decimal SailingDiesel { get; set; }

        // Fileds to display based on VCN

        [DataMember]
        public string Voyage { get; set; }
        [DataMember]
        public string Berth { get; set; }
        [DataMember]
        public System.DateTime DateFrom { get; set; }
        [DataMember]
        public System.DateTime DateTo { get; set; }
        [DataMember]
        public Nullable<System.DateTime> InwardPilotOnBoard { get; set; }
        [DataMember]
        public Nullable<System.DateTime> InwardFirstLine { get; set; }
        [DataMember]
        public Nullable<System.DateTime> InwardAllFast { get; set; }
        [DataMember]
        public Nullable<System.DateTime> InwardPilotAway { get; set; }
        [DataMember]
        public Nullable<System.DateTime> OutwardPilotOnBoard { get; set; }
        [DataMember]
        public Nullable<System.DateTime> OutwardAllCast { get; set; }
        [DataMember]
        public Nullable<System.DateTime> OutwardPilotAway { get; set; }
        [DataMember]
        public string ArrivingFWD { get; set; }
        [DataMember]
        public string ArrivingAFT { get; set; }
        [DataMember]
        public string SailingFWD { get; set; }
        [DataMember]
        public string SailingAFT { get; set; }
        [DataMember]
        public string DraftArrivalFwd { get; set; }
        [DataMember]
        public string DraftArrivalAft { get; set; }
        [DataMember]
        public string DraftSailingFwd { get; set; }
        [DataMember]
        public string DraftSailingAft { get; set; }

        [DataMember]
        public string CurrentBerth { get; set; }
        [DataMember]
        public string BerthFrom { get; set; }
        [DataMember]
        public string BerthTo { get; set; }

        //[DataMember]
        //public string MovementType { get; set; }
        //[DataMember]
        //public string CraftName { get; set; }
        //[DataMember]
        //public string CRAFTTYPE { get; set; }
        //[DataMember]
        //public long ForwardDraftM { get; set; }
        //[DataMember]
        //public long AftDraftM { get; set; }

        [DataMember]
        public string SDateFrom { get; set; }
        [DataMember]
        public string SDateTo { get; set; }


        public List<StatementTugsVO> SailingDetails { get; set; }
        public List<StatementTugsVO> ArrivalDetails { get; set; }
             
    }

    public class StatementFactEventVO
    {

        [DataMember]
        public string SubCatCode { get; set; }
        [DataMember]
        public string KeyEventCode { get; set; }
        [DataMember]
        public System.DateTime KeyEventDateTime { get; set; }
        [DataMember]
        public string OperationsDelay { get; set; }
    }
    public class StatementTugsVO
    {
        [DataMember]
        public string MovementType { get; set; }
        [DataMember]
        public string CraftName { get; set; }
        [DataMember]
        public string CRAFTTYPE { get; set; }
        [DataMember]
        public long ForwardDraftM { get; set; }
        [DataMember]
        public long AftDraftM { get; set; }
    }

    public class StatementCommodityVO
    {
        [DataMember]
        public int StatementCommodityID { get; set; }
        [DataMember]
        public int StatementFactID { get; set; }
        [DataMember]
        public int TerminalOperatorID { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string QuayCode { get; set; }
        [DataMember]
        public string BerthCode { get; set; }
        [DataMember]
        public string CargoType { get; set; }
        [DataMember]
        public string Package { get; set; }
        [DataMember]
        public string UOM { get; set; }
        [DataMember]
        public string Commodity { get; set; }
        [DataMember]
        public Nullable<decimal> Quantity { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
        [DataMember]
        public string CommodityBerthKey { get; set; }          
    }
}
