using IPMS.Domain.Models;
using Core.Repository;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;


namespace IPMS.Data.Mapping
{
    public class ResourceAllocationConfigRuleMap : EntityTypeConfiguration<ResourceAllocationConfigRule>
    {
        public ResourceAllocationConfigRuleMap()
        {
            // Primary Key
            this.HasKey(t => t.ResourceAllocationConfigRuleID);

            // Properties
            this.Property(t => t.PortCode)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.PilotCapacity)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("ResourceAllocationConfigRule");
            this.Property(t => t.ResourceAllocationConfigRuleID).HasColumnName("ResourceAllocationConfigRuleID");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.PilotCapacity).HasColumnName("PilotCapacity");
            this.Property(t => t.TotalTugs).HasColumnName("TotalTugs");
            this.Property(t => t.EffectedFrom).HasColumnName("EffectedFrom");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.Port)
                .WithMany(t => t.ResourceAllocationConfigRules)
                .HasForeignKey(d => d.PortCode);
            this.HasRequired(t => t.User)
                .WithMany(t => t.ResourceAllocationConfigRules)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.ResourceAllocationConfigRules1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.ResourceAllocationConfigRules)
                .HasForeignKey(d => d.PilotCapacity);

        }
    }
}
