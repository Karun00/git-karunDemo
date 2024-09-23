using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Domain.Models
{
    public class FuelReceiptMap : EntityTypeConfiguration<FuelReceipt>
    {
        public FuelReceiptMap()
        {
            // Primary Key
            this.HasKey(t => t.FuelReceiptID);

            // Properties
            this.Property(t => t.SupplyingModeCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.ModeID)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.PortCode)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.QuayCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.BerthCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.GradeCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.BatchNo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Supplier)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Remarks)
                .HasMaxLength(500);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.Flag)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Other)
              .IsRequired()
              .HasMaxLength(50);

            this.Property(t => t.FuelReceiptNo)
               .IsRequired()
               .HasMaxLength(15);

            this.Property(t => t.Densityat15DegCelsius).HasPrecision(18, 3);

            this.Property(t => t.Densityat20DegCelsius).HasPrecision(18, 3);

            this.Property(t => t.KinematicViscat50DegCelsius).HasPrecision(18, 3);

            this.Property(t => t.ReceivedTempCelsius).HasPrecision(18, 3);

            this.Property(t => t.VCF).HasPrecision(18, 3);

            this.Property(t => t.Qttyat20Degree1).HasPrecision(18, 3);

            this.Property(t => t.Qttyat20Degree2).HasPrecision(18, 3);

            this.Property(t => t.FlashPoint).HasPrecision(18, 3);

            this.Property(t => t.SulphurContent).HasPrecision(18, 3);

            this.Property(t => t.WaterContent).HasPrecision(10, 3);


            // Table & Column Mappings
            this.ToTable("FuelReceipt");
            this.Property(t => t.FuelReceiptID).HasColumnName("FuelReceiptID");
            this.Property(t => t.FuelRequisitionID).HasColumnName("FuelRequisitionID");
            this.Property(t => t.SupplyingModeCode).HasColumnName("SupplyingModeCode");
            this.Property(t => t.ModeID).HasColumnName("ModeID");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.QuayCode).HasColumnName("QuayCode");
            this.Property(t => t.BerthCode).HasColumnName("BerthCode");
            this.Property(t => t.ReceiptDate).HasColumnName("ReceiptDate");
            this.Property(t => t.GradeCode).HasColumnName("GradeCode");
            this.Property(t => t.StartReading).HasColumnName("StartReading");
            this.Property(t => t.FinishReading).HasColumnName("FinishReading");
            this.Property(t => t.ReceivedQty).HasColumnName("ReceivedQty");
            this.Property(t => t.ReceivedTempCelsius).HasColumnName("ReceivedTempCelsius");
            this.Property(t => t.VCF).HasColumnName("VCF");
            this.Property(t => t.Qttyat20Degree1).HasColumnName("Qttyat20Degree1");
            this.Property(t => t.Qttyat20Degree2).HasColumnName("Qttyat20Degree2");
            this.Property(t => t.PumpStartDateTime).HasColumnName("PumpStartDateTime");
            this.Property(t => t.PumpFinishDateTime).HasColumnName("PumpFinishDateTime");
            this.Property(t => t.Densityat15DegCelsius).HasColumnName("Densityat15DegCelsius");
            this.Property(t => t.Densityat20DegCelsius).HasColumnName("Densityat20DegCelsius");
            this.Property(t => t.FlashPoint).HasColumnName("FlashPoint");
            this.Property(t => t.BatchNo).HasColumnName("BatchNo");
            this.Property(t => t.KinematicViscat50DegCelsius).HasColumnName("KinematicViscat50DegCelsius");
            this.Property(t => t.WaterContent).HasColumnName("WaterContent");
            this.Property(t => t.SulphurContent).HasColumnName("SulphurContent");
            this.Property(t => t.Supplier).HasColumnName("Supplier");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.Flag).HasColumnName("Flag");
            this.Property(t => t.Other).HasColumnName("Other");
            this.Property(t => t.FuelReceiptNo).HasColumnName("FuelReceiptNo");
            this.Property(t => t.WorkflowInstanceId).HasColumnName("WorkflowInstanceId");

            // Relationships
            this.HasRequired(t => t.Berth)
                .WithMany(t => t.FuelReceipts)
                .HasForeignKey(d => new { d.PortCode, d.QuayCode, d.BerthCode });
            this.HasRequired(t => t.User)
                .WithMany(t => t.FuelReceipts)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.FuelRequisition)
                .WithMany(t => t.FuelReceipts)
                .HasForeignKey(d => d.FuelRequisitionID);
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.FuelReceipts)
                .HasForeignKey(d => d.GradeCode);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.FuelReceipts1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.SubCategory1)
                .WithMany(t => t.FuelReceipts1)
                .HasForeignKey(d => d.SupplyingModeCode);
            this.HasOptional(t => t.WorkflowInstance)
               .WithMany(t => t.FuelReceipts)
               .HasForeignKey(d => d.WorkflowInstanceId);

        }
    }
}
