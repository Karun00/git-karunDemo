using IPMS.Domain.Models;
using Core.Repository;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;


namespace IPMS.Data.Mapping
{
    public class Hour24Report625Map : EntityTypeConfiguration<Hour24Report625>
    {
        public Hour24Report625Map()
        {
            // Primary Key
            this.HasKey(t => t.Hour24Report625ID);

            // Properties
            this.Property(t => t.OperatorName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.LincseNumber)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CDName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.CDDesignation)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CDContactNumber)
                .HasMaxLength(15);

            this.Property(t => t.CDMobileNumber)
                .HasMaxLength(15);

            this.Property(t => t.CDEmailID)
                .HasMaxLength(50);

            this.Property(t => t.CDAddress)
                .HasMaxLength(500);

            this.Property(t => t.NONatureCode)
                .HasMaxLength(4);

            this.Property(t => t.IODSpecificLocation)
                .HasMaxLength(500);

            this.Property(t => t.IODOccuranceBriefDescription)
                .HasMaxLength(500);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.PortCode)
            .IsRequired()
            .HasMaxLength(2);

            this.Property(t => t.Timeperiod)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("Hour24Report625");
            this.Property(t => t.Hour24Report625ID).HasColumnName("Hour24Report625ID");
            this.Property(t => t.OperatorName).HasColumnName("OperatorName");
            this.Property(t => t.LincseNumber).HasColumnName("LincseNumber");
            this.Property(t => t.CDName).HasColumnName("CDName");
            this.Property(t => t.CDDesignation).HasColumnName("CDDesignation");
            this.Property(t => t.CDContactNumber).HasColumnName("CDContactNumber");
            this.Property(t => t.CDMobileNumber).HasColumnName("CDMobileNumber");
            this.Property(t => t.CDEmailID).HasColumnName("CDEmailID");
            this.Property(t => t.CDAddress).HasColumnName("CDAddress");
            this.Property(t => t.NONatureCode).HasColumnName("NONatureCode");
            this.Property(t => t.IODOccuranceDateTime).HasColumnName("IODOccuranceDateTime");
            this.Property(t => t.IODSpecificLocation).HasColumnName("IODSpecificLocation");
            this.Property(t => t.IODOccuranceBriefDescription).HasColumnName("IODOccuranceBriefDescription");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.Timeperiod).HasColumnName("Timeperiod");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.Hour24Report625)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.Hour24Report6251)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasOptional(t => t.SubCategory)
                .WithMany(t => t.Hour24Report625)
                .HasForeignKey(d => d.NONatureCode);

            this.HasRequired(t => t.Port24)
       .WithMany(t => t.Hour24Port)
       .HasForeignKey(d => d.PortCode);

        }
    }
}
