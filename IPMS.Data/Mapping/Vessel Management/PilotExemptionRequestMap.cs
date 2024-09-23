using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class PilotExemptionRequestMap : EntityTypeConfiguration<PilotExemptionRequest>
    {
        public PilotExemptionRequestMap()
        {
            // Primary Key
            this.HasKey(t => t.PilotExemptionRequestID);

            // Properties
            this.Property(t => t.PilotRoleCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.MovementTypeCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.Remarks)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("PilotExemptionRequest");
            this.Property(t => t.PilotExemptionRequestID).HasColumnName("PilotExemptionRequestID");
            this.Property(t => t.PilotID).HasColumnName("PilotID");
            this.Property(t => t.MovementDate).HasColumnName("MovementDate");
            this.Property(t => t.VesselID).HasColumnName("VesselID");
            this.Property(t => t.PilotRoleCode).HasColumnName("PilotRoleCode");
            this.Property(t => t.MovementTypeCode).HasColumnName("MovementTypeCode");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.Pilot)
                .WithMany(t => t.PilotExemptionRequests)
                .HasForeignKey(d => d.PilotID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.PilotExemptionRequests)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.PilotExemptionRequests1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.PilotExemptionRequests)
                .HasForeignKey(d => d.MovementTypeCode);
            this.HasRequired(t => t.SubCategory1)
                .WithMany(t => t.PilotExemptionRequests1)
                .HasForeignKey(d => d.PilotRoleCode);
            this.HasRequired(t => t.Vessel)
                .WithMany(t => t.PilotExemptionRequests)
                .HasForeignKey(d => d.VesselID);

        }
    }
}
