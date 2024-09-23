using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class TerminalOperatorCargoHandlingMap : EntityTypeConfiguration<TerminalOperatorCargoHandling>
    {
   
        public TerminalOperatorCargoHandlingMap()
        {
            // Primary Key
            this.HasKey(t => t.TerminalOperatorCargoHandlingID);

            // Properties
            this.Property(t => t.CargoTypeCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("TerminalOperatorCargoHandling");
            this.Property(t => t.TerminalOperatorID).HasColumnName("TerminalOperatorID");
            this.Property(t => t.CargoTypeCode).HasColumnName("CargoTypeCode");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.TerminalOperatorCargoHandlingID).HasColumnName("TerminalOperatorCargoHandlingID");

            // Relationships
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.TerminalOperatorCargoHandlings)
                .HasForeignKey(d => d.CargoTypeCode);
            this.HasRequired(t => t.TerminalOperator)
                .WithMany(t => t.TerminalOperatorCargoHandlings)
                .HasForeignKey(d => d.TerminalOperatorID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.TerminalOperatorCargoHandlings)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.User1)
                .WithMany(t => t.TerminalOperatorCargoHandlings1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
