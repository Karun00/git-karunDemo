using IPMS.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IPMS.Data.Mapping
{
    public class PortContentRoleMap : EntityTypeConfiguration<PortContentRole>
    {
        public PortContentRoleMap()
        {
            // Primary Key
            this.HasKey(t => new { t.PortContentID, t.RoleID, t.UserType, t.RecordStatus, t.CreatedBy, t.CreatedDate, t.ModifiedBy, t.ModifiedDate });

            // Properties
            this.Property(t => t.PortContentID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RoleID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UserType)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.CreatedBy)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ModifiedBy)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("PortContentRole");
            this.Property(t => t.PortContentID).HasColumnName("PortContentID");
            this.Property(t => t.RoleID).HasColumnName("RoleID");
            this.Property(t => t.UserType).HasColumnName("UserType");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.PortContent)
                .WithMany(t => t.PortContentRoles)
                .HasForeignKey(d => d.PortContentID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.PortContentRoles)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.PortContentRoles1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.Role)
                .WithMany(t => t.PortContentRoles)
                .HasForeignKey(d => d.RoleID);
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.PortContentRoles)
                .HasForeignKey(d => d.UserType);
        }
    }
}
