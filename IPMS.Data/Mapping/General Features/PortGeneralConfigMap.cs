using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class PortGeneralConfigMap : EntityTypeConfiguration<PortGeneralConfig>
    {
        public PortGeneralConfigMap()
        {
            // Primary Key
            this.HasKey(t => t.PortGeneralConfigID);

            // Properties
            this.Property(t => t.PortCode)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.ConfigName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.ConfigValue)
                .HasMaxLength(200);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ConfigLabelName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.GroupName)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("PortGeneralConfig");
            this.Property(t => t.PortGeneralConfigID).HasColumnName("PortGeneralConfigID");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.ConfigName).HasColumnName("ConfigName");
            this.Property(t => t.ConfigValue).HasColumnName("ConfigValue");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.ConfigLabelName).HasColumnName("ConfigLabelName");
            this.Property(t => t.GroupName).HasColumnName("GroupName");

            // Relationships
            this.HasRequired(t => t.Port)
                .WithMany(t => t.PortGeneralConfigs)
                .HasForeignKey(d => d.PortCode);
            this.HasRequired(t => t.User)
                .WithMany(t => t.PortGeneralConfigs)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.PortGeneralConfigs1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
