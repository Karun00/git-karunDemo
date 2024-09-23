using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
	public class ServiceRequestDocumentMap : EntityTypeConfiguration<ServiceRequestDocument>
	{
		public ServiceRequestDocumentMap()
		{
            // Primary Key
            this.HasKey(t => t.ServiceRequestDocumentID);

			// Properties
            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.DocumentCode)
                .HasMaxLength(4);

            //this.Property(t => t.ServiceRequestDocumentID)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);


			// Table & Column Mappings
            this.ToTable("ServiceRequestDocument");
            this.Property(t => t.ServiceRequestID).HasColumnName("ServiceRequestID");
            this.Property(t => t.DocumentID).HasColumnName("DocumentID");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.DocumentCode).HasColumnName("DocumentCode");
            this.Property(t => t.ServiceRequestDocumentID).HasColumnName("ServiceRequestDocumentID");

			// Relationships
            this.HasRequired(t => t.Document)
                .WithMany(t => t.ServiceRequestDocuments)
                .HasForeignKey(d => d.DocumentID);
            this.HasRequired(t => t.ServiceRequest)
                .WithMany(t => t.ServiceRequestDocuments)
                .HasForeignKey(d => d.ServiceRequestID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.ServiceRequestDocuments)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.SubCategory)
                .WithMany(t => t.ServiceRequestDocuments)
                .HasForeignKey(d => d.DocumentCode);
            this.HasOptional(t => t.User1)
                .WithMany(t => t.ServiceRequestDocuments1)
                .HasForeignKey(d => d.ModifiedBy);
		}
	}
}
