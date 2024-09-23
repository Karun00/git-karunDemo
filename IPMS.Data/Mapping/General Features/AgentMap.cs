using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
	public class AgentMap : EntityTypeConfiguration<Agent>
	{
		public AgentMap()
		{
			// Primary Key
			this.HasKey(t => t.AgentID);

            this.Property(t => t.AnonymousUserYn)
				.IsFixedLength()
				.HasMaxLength(1);

           

			// Properties
			this.Property(t => t.ReferenceNo)
				.IsRequired()
				.HasMaxLength(20);

			this.Property(t => t.RegisteredName)
				.IsRequired()
				.HasMaxLength(100);

			this.Property(t => t.TradingName)
				.IsRequired()
				.HasMaxLength(100);

			this.Property(t => t.RegistrationNumber)
				.IsRequired()
				.HasMaxLength(50);

			this.Property(t => t.VATNumber)
				.IsRequired()
				.HasMaxLength(50);

			this.Property(t => t.IncomeTaxNumber)
				.IsRequired()
				.HasMaxLength(50);

			this.Property(t => t.SkillsDevLevyNumber)
				.IsRequired()
				.HasMaxLength(50);

			this.Property(t => t.SARSTaxClearance)
				.IsRequired()
				.IsFixedLength()
				.HasMaxLength(1);

			this.Property(t => t.SAASOA)
				.IsRequired()
				.IsFixedLength()
				.HasMaxLength(1);

			this.Property(t => t.QualifyBBBEECodes)
				.IsRequired()
				.IsFixedLength()
				.HasMaxLength(1);

			this.Property(t => t.BBBEEStatus)
				.IsRequired()
				.HasMaxLength(4);

			this.Property(t => t.VerifyBBBEEStatus)
				.IsRequired()
				.IsFixedLength()
				.HasMaxLength(1);

			this.Property(t => t.RecordStatus)
				.IsFixedLength()
				.HasMaxLength(1);

            //this.Property(t => t.FromDate)
            //    .IsRequired();

            //this.Property(t => t.ToDate)
            //    .IsRequired();

            this.Property(t => t.WorkflowInstanceId);
				

			// Table & Column Mappings
			this.ToTable("Agent");
			this.Property(t => t.AgentID).HasColumnName("AgentID");
			this.Property(t => t.ReferenceNo).HasColumnName("ReferenceNo");
			this.Property(t => t.RegisteredName).HasColumnName("RegisteredName");
			this.Property(t => t.TradingName).HasColumnName("TradingName");
			this.Property(t => t.RegistrationNumber).HasColumnName("RegistrationNumber");
			this.Property(t => t.VATNumber).HasColumnName("VATNumber");
			this.Property(t => t.IncomeTaxNumber).HasColumnName("IncomeTaxNumber");
			this.Property(t => t.SkillsDevLevyNumber).HasColumnName("SkillsDevLevyNumber");
			this.Property(t => t.BusinessAddressID).HasColumnName("BusinessAddressID");
			this.Property(t => t.PostalAddressID).HasColumnName("PostalAddressID");
			this.Property(t => t.TelephoneNo1).HasColumnName("TelephoneNo1");
			this.Property(t => t.TelephoneNo2).HasColumnName("TelephoneNo2");
			this.Property(t => t.FaxNo).HasColumnName("FaxNo");
			this.Property(t => t.AuthorizedContactPersonID).HasColumnName("AuthorizedContactPersonID");
			this.Property(t => t.SARSTaxClearance).HasColumnName("SARSTaxClearance");
			this.Property(t => t.SAASOA).HasColumnName("SAASOA");
			this.Property(t => t.QualifyBBBEECodes).HasColumnName("QualifyBBBEECodes");
			this.Property(t => t.BBBEEStatus).HasColumnName("BBBEEStatus");
			this.Property(t => t.VerifyBBBEEStatus).HasColumnName("VerifyBBBEEStatus");
			this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.AnonymousUserYn).HasColumnName("AnonymousUserYn");

            //this.Property(t => t.FromDate).HasColumnName("FromDate");
            //this.Property(t => t.ToDate).HasColumnName("ToDate");

			this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
			this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
			this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
			this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            this.Property(t => t.WorkflowInstanceId).HasColumnName("WorkflowInstanceId");

			// Relationships
			this.HasRequired(t => t.Address)
				.WithMany(t => t.Agents)
				.HasForeignKey(d => d.BusinessAddressID);
			this.HasOptional(t => t.Address1)
				.WithMany(t => t.Agents1)
				.HasForeignKey(d => d.PostalAddressID);
			this.HasRequired(t => t.AuthorizedContactPerson)
				.WithMany(t => t.Agents)
				.HasForeignKey(d => d.AuthorizedContactPersonID);      				
			this.HasRequired(t => t.User)
				.WithMany(t => t.Agents)
				.HasForeignKey(d => d.CreatedBy);
				
			this.HasOptional(t => t.User1)
				.WithMany(t => t.Agents1)
				.HasForeignKey(d => d.ModifiedBy);

            //this.HasOptional(t => t.WorkflowInstance)
            //  .WithMany(t => t.Agents)
            //  .HasForeignKey(d => d.WorkflowInstanceId);

		}
	}
}
