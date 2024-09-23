using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class RevenueStopListVO
    {
        [DataMember]
        public int RevenueStopListID { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string RegisteredName { get; set; }

        [DataMember]
        public string RegistrationNumber { get; set; }
        [DataMember]
        public string AccountNo { get; set; }
        [DataMember]
        public int AgentID { get; set; }
        [DataMember]
        public int AgentAccountID { get; set; }
        [DataMember]
        public string StopDate { get; set; }
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
        public virtual Agent Agent { get; set; }
        [DataMember]
        public virtual Port Port { get; set; }
        //[DataMember]
        //public ICollection<RevenueAccountStatusVO> RevenueAccountStatus { get; set; }
        [DataMember]
        public string AccountStatus { get; set; }
        [DataMember]
        public string AccountStatusName { get; set; }
        [DataMember]
        public int RevenueAccountStatusID { get; set; }
        [DataMember]
        public virtual User User { get; set; }
        [DataMember]
        public virtual User User1 { get; set; }
    }
}

