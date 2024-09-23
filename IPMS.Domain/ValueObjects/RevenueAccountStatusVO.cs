using IPMS.Domain.Models;
using System;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class RevenueAccountStatusVO
    {
         [DataMember]
        public int RevenueAccountStatusID { get; set; }
        [DataMember]
        public int RevenueStopListID { get; set; }
        [DataMember]
        public string AccountStatusCode { get; set; }
        [DataMember]
        public virtual SubCategory SubCategory { get; set; }
        [DataMember]
        public virtual RevenueStopList RevenueStopList { get; set; }
    }
}
