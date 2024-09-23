using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class ReportBuilderMap : EntityTypeConfiguration<ReportBuilder>
    {
        public ReportBuilderMap()
        {
            // Primary Key
            this.HasKey(t => t.ReportbuilderId);

            // Properties
            this.Property(t => t.Schemaname)
                .HasMaxLength(10);

            this.Property(t => t.ReportCategory)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ReportObjectType)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.ReportObjectName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ReportDescription)
                .HasMaxLength(200);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("ReportBuilder");
            this.Property(t => t.ReportbuilderId).HasColumnName("ReportbuilderId");
            this.Property(t => t.Schemaname).HasColumnName("Schemaname");
            this.Property(t => t.ReportCategory).HasColumnName("ReportCategory");
            this.Property(t => t.ReportObjectType).HasColumnName("ReportObjectType");
            this.Property(t => t.ReportObjectName).HasColumnName("ReportObjectName");
            this.Property(t => t.ReportDescription).HasColumnName("ReportDescription");
            this.Property(t => t.ReportQuery).HasColumnName("ReportQuery");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.ReportBuilders)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.User1)
                .WithMany(t => t.ReportBuilders1)
                .HasForeignKey(d => d.ModifiedBy);
        }
    }
}
