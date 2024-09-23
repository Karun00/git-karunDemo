using IPMS.Domain.Models;
using Core.Repository;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPMS.Data.Mapping
{
    public class AutomatedSlotBlockingMap : EntityTypeConfiguration<AutomatedSlotBlocking>
    {
        public AutomatedSlotBlockingMap()
        {
            // Primary Key
            this.HasKey(t => t.AutomatedSlotBlockingId);
           
            // Table & Column Mappings
            this.ToTable("AutomatedSlotBlocking");
            this.Property(t => t.AutomatedSlotBlockingId).HasColumnName("AutomatedSlotBlockingID");
            this.Property(t => t.FromDate).HasColumnName("FromDate");
            this.Property(t => t.ToDate).HasColumnName("ToDate");            
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.Reason).HasColumnName("Reason");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.SlotFrom).HasColumnName("SlotFrom");
            this.Property(t => t.SlotTo).HasColumnName("SlotTo");
            this.Property(t => t.TotalSlots).HasColumnName("TotalSlots");
            this.Property(t => t.Other).HasColumnName("Other");
            

            this.HasRequired(t => t.User)
               .WithMany(t => t.AutomatedSlotBlockings)
               .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.AutomatedSlotBlockings1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.Port)
                .WithMany(t => t.AutomatedSlotBlockings)
                .HasForeignKey(d => d.PortCode);
            this.HasRequired(t => t.SubCategory)
             .WithMany(t => t.AutomatedSlotBlockings)
             .HasForeignKey(d => d.Reason);

        }
    }
}
