using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
	public class ChangePasswordLogMap : EntityTypeConfiguration<ChangePasswordLog>
	{
        public ChangePasswordLogMap()
		{
            // Primary Key
            this.HasKey(t => t.LogTransId);

            // Properties
            this.Property(t => t.OldPwd)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.NewPwd)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("ChangePasswordLog");
            this.Property(t => t.LogTransId).HasColumnName("LogTransId");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.ChangeDateTime).HasColumnName("ChangeDateTime");
            this.Property(t => t.OldPwd).HasColumnName("OldPwd");
            this.Property(t => t.NewPwd).HasColumnName("NewPwd");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.ChangePasswordLogs)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.User1)
                .WithMany(t => t.ChangePasswordLogs1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.User2)
                .WithMany(t => t.ChangePasswordLogs2)
                .HasForeignKey(d => d.UserID);

		}
	}
}
