using IPMS.Domain.Models;
using Core.Repository;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPMS.Data.Mapping
{
    public class ServiceTypeDesignationMap : EntityTypeConfiguration<ServiceTypeDesignation>
    {
        public ServiceTypeDesignationMap()
        {
            // Primary Key
            this.HasKey(t => t.ServiceTypeDesignationID);

            // Properties
            this.Property(t => t.PortCode)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.DesignationCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.CraftType)
                .HasMaxLength(4);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("ServiceTypeDesignation");
            this.Property(t => t.ServiceTypeDesignationID).HasColumnName("ServiceTypeDesignationID");
            this.Property(t => t.ServiceTypeID).HasColumnName("ServiceTypeID");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.DesignationCode).HasColumnName("DesignationCode");
            this.Property(t => t.CraftType).HasColumnName("CraftType");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.Port)
                .WithMany(t => t.ServiceTypeDesignations)
                .HasForeignKey(d => d.PortCode);
            this.HasRequired(t => t.ServiceType)
                .WithMany(t => t.ServiceTypeDesignations)
                .HasForeignKey(d => d.ServiceTypeID);
            this.HasOptional(t => t.SubCategory)
                .WithMany(t => t.ServiceTypeDesignations)
                .HasForeignKey(d => d.CraftType);
            this.HasRequired(t => t.User)
                .WithMany(t => t.ServiceTypeDesignations)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.SubCategory1)
                .WithMany(t => t.ServiceTypeDesignations1)
                .HasForeignKey(d => d.DesignationCode);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.ServiceTypeDesignations1)
                .HasForeignKey(d => d.ModifiedBy);
        }
    }
}
