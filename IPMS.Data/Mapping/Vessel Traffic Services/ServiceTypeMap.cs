using IPMS.Domain.Models;
using Core.Repository;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPMS.Data.Mapping
{
    public class ServiceTypeMap : EntityTypeConfiguration<ServiceType>
    {
        public ServiceTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ServiceTypeID);

            // Properties
            this.Property(t => t.ServiceTypeName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ServiceTypeCode)
                .HasMaxLength(4);

            this.Property(t => t.IsServiceType)
             .IsRequired()
             .IsFixedLength()
             .HasMaxLength(1);

            this.Property(t => t.ServiceUOM)
            .HasMaxLength(4);

            // Table & Column Mappings
            this.ToTable("ServiceType");
            this.Property(t => t.ServiceTypeID).HasColumnName("ServiceTypeID");
            this.Property(t => t.ServiceTypeName).HasColumnName("ServiceTypeName");
            this.Property(t => t.IsCraft).HasColumnName("IsCraft");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.ServiceTypeCode).HasColumnName("ServiceTypeCode");
            this.Property(t => t.IsServiceType).HasColumnName("IsServiceType");
            this.Property(t => t.ServiceUOM).HasColumnName("ServiceUOM");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.ServiceTypes)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.ServiceTypes1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasOptional(t => t.SubCategory)
                 .WithMany(t => t.ServiceTypes)
                 .HasForeignKey(d => d.ServiceUOM);
        }
    }
}
