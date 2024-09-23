using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class IncidentNatureMap : EntityTypeConfiguration<IncidentNature>
    {
        public IncidentNatureMap()
        {
            // Primary Key
            this.HasKey(t => t.IncidentNatureID);

            // Properties
            this.Property(t => t.IncidentNature1)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("IncidentNature");
            this.Property(t => t.IncidentNatureID).HasColumnName("IncidentNatureID");
            this.Property(t => t.IncidentID).HasColumnName("IncidentID");
            this.Property(t => t.IncidentNature1).HasColumnName("IncidentNature");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.Incident)
                .WithMany(t => t.IncidentNatures)
                .HasForeignKey(d => d.IncidentID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.IncidentNatures)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.IncidentNatures)
                .HasForeignKey(d => d.IncidentNature1);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.IncidentNatures1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
