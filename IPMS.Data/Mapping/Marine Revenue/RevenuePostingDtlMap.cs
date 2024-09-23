using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class RevenuePostingDtlMap : EntityTypeConfiguration<RevenuePostingDtl>
    {
        public RevenuePostingDtlMap()
        {
            // Primary Key
            this.HasKey(t => t.RevenuePostingDtlID);

            // Properties
            this.Property(t => t.GroupCode)
                .HasMaxLength(10);

            this.Property(t => t.RevenuePostingDtlSrno);


            this.Property(t => t.VCN)
               .HasMaxLength(12);

            this.Property(t => t.MaterialCode)
                .HasMaxLength(10);

            this.Property(t => t.Units)
                .HasMaxLength(10);

            this.Property(t => t.UOM)
                .HasMaxLength(10);

            this.Property(t => t.MomentType)
                .HasMaxLength(4);

            this.Property(t => t.ServiceType)
                .HasMaxLength(4);

            // Table & Column Mappings
            this.ToTable("RevenuePostingDtl");
            this.Property(t => t.RevenuePostingDtlID).HasColumnName("RevenuePostingDtlID");
            this.Property(t => t.RevenuePostingDtlSrno).HasColumnName("RevenuePostingDtlSrno");

            
            this.Property(t => t.RevenuePostingID).HasColumnName("RevenuePostingID");
            this.Property(t => t.VCN).HasColumnName("VCN");
            this.Property(t => t.GroupCode).HasColumnName("GroupCode");
            this.Property(t => t.MaterialCode).HasColumnName("MaterialCode");
            this.Property(t => t.Units).HasColumnName("Units");
            this.Property(t => t.UOM).HasColumnName("UOM");
            this.Property(t => t.ReferenceID).HasColumnName("ReferenceID");
            this.Property(t => t.MomentType).HasColumnName("MomentType");
            this.Property(t => t.ServiceType).HasColumnName("ServiceType");
            this.Property(t => t.PostedOn).HasColumnName("PostedOn");
            this.Property(t => t.PostingFrom).HasColumnName("PostingFrom");

            

            // Relationships
            this.HasOptional(t => t.RevenuePosting)
                .WithMany(t => t.RevenuePostingDtls)
                .HasForeignKey(d => d.RevenuePostingID);

        }
    }
}
