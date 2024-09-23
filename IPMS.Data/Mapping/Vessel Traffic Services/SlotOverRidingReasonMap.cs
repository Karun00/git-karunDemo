using IPMS.Domain.Models;
using Core.Repository;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPMS.Data.Mapping
{
    public class SlotOverRidingReasonsMap : EntityTypeConfiguration<SlotOverRidingReasons>
    { 
        public SlotOverRidingReasonsMap()
        {
            // Primary Key
            this.HasKey(t => t.OverRideSlotID);
           
            // Table & Column Mappings
            this.ToTable("SlotOverRidingReasons");
            this.Property(t => t.VesselCallMovementID).HasColumnName("VesselCallMovementID");
            this.Property(t => t.ReasonCode).HasColumnName("ReasonCode");           
            this.Property(t => t.EnteredDateAndTime).HasColumnName("EnteredDateAndTime");           
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.PreviousSlot).HasColumnName("PreviousSlot");
            this.Property(t => t.OverriddenSlot).HasColumnName("OverriddenSlot");
            this.Property(t => t.PreviousSlotDate).HasColumnName("PreviousSlotDate");
           // this.Property(t => t.OverriddenSlotDate).HasColumnName("OverriddenSlotDate");
            
            
            this.HasRequired(t => t.User)
                .WithMany(t => t.SlotOverRidingReasons)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.SlotOverRidingReasons1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.VesselCallMovement)
             .WithMany(t => t.SlotOverRidingReasons)
             .HasForeignKey(d => d.VesselCallMovementID);
            this.HasRequired(t => t.SubCategory)
             .WithMany(t => t.SlotOverRidingReasons)
             .HasForeignKey(d => d.ReasonCode);

        }
    }
}
