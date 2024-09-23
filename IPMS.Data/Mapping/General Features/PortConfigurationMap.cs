using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class PortConfigurationMap : EntityTypeConfiguration<PortConfiguration>
    {
        public PortConfigurationMap()
        {
            // Primary Key
            this.HasKey(t => t.PortCode);

            // Properties
            this.Property(t => t.PortCode)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.ApproveCode)
                .HasMaxLength(4);

            this.Property(t => t.RejectCode)
                .HasMaxLength(4);

            this.Property(t => t.WorkFlowInitialStatus)
                .HasMaxLength(4);
            this.Property(t => t.SERVREQPRECOND1);

            this.Property(t => t.CancelCode)
                .HasMaxLength(4);

            

            // Table & Column Mappings
            this.ToTable("PortConfiguration");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.ApproveCode).HasColumnName("ApproveCode");
            this.Property(t => t.RejectCode).HasColumnName("RejectCode");
            this.Property(t => t.WorkFlowInitialStatus).HasColumnName("WorkFlowInitialStatus");
            this.Property(t => t.SERVREQPRECOND1).HasColumnName("SERVREQPRECOND1");
            this.Property(t => t.CancelCode).HasColumnName("CancelCode");
            this.Property(t => t.IncorrectPWDCount).HasColumnName("IncorrectPWDCount");
            // Relationships
            this.HasRequired(t => t.Port)
                .WithOptional(t => t.PortConfiguration);
            this.HasOptional(t => t.ApproveCodeSubCategory)
                .WithMany(t => t.PortConfigurations)
                .HasForeignKey(d => d.ApproveCode);
            this.HasOptional(t => t.RejectCodeSubCategory)
                .WithMany(t => t.PortConfigurations1)
                .HasForeignKey(d => d.RejectCode);
            this.HasOptional(t => t.WorkFlowInitialSubCategory)
                .WithMany(t => t.PortConfigurations2)
                .HasForeignKey(d => d.WorkFlowInitialStatus);
            this.HasOptional(t => t.CancelCodeSubCategory)
                .WithMany(t => t.PortConfigurations3)
                .HasForeignKey(d => d.CancelCode);
        }
    }
}

