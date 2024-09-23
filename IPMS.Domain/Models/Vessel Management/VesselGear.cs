using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
	public partial class VesselGear : EntityBase
	{
		public int VesselGearID { get; set; }
		public int VesselID { get; set; }
		public Nullable<long> GearTypeM { get; set; }
		public Nullable<long> SafeWorkingLoad { get; set; }
		public Nullable<long> GearCapacityCBM { get; set; }
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
