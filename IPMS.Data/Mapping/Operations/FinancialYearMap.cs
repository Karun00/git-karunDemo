using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class FinancialYearMap : EntityTypeConfiguration<FinancialYear>
    {
        public FinancialYearMap()
        {
            // Primary Key
            this.HasKey(t => t.FinancialYearID);

            // Properties
            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.IsCurrentFinancialYear)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("FinancialYear");
            this.Property(t => t.FinancialYearID).HasColumnName("FinancialYearID");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.IsCurrentFinancialYear).HasColumnName("IsCurrentFinancialYear");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.FinancialYears)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.FinancialYears1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
