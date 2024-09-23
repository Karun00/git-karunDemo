using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class ArrivalAgentMap : EntityTypeConfiguration<ArrivalAgent>
    {
        public ArrivalAgentMap()
        {
            // Primary Key
            this.HasKey(t => t.ArrivalAgentID);
            // Properties
            this.Property(t => t.VCN)
                .HasMaxLength(12);

            this.Property(t => t.IsPrimary)                
                .HasMaxLength(1);

            this.ToTable("ArrivalAgent");
            this.Property(t => t.ArrivalAgentID).HasColumnName("ArrivalAgentID");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.VCN).HasColumnName("VCN");
            this.Property(t => t.IsPrimary).HasColumnName("IsPrimary");

            this.HasRequired(t => t.ArrivalNotification)
            .WithMany(t => t.ArrivalAgents)
            .HasForeignKey(d => d.VCN);

            this.HasRequired(t => t.Agent)
            .WithMany(t => t.ArrivalAgents)
            .HasForeignKey(d => d.AgentID);

        }
    }
}
