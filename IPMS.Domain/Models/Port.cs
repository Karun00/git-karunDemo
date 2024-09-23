using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;


namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class Port : EntityBase
    {
        public Port()
        {
            this.Appl_Port_Workflow = new List<ApplicantPortWorkflow>();
            this.Berths = new List<Berth>();
            this.Quays = new List<Quay>();
            this.Bollards = new List<Bollard>();
        }

        [DataMember]
        public long PortID { get; set; }

        [DataMember]
        public string PortCode { get; set; }

        [DataMember]
        public string PortName { get; set; }

        [DataMember]
        public string InternationalCharacter { get; set; }

        [DataMember]
        public string GeographicLocation { get; set; }

        [DataMember]
        public decimal ContactNo { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public Nullable<decimal> Fax { get; set; }

        [DataMember]
        public string Website { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public long CreatedBy { get; set; }

        [DataMember]
        public System.DateTime CreatedDate { get; set; }

        [DataMember]
        public Nullable<long> ModifiedBy { get; set; }

        [DataMember]
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public virtual ICollection<ApplicantPortWorkflow> Appl_Port_Workflow { get; set; }

        [DataMember]
        public virtual ICollection<Berth> Berths { get; set; }

        [DataMember]
        public virtual ICollection<Quay> Quays { get; set; }

        public virtual ICollection<Bollard> Bollards { get; set; }
        //public string mode { get; set; }
    }
}
