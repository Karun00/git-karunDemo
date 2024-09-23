using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
	[DataContract]
	public partial class WaterServiceTaskExecution : EntityBase
	{
		[DataMember]
		public int WaterServiceTaskExecutionId { get; set; }
		[DataMember]
		public int MovementResourceAllocationID { get; set; }
		[DataMember]
		public System.DateTime StartTime { get; set; }
		[DataMember]
		public string MeterSerialNumber { get; set; }
		[DataMember]
		public decimal OpeningMeterReading { get; set; }
		[DataMember]
		public decimal ClosingMeterReading { get; set; }
		[DataMember]
		public Nullable<System.DateTime> EndTime { get; set; }
		[DataMember]
		public string Remarks { get; set; }
		[DataMember]
		public string Deficiencies { get; set; }
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
		public virtual MovementResourceAllocation MovementResourceAllocation { get; set; }
		[DataMember]
		public virtual User User { get; set; }
		[DataMember]
		public virtual User User1 { get; set; }
	}
}
