using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class PermitRequestAreaMap : EntityTypeConfiguration<PermitRequestArea>
    {
        public PermitRequestAreaMap()
        {
            // Primary Key
            this.HasKey(t => t.PermitRequestAreaID);

            // Properties
            this.Property(t => t.PermitRequestAreaCode)
                .IsRequired()
                .HasMaxLength(4);

            // Table & Column Mappings
            this.ToTable("PermitRequestArea");
            this.Property(t => t.PermitRequestAreaID).HasColumnName("PermitRequestAreaID");
            this.Property(t => t.PermitRequestID).HasColumnName("PermitRequestID");
            this.Property(t => t.PermitRequestAreaCode).HasColumnName("PermitRequestAreaCode");

            // Relationships
            this.HasRequired(t => t.PermitRequest)
                .WithMany(t => t.PermitRequestAreas)
                .HasForeignKey(d => d.PermitRequestID);
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.PermitRequestAreas)
                .HasForeignKey(d => d.PermitRequestAreaCode);

        }
    }
}
