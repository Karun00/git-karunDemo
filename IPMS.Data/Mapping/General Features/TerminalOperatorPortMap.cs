using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class TerminalOperatorPortMap : EntityTypeConfiguration<TerminalOperatorPort>
    {
        public TerminalOperatorPortMap()
        {
            // Primary Key
            this.HasKey(t => t.TerminalOperatorPortID);

            // Properties
            this.Property(t => t.PortCode)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("TerminalOperatorPort");
            this.Property(t => t.TerminalOperatorPortID).HasColumnName("TerminalOperatorPortID");
            this.Property(t => t.TerminalOperatorID).HasColumnName("TerminalOperatorID");
            this.Property(t => t.PortCode).HasColumnName("PortCode");           
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");


            // Relationships
            this.HasRequired(t => t.TerminalOperator)
                .WithMany(t => t.TerminalOperatorPorts)
                .HasForeignKey(d => d.TerminalOperatorID);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.TerminalOperatorPorts1)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User2)
                .WithMany(t => t.TerminalOperatorPorts2)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.Port)
                .WithMany(t => t.TerminalOperatorPorts)
                .HasForeignKey(d => d.PortCode);

                

        }
    }
}
