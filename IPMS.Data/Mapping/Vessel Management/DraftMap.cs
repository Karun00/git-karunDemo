using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class DraftMap : EntityTypeConfiguration<Draft>
    {
        public DraftMap()
        {
            // Primary Key
            this.HasKey(t => t.DraftID);

            // Properties
            this.Property(t => t.DraftKey)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.VesselName)
                .HasMaxLength(200);

            this.Property(t => t.IMONo)
                .HasMaxLength(15);

            this.Property(t => t.EntityCode)
                .IsRequired()
                .HasMaxLength(25);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("Draft");
            this.Property(t => t.DraftID).HasColumnName("DraftID");
            this.Property(t => t.DraftKey).HasColumnName("DraftKey");
            this.Property(t => t.VesselName).HasColumnName("VesselName");
            this.Property(t => t.IMONo).HasColumnName("IMONo");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.EntityCode).HasColumnName("EntityCode");
            this.Property(t => t.EntityData).HasColumnName("EntityData");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasOptional(t => t.Agent)
                .WithMany(t => t.Drafts)
                .HasForeignKey(d => d.AgentID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.Drafts)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.Drafts1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
