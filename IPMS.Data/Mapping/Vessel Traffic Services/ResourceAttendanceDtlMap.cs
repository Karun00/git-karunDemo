using IPMS.Domain.Models;
using System.Data.Entity.ModelConfiguration;

namespace IPMS.Data.Mapping
{
    public class ResourceAttendanceDtlMap : EntityTypeConfiguration<ResourceAttendanceDtl>
    {
        public ResourceAttendanceDtlMap()
        {
            // Primary Key
            this.HasKey(t => t.ResourceAttendanceDtlID);

            // Properties
            this.Property(t => t.AttendanceStatus)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("ResourceAttendanceDtl");
            this.Property(t => t.ResourceAttendanceDtlID).HasColumnName("ResourceAttendanceDtlID");
            this.Property(t => t.ResourceAttendanceID).HasColumnName("ResourceAttendanceID");
            this.Property(t => t.EmployeeID).HasColumnName("EmployeeID");
            this.Property(t => t.AttendanceStatus).HasColumnName("AttendanceStatus");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.ShiftID).HasColumnName("ShiftID");
            this.Property(t => t.AttendanceDate).HasColumnName("AttendanceDate");

            // Relationships
            this.HasRequired(t => t.Employee)
                .WithMany(t => t.ResourceAttendanceDtls)
                .HasForeignKey(d => d.EmployeeID);
            this.HasRequired(t => t.ResourceAttendance)
                .WithMany(t => t.ResourceAttendanceDtls)
                .HasForeignKey(d => d.ResourceAttendanceID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.ResourceAttendanceDtls)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.ResourceAttendanceDtls1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.Shift)
            .WithMany(t => t.ResourceAttendancesdtl)
            .HasForeignKey(d => d.ShiftID);

        }
    }
}
