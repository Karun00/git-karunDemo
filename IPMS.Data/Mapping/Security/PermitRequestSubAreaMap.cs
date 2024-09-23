using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class PermitRequestSubAreaMap: EntityTypeConfiguration<PermitRequestSubArea>
    {
        public PermitRequestSubAreaMap()
        {
            // Primary Key
            this.HasKey(t => t.PermitRequestSubAreaID);

            // Properties
            this.Property(t => t.PermitRequestSubAreaCode)
                .IsRequired()
                .HasMaxLength(4);

            // Table & Column Mappings
            this.ToTable("PermitRequestSubArea");
            this.Property(t => t.PermitRequestSubAreaID).HasColumnName("PermitRequestSubAreaID");          
            this.Property(t => t.PermitRequestID).HasColumnName("PermitRequestID");
            this.Property(t => t.PermitRequestSubAreaCode).HasColumnName("PermitRequestSubAreaCode");
            this.Property(t => t.PermitRequestAreaCode).HasColumnName("PermitRequestAreaCode");

            // Relationships
            this.HasRequired(t => t.PermitRequest)
                .WithMany(t => t.PermitRequestSubAreas)
                .HasForeignKey(d => d.PermitRequestID);

            this.HasRequired(t => t.SuperCategory)
               .WithMany(t => t.PermitRequestSubAreas)
               .HasForeignKey(d => d.PermitRequestAreaCode);

            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.PermitRequestSubAreas)
                .HasForeignKey(d => d.PermitRequestSubAreaCode);

          

        }
    }
}
