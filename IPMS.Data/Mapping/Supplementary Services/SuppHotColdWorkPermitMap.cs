using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class SuppHotColdWorkPermitMap : EntityTypeConfiguration<SuppHotColdWorkPermit>
    {
        public SuppHotColdWorkPermitMap()
        {
            // Primary Key
            this.HasKey(t => t.SuppHotColdWorkPermitID);

            // Properties
            this.Property(t => t.GassFreeCertificateAvailable)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.GassFreeIssuingAuthority)
                //.IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.OtherLocation)
             .HasMaxLength(50);


            // Table & Column Mappings
            this.ToTable("SuppHotColdWorkPermit");
            this.Property(t => t.SuppHotColdWorkPermitID).HasColumnName("SuppHotColdWorkPermitID");
            this.Property(t => t.SuppServiceRequestID).HasColumnName("SuppServiceRequestID");
            this.Property(t => t.GassFreeCertificateAvailable).HasColumnName("GassFreeCertificateAvailable");
            this.Property(t => t.GassFreeCertificateValidity).HasColumnName("GassFreeCertificateValidity");
            this.Property(t => t.GassFreeIssuingAuthority).HasColumnName("GassFreeIssuingAuthority");
            this.Property(t => t.LocationID).HasColumnName("LocationID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.OtherLocation).HasColumnName("OtherLocation");

            // Relationships
            this.HasRequired(t => t.Location)
                .WithMany(t => t.SuppHotColdWorkPermits)
                .HasForeignKey(d => d.LocationID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.SuppHotColdWorkPermits)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.SuppHotColdWorkPermits1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.SuppServiceRequest)
                .WithMany(t => t.SuppHotColdWorkPermits)
                .HasForeignKey(d => d.SuppServiceRequestID);

        }
    }
}
