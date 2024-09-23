using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class CraftReminderConfigMap : EntityTypeConfiguration<CraftReminderConfig>
    {
        public CraftReminderConfigMap()
        {
            // Primary Key
            this.HasKey(t => t.CraftReminderConfigID);

            // Properties
            this.Property(t => t.ReminderName)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.IssuingAuthority)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ParticularsNo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.AlertPeriod1)
                .HasMaxLength(4);

            this.Property(t => t.AlertPeriod2)
                .HasMaxLength(4);

            this.Property(t => t.AlertPeriod3)
                .HasMaxLength(4);

            this.Property(t => t.ReminderStatus)
                .HasMaxLength(4);

            this.Property(t => t.ExitReminderConfig)
                .HasMaxLength(4);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("CraftReminderConfig");
            this.Property(t => t.CraftReminderConfigID).HasColumnName("CraftReminderConfigID");
            this.Property(t => t.CraftID).HasColumnName("CraftID");
            this.Property(t => t.ReminderName).HasColumnName("ReminderName");
            this.Property(t => t.ParticularsNo).HasColumnName("ParticularsNo");
            this.Property(t => t.IssuingAuthority).HasColumnName("IssuingAuthority");
            this.Property(t => t.DateOfIssue).HasColumnName("DateOfIssue");
            this.Property(t => t.DateOfValidity).HasColumnName("DateOfValidity");
            this.Property(t => t.AlertOccurance1).HasColumnName("AlertOccurance1");
            this.Property(t => t.AlertPeriod1).HasColumnName("AlertPeriod1");
            this.Property(t => t.AlertOccurance2).HasColumnName("AlertOccurance2");
            this.Property(t => t.AlertPeriod2).HasColumnName("AlertPeriod2");
            this.Property(t => t.AlertOccurance3).HasColumnName("AlertOccurance3");
            this.Property(t => t.AlertPeriod3).HasColumnName("AlertPeriod3");
            this.Property(t => t.ReminderStatus).HasColumnName("ReminderStatus");
            this.Property(t => t.ExitReminderConfig).HasColumnName("ExitReminderConfig");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.Craft)
                .WithMany(t => t.CraftReminderConfigs)
                .HasForeignKey(d => d.CraftID);
            this.HasOptional(t => t.SubCategory)
                .WithMany(t => t.CraftReminderConfigs)
                .HasForeignKey(d => d.AlertPeriod1);
            this.HasOptional(t => t.SubCategory1)
                .WithMany(t => t.CraftReminderConfigs1)
                .HasForeignKey(d => d.AlertPeriod2);
            this.HasOptional(t => t.SubCategory2)
                .WithMany(t => t.CraftReminderConfigs2)
                .HasForeignKey(d => d.AlertPeriod3);
            this.HasRequired(t => t.User)
                .WithMany(t => t.CraftReminderConfigs)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.SubCategory3)
                .WithMany(t => t.CraftReminderConfigs3)
                .HasForeignKey(d => d.ExitReminderConfig);
            this.HasOptional(t => t.User1)
                .WithMany(t => t.CraftReminderConfigs1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.SubCategory4)
                .WithMany(t => t.CraftReminderConfigs4)
                .HasForeignKey(d => d.ReminderName);
            this.HasOptional(t => t.SubCategory5)
                .WithMany(t => t.CraftReminderConfigs5)
                .HasForeignKey(d => d.ReminderStatus);

        }
    }
}
