using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class ReportQueryTemplateMap : EntityTypeConfiguration<ReportQueryTemplate>
    {
        public ReportQueryTemplateMap()
        {

            // Primary Key
            this.HasKey(t => t.QueryTemplateId);

            // Properties
            this.Property(t => t.QueryTemplateName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.ReportHeader)
                .HasMaxLength(2000);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("ReportQueryTemplate");
            this.Property(t => t.QueryTemplateId).HasColumnName("QueryTemplateId");
            this.Property(t => t.ReportbuilderId).HasColumnName("ReportbuilderId");
            this.Property(t => t.QueryTemplateName).HasColumnName("QueryTemplateName");
            this.Property(t => t.ReportHeader).HasColumnName("ReportHeader");
            this.Property(t => t.UserQuery).HasColumnName("UserQuery");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasOptional(t => t.ReportBuilder)
                .WithMany(t => t.ReportQueryTemplates)
                .HasForeignKey(d => d.ReportbuilderId);
            this.HasRequired(t => t.User)
                .WithMany(t => t.ReportQueryTemplate)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.User1)
                .WithMany(t => t.ReportQueryTemplate1)
                .HasForeignKey(d => d.ModifiedBy);


        }
    }
}
