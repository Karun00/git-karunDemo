using IPMS.Domain.Models;
using Core.Repository;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;


namespace IPMS.Data.Mapping
{
    public class AutomatedSlotConfigurationMap : EntityTypeConfiguration<AutomatedSlotConfiguration>
    {
        public AutomatedSlotConfigurationMap()
        {
            // Primary Key
            this.HasKey(t => t.SlotCofiguratinid);

            // Properties
            this.Property(t => t.PortCode)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.OperationalPeriod)
       .HasMaxLength(5);

            // Table & Column Mappings
            this.ToTable("AutomatedSlotConfiguration");
            this.Property(t => t.SlotCofiguratinid).HasColumnName("SlotCofiguratinid");
            this.Property(t => t.EffectiveFrm).HasColumnName("EffectiveFrm");
            this.Property(t => t.Duration).HasColumnName("Duration");
            this.Property(t => t.NoofSlots).HasColumnName("NoofSlots");
            this.Property(t => t.ExtendableSlots).HasColumnName("ExtendableSlots");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModfiedBy).HasColumnName("ModfiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.OperationalPeriod).HasColumnName("OperationalPeriod");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.AutomatedSlotConfigurations)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.AutomatedSlotConfigurations1)
                .HasForeignKey(d => d.ModfiedBy);
            this.HasRequired(t => t.Port)
                .WithMany(t => t.AutomatedSlotConfigurations)
                .HasForeignKey(d => d.PortCode);

        }
    }
}
