using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
	public class ArrivalApprovalMap : EntityTypeConfiguration<ArrivalApproval>
	{
		public ArrivalApprovalMap()
		{
			// Primary Key
			this.HasKey(t => new { t.VCN, t.RoleID });

			// Properties
			this.Property(t => t.VCN)
				.IsRequired()
                .HasMaxLength(12);

			this.Property(t => t.RoleID)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

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
			this.ToTable("ArrivalApproval");
			this.Property(t => t.VCN).HasColumnName("VCN");
			this.Property(t => t.RoleID).HasColumnName("RoleID");
			this.Property(t => t.WFStatus).HasColumnName("WFStatus");
			this.Property(t => t.ApprovedBy).HasColumnName("ApprovedBy");
			this.Property(t => t.ApprovedDate).HasColumnName("ApprovedDate");
			this.Property(t => t.RejectComments).HasColumnName("RejectComments");
			this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
			this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
			this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
			this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
			this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

			// Relationships
			this.HasRequired(t => t.User)
				.WithMany(t => t.ArrivalApprovals)
				.HasForeignKey(d => d.ApprovedBy);
			this.HasRequired(t => t.User1)
				.WithMany(t => t.ArrivalApprovals1)
				.HasForeignKey(d => d.CreatedBy);
			this.HasRequired(t => t.User2)
				.WithMany(t => t.ArrivalApprovals2)
				.HasForeignKey(d => d.ModifiedBy);
			this.HasRequired(t => t.Role)
				.WithMany(t => t.ArrivalApprovals)
				.HasForeignKey(d => d.RoleID);
			this.HasRequired(t => t.ArrivalNotification)
				.WithMany(t => t.ArrivalApprovals)
				.HasForeignKey(d => d.VCN);
			this.HasRequired(t => t.SubCategory)
				.WithMany(t => t.ArrivalApprovals)
				.HasForeignKey(d => d.WFStatus);

		}
	}
}
