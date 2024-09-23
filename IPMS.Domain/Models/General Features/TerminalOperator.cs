using Core.Repository;
using System;
using System.Collections.Generic;

namespace IPMS.Domain.Models
{
    public partial class TerminalOperator : EntityBase
    {
        public TerminalOperator()
        {
            this.ArrivalNotifications = new List<ArrivalNotification>();
            this.TerminalOperatorBerths = new List<TerminalOperatorBerth>();
            this.TerminalOperatorCargoHandlings = new List<TerminalOperatorCargoHandling>();
            this.TerminalOperatorPorts = new List<TerminalOperatorPort>();
            this.StatementCommodities = new List<StatementCommodity>();
        }

        public int TerminalOperatorID { get; set; }

        public string RegisteredName { get; set; }

        public string TradingName { get; set; }

        public string RegistrationNumber { get; set; }

        public System.DateTime RegistrationDate { get; set; }

        public System.DateTime ValidityDate { get; set; }

        public string PremiseLocation { get; set; }

        public int BusinessAddressID { get; set; }

        public Nullable<int> PostalAddressID { get; set; }

        public string TelephoneNo1 { get; set; }

        public string TelephoneNo2 { get; set; }

        public string FaxNo { get; set; }

        public string LicensedFor { get; set; }

        public string RecordStatus { get; set; }

        public int CreatedBy { get; set; }

        public System.DateTime CreatedDate { get; set; }

        public Nullable<int> ModifiedBy { get; set; }

        public System.DateTime ModifiedDate { get; set; }

        public  Address PostalAddress { get; set; }

        public  Address BusinessAddress { get; set; }

        public  ICollection<ArrivalNotification> ArrivalNotifications { get; set; }

        public  SubCategory SubCategory { get; set; }

        public  User User { get; set; }

        public  User User1 { get; set; }

        public  ICollection<TerminalOperatorBerth> TerminalOperatorBerths { get; set; }

        public  ICollection<TerminalOperatorCargoHandling> TerminalOperatorCargoHandlings { get; set; }

        public  ICollection<TerminalOperatorPort> TerminalOperatorPorts { get; set; }
                        
        public ICollection<StatementCommodity> StatementCommodities { get; set; }
    }
}
