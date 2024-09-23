using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class ReportQueryOperatorMap : EntityTypeConfiguration<ReportQueryOperator>
    {
        public ReportQueryOperatorMap()
        {
            // Primary Key
            this.HasKey(t => t.OperatorId);

            // Properties
            this.Property(t => t.OperatorName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.OperatorValue)
                .IsRequired()
                .HasMaxLength(15);

            // Table & Column Mappings
            this.ToTable("ReportQueryOperator");
            this.Property(t => t.OperatorId).HasColumnName("OperatorId");
            this.Property(t => t.OperatorName).HasColumnName("OperatorName");
            this.Property(t => t.OperatorValue).HasColumnName("OperatorValue");
        }
    }
}


