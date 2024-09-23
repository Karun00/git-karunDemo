using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class PermitRequestVerifyedDetailMap : EntityTypeConfiguration<PermitRequestVerifyedDetail>
    {
        public PermitRequestVerifyedDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.PermitRequestverifyedID);

            // Properties
            this.Property(t => t.CreminalCheck)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.Flag)
                .HasMaxLength(4);

            this.Property(t => t.RecordStatus)
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("PermitRequestVerifyedDetails");
            this.Property(t => t.PermitRequestverifyedID).HasColumnName("PermitRequestverifyedID");
            this.Property(t => t.permitrRequestID).HasColumnName("permitrRequestID");
            this.Property(t => t.CreminalCheck).HasColumnName("CreminalCheck");
            this.Property(t => t.Comments).HasColumnName("Comments");
            this.Property(t => t.Flag).HasColumnName("Flag");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.verifyedUserID).HasColumnName("verifyedUserID");
            this.Property(t => t.verifyedDate).HasColumnName("verifyedDate");

            // Relationships
            this.HasOptional(t => t.PermitRequest)
                .WithMany(t => t.PermitRequestVerifyedDetails)
                .HasForeignKey(d => d.permitrRequestID);

        }
    }
}
