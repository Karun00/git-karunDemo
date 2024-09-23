using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class VesselCertificateDetailMap : EntityTypeConfiguration<VesselCertificateDetail>
    {
        public VesselCertificateDetailMap()
        {


            // Primary Key

            this.HasKey(t => t.VACERTID);

            this.HasKey(t => new { t.VesselID, t.CertificateName });

            //// Properties
            this.Property(t => t.VACERTID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.CertificateName)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.CertificateNo)
                .HasMaxLength(20);

            

            // Table & Column Mappings
            this.ToTable("VesselCertificateDetails");
            this.Property(t => t.VesselID).HasColumnName("VesselID");
            this.Property(t => t.CertificateName).HasColumnName("CertificateName");
            this.Property(t => t.CertificateNo).HasColumnName("CertificateNo");
            this.Property(t => t.DateOfIssue).HasColumnName("DateOfIssue");
            this.Property(t => t.DateOfValidity).HasColumnName("DateOfValidity");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");


            // Relationships
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.VesselCertificateDetails)
                .HasForeignKey(d => d.CertificateName);
            this.HasRequired(t => t.Vessel)
                .WithMany(t => t.VesselCertificateDetails)
                .HasForeignKey(d => d.VesselID);
            this.HasRequired(t => t.User)
            .WithMany(t => t.VesselCertificateDetails)
            .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.VesselCertificateDetails1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.Vessel)
                .WithMany(t => t.VesselCertificateDetails)
                .HasForeignKey(d => d.VesselID);

        }
    }
}
