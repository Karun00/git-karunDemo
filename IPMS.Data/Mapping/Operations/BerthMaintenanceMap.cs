using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class BerthMaintenanceMap : EntityTypeConfiguration<BerthMaintenance>
    {
        public BerthMaintenanceMap()
        {
            // Primary Key
            this.HasKey(t => t.BerthMaintenanceID);

            // Properties
            this.Property(t => t.PortCode)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.ProjectNo)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.MaintenanceTypeCode)
                .IsRequired()
                .HasMaxLength(4);


            this.Property(t => t.MaintPortCode)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.MaintQuayCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.MaintBerthCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.FromPortCode)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.FromQuayCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.FromBerthCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.FromBollard)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.ToPortCode)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.ToQuayCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.ToBerthCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.ToBollard)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.OccupationTypeCode)
                .HasMaxLength(4);

            this.Property(t => t.Precinct)
                .HasMaxLength(20);

            this.Property(t => t.DisciplineCode)
                .HasMaxLength(4);

            this.Property(t => t.SpecialConditions)
                .HasMaxLength(200);

            this.Property(t => t.Description)
                .HasMaxLength(200);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.BerthMaintenanceNo)
               .IsRequired()
               .HasMaxLength(15);

            // Table & Column Mappings
            this.ToTable("BerthMaintenance");
            this.Property(t => t.BerthMaintenanceID).HasColumnName("BerthMaintenanceID");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.ProjectNo).HasColumnName("ProjectNo");
            this.Property(t => t.MaintenanceTypeCode).HasColumnName("MaintenanceTypeCode");
            this.Property(t => t.MaintPortCode).HasColumnName("MaintPortCode");
            this.Property(t => t.MaintQuayCode).HasColumnName("MaintQuayCode");
            this.Property(t => t.MaintBerthCode).HasColumnName("MaintBerthCode");
            this.Property(t => t.FromPortCode).HasColumnName("FromPortCode");
            this.Property(t => t.FromQuayCode).HasColumnName("FromQuayCode");
            this.Property(t => t.FromBerthCode).HasColumnName("FromBerthCode");
            this.Property(t => t.FromBollard).HasColumnName("FromBollard");
            this.Property(t => t.ToPortCode).HasColumnName("ToPortCode");
            this.Property(t => t.ToQuayCode).HasColumnName("ToQuayCode");
            this.Property(t => t.ToBerthCode).HasColumnName("ToBerthCode");
            this.Property(t => t.ToBollard).HasColumnName("ToBollard");
            this.Property(t => t.PeriodFrom).HasColumnName("PeriodFrom");
            this.Property(t => t.PeriodTo).HasColumnName("PeriodTo");
            this.Property(t => t.OccupationTypeCode).HasColumnName("OccupationTypeCode");
            this.Property(t => t.Precinct).HasColumnName("Precinct");
            this.Property(t => t.DisciplineCode).HasColumnName("DisciplineCode");
            this.Property(t => t.SpecialConditions).HasColumnName("SpecialConditions");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.WorkflowInstanceId).HasColumnName("WorkflowInstanceId");
            this.Property(t => t.BerthMaintenanceNo).HasColumnName("BerthMaintenanceNo");

            // Relationships
            this.HasRequired(t => t.Berth)
                .WithMany(t => t.BerthMaintenances)
                .HasForeignKey(d => new { d.MaintPortCode, d.MaintQuayCode, d.MaintBerthCode });
            this.HasRequired(t => t.User)
                .WithMany(t => t.BerthMaintenances)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.SubCategory)
                .WithMany(t => t.BerthMaintenances)
                .HasForeignKey(d => d.DisciplineCode);
            this.HasRequired(t => t.Bollard)
                .WithMany(t => t.BerthMaintenances)
                .HasForeignKey(d => new { d.FromPortCode, d.FromQuayCode, d.FromBerthCode, d.FromBollard });
            this.HasRequired(t => t.SubCategory1)
                .WithMany(t => t.BerthMaintenances1)
                .HasForeignKey(d => d.MaintenanceTypeCode);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.BerthMaintenances1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.Port)
                .WithMany(t => t.BerthMaintenances)
                .HasForeignKey(d => d.PortCode);
            this.HasRequired(t => t.Bollard1)
                .WithMany(t => t.BerthMaintenances1)
                .HasForeignKey(d => new { d.ToPortCode, d.ToQuayCode, d.ToBerthCode, d.ToBollard });
            this.HasOptional(t => t.WorkflowInstance)
            .WithMany(t => t.BerthMaintenances)
            .HasForeignKey(d => d.WorkflowInstanceId);

        }
    }
}
