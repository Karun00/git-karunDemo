using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
	public partial class VesselGrab : EntityBase
	{
		public int VesselGrabID { get; set; }
		public int VesselID { get; set; }
		public Nullable<long> GrabTypeM { get; set; }
		public Nullable<long> SafeWorkingLoad { get; set; }
		public Nullable<long> GrabCapacityCBM { get; set; }
		public string Description { get; set; }
		public string RecordStatus { get; set; }
		public int CreatedBy { get; set; }
		public System.DateTime CreatedDate { get; set; }
		public int ModifiedBy { get; set; }
		public System.DateTime ModifiedDate { get; set; }
		public virtual User User { get; set; }
		public virtual User User1 { get; set; }
		public virtual Vessel Vessel { get; set; }
	}
}
