using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
	public class VesselAgentChangeApprovalMap : EntityTypeConfiguration<VesselAgentChangeApproval>
	{
		public VesselAgentChangeApprovalMap()
		{
			// Primary Key
			this.HasKey(t => t.VesselAgentChangeApprovalID);

			// Properties
			this.Property(t => t.WFStatus)
				.IsRequired()
                .HasMaxLength(4);

			this.Property(t => t.RejectComments)
				.HasMaxLength(200);

			this.Property(t => t.RecordStatus)
				.IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

			// Table & Column Mappings
			this.ToTable("VesselAgentChangeApproval");
			this.Property(t => t.VesselAgentChangeApprovalID).HasColumnName("VesselAgentChangeApprovalID");
			this.Property(t => t.VesselAgentChangeID).HasColumnName("VesselAgentChangeID");
			this.Property(t => t.WFStatus).HasColumnName("WFStatus");
			this.Property(t => t.VerifiedBy).HasColumnName("VerifiedBy");
			this.Property(t => t.VerifiedDate).HasColumnName("VerifiedDate");
			this.Property(t => t.ApprovedBy).HasColumnName("ApprovedBy");
			this.Property(t => t.ApprovedDate).HasColumnName("ApprovedDate");
			this.Property(t => t.RejectComments).HasColumnName("RejectComments");
			this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
			this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
			this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
			this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
			this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

			// Relationships
			this.HasRequired(t => t.SubCategory)
				.WithMany(t => t.VesselAgentChangeApprovals)
				.HasForeignKey(d => d.WFStatus);
			this.HasRequired(t => t.User)
				.WithMany(t => t.VesselAgentChangeApprovals)
				.HasForeignKey(d => d.ApprovedBy);
			this.HasRequired(t => t.User1)
				.WithMany(t => t.VesselAgentChangeApprovals1)
				.HasForeignKey(d => d.CreatedBy);
			this.HasRequired(t => t.User2)
				.WithMany(t => t.VesselAgentChangeApprovals2)
				.HasForeignKey(d => d.ModifiedBy);
			this.HasRequired(t => t.User3)
				.WithMany(t => t.VesselAgentChangeApprovals3)
				.HasForeignKey(d => d.VerifiedBy);
			this.HasRequired(t => t.VesselAgentChange)
				.WithMany(t => t.VesselAgentChangeApprovals)
				.HasForeignKey(d => d.VesselAgentChangeID);

		}
	}
}
