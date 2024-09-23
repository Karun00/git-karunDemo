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
    public class RevenuePostingMap : EntityTypeConfiguration<RevenuePosting>
    {
        public RevenuePostingMap()
        {
            // Primary Key
            this.HasKey(t => t.RevenuePostingID);

            // Properties
            this.Property(t => t.vcn)
                .HasMaxLength(12);

            this.Property(t => t.SAPAccNo)
                .HasMaxLength(16);

            this.Property(t => t.PostingStatus)
                .IsFixedLength()
                .HasMaxLength(4);

            this.Property(t => t.PortCode)
                .IsRequired()
                .HasMaxLength(2);

            // Table & Column Mappings
            this.ToTable("RevenuePosting");
            this.Property(t => t.RevenuePostingID).HasColumnName("RevenuePostingID");
            this.Property(t => t.vcn).HasColumnName("vcn");
            this.Property(t => t.PostedDate).HasColumnName("PostedDate");
            this.Property(t => t.SAPAccNo).HasColumnName("SAPAccNo");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.PostingStatus).HasColumnName("PostingStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasOptional(t => t.Agent)
                .WithMany(t => t.RevenuePostings)
                .HasForeignKey(d => d.AgentID);
            this.HasOptional(t => t.ArrivalNotification)
                .WithMany(t => t.RevenuePostings)
                .HasForeignKey(d => d.vcn);
            this.HasRequired(t => t.Port)
              .WithMany(t => t.RevenuePostings)
              .HasForeignKey(d => d.PortCode);
            this.HasOptional(t => t.User)
                .WithMany(t => t.RevenuePostings)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.User1)
                .WithMany(t => t.RevenuePostings1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
