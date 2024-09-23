using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class VesselAgentChangeMap : EntityTypeConfiguration<VesselAgentChange>
    {
        public VesselAgentChangeMap()
        {
            // Primary Key
            this.HasKey(t => t.VesselAgentChangeID);

            // Properties
            this.Property(t => t.VCN)
                .IsRequired()
                .HasMaxLength(12);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.IsFinal)
               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed)
               .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("VesselAgentChange");
            this.Property(t => t.VesselAgentChangeID).HasColumnName("VesselAgentChangeID");
            this.Property(t => t.VCN).HasColumnName("VCN");
            this.Property(t => t.ProposedAgent).HasColumnName("ProposedAgent");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.ReasonForTransferCode).HasColumnName("ReasonForTransferCode");
            this.Property(t => t.EffectiveDateTime).HasColumnName("EffectiveDateTime");
            this.Property(t => t.WorkflowInstanceId).HasColumnName("WorkflowInstanceId");
            this.Property(t => t.IsFinal).HasColumnName("IsFinal");
            //this.Property(t => t.ProposedAgentName).HasColumnName("ProposedAgentName").IsOptional();
            //this.Property(t => t.RequestedAgentName).HasColumnName("RequestedAgentName").IsOptional();
            // Relationships
            this.HasRequired(t => t.Agent)
                .WithMany(t => t.VesselAgentChanges)
                .HasForeignKey(d => d.ProposedAgent);
            this.HasRequired(t => t.ArrivalNotification)
                .WithMany(t => t.VesselAgentChanges)
                .HasForeignKey(d => d.VCN);
            this.HasRequired(t => t.CreatedUser)
                .WithMany(t => t.VesselAgentChanges)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.ModifiedUser)
                .WithMany(t => t.VesselAgentChanges1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.VesselAgentChanges)
            .HasForeignKey(d => d.ReasonForTransferCode);

            this.HasRequired(t => t.WorkflowInstatnce)
                .WithMany(t => t.VesselAgentChanges)
            .HasForeignKey(d => d.WorkflowInstanceId);


        }
    }
}
