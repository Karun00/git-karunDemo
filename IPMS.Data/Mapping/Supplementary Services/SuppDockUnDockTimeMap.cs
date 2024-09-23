using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class SuppDockUnDockTimeMap : EntityTypeConfiguration<SuppDockUnDockTime>
    {
        public SuppDockUnDockTimeMap()
        {
            // Primary Key
            this.HasKey(t => t.SuppDockUnDockTimeID);

            // Properties
            this.Property(t => t.Chamber)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("SuppDockUnDockTime");
            this.Property(t => t.SuppDockUnDockTimeID).HasColumnName("SuppDockUnDockTimeID");
            this.Property(t => t.SuppDryDockID).HasColumnName("SuppDryDockID");
            this.Property(t => t.Chamber).HasColumnName("Chamber");
            this.Property(t => t.EnteredDockDateTime).HasColumnName("EnteredDockDateTime");
            this.Property(t => t.OnBlocksDateTime).HasColumnName("OnBlocksDateTime");
            this.Property(t => t.DryDockDateTime).HasColumnName("DryDockDateTime");
            this.Property(t => t.FinishedDockDateTime).HasColumnName("FinishedDockDateTime");
            this.Property(t => t.OffBlocksDateTime).HasColumnName("OffBlocksDateTime");
            this.Property(t => t.LeftDockDateTime).HasColumnName("LeftDockDateTime");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.SuppDockUnDockTimes)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.SuppDockUnDockTimes1)
                .HasForeignKey(d => d.ModifiedBy);
            //this.HasRequired(t => t.SuppDryDock)
            //    .WithMany(t => t.SuppDockUnDockTimes)
            //    .HasForeignKey(d => d.SuppDryDockID);

        }
    }
}
