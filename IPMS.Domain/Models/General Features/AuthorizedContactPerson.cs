using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
	[DataContract]
	public partial class AuthorizedContactPerson : EntityBase
	{
		public AuthorizedContactPerson()
		{
			this.Agents = new List<Agent>();
            this.LicenseRequests = new List<LicenseRequest>();
		}


		 [DataMember]
		public int AuthorizedContactPersonID { get; set; }
		 [DataMember]
		public string AuthorizedContactPersonType { get; set; }
		 [DataMember]
		public string FirstName { get; set; }
		 [DataMember]
		public string SurName { get; set; }
		 [DataMember]
		public string IdentityNo { get; set; }
		 [DataMember]
		public string Designation { get; set; }
		 [DataMember]
		public string CellularNo { get; set; }
		 [DataMember]
		public string EmailID { get; set; }
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
		public  List<Agent> Agents { get; set; }
		 [DataMember]
		public  SubCategory SubCategory { get; set; }
		 [DataMember]
		public  User User { get; set; }
		 [DataMember]
		public  User User1 { get; set; }
         [DataMember]
         public  List<LicenseRequest> LicenseRequests { get; set; }

         //[DataMember]
         //public decimal AuthorizedContactPersonID { get; set; }
	 //[DataMember]
	 //   public string FirstName { get; set; }
	 //[DataMember]
	 //   public string SurName { get; set; }
	 //[DataMember]
	 //   public string IdentityNo { get; set; }
	 //[DataMember]
	 //   public string Designation { get; set; }
	 //[DataMember]
	 //   public decimal CellularNo { get; set; }
	 //[DataMember]
	 //   public string EmailID { get; set; }
	 //[DataMember]
	 //   public string RecordStatus { get; set; }
	 //[DataMember]
	 //   public decimal CreatedBy { get; set; }
	 //[DataMember]
	 //   public System.DateTime CreatedDate { get; set; }
	 //[DataMember]
	 //   public Nullable<decimal> ModifiedBy { get; set; }
	 //[DataMember]
	 //   public Nullable<System.DateTime> ModifiedDate { get; set; }
	 //   [DataMember]
	 //   public virtual ICollection<Agent> Agents { get; set; }
	 //   [DataMember]
	 //   public virtual ICollection<Applicant> Applicants { get; set; }
	 //    [DataMember]
	 //   public virtual User User { get; set; }
	 //    [DataMember]
	 //   public virtual User User1 { get; set; }
	}
}
