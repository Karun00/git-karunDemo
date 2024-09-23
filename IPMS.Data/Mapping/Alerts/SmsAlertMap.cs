using IPMS.Domain.Models;
using System.Data.Entity.ModelConfiguration;

namespace IPMS.Data.Mapping
{
    public class SmsAlertMap : EntityTypeConfiguration<SmsAlert>
    {
        public SmsAlertMap()
        {
            // Primary Key
            this.HasKey(t => new { t.SmsTblPkID, t.SmsParam, t.MobileNo, t.SmsContent, t.Status });

            // Properties
            this.Property(t => t.SmsTblPkID)
                .HasDatabaseGeneratedOption(null);
            
            

            this.Property(t => t.SmsParam)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.MobileNo)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.SmsContent)
                .IsRequired()
                .HasMaxLength(400);

            this.Property(t => t.Status)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.Remarks)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Sms_Alert");
            this.Property(t => t.SmsTblPkID).HasColumnName("SmsTblPkID");
            this.Property(t => t.SmsParam).HasColumnName("SmsParam");
            this.Property(t => t.MobileNo).HasColumnName("MobileNo");
            this.Property(t => t.SmsContent).HasColumnName("SmsContent");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.SmsCreaDate).HasColumnName("SmsCreaDate");
            this.Property(t => t.SendDate).HasColumnName("SendDate");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
        }
    }
}
