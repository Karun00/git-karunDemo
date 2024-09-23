using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public class Section625GDetail2VO
    {
        public int Section625GDetail2ID { get; set; }
        public int Section625GID { get; set; }
        public string Description { get; set; }
        public string ResponsiblePerson { get; set; }
        public Nullable<System.DateTime> TargetCompletion { get; set; }
        public Nullable<System.DateTime> DateCompletion { get; set; }
    }
}
