using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Domain.Models
{
    public class FuelRequisitionMap : EntityTypeConfiguration<FuelRequisition>
    {
        public FuelRequisitionMap()
        {
            // Primary Key
            this.HasKey(t => t.FuelRequisitionID);

            // Properties
            this.Property(t => t.PortCode)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.FuelRequistionNo)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.OilTypeCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.GradeCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.UOMCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.Remarks)
                .HasMaxLength(500);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("FuelRequisition");
            this.Property(t => t.FuelRequisitionID).HasColumnName("FuelRequisitionID");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.FuelRequistionNo).HasColumnName("FuelRequistionNo");
            this.Property(t => t.CraftID).HasColumnName("CraftID");
            this.Property(t => t.RequisitionDate).HasColumnName("RequisitionDate");
            this.Property(t => t.OilTypeCode).HasColumnName("OilTypeCode");
            this.Property(t => t.GradeCode).HasColumnName("GradeCode");
            this.Property(t => t.UOMCode).HasColumnName("UOMCode");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.RequiredDate).HasColumnName("RequiredDate");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.WorkflowInstanceId).HasColumnName("WorkflowInstanceId");

            // Relationships
            this.HasRequired(t => t.Craft)
                .WithMany(t => t.FuelRequisitions)
                .HasForeignKey(d => d.CraftID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.FuelRequisitions)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.FuelRequisitions)
                .HasForeignKey(d => d.GradeCode);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.FuelRequisitions1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.SubCategory1)
                .WithMany(t => t.FuelRequisitions1)
                .HasForeignKey(d => d.OilTypeCode);
            this.HasRequired(t => t.Port)
                .WithMany(t => t.FuelRequisitions)
                .HasForeignKey(d => d.PortCode);
            this.HasRequired(t => t.SubCategory2)
                .WithMany(t => t.FuelRequisitions2)
                .HasForeignKey(d => d.UOMCode);
            this.HasOptional(t => t.WorkflowInstance)
                .WithMany(t => t.FuelRequisitions)
                .HasForeignKey(d => d.WorkflowInstanceId);

        }
    }
}
