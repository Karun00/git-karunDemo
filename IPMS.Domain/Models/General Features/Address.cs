using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class Address : EntityBase
    {
        public Address()
        {
            this.Agents = new List<Agent>();
            this.Agents1 = new List<Agent>();
            this.Pilots = new List<Pilot>();
            this.Pilots1 = new List<Pilot>();
            this.LicenseRequests = new List<LicenseRequest>();
            this.LicenseRequests1 = new List<LicenseRequest>();
            this.TerminalOperators = new List<TerminalOperator>();
            this.TerminalOperators1 = new List<TerminalOperator>();
        }

        [DataMember]
        public int AddressID { get; set; }
        [DataMember]
        public string AddressType { get; set; }
        [DataMember]
        public string NumberStreet { get; set; }
        [DataMember]
        public string Suburb { get; set; }
        [DataMember]
        public string TownCity { get; set; }
        [DataMember]
        public string PostalCode { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        [DataMember]
        public  SubCategory SubCategory { get; set; }

        [DataMember]
        public string CountryCode { get; set; }
        [DataMember]
        public  SubCategory SubCategory1 { get; set; }
        [DataMember]
        public  User User { get; set; }
        [DataMember]
        public  User User1 { get; set; }
        [DataMember]
        public  List<Agent> Agents { get; set; }
        [DataMember]
        public  List<Agent> Agents1 { get; set; }
        [DataMember]
        public  List<LicenseRequest> LicenseRequests { get; set; }
        [DataMember]
        public  List<LicenseRequest> LicenseRequests1 { get; set; }
        //   [DataMember]
        //public  ICollection<TerminalOperator> TerminalOperators { get; set; }
        //   [DataMember]
        //public  ICollection<TerminalOperator> TerminalOperators1 { get; set; }
        [DataMember]
        public  ICollection<Pilot> Pilots { get; set; }
        [DataMember]
        public  ICollection<Pilot> Pilots1 { get; set; }
        [DataMember]
        public  List<TerminalOperator> TerminalOperators { get; set; }
        [DataMember]
        public  List<TerminalOperator> TerminalOperators1 { get; set; }      
    }
}
