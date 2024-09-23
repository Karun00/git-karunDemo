using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class UserPortMap : EntityTypeConfiguration<UserPort>
    {
        public UserPortMap()
        {
            // Primary Key
            this.HasKey(t => new { t.UserID, t.PortCode });

            // Properties
            this.Property(t => t.UserID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PortCode)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.WFStatus)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.RejectComments)
                .HasMaxLength(200);

            this.Property(t => t.RecordStatus)
                .IsFixedLength()
                .HasMaxLength(1);
            this.Property(t => t.WorkflowInstanceId);

            this.Property(t => t.IsFinal)
               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed)
               .HasMaxLength(2);

     
            // Table & Column Mappings
            this.ToTable("UserPort");
            this.Property(t => t.UserID).HasColumnName("UserID");
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
            this.Property(t => t.IsFinal).HasColumnName("IsFinal");			// Relationships

            // Relationships
            this.HasRequired(t => t.Port)
                .WithMany(t => t.UserPorts)
                .HasForeignKey(d => d.PortCode);
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.UserPorts)
                .HasForeignKey(d => d.WFStatus);
            this.HasRequired(t => t.User)
                .WithMany(t => t.UserPorts3)
                .HasForeignKey(d => d.ApprovedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.UserPorts1)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.User2)
                .WithMany(t => t.UserPorts2)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.User3)
                .WithMany(t => t.UserPorts)
                .HasForeignKey(d => d.UserID);
            this.HasRequired(t => t.User4)
                .WithMany(t => t.UserPorts4)
                .HasForeignKey(d => d.VerifiedBy);

        }
    }
}
