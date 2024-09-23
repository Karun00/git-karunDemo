using Core.Repository;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class Section625GDetail2:EntityBase
    {
        public int Section625GDetail2ID { get; set; }
        public int Section625GID { get; set; }
        public string Description { get; set; }
        public string ResponsiblePerson { get; set; }
        public Nullable<System.DateTime> TargetCompletion { get; set; }
        public Nullable<System.DateTime> DateCompletion { get; set; }
        public int Hour24Report625ID { get; set; }
        public virtual Hour24Report625 Hour24Report625 { get; set; }
        public virtual Section625G Section625G { get; set; }
    }
}
