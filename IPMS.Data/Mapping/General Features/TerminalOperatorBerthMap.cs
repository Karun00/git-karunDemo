using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class TerminalOperatorBerthMap : EntityTypeConfiguration<TerminalOperatorBerth>
    {
   
        public TerminalOperatorBerthMap()
        {
            // Primary Key
            this.HasKey(t => t.TerminalOperatorBerthID);

            // Properties
            this.Property(t => t.PortCode)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.QuayCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.BerthCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("TerminalOperatorBerth");
            this.Property(t => t.TerminalOperatorID).HasColumnName("TerminalOperatorID");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.QuayCode).HasColumnName("QuayCode");
            this.Property(t => t.BerthCode).HasColumnName("BerthCode");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.TerminalOperatorBerthID).HasColumnName("TerminalOperatorBerthID");

            // Relationships
            this.HasRequired(t => t.Berth)
                .WithMany(t => t.TerminalOperatorBerths)
                .HasForeignKey(d => new { d.PortCode, d.QuayCode, d.BerthCode });
            this.HasRequired(t => t.TerminalOperator)
                .WithMany(t => t.TerminalOperatorBerths)
                .HasForeignKey(d => d.TerminalOperatorID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.TerminalOperatorBerths)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.User1)
                .WithMany(t => t.TerminalOperatorBerths1)
                .HasForeignKey(d => d.ModifiedBy);

        }

    }
}
