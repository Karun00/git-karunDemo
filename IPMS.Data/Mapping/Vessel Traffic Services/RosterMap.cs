using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class RosterMap : EntityTypeConfiguration<Roster>
    {
        public RosterMap()
        {
            // Primary Key
            this.HasKey(t => t.RosterID);

            // Properties
            this.Property(t => t.RosterCode)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Designation)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("Roster");
            this.Property(t => t.RosterID).HasColumnName("RosterID");
            this.Property(t => t.RosterCode).HasColumnName("RosterCode");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.Week).HasColumnName("Week");
            this.Property(t => t.Designation).HasColumnName("Designation");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.Rosters)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.Rosters)
                .HasForeignKey(d => d.Designation);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.Rosters1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
