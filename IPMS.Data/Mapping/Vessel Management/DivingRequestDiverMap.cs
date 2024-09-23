using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class DivingRequestDiverMap : EntityTypeConfiguration<DivingRequestDiver>
    {
        public DivingRequestDiverMap()
        {
            // Primary Key
            this.HasKey(t => t.DivingRequestDiverID);

            // Properties
            this.Property(t => t.DiverName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.DiverType)
                .IsRequired()
                .HasMaxLength(10);

            // -- endP

            // Table & Column Mappings
            this.ToTable("DivingRequestDiver");
            this.Property(t => t.DivingRequestDiverID).HasColumnName("DivingRequestDiverID");
            this.Property(t => t.DivingRequestID).HasColumnName("DivingRequestID");
            this.Property(t => t.DiverName).HasColumnName("DiverName");
            this.Property(t => t.TimeLeftSurface).HasColumnName("TimeLeftSurface");
            this.Property(t => t.TimeArrivedSurface).HasColumnName("TimeArrivedSurface");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // -- Added by Sandeep on 07-08-2014

            this.Property(t => t.DiverType).HasColumnName("DiverType");

            // -- end

            // Relationships
            this.HasRequired(t => t.DivingRequest)
                .WithMany(t => t.DivingRequestDivers)
                .HasForeignKey(d => d.DivingRequestID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.DivingRequestDivers)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.DivingRequestDivers1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
