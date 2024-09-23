using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class MarpolMap : EntityTypeConfiguration<Marpol>
    {
        public MarpolMap()
        {
            // Primary Key
            this.HasKey(t => t.ClassCode);

            // Properties
            this.Property(t => t.ClassCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.MarpolCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.ClassName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.RecordStatus)
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("Marpol");
            this.Property(t => t.ClassCode).HasColumnName("ClassCode");
            this.Property(t => t.MarpolCode).HasColumnName("MarpolCode");
            this.Property(t => t.ClassName).HasColumnName("ClassName");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships           
            this.HasRequired(t => t.User)
                .WithMany(t => t.Marpols)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.Marpols1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.Marpols)
                .HasForeignKey(d => d.MarpolCode);
        }
    }
}
