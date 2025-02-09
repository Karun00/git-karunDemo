﻿using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;


namespace IPMS.Domain.Models
{
    public partial class StatementCommodity : EntityBase
    { 
        [DataMember]
        public int StatementCommodityID { get; set; }
        [DataMember]
        public int StatementFactID { get; set; }
        [DataMember]
        public int TerminalOperatorID { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string QuayCode { get; set; }
        [DataMember]
        public string BerthCode { get; set; }
        [DataMember]
        public string CargoType { get; set; }
        [DataMember]
        public string Package { get; set; }
        [DataMember]
        public string UOM { get; set; }
        [DataMember]
        public string Commodity { get; set; }
        [DataMember]
        public Nullable<decimal> Quantity { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }     
        [DataMember]
        public virtual User User { get; set; }
        [DataMember]
        public virtual User User1 { get; set; }
        [DataMember]
        public virtual TerminalOperator TerminalOperator { get; set; }
        [DataMember]
        public virtual SubCategory SubCategory { get; set; }
        [DataMember]
        public virtual SubCategory SubCategory1 { get; set; }        
        [DataMember]
        public virtual SubCategory SubCategory2 { get; set; }
        [DataMember]
        public virtual SubCategory SubCategory3 { get; set; }
        [DataMember]
        public virtual StatementFact StatementFact { get; set; }
        [DataMember]
        public virtual Berth Berth { get; set; }
    
    }
}
