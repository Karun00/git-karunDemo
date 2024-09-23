using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
	public class ServiceRequestSailingMap : EntityTypeConfiguration<ServiceRequestSailing>
	{
		public ServiceRequestSailingMap()
		{
			// Primary Key
			this.HasKey(t => t.ServiceRequestSailingID);

			// Properties
			this.Property(t => t.MarineRevenueCleared)
				.IsRequired()
				.IsFixedLength()
				.HasMaxLength(1);

			this.Property(t => t.RecordStatus)
				.IsRequired()
				.IsFixedLength()
				.HasMaxLength(1);

			// Table & Column Mappings
			this.ToTable("ServiceRequestSailing");
			this.Property(t => t.ServiceRequestSailingID).HasColumnName("ServiceRequestSailingID");
			this.Property(t => t.ServiceRequestID).HasColumnName("ServiceRequestID");
			this.Property(t => t.MarineRevenueCleared).HasColumnName("MarineRevenueCleared");
			this.Property(t => t.DocumentID).HasColumnName("DocumentID");
			this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
			this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
			this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
			this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
			this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

			// Relationships
			this.HasRequired(t => t.Document)
				.WithMany(t => t.ServiceRequestSailings)
				.HasForeignKey(d => d.DocumentID);
			this.HasRequired(t => t.ServiceRequest)
				.WithMany(t => t.ServiceRequestSailings)
				.HasForeignKey(d => d.ServiceRequestID);
			this.HasRequired(t => t.User)
				.WithMany(t => t.ServiceRequestSailings)
				.HasForeignKey(d => d.CreatedBy);
			this.HasRequired(t => t.User1)
				.WithMany(t => t.ServiceRequestSailings1)
				.HasForeignKey(d => d.ModifiedBy);

		}
	}
}
