using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class SuppServiceRequestMap : EntityTypeConfiguration<SuppServiceRequest>
    {
        public SuppServiceRequestMap()
        {
            // Primary Key
            this.HasKey(t => t.SuppServiceRequestID);

            // Properties
            this.Property(t => t.VCN)
                .IsRequired()
                .HasMaxLength(12);

            this.Property(t => t.ServiceType)
                .IsRequired()
                .HasMaxLength(12);

            this.Property(t => t.PortCode)
               // .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.QuayCode)
               // .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.BerthCode)
                //.IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.Remarks)
                .HasMaxLength(2000);

            this.Property(t => t.TermsandConditions)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

                     
            this.Property(t => t.IsStartTime)
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("SuppServiceRequest");
            this.Property(t => t.SuppServiceRequestID).HasColumnName("SuppServiceRequestID");
            this.Property(t => t.VCN).HasColumnName("VCN");
            this.Property(t => t.ServiceType).HasColumnName("ServiceType");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.QuayCode).HasColumnName("QuayCode");
            this.Property(t => t.BerthCode).HasColumnName("BerthCode");
            this.Property(t => t.FromDate).HasColumnName("FromDate");
            this.Property(t => t.ToDate).HasColumnName("ToDate");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.TermsandConditions).HasColumnName("TermsandConditions");
            this.Property(t => t.WorkflowInstanceID).HasColumnName("WorkflowInstanceID");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AgentId).HasColumnName("AgentId");
            this.Property(t => t.IsStartTime).HasColumnName("IsStartTime");

            // Relationships
            this.HasRequired(t => t.ArrivalNotification)
                .WithMany(t => t.SuppServiceRequests)
                .HasForeignKey(d => d.VCN);
            //this.HasRequired(t => t.Berth)
            //    .WithMany(t => t.SuppServiceRequests)
            //    .HasForeignKey(d => new { d.PortCode, d.QuayCode, d.BerthCode });
            this.HasOptional(t => t.Berth)
                .WithMany(t => t.SuppServiceRequests)
                .HasForeignKey(d => new { d.PortCode, d.QuayCode, d.BerthCode });
            this.HasRequired(t => t.SubCategory)
               .WithMany(t => t.SuppServiceRequests)
               .HasForeignKey(d => d.ServiceType);
            this.HasRequired(t => t.User)
                .WithMany(t => t.SuppServiceRequests)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.SuppServiceRequests1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasOptional(t => t.WorkflowInstance)
                .WithMany(t => t.SuppServiceRequests)
                .HasForeignKey(d => d.WorkflowInstanceID);

            this.HasOptional(t => t.Agent)
                .WithMany(t => t.SuppServiceRequests)
                .HasForeignKey(d => d.AgentId);

        }
    }
}
