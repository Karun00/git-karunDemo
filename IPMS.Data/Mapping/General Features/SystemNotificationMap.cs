using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class SystemNotificationMap : EntityTypeConfiguration<SystemNotification>
    {
        public SystemNotificationMap()
        {
            // Primary Key
            this.HasKey(t => new { t.UserID, t.NotificationId });

            // Properties
            this.Property(t => t.UserID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NotificationText)
                .HasMaxLength(2000);

            this.Property(t => t.IsRead)
             .IsRequired()
             .IsFixedLength()
             .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("SystemNotification");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.NotificationId).HasColumnName("NotificationId");
            this.Property(t => t.NotificationText).HasColumnName("NotificationText");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.IsRead).HasColumnName("IsRead");

            // Relationships
            this.HasRequired(t => t.Notification)
                .WithMany(t => t.SystemNotifications)
                .HasForeignKey(d => d.NotificationId);
            this.HasRequired(t => t.User)
                .WithMany(t => t.SystemNotifications)
                .HasForeignKey(d => d.UserID);
            this.HasRequired(t => t.Port)
                .WithMany(t => t.SystemNotifications)
                .HasForeignKey(d => d.PortCode);

        }
    }
}
