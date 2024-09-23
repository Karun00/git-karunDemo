using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class ReportQueryDataTypeOperatorMap : EntityTypeConfiguration<ReportQueryDataTypeOperator>
    {
        public ReportQueryDataTypeOperatorMap()
        {
            // Primary Key
            this.HasKey(t => new { t.OperatorId, t.ApplicableDataType });

            // Properties
            this.Property(t => t.OperatorId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ApplicableDataType)
                .IsRequired()
                .HasMaxLength(15);

            // Table & Column Mappings
            this.ToTable("ReportQueryDataTypeOperator");
            this.Property(t => t.OperatorId).HasColumnName("OperatorId");
            this.Property(t => t.ApplicableDataType).HasColumnName("ApplicableDataType");

            // Relationships
            this.HasRequired(t => t.ReportQueryOperator)
                .WithMany(t => t.ReportQueryDataTypeOperators)
                .HasForeignKey(d => d.OperatorId);

        }
    }
}


