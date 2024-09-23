using IPMS.Domain.Models;
using System.Data.Entity.ModelConfiguration;

namespace IPMS.Data.Mapping
{
    public class EmailAlertMap : EntityTypeConfiguration<EmailAlert>
    {
        public EmailAlertMap()
        {
            // Primary Key
            this.HasKey(t => new { t.EmaliTblPkID, t.EmailParam, t.EmailID, t.EmailContent, t.Status });

            // Properties
            this.Property(t => t.EmaliTblPkID)
                .HasDatabaseGeneratedOption(null);

            this.Property(t => t.EmailParam)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.EmailID)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EmailContent)
                .IsRequired()
                .HasMaxLength(2000);

            this.Property(t => t.Status)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.Remarks)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Email_Alert");
            this.Property(t => t.EmaliTblPkID).HasColumnName("EmaliTblPkID");
            this.Property(t => t.EmailParam).HasColumnName("EmailParam");
            this.Property(t => t.EmailID).HasColumnName("EmailID");
            this.Property(t => t.EmailContent).HasColumnName("EmailContent");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.EmailCreaDate).HasColumnName("EmailCreaDate");
            this.Property(t => t.SendDate).HasColumnName("SendDate");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
        }
    }
}
