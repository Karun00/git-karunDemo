using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class BerthCargoMap : EntityTypeConfiguration<BerthCargo>
    {
        public BerthCargoMap()
        {
            this.HasKey(t => t.BerthCargoID);

            // Properties
            this.Property(t => t.PortCode)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.QuayCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.BerthCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.CargoTypeCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("BerthCargo");
            this.Property(t => t.BerthCargoID).HasColumnName("BerthCargoID");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.QuayCode).HasColumnName("QuayCode");
            this.Property(t => t.BerthCode).HasColumnName("BerthCode");
            this.Property(t => t.CargoTypeCode).HasColumnName("CargoTypeCode");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.Berth)
                .WithMany(t => t.BerthCargoes)
                .HasForeignKey(d => new { d.PortCode, d.QuayCode, d.BerthCode });
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.BerthCargoes)
                .HasForeignKey(d => d.CargoTypeCode);
            this.HasRequired(t => t.User)
                .WithMany(t => t.BerthCargoes)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.BerthCargoes1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
