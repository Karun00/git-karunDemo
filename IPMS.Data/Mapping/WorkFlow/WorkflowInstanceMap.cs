using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
	public class WorkflowInstanceMap : EntityTypeConfiguration<WorkflowInstance>
	{
		public WorkflowInstanceMap()
		{
            // Primary Key
            this.HasKey(t => t.WorkflowInstanceId);

            // Properties
            this.Property(t => t.PortCode)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.ReferenceID)
                .IsRequired()
                .HasMaxLength(12);

            this.Property(t => t.WorkflowTaskCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.UserType)
                .HasMaxLength(4);

            this.Property(t => t.WorkflowProcessId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            // Table & Column Mappings
            this.ToTable("WorkflowInstance");
            this.Property(t => t.WorkflowInstanceId).HasColumnName("WorkflowInstanceId");
            this.Property(t => t.EntityID).HasColumnName("EntityID");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.ReferenceID).HasColumnName("ReferenceID");
            this.Property(t => t.WorkflowTaskCode).HasColumnName("WorkflowTaskCode");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.UserTypeId).HasColumnName("UserTypeId");
            this.Property(t => t.UserType).HasColumnName("UserType");
            this.Property(t => t.WorkflowProcessId).HasColumnName("WorkflowProcessId");

            // Relationships
            this.HasRequired(t => t.Entity)
                .WithMany(t => t.WorkflowInstances)
                .HasForeignKey(d => d.EntityID);
            this.HasRequired(t => t.Port)
                .WithMany(t => t.WorkflowInstances)
                .HasForeignKey(d => d.PortCode);
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.WorkflowInstances)
                .HasForeignKey(d => d.WorkflowTaskCode);
            this.HasRequired(t => t.User)
                .WithMany(t => t.WorkflowInstances)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.User1)
                .WithMany(t => t.WorkflowInstances1)
                .HasForeignKey(d => d.ModifiedBy);


		}
	}
}
