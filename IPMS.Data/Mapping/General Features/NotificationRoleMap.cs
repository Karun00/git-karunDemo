using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class NotificationRoleMap : EntityTypeConfiguration<NotificationRole>
    {
        public NotificationRoleMap()
        {
            // Primary Key
            this.HasKey(t => t.NotificationRoleID);

            // Properties
            this.Property(t => t.NotificationTemplateCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("NotificationRole");
            this.Property(t => t.NotificationRoleID).HasColumnName("NotificationRoleID");
            this.Property(t => t.NotificationTemplateCode).HasColumnName("NotificationTemplateCode");
            this.Property(t => t.RoleID).HasColumnName("RoleID");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.NotificationRoles)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.User1)
                .WithMany(t => t.NotificationRoles1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.NotificationTemplate)
                .WithMany(t => t.NotificationRoles)
                .HasForeignKey(d => d.NotificationTemplateCode);
            this.HasRequired(t => t.Role)
                .WithMany(t => t.NotificationRoles)
                .HasForeignKey(d => d.RoleID);
        }
    }
}
