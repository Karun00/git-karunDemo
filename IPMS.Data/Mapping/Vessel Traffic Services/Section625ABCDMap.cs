using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class Section625ABCDMap : EntityTypeConfiguration<Section625ABCD>
    {
        public Section625ABCDMap()
        {
            // Primary Key
            this.HasKey(t => t.Section625ABCDID);

            // Properties
            this.Property(t => t.TOMSLogEntryNo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.OperatorName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.LincseNumber)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CompanyRegistrationNumber)
                .HasMaxLength(50);

            this.Property(t => t.SiteTerminal)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CDName)
                .HasMaxLength(200);

            this.Property(t => t.CDDesignation)
                .HasMaxLength(50);

            this.Property(t => t.CDContactNumber)
                .HasMaxLength(15);

            this.Property(t => t.CDMobileNumber)
                .HasMaxLength(15);

            this.Property(t => t.CDEmailID)
                .HasMaxLength(50);

            this.Property(t => t.CDAddress)
                .HasMaxLength(500);

            this.Property(t => t.ChangeControlLicensedOperator)
                .HasMaxLength(200);

            this.Property(t => t.AnticipatedImpactOnBBBEERating)
                .HasMaxLength(200);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("Section625ABCD");
            this.Property(t => t.Section625ABCDID).HasColumnName("Section625ABCDID");
            this.Property(t => t.TOMSLogEntryNo).HasColumnName("TOMSLogEntryNo");
            this.Property(t => t.OperatorName).HasColumnName("OperatorName");
            this.Property(t => t.LincseNumber).HasColumnName("LincseNumber");
            this.Property(t => t.CompanyRegistrationNumber).HasColumnName("CompanyRegistrationNumber");
            this.Property(t => t.SiteTerminal).HasColumnName("SiteTerminal");
            this.Property(t => t.ChangeControlDateTime).HasColumnName("ChangeControlDateTime");
            this.Property(t => t.CDName).HasColumnName("CDName");
            this.Property(t => t.CDDesignation).HasColumnName("CDDesignation");
            this.Property(t => t.CDContactNumber).HasColumnName("CDContactNumber");
            this.Property(t => t.CDMobileNumber).HasColumnName("CDMobileNumber");
            this.Property(t => t.CDEmailID).HasColumnName("CDEmailID");
            this.Property(t => t.CDAddress).HasColumnName("CDAddress");
            this.Property(t => t.ChangeControlLicensedOperator).HasColumnName("ChangeControlLicensedOperator");
            this.Property(t => t.AnticipatedImpactOnBBBEERating).HasColumnName("AnticipatedImpactOnBBBEERating");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.Hour24Report625ID).HasColumnName("Hour24Report625ID");

            // Relationships
            this.HasOptional(t => t.Hour24Report625)
                .WithMany(t => t.Section625ABCD)
                .HasForeignKey(d => d.Hour24Report625ID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.Section625ABCD)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.Section625ABCD1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
