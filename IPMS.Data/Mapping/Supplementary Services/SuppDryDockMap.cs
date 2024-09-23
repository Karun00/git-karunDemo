using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class SuppDryDockMap : EntityTypeConfiguration<SuppDryDock>
    {
        public SuppDryDockMap()
        {
            // Primary Key
            this.HasKey(t => t.SuppDryDockID);

            // Properties
            this.Property(t => t.VCN)
                .IsRequired()
                .HasMaxLength(12);

            //this.Property(t => t.PortCode)
            //    .IsRequired()
            //    .HasMaxLength(2);

            this.Property(t => t.BarkeelCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.Remarks)
                .HasMaxLength(200);

            this.Property(t => t.TermsandConditions)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);


            this.Property(t => t.DockPortCode)
                .HasMaxLength(2);

            this.Property(t => t.DockQuayCode)
                .HasMaxLength(4);

            this.Property(t => t.DockBerthCode)
                .HasMaxLength(4);

            this.Property(t => t.ScheduleStatus)
                .HasMaxLength(4);

            this.Property(t => t.Chamber)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SuppDryDock");
            this.Property(t => t.SuppDryDockID).HasColumnName("SuppDryDockID");
            this.Property(t => t.VCN).HasColumnName("VCN");
          //  this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.FromDate).HasColumnName("FromDate");
            this.Property(t => t.ToDate).HasColumnName("ToDate");
            this.Property(t => t.BarkeelCode).HasColumnName("BarkeelCode");
            this.Property(t => t.CargoTons).HasColumnName("CargoTons");
            this.Property(t => t.Ballast).HasColumnName("Ballast");
            this.Property(t => t.Bunkers).HasColumnName("Bunkers");
            this.Property(t => t.ExtensionDateTime).HasColumnName("ExtensionDateTime");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.TermsandConditions).HasColumnName("TermsandConditions");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.WorkflowInstanceID).HasColumnName("WorkflowInstanceID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.DockPortCode).HasColumnName("DockPortCode");
            this.Property(t => t.DockQuayCode).HasColumnName("DockQuayCode");
            this.Property(t => t.DockBerthCode).HasColumnName("DockBerthCode");          
            this.Property(t => t.ScheduleFromDate).HasColumnName("ScheduleFromDate");
            this.Property(t => t.ScheduleToDate).HasColumnName("ScheduleToDate");
            this.Property(t => t.ScheduleStatus).HasColumnName("ScheduleStatus");
            this.Property(t => t.Chamber).HasColumnName("Chamber");
            this.Property(t => t.EnteredDockDateTime).HasColumnName("EnteredDockDateTime");
            this.Property(t => t.OnBlocksDateTime).HasColumnName("OnBlocksDateTime");
            this.Property(t => t.DryDockDateTime).HasColumnName("DryDockDateTime");
            this.Property(t => t.FinishedDockDateTime).HasColumnName("FinishedDockDateTime");
            this.Property(t => t.OffBlocksDateTime).HasColumnName("OffBlocksDateTime");
            this.Property(t => t.LeftDockDateTime).HasColumnName("LeftDockDateTime");

            // Relationships
            this.HasOptional(t => t.Berth)
                .WithMany(t => t.SuppDryDocks)
                .HasForeignKey(d => new { d.DockPortCode, d.DockQuayCode, d.DockBerthCode });
            this.HasRequired(t => t.ArrivalNotification)
                .WithMany(t => t.SuppDryDocks)
                .HasForeignKey(d => d.VCN);
            //this.HasRequired(t => t.Port)
            //    .WithMany(t => t.SuppDryDocks)
            //    .HasForeignKey(d => d.PortCode);
            this.HasRequired(t => t.User)
                .WithMany(t => t.SuppDryDocks)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.SuppDryDocks1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasOptional(t => t.WorkflowInstance)
                .WithMany(t => t.SuppDryDocks)
                .HasForeignKey(d => d.WorkflowInstanceID);

        }
    }
}
