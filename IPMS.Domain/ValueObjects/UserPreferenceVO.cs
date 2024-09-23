using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
using IPMS.Domain.Models;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class UserPreferenceVO 
    {
       
        [DataMember]
        public int Userid { get; set; }
        [DataMember]
        public string DashBoardConfig { get; set; }
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
        public int RoleID { get; set; }
        [DataMember]
        public string EntityCode { get; set; }
        [DataMember]
        public string Entityname { get; set; }

        //[DataMember]
        //public string UserEntityCode { get; set; }
        //[DataMember]
        //public string UserEntityname { get; set; }

    }
}
