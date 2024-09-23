using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class ReportQueryLookupMap : EntityTypeConfiguration<ReportQueryLookup>
    {
        public ReportQueryLookupMap()
        {

            // Primary Key
            this.HasKey(t => t.LookupColumnname);

            // Properties
            this.Property(t => t.LookupName)
                .IsRequired()
                .HasMaxLength(200);           

            // Table & Column Mappings
            this.ToTable("ReportQueryLookup");
            this.Property(t => t.LookupColumnname).HasColumnName("LookupColumnname");
            this.Property(t => t.LookupName).HasColumnName("LookupName");

        }
    }
}
