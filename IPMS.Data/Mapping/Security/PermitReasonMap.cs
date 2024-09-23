using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Data.Mapping
{
    public class PermitReasonMap : EntityTypeConfiguration<PermitReason>
    {
        public PermitReasonMap()
        {
            // Primary Key
            this.HasKey(t => t.PermitReasonID);

            // Properties
            this.Property(t => t.ReasonCode)
                .IsRequired()
                .HasMaxLength(4);

            // Table & Column Mappings
            this.ToTable("PermitReason");
            this.Property(t => t.PermitReasonID).HasColumnName("PermitReasonID");
            this.Property(t => t.PermitRequestID).HasColumnName("PermitRequestID");
            this.Property(t => t.ReasonCode).HasColumnName("ReasonCode");
           

            // Relationships
            this.HasRequired(t => t.PermitRequest)
                .WithMany(t => t.PermitReasons)
                .HasForeignKey(d => d.PermitRequestID);
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.PermitReasons)
                .HasForeignKey(d => d.ReasonCode);

        }
    }
}

