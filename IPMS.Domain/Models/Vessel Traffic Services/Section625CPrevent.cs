using Core.Repository;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models 
{
    public partial class Section625CPrevent:EntityBase
    {
        public int Section625CPreventID { get; set; }
        public int Section625CID { get; set; }
        public string PreventStep { get; set; }
        public System.DateTime TargetDateTime { get; set; }
        public System.DateTime ActionBy { get; set; }
        public System.DateTime CompletedDate { get; set; }
        public int Hour24Report625ID { get; set; }
        public virtual Hour24Report625 Hour24Report625 { get; set; }
        public virtual Section625C Section625C { get; set; }
    }
}
