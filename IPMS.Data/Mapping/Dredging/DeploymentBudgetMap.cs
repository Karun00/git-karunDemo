using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class DeploymentBudgetMap : EntityTypeConfiguration<DeploymentBudget>
    {

        public DeploymentBudgetMap()
        {
            // Primary Key
            this.HasKey(t => t.DeploymentBudgetID);

            // Properties
            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.DredgingType)
                .HasMaxLength(4);

            // Table & Column Mappings
            this.ToTable("DeploymentBudget");
            this.Property(t => t.DeploymentBudgetID).HasColumnName("DeploymentBudgetID");
            this.Property(t => t.DeploymentPlanID).HasColumnName("DeploymentPlanID");
            this.Property(t => t.Budget).HasColumnName("Budget");
            this.Property(t => t.DredgPlan).HasColumnName("DredgPlan");
            this.Property(t => t.Jan).HasColumnName("Jan");
            this.Property(t => t.JanCraftID).HasColumnName("JanCraftID");
            this.Property(t => t.Feb).HasColumnName("Feb");
            this.Property(t => t.FebCraftID).HasColumnName("FebCraftID");
            this.Property(t => t.Mar).HasColumnName("Mar");
            this.Property(t => t.MarCraftID).HasColumnName("MarCraftID");
            this.Property(t => t.Apr).HasColumnName("Apr");
            this.Property(t => t.AprCraftID).HasColumnName("AprCraftID");
            this.Property(t => t.May).HasColumnName("May");
            this.Property(t => t.MayCraftID).HasColumnName("MayCraftID");
            this.Property(t => t.Jun).HasColumnName("Jun");
            this.Property(t => t.JunCraftID).HasColumnName("JunCraftID");
            this.Property(t => t.Jul).HasColumnName("Jul");
            this.Property(t => t.JulCraftID).HasColumnName("JulCraftID");
            this.Property(t => t.Aug).HasColumnName("Aug");
            this.Property(t => t.AugCraftID).HasColumnName("AugCraftID");
            this.Property(t => t.Sep).HasColumnName("Sep");
            this.Property(t => t.SepCraftID).HasColumnName("SepCraftID");
            this.Property(t => t.Oct).HasColumnName("Oct");
            this.Property(t => t.OctCraftID).HasColumnName("OctCraftID");
            this.Property(t => t.Nov).HasColumnName("Nov");
            this.Property(t => t.NovCraftID).HasColumnName("NovCraftID");
            this.Property(t => t.Dec).HasColumnName("Dec");
            this.Property(t => t.DecCraftID).HasColumnName("DecCraftID");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.DredgingType).HasColumnName("DredgingType");

            // Relationships
            this.HasOptional(t => t.Craft)
                .WithMany(t => t.DeploymentBudgets)
                .HasForeignKey(d => d.AprCraftID);
            this.HasOptional(t => t.Craft1)
                .WithMany(t => t.DeploymentBudgets1)
                .HasForeignKey(d => d.AugCraftID);
            this.HasOptional(t => t.Craft2)
                .WithMany(t => t.DeploymentBudgets2)
                .HasForeignKey(d => d.DecCraftID);
            this.HasOptional(t => t.Craft3)
                .WithMany(t => t.DeploymentBudgets3)
                .HasForeignKey(d => d.FebCraftID);
            this.HasOptional(t => t.Craft4)
                .WithMany(t => t.DeploymentBudgets4)
                .HasForeignKey(d => d.JanCraftID);
            this.HasOptional(t => t.Craft5)
                .WithMany(t => t.DeploymentBudgets5)
                .HasForeignKey(d => d.JulCraftID);
            this.HasOptional(t => t.Craft6)
                .WithMany(t => t.DeploymentBudgets6)
                .HasForeignKey(d => d.JunCraftID);
            this.HasOptional(t => t.Craft7)
                .WithMany(t => t.DeploymentBudgets7)
                .HasForeignKey(d => d.MarCraftID);
            this.HasOptional(t => t.Craft8)
                .WithMany(t => t.DeploymentBudgets8)
                .HasForeignKey(d => d.MayCraftID);
            this.HasOptional(t => t.Craft9)
                .WithMany(t => t.DeploymentBudgets9)
                .HasForeignKey(d => d.NovCraftID);
            this.HasOptional(t => t.Craft10)
                .WithMany(t => t.DeploymentBudgets10)
                .HasForeignKey(d => d.OctCraftID);
            this.HasOptional(t => t.Craft11)
                .WithMany(t => t.DeploymentBudgets11)
                .HasForeignKey(d => d.SepCraftID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.DeploymentBudgets)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.DeploymentPlan)
                .WithMany(t => t.DeploymentBudgets)
                .HasForeignKey(d => d.DeploymentPlanID);
            this.HasOptional(t => t.SubCategory)
                .WithMany(t => t.DeploymentBudgets)
                .HasForeignKey(d => d.DredgingType);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.DeploymentBudgets1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
