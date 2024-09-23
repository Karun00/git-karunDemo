using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class SuppFloatingCraneMap : EntityTypeConfiguration<SuppFloatingCrane>
    {
        public SuppFloatingCraneMap()
        {
            // Primary Key
            this.HasKey(t => t.SuppFloatingCraneID);

            // Properties
            this.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("SuppFloatingCrane");
            this.Property(t => t.SuppFloatingCraneID).HasColumnName("SuppFloatingCraneID");
            this.Property(t => t.SuppServiceRequestID).HasColumnName("SuppServiceRequestID");
            this.Property(t => t.MassMT).HasColumnName("MassMT");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.SuppFloatingCranes)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.SuppFloatingCranes1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.SuppServiceRequest)
                .WithMany(t => t.SuppFloatingCranes)
                .HasForeignKey(d => d.SuppServiceRequestID);

        }
    }
}
