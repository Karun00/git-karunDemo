using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public class MovementResourceAllocationVO
    {
        public int ServiceRequestId { get; set; }
        public int Slot { get; set; }
        public DateTime SlotDate { get; set; }
        public string VCN { get; set; }
        public string VesselName { get; set; }
        public List<ResourceAllocationSlotVO> ResourceAllocationSlots { get; set; }
        public string MovementType { get; set; }
        public bool IsConfirm { get; set; }
        public string OperationType { get; set; }//Save,Confirm, Deallocate for movementtype

        public string AnyDangerousGoodsonBoard { get; set; }
        public string VesselType { get; set; }
        public DateTime ETA { get; set; }
        public string ReasonForVisit { get; set; }
        public string CargoType { get; set; }
        public decimal LOA { get; set; }
        public decimal GRT { get; set; }
        public decimal DWT { get; set; }
        public string CurrentBerth { get; set; }
        public string ToBerth { get; set; }
        public Nullable<DateTime> MovementDateTime { get; set; }
        public decimal Beam { get; set; }
        public string ArrivalDraft { get; set; }
        public string TidalCondition { get; set; }
        public string DayLightCondition { get; set; }
        public string FromBollard { get; set; }
        public string ToBollard { get; set; }
        public string MovementTypeCode { get; set; }
        public string MovementStatus { get; set; }

        //-- Added by sandeep on 11-06-2015
        public string SideAlongSide { get; set; }
        //-- end
    }
}
