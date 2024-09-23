using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class Agent : EntityBase
    {
        public Agent()
        {
            this.AgentDocuments = new List<AgentDocument>();
            this.AgentPorts = new List<AgentPort>();
            this.ArrivalAgents = new List<ArrivalAgent>();
            this.ArrivalNotifications = new List<ArrivalNotification>();
            this.VesselAgentChanges = new List<VesselAgentChange>();
            this.VesselCalls = new List<VesselCall>();
            this.RevenueStopLists = new List<RevenueStopList>();
            this.Drafts = new List<Draft>();
            this.DepartureNotices = new List<DepartureNotice>();
            this.RevenuePostings = new List<RevenuePosting>();
            this.AgentAccounts = new List<AgentAccount>();
            this.SuppServiceRequests = new List<SuppServiceRequest>();
        }


        [DataMember]
        public string AnonymousUserYn { get; set; }

        [DataMember]
        public int AgentID { get; set; }
        [DataMember]
        public string ReferenceNo { get; set; }

        [DataMember]
        public Nullable<int> WorkflowInstanceId { get; set; }

        [DataMember]
        public  WorkflowInstance WorkflowInstance { get; set; }

        [DataMember]
        public string RegisteredName { get; set; }
        [DataMember]
        public string TradingName { get; set; }
        [DataMember]
        public string RegistrationNumber { get; set; }
        [DataMember]
        public string VATNumber { get; set; }

        [DataMember]
        public string IncomeTaxNumber { get; set; }
        [DataMember]
        public string SkillsDevLevyNumber { get; set; }
        [DataMember]
        public int BusinessAddressID { get; set; }
        [DataMember]
        public Nullable<int> PostalAddressID { get; set; }
        [DataMember]
        public string TelephoneNo1 { get; set; }
        [DataMember]
        public string TelephoneNo2 { get; set; }
        [DataMember]
        public string FaxNo { get; set; }
        [DataMember]
        public int AuthorizedContactPersonID { get; set; }
        [DataMember]
        public string SARSTaxClearance { get; set; }
        [DataMember]
        public string SAASOA { get; set; }
        [DataMember]
        public string QualifyBBBEECodes { get; set; }
        [DataMember]
        public string BBBEEStatus { get; set; }
        [DataMember]
        public string VerifyBBBEEStatus { get; set; }
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
        public  Address Address { get; set; }
        [DataMember]
        public  Address Address1 { get; set; }
        [DataMember]
        public  AuthorizedContactPerson AuthorizedContactPerson { get; set; }
        //[DataMember]
        //public  SubCategory SubCategory { get; set; }
        [DataMember]
        public  User User { get; set; }
        [DataMember]
        public  User User1 { get; set; }
        [DataMember]
        public  ICollection<AgentDocument> AgentDocuments { get; set; }
        [DataMember]
        public  ICollection<AgentPort> AgentPorts { get; set; }
        [DataMember]
        public  ICollection<ArrivalAgent> ArrivalAgents { get; set; }
        [DataMember]
        public  ICollection<ArrivalNotification> ArrivalNotifications { get; set; }
        [DataMember]
        public  ICollection<VesselAgentChange> VesselAgentChanges { get; set; }
        [DataMember]
        public  ICollection<VesselCall> VesselCalls { get; set; }
        [DataMember]
        public  ICollection<Draft> Drafts { get; set; }

        //[DataMember]
        //public Nullable<System.DateTime> FromDate { get; set; }

        //[DataMember]
        //public Nullable<System.DateTime> ToDate { get; set; }
        [DataMember]
        public  ICollection<RevenueStopList> RevenueStopLists { get; set; }

        [NotMapped]
        public Nullable<System.DateTime> SubmissionDate { get; set; }
        // -- Added By Santosh on 05-12-2014
        [DataMember]
        public  ICollection<DepartureNotice> DepartureNotices { get; set; }
        // -- end

        //[DataMember]
        //public Nullable<System.DateTime> ValidityDate { get; set; }

        // -- Added By Amala on 07-1-2015
        [DataMember]
        public  ICollection<RevenuePosting> RevenuePostings { get; set; }
         [DataMember]
        public  ICollection<AgentAccount> AgentAccounts { get; set; }

         // -- Added by Omprakash on 03-June-2015
         [DataMember]
         public  ICollection<SuppServiceRequest> SuppServiceRequests { get; set; }
        // -- end
    }
}
