using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class DredgingPriorityMap : EntityTypeConfiguration<DredgingPriority>
    {
        public DredgingPriorityMap()
        {
            // Primary Key
            this.HasKey(t => t.DredgingPriorityID);

            // Properties
            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("DredgingPriority");
            this.Property(t => t.DredgingPriorityID).HasColumnName("DredgingPriorityID");
            this.Property(t => t.DeploymentPlanID).HasColumnName("DeploymentPlanID");
            this.Property(t => t.FromDate).HasColumnName("FromDate");
            this.Property(t => t.ToDate).HasColumnName("ToDate");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.FinancialYearID).HasColumnName("FinancialYearID");
            // Relationships
            this.HasRequired(t => t.DeploymentPlan)
                .WithMany(t => t.DredgingPriorities)
                .HasForeignKey(d => d.DeploymentPlanID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.DredgingPriorities)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.FinancialYear)
               .WithMany(t => t.DredgingPriorities)
               .HasForeignKey(d => d.FinancialYearID);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.DredgingPriorities1)
                .HasForeignKey(d => d.ModifiedBy);
          

        }
    }
}
