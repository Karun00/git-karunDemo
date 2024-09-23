using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class SuppServiceResourceAllocMap : EntityTypeConfiguration<SuppServiceResourceAlloc>
    {
        public SuppServiceResourceAllocMap()
        {
            // Primary Key
            this.HasKey(t => t.SuppServiceResourceAllocID);

            // Properties
            this.Property(t => t.AllocSlot)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("SuppServiceResourceAlloc");
            this.Property(t => t.SuppServiceResourceAllocID).HasColumnName("SuppServiceResourceAllocID");
            this.Property(t => t.SuppWaterServiceID).HasColumnName("SuppWaterServiceID");
            this.Property(t => t.AllocDate).HasColumnName("AllocDate");
            this.Property(t => t.AllocSlot).HasColumnName("AllocSlot");
            this.Property(t => t.ResourceID).HasColumnName("ResourceID");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.Employee)
                .WithMany(t => t.SuppServiceResourceAllocs)
                .HasForeignKey(d => d.ResourceID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.SuppServiceResourceAllocs)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.SuppServiceResourceAllocs1)
                .HasForeignKey(d => d.ModifiedBy);
        }
    }
}
