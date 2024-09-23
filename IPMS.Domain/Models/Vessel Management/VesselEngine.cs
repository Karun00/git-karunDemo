using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
	public partial class VesselEngine : EntityBase
	{
		public int VesselEngineID { get; set; }
		public int VesselID { get; set; }
		public Nullable<long> EnginePower { get; set; }
		public string EngineType { get; set; }
		public string PropulsionType { get; set; }
		public Nullable<long> NoOfPropellers { get; set; }
		public Nullable<long> MaxSpeed { get; set; }
		public string RecordStatus { get; set; }
		public int CreatedBy { get; set; }
		public System.DateTime CreatedDate { get; set; }
		public int ModifiedBy { get; set; }
		public System.DateTime ModifiedDate { get; set; }
		public virtual SubCategory SubCategory { get; set; }
		public virtual SubCategory SubCategory1 { get; set; }
		public virtual User User { get; set; }
		public virtual User User1 { get; set; }
		public virtual Vessel Vessel { get; set; }
	}
}
