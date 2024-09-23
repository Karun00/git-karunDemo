using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class DredgingOperationMap : EntityTypeConfiguration<DredgingOperation>
    {
        public DredgingOperationMap()
        {
            // Primary Key
            this.HasKey(t => t.DredgingOperationID);

            // Properties
            this.Property(t => t.TypeCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.DPARemarks)
                .HasMaxLength(500);

            this.Property(t => t.AreaType)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.PortCode)
                .HasMaxLength(2);

            this.Property(t => t.QuayCode)
                .HasMaxLength(4);

            this.Property(t => t.BerthCode)
                .HasMaxLength(4);

            this.Property(t => t.OccupationDuration)
                .HasMaxLength(10);

            this.Property(t => t.DredgingTask)
                .HasMaxLength(500);

            this.Property(t => t.DredgingDelay)
                .HasMaxLength(500);

            this.Property(t => t.DVRemarks)
                .HasMaxLength(500);

            this.Property(t => t.DredgingStatus)
                .HasMaxLength(4);

            this.Property(t => t.IsDPAFinal)
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.IsDOFinal)
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.IsDVFinal)
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.DredgerName)
                .HasMaxLength(30);
            this.Property(t => t.VolumeOccupationDuration)
               .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("DredgingOperation");
            this.Property(t => t.DredgingOperationID).HasColumnName("DredgingOperationID");
            this.Property(t => t.DredgingPriorityID).HasColumnName("DredgingPriorityID");
            this.Property(t => t.Priority).HasColumnName("Priority");
            this.Property(t => t.AreaLocationID).HasColumnName("AreaLocationID");
            this.Property(t => t.TypeCode).HasColumnName("TypeCode");
            this.Property(t => t.RequiredDate).HasColumnName("RequiredDate");
            this.Property(t => t.DesignDepth).HasColumnName("DesignDepth");
            this.Property(t => t.PromulgateDepth).HasColumnName("PromulgateDepth");
            this.Property(t => t.Requirement).HasColumnName("Requirement");
            this.Property(t => t.DPARemarks).HasColumnName("DPARemarks");
            this.Property(t => t.AreaType).HasColumnName("AreaType");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.QuayCode).HasColumnName("QuayCode");
            this.Property(t => t.BerthCode).HasColumnName("BerthCode");
            this.Property(t => t.DPAWorkflowInstanceID).HasColumnName("DPAWorkflowInstanceID");
            this.Property(t => t.OccupationFrom).HasColumnName("OccupationFrom");
            this.Property(t => t.OccupationTo).HasColumnName("OccupationTo");
            this.Property(t => t.OccupationDuration).HasColumnName("OccupationDuration");
            this.Property(t => t.DOWorkflowInstanceID).HasColumnName("DOWorkflowInstanceID");
            this.Property(t => t.Volume).HasColumnName("Volume");
            this.Property(t => t.CraftID).HasColumnName("CraftID");
            this.Property(t => t.DredgingTask).HasColumnName("DredgingTask");
            this.Property(t => t.DredgingDelay).HasColumnName("DredgingDelay");
            this.Property(t => t.DVRemarks).HasColumnName("DVRemarks");
            this.Property(t => t.DredgingStatus).HasColumnName("DredgingStatus");
            this.Property(t => t.IsDPAFinal).HasColumnName("IsDPAFinal");
            this.Property(t => t.IsDOFinal).HasColumnName("IsDOFinal");
            this.Property(t => t.IsDVFinal).HasColumnName("IsDVFinal");
            this.Property(t => t.FinancialYearID).HasColumnName("FinancialYearID");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.DVWorkflowInstanceID).HasColumnName("DVWorkflowInstanceID");
            this.Property(t => t.DredgerName).HasColumnName("DredgerName");
            this.Property(t => t.VolumeOccupationFrom).HasColumnName("VolumeOccupationFrom");
            this.Property(t => t.VolumeOccupationTo).HasColumnName("VolumeOccupationTo");
            this.Property(t => t.VolumeOccupationDuration).HasColumnName("VolumeOccupationDuration");
            // Relationships
            this.HasOptional(t => t.Berth)
                .WithMany(t => t.DredgingOperations)
                .HasForeignKey(d => new { d.PortCode, d.QuayCode, d.BerthCode });
            this.HasOptional(t => t.Craft)
                .WithMany(t => t.DredgingOperations)
                .HasForeignKey(d => d.CraftID);
            this.HasOptional(t => t.Location)
                .WithMany(t => t.DredgingOperations)
                .HasForeignKey(d => d.AreaLocationID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.DredgingOperations)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.WorkflowInstance)
                .WithMany(t => t.DredgingOperations)
                .HasForeignKey(d => d.DOWorkflowInstanceID);
            this.HasOptional(t => t.WorkflowInstance1)
                .WithMany(t => t.DredgingOperations1)
                .HasForeignKey(d => d.DPAWorkflowInstanceID);
            this.HasRequired(t => t.DredgingPriority)
                .WithMany(t => t.DredgingOperations)
                .HasForeignKey(d => d.DredgingPriorityID);
            this.HasOptional(t => t.SubCategory)
                .WithMany(t => t.DredgingOperations)
                .HasForeignKey(d => d.DredgingStatus);
            //this.HasOptional(t => t.SubCategory1)
            //    .WithMany(t => t.DredgingOperations1)
            //    .HasForeignKey(d => d.DredgingTask);
            this.HasOptional(t => t.FinancialYear)
                .WithMany(t => t.DredgingOperations)
                .HasForeignKey(d => d.FinancialYearID);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.DredgingOperations1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.SubCategory2)
                .WithMany(t => t.DredgingOperations2)
                .HasForeignKey(d => d.TypeCode);
            this.HasOptional(t => t.WorkflowInstance2)
                .WithMany(t => t.DredgingOperations2)
                .HasForeignKey(d => d.DVWorkflowInstanceID);

        }
    }
}
