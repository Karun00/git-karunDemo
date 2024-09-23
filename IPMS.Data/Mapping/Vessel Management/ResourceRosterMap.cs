using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Domain.Models
{
    public class ResourceRosterMap : EntityTypeConfiguration<ResourceRoster>
    {
        public ResourceRosterMap()
        {
            // Primary Key
            this.HasKey(t => t.ResourceRosterID);

            // Properties
            this.Property(t => t.Weekday)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("ResourceRoster");
            this.Property(t => t.ResourceRosterID).HasColumnName("ResourceRosterID");
            this.Property(t => t.ResourceGroupID).HasColumnName("ResourceGroupID");
            this.Property(t => t.Weekday).HasColumnName("Weekday");
            this.Property(t => t.ShiftID).HasColumnName("ShiftID");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.ResourceGroup)
                .WithMany(t => t.ResourceRosters)
                .HasForeignKey(d => d.ResourceGroupID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.ResourceRosters)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.ResourceRosters1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.Shift)
                .WithMany(t => t.ResourceRosters)
                .HasForeignKey(d => d.ShiftID);
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.ResourceRosters)
                .HasForeignKey(d => d.Weekday);

        }
    }
}
