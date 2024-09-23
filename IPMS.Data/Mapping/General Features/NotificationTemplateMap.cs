using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class NotificationTemplateMap : EntityTypeConfiguration<NotificationTemplate>
    {
        public NotificationTemplateMap()
        {
            // Primary Key
            this.HasKey(t => t.NotificationTemplateCode);

            // Properties
            this.Property(t => t.NotificationTemplateCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.NotificationTemplateName)
                .IsRequired()
                .HasMaxLength(60);

            this.Property(t => t.IsEmail)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.EmailSubject)
                .HasMaxLength(200);

            this.Property(t => t.IsSMS)
               .IsFixedLength()
               .HasMaxLength(1);

            this.Property(t => t.SMSTemplate)
                .HasMaxLength(2000);

            this.Property(t => t.IsSysMessage)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SysMessageTemplate)
                .HasMaxLength(2000);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.NotificationTemplateBase)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("NotificationTemplate");
            this.Property(t => t.NotificationTemplateCode).HasColumnName("NotificationTemplateCode");
            this.Property(t => t.NotificationTemplateName).HasColumnName("NotificationTemplateName");
            this.Property(t => t.RecordStatus).HasColumnName("NotificationTemplateBase");
            this.Property(t => t.EntityID).HasColumnName("EntityID");
            this.Property(t => t.WorkflowTaskCode).HasColumnName("WorkflowTaskCode");
            this.Property(t => t.IsEmail).HasColumnName("IsEmail");
            this.Property(t => t.EmailSubject).HasColumnName("EmailSubject");
            this.Property(t => t.EmailTemplate).HasColumnName("EmailTemplate");
            this.Property(t => t.IsSMS).HasColumnName("IsSMS");
            this.Property(t => t.SMSTemplate).HasColumnName("SMSTemplate");
            this.Property(t => t.IsSysMessage).HasColumnName("IsSysMessage");
            this.Property(t => t.SysMessageTemplate).HasColumnName("SysMessageTemplate");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.Entity)
                .WithMany(t => t.NotificationTemplates)
                .HasForeignKey(d => d.EntityID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.NotificationTemplates)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User)
                .WithMany(t => t.NotificationTemplates)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.SubCategory)
               .WithMany(t => t.NotificationTemplates)
               .HasForeignKey(d => d.WorkflowTaskCode);

        }
    }
}
