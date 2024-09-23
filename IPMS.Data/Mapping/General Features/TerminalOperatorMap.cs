using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class TerminalOperatorMap : EntityTypeConfiguration<TerminalOperator>
    {
        public TerminalOperatorMap()
        {
            // Primary Key
            this.HasKey(t => t.TerminalOperatorID);

            // Properties
            this.Property(t => t.RegisteredName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.TradingName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.RegistrationNumber)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.PremiseLocation)
                .HasMaxLength(50);

            this.Property(t => t.TelephoneNo1)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.TelephoneNo2)
                .HasMaxLength(15);

            this.Property(t => t.FaxNo)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.LicensedFor)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("TerminalOperator");
            this.Property(t => t.TerminalOperatorID).HasColumnName("TerminalOperatorID");
            this.Property(t => t.RegisteredName).HasColumnName("RegisteredName");
            this.Property(t => t.TradingName).HasColumnName("TradingName");
            this.Property(t => t.RegistrationNumber).HasColumnName("RegistrationNumber");
            this.Property(t => t.RegistrationDate).HasColumnName("RegistrationDate");
            this.Property(t => t.ValidityDate).HasColumnName("ValidityDate");
            this.Property(t => t.PremiseLocation).HasColumnName("PremiseLocation");
            this.Property(t => t.BusinessAddressID).HasColumnName("BusinessAddressID");
            this.Property(t => t.PostalAddressID).HasColumnName("PostalAddressID");
            this.Property(t => t.TelephoneNo1).HasColumnName("TelephoneNo1");
            this.Property(t => t.TelephoneNo2).HasColumnName("TelephoneNo2");
            this.Property(t => t.FaxNo).HasColumnName("FaxNo");
            this.Property(t => t.LicensedFor).HasColumnName("LicensedFor");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.PostalAddress)
              .WithMany(t => t.TerminalOperators)
              .HasForeignKey(d => d.BusinessAddressID);
            this.HasOptional(t => t.BusinessAddress)
                .WithMany(t => t.TerminalOperators1)
                .HasForeignKey(d => d.PostalAddressID);
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.TerminalOperators)
                .HasForeignKey(d => d.LicensedFor);
            this.HasRequired(t => t.User)
                .WithMany(t => t.TerminalOperators)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.User1)
                .WithMany(t => t.TerminalOperators1)
                .HasForeignKey(d => d.ModifiedBy);
        }
    }
}
