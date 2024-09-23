using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Domain.Models
{
    public class ResourceEmployeeGroupMap : EntityTypeConfiguration<ResourceEmployeeGroup>
    {
        public ResourceEmployeeGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.ResourceEmployeeGroupID);

            // Properties
            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("ResourceEmployeeGroup");
            this.Property(t => t.ResourceEmployeeGroupID).HasColumnName("ResourceEmployeeGroupID");
            this.Property(t => t.ResourceGroupID).HasColumnName("ResourceGroupID");
            this.Property(t => t.EmployeeID).HasColumnName("EmployeeID");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.Employee)
                .WithMany(t => t.ResourceEmployeeGroups)
                .HasForeignKey(d => d.EmployeeID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.ResourceEmployeeGroups)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.ResourceEmployeeGroups1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.ResourceGroup)
                .WithMany(t => t.ResourceEmployeeGroups)
                .HasForeignKey(d => d.ResourceGroupID);

        }
    }
}
