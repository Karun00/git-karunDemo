using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class StatementFactBunkerMap : EntityTypeConfiguration<StatementFactBunker>
    {
        public StatementFactBunkerMap()
        {
            // Primary Key
            this.HasKey(t => t.StatementFactBunkerID);

            // Properties
            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("StatementFactBunker");
            this.Property(t => t.StatementFactBunkerID).HasColumnName("StatementFactBunkerID");
            this.Property(t => t.StatementFactID).HasColumnName("StatementFactID");
            this.Property(t => t.ArrivalFuel).HasColumnName("ArrivalFuel");
            this.Property(t => t.ArrivalDiesel).HasColumnName("ArrivalDiesel");
            this.Property(t => t.SailingFuel).HasColumnName("SailingFuel");
            this.Property(t => t.SailingDiesel).HasColumnName("SailingDiesel");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.StatementFact)
                .WithMany(t => t.StatementFactBunkers)
                .HasForeignKey(d => d.StatementFactID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.StatementFactBunkers)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.StatementFactBunkers1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
