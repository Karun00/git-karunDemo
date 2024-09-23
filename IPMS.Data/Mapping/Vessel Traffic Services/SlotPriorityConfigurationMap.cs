using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class SlotPriorityConfigurationMap : EntityTypeConfiguration<SlotPriorityConfiguration>
    {
        public SlotPriorityConfigurationMap()
        {
            // Primary Key
            this.HasKey(t => new { t.SlotCofiguratinid, t.VesselType, t.Priority, t.NoofVessels, t.RecordStatus });

            // Properties
            this.Property(t => t.SlotCofiguratinid)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.VesselType)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.Priority)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NoofVessels)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("SlotPriorityConfiguration");
            this.Property(t => t.SlotCofiguratinid).HasColumnName("SlotCofiguratinid");
            this.Property(t => t.VesselType).HasColumnName("VesselType");
            this.Property(t => t.Priority).HasColumnName("Priority");
            this.Property(t => t.NoofVessels).HasColumnName("NoofVessels");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");

            // Relationships
            this.HasRequired(t => t.AutomatedSlotConfiguration)
                .WithMany(t => t.SlotPriorityConfigurations)
                .HasForeignKey(d => d.SlotCofiguratinid);
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.SlotPriorityConfigurations)
                .HasForeignKey(d => d.VesselType);

        }
    }
}
