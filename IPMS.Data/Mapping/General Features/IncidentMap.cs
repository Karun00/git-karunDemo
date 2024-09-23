using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class IncidentMap : EntityTypeConfiguration<Incident>
    {
        public IncidentMap()
        {
            // Primary Key
            this.HasKey(t => t.IncidentID);

            // Properties
            this.Property(t => t.PortCode)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.IncidentLocation)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.IncidentDescription)
                .IsRequired()
                .HasMaxLength(1000);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("Incident");
            this.Property(t => t.IncidentID).HasColumnName("IncidentID");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.IncidentLocation).HasColumnName("IncidentLocation");
            this.Property(t => t.IncidentDescription).HasColumnName("IncidentDescription");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.Incidents)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.Incidents1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.Port)
                .WithMany(t => t.Incidents)
                .HasForeignKey(d => d.PortCode);

        }
    }
}
