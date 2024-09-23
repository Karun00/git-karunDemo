using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Domain.DTOS
{
    
    public class PortRegistryMap : EntityTypeConfiguration<PortRegistry>
    {
        public PortRegistryMap()
        {
            // Primary Key
            this.HasKey(t => t.PortCode);

            // Properties
            this.Property(t => t.PortCode)
                .IsRequired()
                .HasMaxLength(5);

            this.Property(t => t.PortName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.IsSA)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.IsTNPA)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("PortRegistry");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.PortName).HasColumnName("PortName");
            this.Property(t => t.IsSA).HasColumnName("IsSA");
            this.Property(t => t.IsTNPA).HasColumnName("IsTNPA");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.PortRegistries)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.PortRegistries1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
