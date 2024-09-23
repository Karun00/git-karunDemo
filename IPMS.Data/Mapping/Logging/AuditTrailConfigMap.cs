using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class AuditTrailConfigMap : EntityTypeConfiguration<AuditTrailConfig>
    {
          public AuditTrailConfigMap()
        {
            // Primary Key
            this.HasKey(t => t.AuditTrailConfigID);

            // Properties
            this.Property(t => t.ControlerName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ActionName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UserFriendlyDescription)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.IsAuditTrailRequired)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.IsSecurityAuditTrail)
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("AuditTrailConfig");
            this.Property(t => t.AuditTrailConfigID).HasColumnName("AuditTrailConfigID");
            this.Property(t => t.ControlerName).HasColumnName("ControlerName");
            this.Property(t => t.ActionName).HasColumnName("ActionName");
            this.Property(t => t.UserFriendlyDescription).HasColumnName("UserFriendlyDescription");
            this.Property(t => t.IsAuditTrailRequired).HasColumnName("IsAuditTrailRequired");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.IsSecurityAuditTrail).HasColumnName("IsSecurityAuditTrail");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.AuditTrailConfigs)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User)
                .WithMany(t => t.AuditTrailConfigs)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
