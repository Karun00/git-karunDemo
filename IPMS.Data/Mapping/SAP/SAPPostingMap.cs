using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class SAPPostingMap : EntityTypeConfiguration<SAPPosting>
    {  
        public SAPPostingMap()
        {
            // Primary Key
            this.HasKey(t => t.SAPPostingID);

            // Properties
            this.Property(t => t.MessageType)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.NotificationTemplateCode)
                .HasMaxLength(4);

            this.Property(t => t.ReferenceNo)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.PostingStatus)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.Remarks)
                .HasMaxLength(2000);

            this.Property(t => t.SAPReferenceNo)
                .HasMaxLength(12);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

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

            this.Property(t => t.UserType)
                .HasMaxLength(4);

            this.Property(t => t.PortCode)
                .HasMaxLength(2);

            this.Property(t => t.Reason)
                .HasMaxLength(4);


            this.Property(t => t.RevinueAccountNo)
                .HasMaxLength(50);
         
            // Table & Column Mappings
            this.ToTable("SAPPosting");
            this.Property(t => t.SAPPostingID).HasColumnName("SAPPostingID");
            this.Property(t => t.MessageType).HasColumnName("MessageType");
            this.Property(t => t.NotificationTemplateCode).HasColumnName("NotificationTemplateCode");
            this.Property(t => t.ReferenceNo).HasColumnName("ReferenceNo");
            this.Property(t => t.PostingStatus).HasColumnName("PostingStatus");
            this.Property(t => t.TransmitData).HasColumnName("TransmitData");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.SAPReferenceNo).HasColumnName("SAPReferenceNo");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.EmailStatus).HasColumnName("EmailStatus");
            this.Property(t => t.SMSStatus).HasColumnName("SMSStatus");
            this.Property(t => t.SystemNotificationStatus).HasColumnName("SystemNotificationStatus");
            this.Property(t => t.UserTypeId).HasColumnName("UserTypeId");
            this.Property(t => t.UserType).HasColumnName("UserType");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.Reason).HasColumnName("Reason");
            this.Property(t => t.RevinueAccountNo).HasColumnName("RevinueAccountNo");
            this.Property(t => t.MarinePostingId).HasColumnName("MarinePostingId");
            this.Property(t => t.RevenueAgentAccNo).HasColumnName("RevenueAgentAccNo");
                // Relationships

            this.HasOptional(t => t.SAPNotificationTemplate)
            .WithMany(t => t.SAPNotifications)
            .HasForeignKey(d => d.NotificationTemplateCode);
            
            this.HasOptional(t => t.Port)
                .WithMany(t => t.SAPPostings)
                .HasForeignKey(d => d.PortCode);
            this.HasRequired(t => t.User)
                .WithMany(t => t.SAPPostings)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.SAPPostings)
                .HasForeignKey(d => d.MessageType);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.SAPPostings1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.SubCategory1)
                .WithMany(t => t.SAPPostings1)
                .HasForeignKey(d => d.PostingStatus);
            this.HasOptional(t => t.SubCategory2)
                .WithMany(t => t.SAPPostings2)
                .HasForeignKey(d => d.Reason);



            //this.HasRequired(t => t.SAPNotificationTemplate)
            //.WithMany(t => t.SAPNotifications)
            //.HasForeignKey(d => d.NotificationTemplateCode);

  

        }
    }
}
