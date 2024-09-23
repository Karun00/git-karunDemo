using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class AgentPortMap : EntityTypeConfiguration<AgentPort>
    {
        public AgentPortMap()
        {
            // Primary Key
            this.HasKey(t => t.AgentPortID);

            // Properties
            this.Property(t => t.PortCode)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.WFStatus)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.RejectComments)
                .HasMaxLength(200);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("AgentPort");
            this.Property(t => t.AgentPortID).HasColumnName("AgentPortID");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.WFStatus).HasColumnName("WFStatus");
            this.Property(t => t.VerifiedBy).HasColumnName("VerifiedBy");
            this.Property(t => t.VerifiedDate).HasColumnName("VerifiedDate");
            this.Property(t => t.ApprovedBy).HasColumnName("ApprovedBy");
            this.Property(t => t.ApprovedDate).HasColumnName("ApprovedDate");
            this.Property(t => t.RejectComments).HasColumnName("RejectComments");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            this.Property(t => t.FromDate).HasColumnName("FromDate");
            this.Property(t => t.ToDate).HasColumnName("ToDate");

            // Relationships
            this.HasRequired(t => t.Agent)
                .WithMany(t => t.AgentPorts)
                .HasForeignKey(d => d.AgentID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.AgentPorts)
                .HasForeignKey(d => d.ApprovedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.AgentPorts1)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.User2)
                .WithMany(t => t.AgentPorts2)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.Port)
                .WithMany(t => t.AgentPorts)
                .HasForeignKey(d => d.PortCode);
            this.HasRequired(t => t.User3)
                .WithMany(t => t.AgentPorts3)
                .HasForeignKey(d => d.VerifiedBy);
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.AgentPorts)
                .HasForeignKey(d => d.WFStatus);
                

        }
    }
}
