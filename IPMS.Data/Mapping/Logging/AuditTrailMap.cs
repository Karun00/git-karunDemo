using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class AuditTrailMap : EntityTypeConfiguration<AuditTrail>
    {
        public AuditTrailMap()
        {
            // Primary Key
            this.HasKey(t => t.AuditTrailID);

            // Properties
            this.Property(t => t.Content)
                .IsRequired();

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.UserIPAddress)
              .HasMaxLength(15);
            
            this.Property(t => t.UserName)
                .HasMaxLength(30);

            this.Property(t => t.Parameters)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("AuditTrail");
            this.Property(t => t.AuditTrailID).HasColumnName("AuditTrailID");
            this.Property(t => t.AuditTrailConfigID).HasColumnName("AuditTrailConfigID");
            this.Property(t => t.EntryORExit).HasColumnName("EntryORExit");
            this.Property(t => t.Content).HasColumnName("Content");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.AuditDateTime).HasColumnName("AuditDateTime");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.UserIPAddress).HasColumnName("UserIPAddress");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.Parameters).HasColumnName("Parameters");

            // Relationships
            this.HasRequired(t => t.AuditTrailConfig)
                .WithMany(t => t.AuditTrails)
                .HasForeignKey(d => d.AuditTrailConfigID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.AuditTrails)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User)
                .WithMany(t => t.AuditTrails)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
