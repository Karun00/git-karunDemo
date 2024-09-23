using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class DeploymentPlanMap : EntityTypeConfiguration<DeploymentPlan>
    {

        public DeploymentPlanMap()
        {
            // Primary Key
            this.HasKey(t => t.DeploymentPlanID);

            // Properties
            this.Property(t => t.PortCode)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("DeploymentPlan");
            this.Property(t => t.DeploymentPlanID).HasColumnName("DeploymentPlanID");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.FinancialYearID).HasColumnName("FinancialYearID");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.DeploymentPlans)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.FinancialYear)
                .WithMany(t => t.DeploymentPlans)
                .HasForeignKey(d => d.FinancialYearID);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.DeploymentPlans1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.Port)
                .WithMany(t => t.DeploymentPlans)
                .HasForeignKey(d => d.PortCode);

        }
    }
}

