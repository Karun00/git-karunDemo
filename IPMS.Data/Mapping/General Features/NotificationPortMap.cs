using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class NotificationPortMap : EntityTypeConfiguration<NotificationPort>
    {
        public NotificationPortMap()
        {
            // Primary Key
            this.HasKey(t => new { t.NotificationTemplateCode, t.PortCode });

            // Properties
            this.Property(t => t.NotificationTemplateCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.PortCode)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("NotificationPort");
            this.Property(t => t.NotificationTemplateCode).HasColumnName("NotificationTemplateCode");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.NotificationPorts)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.User1)
                .WithMany(t => t.NotificationPorts1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.NotificationTemplate)
                .WithMany(t => t.NotificationPorts)
                .HasForeignKey(d => d.NotificationTemplateCode);
            this.HasRequired(t => t.Port)
                .WithMany(t => t.NotificationPorts)
                .HasForeignKey(d => d.PortCode);

        }
    }
}
