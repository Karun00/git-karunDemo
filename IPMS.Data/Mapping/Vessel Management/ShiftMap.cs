using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Domain.Models
{
    public class ShiftMap : EntityTypeConfiguration<Shift>
    {
        public ShiftMap()
        {
            // Primary Key
            this.HasKey(t => t.ShiftID);

            // Properties
            this.Property(t => t.PortCode)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.ShiftName)
                .HasMaxLength(100);

            this.Property(t => t.IsShiftOff)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // -- Added by sandeep on 07-01-2015
            this.Property(t => t.IsContinuousShift)
               .IsFixedLength()
               .HasMaxLength(1);
            // -- end

            // Table & Column Mappings
            this.ToTable("Shift");
            this.Property(t => t.ShiftID).HasColumnName("ShiftID");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.ShiftName).HasColumnName("ShiftName");
            this.Property(t => t.StartTime).HasColumnName("StartTime");
            this.Property(t => t.EndTime).HasColumnName("EndTime");
            this.Property(t => t.IsShiftOff).HasColumnName("IsShiftOff");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            // -- Added by sandeep on 07-01-2015
            this.Property(t => t.FirstShiftID).HasColumnName("FirstShiftID");
            this.Property(t => t.SecondShiftID).HasColumnName("SecondShiftID");
            this.Property(t => t.IsContinuousShift).HasColumnName("IsContinuousShift");
            this.Property(t => t.RollOverOn).HasColumnName("RollOverOn");
            
            // -- end

            // Relationships
            this.HasRequired(t => t.Port)
                .WithMany(t => t.Shifts)
                .HasForeignKey(d => d.PortCode);
            this.HasRequired(t => t.User)
                .WithMany(t => t.Shifts)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.Shifts1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
