using IPMS.Domain.Models;
using Core.Repository;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;


namespace IPMS.Data.Mapping
{
    public class ResourceGangConfigMap : EntityTypeConfiguration<ResourceGangConfig>
    {
        public ResourceGangConfigMap()
        {
            // Primary Key
            this.HasKey(t => t.ResourceGangConfigID);

            // Properties
            // Table & Column Mappings
            this.ToTable("ResourceGangConfig");
            this.Property(t => t.ResourceGangConfigID).HasColumnName("ResourceGangConfigID");
            this.Property(t => t.ResourceAllocationConfigRuleID).HasColumnName("ResourceAllocationConfigRuleID");
            this.Property(t => t.FromMeter).HasColumnName("FromMeter");
            this.Property(t => t.ToMeter).HasColumnName("ToMeter");
            this.Property(t => t.NoOfGangs).HasColumnName("NoOfGangs");

            // Relationships
            this.HasRequired(t => t.ResourceAllocationConfigRule)
                .WithMany(t => t.ResourceGangConfigs)
                .HasForeignKey(d => d.ResourceAllocationConfigRuleID);

        }
    }
}
