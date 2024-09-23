using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class ModuleMap : EntityTypeConfiguration<Module>
    {
        public ModuleMap()
        {
            // Primary Key
            this.HasKey(t => t.ModuleID);

            // Properties
            this.Property(t => t.ModuleName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.PageUrl)
                .HasMaxLength(100);

            this.Property(t => t.MobileImage)
             .HasMaxLength(50);


            this.Property(t => t.RecordStatus)
                .IsFixedLength()
                .HasMaxLength(1);

           

            // Table & Column Mappings
            this.ToTable("Module");
            this.Property(t => t.ModuleID).HasColumnName("ModuleID");
            this.Property(t => t.ParentModuleID).HasColumnName("ParentModuleID");
            this.Property(t => t.ModuleName).HasColumnName("ModuleName");
            this.Property(t => t.MobileReference).HasColumnName("MobileReference");
            this.Property(t => t.Count).HasColumnName("Count");
            this.Property(t => t.IsMobile).HasColumnName("IsMobile");
            this.Property(t => t.OrderNo).HasColumnName("OrderNo");
            this.Property(t => t.MobileImage).HasColumnName("MobileImage");
            this.Property(t => t.PageUrl).HasColumnName("PageUrl");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.Modules)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.User1)
                .WithMany(t => t.Modules1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasOptional(t => t.Module2)
                .WithMany(t => t.Module1)
                .HasForeignKey(d => d.ParentModuleID);

        }
    }
}
