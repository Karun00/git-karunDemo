using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class DockingPlanMap : EntityTypeConfiguration<DockingPlan>
    {
        public DockingPlanMap()
        {
            // Primary Key
            this.HasKey(t => t.DockingPlanID);

            // Properties
            this.Property(t => t.Remarks)
                .HasMaxLength(200);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.PortCode)
                .HasMaxLength(2);

            this.Property(t => t.DockingPlanNo)
                .HasMaxLength(15);

            this.Property(t => t.IsFinal)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed)
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("DockingPlan");
            this.Property(t => t.DockingPlanID).HasColumnName("DockingPlanID");
            this.Property(t => t.VesselID).HasColumnName("VesselID");
            this.Property(t => t.WorkflowInstanceID).HasColumnName("WorkflowInstanceID");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.DockingPlanNo).HasColumnName("DockingPlanNo");
            this.Property(t => t.IsFinal).HasColumnName("IsFinal");
            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.DockingPlans)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.DockingPlans1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasOptional(t => t.Port)
                .WithMany(t => t.DockingPlans)
                .HasForeignKey(d => d.PortCode);
            this.HasRequired(t => t.Vessel)
                .WithMany(t => t.DockingPlans)
                .HasForeignKey(d => d.VesselID);
            this.HasRequired(t => t.WorkflowInstance)
                .WithMany(t => t.DockingPlans)
                .HasForeignKey(d => d.WorkflowInstanceID);

        }
    }
}

   