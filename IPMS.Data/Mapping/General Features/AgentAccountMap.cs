using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class AgentAccountMap : EntityTypeConfiguration<AgentAccount>
    {
        public AgentAccountMap()
        {
            // Primary Key
            this.HasKey(t => t.AgentAccountID);

            // Properties
            //this.Property(t => t.AccountName)
            //    .IsRequired()
            //    .HasMaxLength(200);

            this.Property(t => t.AccountNo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.PortCode)
                .IsRequired()
                .HasMaxLength(2);

            // Table & Column Mappings
            this.ToTable("AgentAccount");
            this.Property(t => t.AgentAccountID).HasColumnName("AgentAccountID");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            //this.Property(t => t.AccountName).HasColumnName("AccountName");
         
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.PortCode).HasColumnName("PortCode");

            // Relationships
            this.HasRequired(t => t.Agent)
                .WithMany(t => t.AgentAccounts)
                .HasForeignKey(d => d.AgentID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.AgentAccounts)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.User1)
                .WithMany(t => t.AgentAccounts1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.Port)
                .WithMany(t => t.AgentAccounts)
                .HasForeignKey(d => d.PortCode);

        }
    }
}
