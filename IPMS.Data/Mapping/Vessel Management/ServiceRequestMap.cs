using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class ServiceRequestMap : EntityTypeConfiguration<ServiceRequest>
    {
        public ServiceRequestMap()
        {
            // Primary Key
            this.HasKey(t => t.ServiceRequestID);

            // Properties
            this.Property(t => t.VCN)
                .IsRequired()
                .HasMaxLength(12);

            this.Property(t => t.MovementType)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.SideAlongSideCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.OwnSteam)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.NoMainEngine)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.Comments)
                .HasMaxLength(500);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.IsFinal)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed)
                .HasMaxLength(2);
            this.Property(t => t.IsRecordingCompleted)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed)
                .HasMaxLength(2);

            this.Property(t => t.IsTidal)
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("ServiceRequest");
            this.Property(t => t.ServiceRequestID).HasColumnName("ServiceRequestID");
            this.Property(t => t.VCN).HasColumnName("VCN");
            this.Property(t => t.MovementType).HasColumnName("MovementType");
            this.Property(t => t.MovementDateTime).HasColumnName("MovementDateTime");
            this.Property(t => t.SideAlongSideCode).HasColumnName("SideAlongSideCode");
            this.Property(t => t.OwnSteam).HasColumnName("OwnSteam");
            this.Property(t => t.NoMainEngine).HasColumnName("NoMainEngine");
            this.Property(t => t.Comments).HasColumnName("Comments");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.WorkflowInstanceId).HasColumnName("WorkflowInstanceId");
            this.Property(t => t.IsFinal).HasColumnName("IsFinal");
            this.Property(t => t.IsRecordingCompleted).HasColumnName("IsRecordingCompleted");
            this.Property(t => t.DraftFWD).HasColumnName("DraftFWD");
            this.Property(t => t.DraftAFT).HasColumnName("DraftAFT");
            this.Property(t => t.PassengersEmbarking).HasColumnName("PassengersEmbarking");
            this.Property(t => t.PassengersDisembarking).HasColumnName("PassengersDisembarking");
            this.Property(t => t.BPWorkflowInstanceId).HasColumnName("BPWorkflowInstanceId");
            this.Property(t => t.IsTidal).HasColumnName("IsTidal");
            this.Property(t => t.PreferredDateTime).HasColumnName("PreferredDateTime");
            this.Property(t => t.SlotPeriod).HasColumnName("SlotPeriod");
            this.Property(t => t.MovementSlot).HasColumnName("MovementSlot");

            // Relationships
            this.HasRequired(t => t.ArrivalNotification)
                .WithMany(t => t.ServiceRequests)
                .HasForeignKey(d => d.VCN);
            this.HasRequired(t => t.User)
                .WithMany(t => t.ServiceRequests)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.ServiceRequests1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.ServiceRequests)
                .HasForeignKey(d => d.MovementType);
            this.HasRequired(t => t.SubCategory1)
                .WithMany(t => t.ServiceRequests1)
                .HasForeignKey(d => d.SideAlongSideCode);
            this.HasOptional(t => t.WorkflowInstance)
                .WithMany(t => t.ServiceRequests)
                .HasForeignKey(d => d.WorkflowInstanceId);
            this.HasOptional(t => t.WorkflowInstance1)
               .WithMany(t => t.ServiceRequests1)
               .HasForeignKey(d => d.BPWorkflowInstanceId);

        }
    }
}
