using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class WorkflowTaskRoleMap : EntityTypeConfiguration<WorkflowTaskRole>
    {
        public WorkflowTaskRoleMap()
        {
            // Primary Key
            this.HasKey(t => new { t.EntityID, t.RoleID, t.Step, t.PortCode });

            // Properties
            this.Property(t => t.EntityID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RoleID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Step)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.PortCode)
                .IsRequired()
                .HasMaxLength(2);

            // Table & Column Mappings
            this.ToTable("WorkflowTaskRole");
            this.Property(t => t.EntityID).HasColumnName("EntityID");
            this.Property(t => t.RoleID).HasColumnName("RoleID");
            this.Property(t => t.Step).HasColumnName("Step");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.PortCode).HasColumnName("PortCode");

            // Relationships
            this.HasRequired(t => t.Role)
                .WithMany(t => t.WorkflowTaskRoles)
                .HasForeignKey(d => d.RoleID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.WorkflowTaskRoles)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.User1)
                .WithMany(t => t.WorkflowTaskRoles1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.Entity)
               .WithMany(t => t.WorkflowTaskRole)
               .HasForeignKey(d => d.EntityID);
            this.HasRequired(t => t.Port)
             .WithMany(t => t.WorkflowTaskRole1)
             .HasForeignKey(d => d.PortCode);
        }
    }
}
