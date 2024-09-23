using IPMS.Domain.Models;
using System.Data.Entity.ModelConfiguration;

namespace IPMS.Data.Mapping
{
    public class ResourceAttendanceMap : EntityTypeConfiguration<ResourceAttendance>
    {
        public ResourceAttendanceMap()
        {
            // Primary Key
            this.HasKey(t => t.ResourceAttendanceID);

            // Properties
            this.Property(t => t.Position)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("ResourceAttendance");
            this.Property(t => t.ResourceAttendanceID).HasColumnName("ResourceAttendanceID");
            this.Property(t => t.AttendanceDate).HasColumnName("AttendanceDate");
            this.Property(t => t.Position).HasColumnName("Position");
            this.Property(t => t.ShiftID).HasColumnName("ShiftID");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.ResourceAttendances)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.ResourceAttendances1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.ResourceAttendances)
                .HasForeignKey(d => d.Position);
            this.HasRequired(t => t.Shift)
                .WithMany(t => t.ResourceAttendances)
                .HasForeignKey(d => d.ShiftID);

        }
    }
}
