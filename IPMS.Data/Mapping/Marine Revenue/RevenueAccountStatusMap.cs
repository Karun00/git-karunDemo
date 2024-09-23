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
    public class RevenueAccountStatusMap : EntityTypeConfiguration<RevenueAccountStatus>
    {
        public RevenueAccountStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.RevenueAccountStatusID);

            // Properties
            this.Property(t => t.AccountStatusCode)
                .IsRequired()
                .HasMaxLength(4);

            // Table & Column Mappings
            this.ToTable("RevenueAccountStatus");
            this.Property(t => t.RevenueAccountStatusID).HasColumnName("RevenueAccountStatusID");
            this.Property(t => t.RevenueStopListID).HasColumnName("RevenueStopListID");
            this.Property(t => t.AccountStatusCode).HasColumnName("AccountStatusCode");

            // Relationships
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.RevenueAccountStatus)
                .HasForeignKey(d => d.AccountStatusCode);
            this.HasRequired(t => t.RevenueStopList)
                .WithMany(t => t.RevenueAccountStatus)
                .HasForeignKey(d => d.RevenueStopListID);
        }
    }
}
