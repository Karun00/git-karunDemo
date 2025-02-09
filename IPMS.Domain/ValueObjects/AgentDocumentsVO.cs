﻿using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public class AgentDocumentsVO
    {        
        public int AgentID { get; set; }        
        public int DocumentID { get; set; }        
        public string RecordStatus { get; set; }        
        public int CreatedBy { get; set; }        
        public Nullable<System.DateTime> CreatedDate { get; set; }        
        public Nullable<int> ModifiedBy { get; set; }        
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual Document Document { get; set; }
    }
}
