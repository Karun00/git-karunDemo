using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    public class VesselCallMovementVO
    {


        public int VesselCallMovementID { get; set; }

        public string VCN { get; set; }

        public Nullable<int> ServiceRequestID { get; set; }

        public string FromPositionPortCode { get; set; }

        public string FromPositionQuayCode { get; set; }

        public string FromPositionBerthCode { get; set; }

        public string FromPositionBollardCode { get; set; }

        public string ToPositionPortCode { get; set; }

        public string ToPositionQuayCode { get; set; }

        public string ToPositionBerthCode { get; set; }

        public string ToPositionBollardCode { get; set; }

        public string SlotStatus { get; set; }

        public Nullable<System.DateTime> SlotDate { get; set; }

        public string Slot { get; set; }


        public string MovementStatus { get; set; }

        public string RecordStatus { get; set; }

        public int CreatedBy { get; set; }

        public System.DateTime CreatedDate { get; set; }

        public int ModifiedBy { get; set; }

        public System.DateTime ModifiedDate { get; set; }


        public string VesselName { get; set; }

        public string VesselType { get; set; }

        public DateTime ETA { get; set; }

        public string ReasonForVisit { get; set; }

        public string CargoType { get; set; }

        //public string LOA { get; set; } // sandeep commented
        public decimal LOA { get; set; } // Sandeep Added

        public string MovementType { get; set; }

        public string MovementTypeCode { get; set; }

        public List<string> VesselSlotAllocationDet { get; set; }

        public string ServiceRequest { get; set; }

        //public long LengthOverAll { get; set; }

        public decimal LengthOverAll { get; set; }

        //public long GrossRegisteredTonnage { get; set; }

        public decimal GrossRegisteredTonnage { get; set; }

        public string PortCode { get; set; }

        public string ExtendYn { get; set; }

        public Nullable<DateTime> ETB { get; set; }

        public Nullable<DateTime> ETUB { get; set; }


        public Nullable<DateTime> ATB { get; set; }

        public Nullable<DateTime> ATUB { get; set; }


        public long Quantity { get; set; }


        public string ServiceTypeCode { get; set; }

        public bool IsChanged { get; set; }

        public string Detail { get; set; }

        public string TaskStatus { get; set; }

        public string CurrentBerth { get; set; }

        public string ToBerth { get; set; }

        public string FromBollard1 { get; set; }

        public string FromBollard2 { get; set; }

        public string ToBollard1 { get; set; }

        public string ToBollard2 { get; set; }

        public Nullable<DateTime> MovementDateTime { get; set; } // sandeep added nullable

        //public string Beam { get; set; } // Sandeep commented
        public decimal Beam { get; set; } // Sandeep Added

        public string ArrivalDraft { get; set; }

        //public string GRT { get; set; } // Sandeep commented
        public decimal GRT { get; set; } // Sandeep added

        //public string DWT { get; set; } // Sandeep commented
        public decimal DWT { get; set; } // Sandeep added

        public string PreviousPort { get; set; }

        public string TidalCondition { get; set; }

        public string DayLightCondition { get; set; }

        public string MovementTime { get; set; }

        //Added By Omprakash For Notification on 16th Dec 2014
        public string PortName { get; set; }

        //Added By Omprakash For Notification on 16th Dec 2014
        public string UserName { get; set; }

        //-- Added by sandeep on 20-02-2015
        public int UserID { get; set; }

        public string AnyDangerousGoodsonBoard { get; set; }

        public string FromBollard { get; set; }

        public string ToBollard { get; set; }
        //-- end
        public Nullable<DateTime> ETD { get; set; }

        public string VesselNationality { get; set; }
        public string LastPortOfCall { get; set; }
        public string NextPortOfCall { get; set; }

        //-- Added by sandeep on 29-04-2015
        public string MooringBollardBowPortCode { get; set; }

        public string MooringBollardBowQuayCode { get; set; }

        public string MooringBollardBowBerthCode { get; set; }

        public string MooringBollardBowBollardCode { get; set; }

        public string MooringBollardStemPortCode { get; set; }

        public string MooringBollardStemQuayCode { get; set; }

        public string MooringBollardStemBerthCode { get; set; }

        public string MooringBollardStemBollardCode { get; set; }

        //public string SideAlongSide { get; set; }
        //Added by Srinivas Start
        public string Sidealongside { get; set; }

        public string Tidal { get; set; }

        public string OwnSteam { get; set; }

        public string Nomainengine { get; set; }
        //Added by Srinivas End
        //-- end

        //-- Added by sandeep on 12-08-2015
        public Nullable<System.DateTime> ExtendedSlotDate { get; set; }
        //-- end


        public string ReasonCode { get; set; }
        public string ReasonForDisplay { get; set; }
        public string ReasonName { get; set; }
        public System.DateTime EnteredDateTime { get; set; }
        public string PreviousSlot { get; set; }
        public string OverriddenSlot { get; set; }
        public string PreviousSlotDis { get; set; }
        public Nullable<DateTime> PreviousSlotDate { get; set; }
        public string PreviousSlotStatus { get; set; }
        

        


    }
}
