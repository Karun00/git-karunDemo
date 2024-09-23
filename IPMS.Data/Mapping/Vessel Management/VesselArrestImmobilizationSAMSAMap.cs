using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class VesselArrestImmobilizationSAMSAMap : EntityTypeConfiguration<VesselArrestImmobilizationSAMSA>
    {
        public VesselArrestImmobilizationSAMSAMap()
        {
            // Primary Key
            this.HasKey(t => t.VAISID);

            // Properties
            this.Property(t => t.VCN)
                .IsRequired()
                .HasMaxLength(12);

            this.Property(t => t.VesselArrested)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ArrestedRemarks)
                .HasMaxLength(500);

            this.Property(t => t.VesselReleased)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ReleasedRemarks)
                .HasMaxLength(500);

            this.Property(t => t.Immobilization)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ExactWorkProposed)
                .HasMaxLength(500);

            this.Property(t => t.PollutionPrecautionTaken)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SAMSAStop)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SAMSAStopRemarks)
                .HasMaxLength(500);

            this.Property(t => t.SAMSACleared)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SAMSAClearedRemarks)
                .HasMaxLength(500);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("VesselArrestImmobilizationSAMSA");
            this.Property(t => t.VAISID).HasColumnName("VAISID");
            this.Property(t => t.VCN).HasColumnName("VCN");
            this.Property(t => t.VesselArrested).HasColumnName("VesselArrested");
            this.Property(t => t.ArrestedDate).HasColumnName("ArrestedDate");
            this.Property(t => t.ArrestedRemarks).HasColumnName("ArrestedRemarks");
            this.Property(t => t.VesselReleased).HasColumnName("VesselReleased");
            this.Property(t => t.ReleasedDate).HasColumnName("ReleasedDate");
            this.Property(t => t.ReleasedRemarks).HasColumnName("ReleasedRemarks");
            this.Property(t => t.Immobilization).HasColumnName("Immobilization");
            this.Property(t => t.ImmobilizationStartDate).HasColumnName("ImmobilizationStartDate");
            this.Property(t => t.ImmobilizationEndDate).HasColumnName("ImmobilizationEndDate");
            this.Property(t => t.ExactWorkProposed).HasColumnName("ExactWorkProposed");
            this.Property(t => t.PollutionPrecautionTaken).HasColumnName("PollutionPrecautionTaken");
            this.Property(t => t.ApprovedDate).HasColumnName("ApprovedDate");
            this.Property(t => t.SAMSAStop).HasColumnName("SAMSAStop");
            this.Property(t => t.SAMSAStopDate).HasColumnName("SAMSAStopDate");
            this.Property(t => t.SAMSAStopRemarks).HasColumnName("SAMSAStopRemarks");
            this.Property(t => t.SAMSACleared).HasColumnName("SAMSACleared");
            this.Property(t => t.SAMSAClearedDate).HasColumnName("SAMSAClearedDate");
            this.Property(t => t.SAMSAClearedRemarks).HasColumnName("SAMSAClearedRemarks");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.ArrivalNotification)
                .WithMany(t => t.VesselArrestImmobilizationSAMSAs)
                .HasForeignKey(d => d.VCN);
            this.HasRequired(t => t.User)
                .WithMany(t => t.VesselArrestImmobilizationSAMSAs)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.VesselArrestImmobilizationSAMSAs1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
