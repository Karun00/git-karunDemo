using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class RosterGroupMap : EntityTypeConfiguration<RosterGroup>
    {
        public RosterGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.RosterGroupID);

            // Properties
            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("RosterGroup");
            this.Property(t => t.RosterGroupID).HasColumnName("RosterGroupID");
            this.Property(t => t.RosterID).HasColumnName("RosterID");
            this.Property(t => t.ResourceGroupID).HasColumnName("ResourceGroupID");
            this.Property(t => t.Mon).HasColumnName("Mon");
            this.Property(t => t.Tue).HasColumnName("Tue");
            this.Property(t => t.Wed).HasColumnName("Wed");
            this.Property(t => t.Thu).HasColumnName("Thu");
            this.Property(t => t.Fri).HasColumnName("Fri");
            this.Property(t => t.Sat).HasColumnName("Sat");
            this.Property(t => t.Sun).HasColumnName("Sun");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.ResourceGroup)
                .WithMany(t => t.RosterGroups)
                .HasForeignKey(d => d.ResourceGroupID);
            this.HasRequired(t => t.Roster)
                .WithMany(t => t.RosterGroups)
                .HasForeignKey(d => d.RosterID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.RosterGroups)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.Shift)
                .WithMany(t => t.RosterGroups)
                .HasForeignKey(d => d.Fri);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.RosterGroups1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasOptional(t => t.Shift1)
                .WithMany(t => t.RosterGroups1)
                .HasForeignKey(d => d.Mon);
            this.HasOptional(t => t.Shift2)
                .WithMany(t => t.RosterGroups2)
                .HasForeignKey(d => d.Sat);
            this.HasOptional(t => t.Shift3)
                .WithMany(t => t.RosterGroups3)
                .HasForeignKey(d => d.Sun);
            this.HasOptional(t => t.Shift4)
                .WithMany(t => t.RosterGroups4)
                .HasForeignKey(d => d.Thu);
            this.HasOptional(t => t.Shift5)
                .WithMany(t => t.RosterGroups5)
                .HasForeignKey(d => d.Tue);
            this.HasOptional(t => t.Shift6)
                .WithMany(t => t.RosterGroups6)
                .HasForeignKey(d => d.Wed);

        }
    }
}
