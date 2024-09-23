using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class VisitorPermitMap : EntityTypeConfiguration<VisitorPermit>
    {
        public VisitorPermitMap()
        {
            // Primary Key
            this.HasKey(t => t.VisitorPermitID);

            // Properties
            //this.Property(t => t.Reason)
            //    .HasMaxLength(200);

            this.Property(t => t.AuthorizedPersonName)
                .HasMaxLength(50);

            this.Property(t => t.Division)
                .HasMaxLength(50);

            this.Property(t => t.PositionHeld)
                .HasMaxLength(50);

            this.Property(t => t.EscortName)
                .HasMaxLength(50);

            this.Property(t => t.TelephoneNo)
                .HasMaxLength(50);

            this.Property(t => t.PermitNo)
                .HasMaxLength(50);

            this.Property(t => t.PermitCode)
                .HasMaxLength(4);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.CompanyName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("VisitorPermit");
            this.Property(t => t.VisitorPermitID).HasColumnName("VisitorPermitID");
            this.Property(t => t.PermitRequestID).HasColumnName("PermitRequestID");
            this.Property(t => t.Reason).HasColumnName("Reason");
            this.Property(t => t.AuthorizedPersonName).HasColumnName("AuthorizedPersonName");
            this.Property(t => t.Division).HasColumnName("Division");
            this.Property(t => t.PositionHeld).HasColumnName("PositionHeld");
            this.Property(t => t.EscortName).HasColumnName("EscortName");
            this.Property(t => t.TelephoneNo).HasColumnName("TelephoneNo");
            this.Property(t => t.PermitNo).HasColumnName("PermitNo");
            this.Property(t => t.PermitCode).HasColumnName("PermitCode");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.CompanyName).HasColumnName("CompanyName");

            // Relationships
            this.HasRequired(t => t.PermitRequest)
                .WithMany(t => t.VisitorPermits)
                .HasForeignKey(d => d.PermitRequestID);
            this.HasOptional(t => t.SubCategory)
                .WithMany(t => t.VisitorPermits)
                .HasForeignKey(d => d.PermitCode);
            this.HasRequired(t => t.User)
                .WithMany(t => t.VisitorPermits)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.VisitorPermits1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
