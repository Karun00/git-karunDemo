using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class NotificationMap : EntityTypeConfiguration<Notification>
    {
        public NotificationMap()
        {
            // Primary Key
            this.HasKey(t => t.NotificationId);

            // Properties
            this.Property(t => t.NotificationTemplateCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.Reference)
                .IsRequired()
                .HasMaxLength(12);

            this.Property(t => t.EmailStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SMSStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SystemNotificationStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);


            this.Property(t => t.UserType)
                .HasMaxLength(4);

            this.Property(t => t.PortCode)
               .IsRequired()
               .HasMaxLength(2);

            // Table & Column Mappings
            this.ToTable("Notification");
            this.Property(t => t.NotificationId).HasColumnName("NotificationId");
            this.Property(t => t.NotificationTemplateCode).HasColumnName("NotificationTemplateCode");
            this.Property(t => t.DateTime).HasColumnName("DateTime");
            this.Property(t => t.Reference).HasColumnName("Reference");
            this.Property(t => t.EmailStatus).HasColumnName("EmailStatus");
            this.Property(t => t.SMSStatus).HasColumnName("SMSStatus");
            this.Property(t => t.SystemNotificationStatus).HasColumnName("SystemNotificationStatus");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.UserTypeId).HasColumnName("UserTypeId");
            this.Property(t => t.UserType).HasColumnName("UserType");
            this.Property(t => t.PortCode).HasColumnName("PortCode");


            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.Notifications)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.Notifications1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.NotificationTemplate)
                .WithMany(t => t.Notifications)
                .HasForeignKey(d => d.NotificationTemplateCode);
            this.HasRequired(t => t.Port)
              .WithMany(t => t.Notifications)
              .HasForeignKey(d => d.PortCode);
        }
    }
}
