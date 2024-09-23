using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPMS.Data.Mapping
{
    public class BerthPlanningConfigurationsMap : EntityTypeConfiguration<BerthPlanningConfigurations>
    {
        public BerthPlanningConfigurationsMap()
        {
            // Primary Key
            this.HasKey(t => t.BerthPlanConfigid);

            // Properties
            this.Property(t => t.BerthPlanConfigid).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Properties
            this.Property(t => t.PortCode).HasMaxLength(2);
            this.Property(t => t.RecordStatus).IsRequired().IsFixedLength().HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("BerthPlanningConfigurations");
            this.Property(t => t.BerthPlanConfigid).HasColumnName("BerthPlanConfigid");
            this.Property(t => t.Days).HasColumnName("Days");
            this.Property(t => t.Slot).HasColumnName("Slot");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            
            this.HasRequired(t => t.User).WithMany(t => t.BerthPlanningConfigurations).HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1).WithMany(t => t.BerthPlanningConfigurations1).HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.Port).WithMany(t => t.BerthPlanningConfigurations).HasForeignKey(d => d.PortCode);
        }
    }
}
