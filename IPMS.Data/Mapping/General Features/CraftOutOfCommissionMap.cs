using IPMS.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IPMS.Data.Mapping
{
    public class CraftOutOfCommissionMap : EntityTypeConfiguration<CraftOutOfCommission>
    {
        public CraftOutOfCommissionMap()
        {
            // Primary Key
            this.HasKey(t => t.CraftOutOfCommissionID);

            // Properties
            this.Property(t => t.Reason)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.Remarks)
                .HasMaxLength(500);

            this.Property(t => t.CraftCommissionStatus)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("CraftOutOfCommission");
            this.Property(t => t.CraftOutOfCommissionID).HasColumnName("CraftOutOfCommissionID");
            this.Property(t => t.CraftID).HasColumnName("CraftID");
            this.Property(t => t.ExpectedDuration).HasColumnName("ExpectedDuration");
            this.Property(t => t.Reason).HasColumnName("Reason");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.CraftCommissionStatus).HasColumnName("CraftCommissionStatus");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            //Added By Santosh B on 12-Jan-2015
            this.Property(t => t.OutOfCommissionDate).HasColumnName("OutOfCommissionDate");
            this.Property(t => t.BackToCommissionDate).HasColumnName("BackToCommissionDate");

            // Relationships
            this.HasRequired(t => t.Craft)
                .WithMany(t => t.CraftOutOfCommissions)
                .HasForeignKey(d => d.CraftID);
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.CraftOutOfCommissions)
                .HasForeignKey(d => d.CraftCommissionStatus);
            this.HasRequired(t => t.User)
                .WithMany(t => t.CraftOutOfCommissions)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.User1)
                .WithMany(t => t.CraftOutOfCommissions1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.SubCategory1)
                .WithMany(t => t.CraftOutOfCommissions1)
                .HasForeignKey(d => d.Reason);

        }
    }
}
