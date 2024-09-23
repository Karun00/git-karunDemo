using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class ArrivalReasonMap : EntityTypeConfiguration<ArrivalReason>
	{
		public ArrivalReasonMap()
        {
            // Primary Key
            this.HasKey(t => t.ArrivalReasonID);

            // Properties
            this.Property(t => t.VCN)
                .IsRequired()
                .HasMaxLength(12);

            this.Property(t => t.Reason)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("ArrivalReason");
            this.Property(t => t.ArrivalReasonID).HasColumnName("ArrivalReasonID");
            this.Property(t => t.VCN).HasColumnName("VCN");
            this.Property(t => t.Reason).HasColumnName("Reason");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.ArrivalNotification)
                .WithMany(t => t.ArrivalReasons)
                .HasForeignKey(d => d.VCN);
            this.HasRequired(t => t.User)
                .WithMany(t => t.ArrivalReasons)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.ArrivalReasons1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.ArrivalReasons)
                .HasForeignKey(d => d.Reason);

        }
	}
}
