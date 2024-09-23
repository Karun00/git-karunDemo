using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class RosterDtlMap : EntityTypeConfiguration<RosterDtl>
    {
        public RosterDtlMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ResourceGroupID, t.ShiftID, t.RosterDate, t.RecordStatus, t.CreatedBy, t.CreatedDate, t.ModifiedBy, t.ModifiedDate });
            //this.HasKey(t => new { t.ResourceGroupID, t.RosterDate, t.RecordStatus, t.CreatedBy, t.CreatedDate, t.ModifiedBy, t.ModifiedDate });

            // Properties
            this.Property(t => t.ResourceGroupID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //this.Property(t => t.ShiftID)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.CreatedBy)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ModifiedBy)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("RosterDtl");
            this.Property(t => t.ResourceGroupID).HasColumnName("ResourceGroupID");
            this.Property(t => t.ShiftID).HasColumnName("ShiftID");
            this.Property(t => t.RosterDate).HasColumnName("RosterDate");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // -- Added by sandeep on 08-01-2015
            this.Property(t => t.MainShiftID).HasColumnName("MainShiftID");
            // -- end

            // Relationships
            this.HasRequired(t => t.ResourceGroup)
                .WithMany(t => t.RosterDtls)
                .HasForeignKey(d => d.ResourceGroupID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.RosterDtls)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.RosterDtls1)
                .HasForeignKey(d => d.ModifiedBy);

            this.HasRequired(t => t.Shift)
                .WithMany(t => t.RosterDtls)
                .HasForeignKey(d => d.ShiftID);

            // -- Added by sandeep on 08-01-2015

            this.HasOptional(t => t.Shift1)
               .WithMany(t => t.RosterDtls1)
               .HasForeignKey(d => d.MainShiftID);
            // -- end

        }
    }
}
