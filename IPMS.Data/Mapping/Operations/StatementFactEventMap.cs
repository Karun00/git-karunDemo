using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class StatementFactEventMap : EntityTypeConfiguration<StatementFactEvent>
    {
        public StatementFactEventMap()
        {
            // Primary Key
            this.HasKey(t => t.StatementFactEventID);

            // Properties
            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.DelayType)
                .HasMaxLength(4);

            // Table & Column Mappings
            this.ToTable("StatementFactEvent");
            this.Property(t => t.StatementFactEventID).HasColumnName("StatementFactEventID");
            this.Property(t => t.StatementFactID).HasColumnName("StatementFactID");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.DelayType).HasColumnName("DelayType");
            this.Property(t => t.StartOperational).HasColumnName("StartOperational");
            this.Property(t => t.EndOperational).HasColumnName("EndOperational");
            this.Property(t => t.Duration).HasColumnName("Duration");
            this.Property(t => t.Remarks).HasColumnName("Remarks");

            // Relationships
            this.HasRequired(t => t.StatementFact)
                .WithMany(t => t.StatementFactEvents)
                .HasForeignKey(d => d.StatementFactID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.StatementFactEvents)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.SubCategory)
                .WithMany(t => t.StatementFactEvents)
                .HasForeignKey(d => d.DelayType);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.StatementFactEvents1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
