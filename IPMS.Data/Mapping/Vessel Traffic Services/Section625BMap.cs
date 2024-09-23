using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class Section625BMap : EntityTypeConfiguration<Section625B>
    {
        public Section625BMap()
        {
            // Primary Key
            this.HasKey(t => t.Section625BID);

            // Properties
            this.Property(t => t.IDDisputeSpecificLocation)
                .HasMaxLength(200);

            this.Property(t => t.IDTradeUnionName)
                .HasMaxLength(200);

            this.Property(t => t.IDStrikeStatuS)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.IDViolencePresent)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.IndustrialDisputeDescription)
                .HasMaxLength(500);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("Section625B");
            this.Property(t => t.Section625BID).HasColumnName("Section625BID");
            this.Property(t => t.Section625ABCDID).HasColumnName("Section625ABCDID");
            this.Property(t => t.IDIndustrialDisputeDateTime).HasColumnName("IDIndustrialDisputeDateTime");
            this.Property(t => t.IDTimeReported).HasColumnName("IDTimeReported");
            this.Property(t => t.IDDisputeSpecificLocation).HasColumnName("IDDisputeSpecificLocation");
            this.Property(t => t.IDTradeUnionName).HasColumnName("IDTradeUnionName");
            this.Property(t => t.IDTotalNoOfEmployees).HasColumnName("IDTotalNoOfEmployees");
            this.Property(t => t.IDStrikeStatuS).HasColumnName("IDStrikeStatuS");
            this.Property(t => t.IDImpactOperations).HasColumnName("IDImpactOperations");
            this.Property(t => t.IDViolencePresent).HasColumnName("IDViolencePresent");
            this.Property(t => t.IndustrialDisputeDescription).HasColumnName("IndustrialDisputeDescription");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.Hour24Report625ID).HasColumnName("Hour24Report625ID");

            // Relationships
            this.HasRequired(t => t.Hour24Report625)
                .WithMany(t => t.Section625B)
                .HasForeignKey(d => d.Hour24Report625ID);
            this.HasRequired(t => t.Section625ABCD)
                .WithMany(t => t.Section625B)
                .HasForeignKey(d => d.Section625ABCDID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.Section625B)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.Section625B1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
