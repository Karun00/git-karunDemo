using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class LicenseRequestMap : EntityTypeConfiguration<LicenseRequest>
    {
        public LicenseRequestMap()
        {
            // Primary Key
            this.HasKey(t => t.LicenseRequestID);

            // Properties
            this.Property(t => t.LicenseRequestType)
                .IsRequired()
                .HasMaxLength(4);

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

            this.Property(t => t.TelephoneNo1)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.TelephoneNo2)
                .HasMaxLength(15);

            this.Property(t => t.FaxNo)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.ValidTaxClearanceCertificate)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.BBBEEStatus)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.VerifiedBBBEEStatus)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.BBBEEExemptedMicroEnterprise)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.PublicLiabilityInsurance)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("LicenseRequest");
            this.Property(t => t.LicenseRequestID).HasColumnName("LicenseRequestID");
            this.Property(t => t.LicenseRequestType).HasColumnName("LicenseRequestType");
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
            this.Property(t => t.ValidTaxClearanceCertificate).HasColumnName("ValidTaxClearanceCertificate");
            this.Property(t => t.BBBEEStatus).HasColumnName("BBBEEStatus");
            this.Property(t => t.VerifiedBBBEEStatus).HasColumnName("VerifiedBBBEEStatus");
            this.Property(t => t.BBBEEExemptedMicroEnterprise).HasColumnName("BBBEEExemptedMicroEnterprise");
            this.Property(t => t.PublicLiabilityInsurance).HasColumnName("PublicLiabilityInsurance");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.BusinessAddress)
                .WithMany(t => t.LicenseRequests)
                .HasForeignKey(d => d.BusinessAddressID);
            this.HasOptional(t => t.PostalAddress)
                .WithMany(t => t.LicenseRequests1)
                .HasForeignKey(d => d.PostalAddressID);
            this.HasRequired(t => t.AuthorizedContactPerson)
                .WithMany(t => t.LicenseRequests)
                .HasForeignKey(d => d.AuthorizedContactPersonID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.LicenseRequests)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.LicenseRequests)
                .HasForeignKey(d => d.LicenseRequestType);
            this.HasOptional(t => t.User1)
                .WithMany(t => t.LicenseRequests1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
