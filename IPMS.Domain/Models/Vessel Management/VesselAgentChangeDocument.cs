using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
	[DataContract]
	public partial class VesselAgentChangeDocument : EntityBase
	{
		public int VesselAgentChangeID { get; set; }
		public int DocumentID { get; set; }
        public string DocumentName { get; set; }

        public string FileName { get; set; }

		public string RecordStatus { get; set; }
		public int CreatedBy { get; set; }
		public System.DateTime CreatedDate { get; set; }
		public int ModifiedBy { get; set; }
		public System.DateTime ModifiedDate { get; set; }
		public virtual Document Document { get; set; }
		public virtual User User { get; set; }
		public virtual User User1 { get; set; }
		public virtual VesselAgentChange VesselAgentChange { get; set; }
	}
}
