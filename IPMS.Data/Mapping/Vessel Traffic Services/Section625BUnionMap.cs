using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class Section625BUnionMap : EntityTypeConfiguration<Section625BUnion>
    {
        public Section625BUnionMap()
        {
            // Primary Key
            this.HasKey(t => t.Section625BUnionID);

            // Properties
            this.Property(t => t.UnionName)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Section625BUnion");
            this.Property(t => t.Section625BUnionID).HasColumnName("Section625BUnionID");
            this.Property(t => t.Section625BID).HasColumnName("Section625BID");
            this.Property(t => t.UnionName).HasColumnName("UnionName");
            this.Property(t => t.TotalMembership).HasColumnName("TotalMembership");
            this.Property(t => t.TotalRosteredForShift).HasColumnName("TotalRosteredForShift");
            this.Property(t => t.TotalPresent).HasColumnName("TotalPresent");
            this.Property(t => t.TotalStrike).HasColumnName("TotalStrike");
            this.Property(t => t.TotalLeave).HasColumnName("TotalLeave");
            this.Property(t => t.TotalSick).HasColumnName("TotalSick");
            this.Property(t => t.ReplacementLeave).HasColumnName("ReplacementLeave");
            this.Property(t => t.Hour24Report625ID).HasColumnName("Hour24Report625ID");

            // Relationships
            this.HasRequired(t => t.Hour24Report625)
                .WithMany(t => t.Section625BUnion)
                .HasForeignKey(d => d.Hour24Report625ID);
            this.HasRequired(t => t.Section625B)
                .WithMany(t => t.Section625BUnion)
                .HasForeignKey(d => d.Section625BID);

        }
    }
}
