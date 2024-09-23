using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Domain.Models
{
    public class ResourceGroupMap : EntityTypeConfiguration<ResourceGroup>
    {
        public ResourceGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.ResourceGroupID);

            // Properties
            this.Property(t => t.PortCode)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.ResourceGroupName)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.Position)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("ResourceGroup");
            this.Property(t => t.ResourceGroupID).HasColumnName("ResourceGroupID");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.ResourceGroupName).HasColumnName("ResourceGroupName");
            this.Property(t => t.Position).HasColumnName("Position");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.Port)
                .WithMany(t => t.ResourceGroups)
                .HasForeignKey(d => d.PortCode);
            this.HasRequired(t => t.User)
                .WithMany(t => t.ResourceGroups)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.ResourceGroups1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.ResourceGroups)
                .HasForeignKey(d => d.Position);

        }
    }
}
