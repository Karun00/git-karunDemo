using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class Section625CRecommendedMap : EntityTypeConfiguration<Section625CRecommended>
    {
        public Section625CRecommendedMap()
        {
            // Primary Key
            this.HasKey(t => t.Section625CRecommendedID);

            // Properties
            this.Property(t => t.RecommendedStep)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("Section625CRecommended");
            this.Property(t => t.Section625CRecommendedID).HasColumnName("Section625CRecommendedID");
            this.Property(t => t.Section625CID).HasColumnName("Section625CID");
            this.Property(t => t.RecommendedStep).HasColumnName("RecommendedStep");
            this.Property(t => t.TargetDateTime).HasColumnName("TargetDateTime");
            this.Property(t => t.ActionBy).HasColumnName("ActionBy");
            this.Property(t => t.CompletedDate).HasColumnName("CompletedDate");
            this.Property(t => t.Hour24Report625ID).HasColumnName("Hour24Report625ID");

            // Relationships
            this.HasRequired(t => t.Hour24Report625)
                .WithMany(t => t.Section625CRecommended)
                .HasForeignKey(d => d.Hour24Report625ID);
            this.HasRequired(t => t.Section625C)
                .WithMany(t => t.Section625CRecommended)
                .HasForeignKey(d => d.Section625CID);

        }
    }
}
