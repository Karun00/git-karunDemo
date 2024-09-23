using IPMS.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IPMS.Data.Mapping
{
    public class PortContentMap : EntityTypeConfiguration<PortContent>
    {
        public PortContentMap()
        {
            // Primary Key
            this.HasKey(t => t.PortContentID);

            // Properties
            this.Property(t => t.PortCode)
                .HasMaxLength(2);

            this.Property(t => t.ContentType)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ContentName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.LinkVisibility)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.LinkType)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("PortContent");
            this.Property(t => t.PortContentID).HasColumnName("PortContentID");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.ContentType).HasColumnName("ContentType");
            this.Property(t => t.ContentName).HasColumnName("ContentName");
            this.Property(t => t.LinkVisibility).HasColumnName("LinkVisibility");
            this.Property(t => t.LinkType).HasColumnName("LinkType");
            this.Property(t => t.LinkContent).HasColumnName("LinkContent");
            this.Property(t => t.DocumentID).HasColumnName("DocumentID");
            this.Property(t => t.ParentPortContentID).HasColumnName("ParentPortContentID");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasOptional(t => t.Document)
                .WithMany(t => t.PortContents)
                .HasForeignKey(d => d.DocumentID);
            this.HasOptional(t => t.Port)
                .WithMany(t => t.PortContents)
                .HasForeignKey(d => d.PortCode);
            this.HasRequired(t => t.User)
                .WithMany(t => t.PortContents)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.PortContents1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasOptional(t => t.PortContent2)
                .WithMany(t => t.PortContent1)
                .HasForeignKey(d => d.ParentPortContentID);
        }
    }
}
