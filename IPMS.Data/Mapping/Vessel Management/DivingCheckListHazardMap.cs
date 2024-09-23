using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class DivingCheckListHazardMap : EntityTypeConfiguration<DivingCheckListHazard>
    {
        public DivingCheckListHazardMap()
        {
            // Primary Key
            this.HasKey(t => t.DivingCheckListHazardID);

            // Properties
            this.Property(t => t.Hazard)
                .HasMaxLength(2000);

            this.Property(t => t.Cause)
                .HasMaxLength(2000);

            this.Property(t => t.Action)
                .HasMaxLength(2000);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("DivingCheckListHazard");
            this.Property(t => t.DivingCheckListHazardID).HasColumnName("DivingCheckListHazardID");
            this.Property(t => t.DivingCheckListID).HasColumnName("DivingCheckListID");
            this.Property(t => t.Hazard).HasColumnName("Hazard");
            this.Property(t => t.Cause).HasColumnName("Cause");
            this.Property(t => t.Action).HasColumnName("Action");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.DivingCheckList)
                .WithMany(t => t.DivingCheckListHazards)
                .HasForeignKey(d => d.DivingCheckListID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.DivingCheckListHazards)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.DivingCheckListHazards1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
