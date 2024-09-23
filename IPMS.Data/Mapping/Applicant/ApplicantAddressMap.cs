using IPMS.Domain.Models;
using System.Data.Entity.ModelConfiguration;
namespace IPMS.Data.Mapping
{
    public class ApplicantAddressMap : EntityTypeConfiguration<ApplicantAddress>
    {
        public ApplicantAddressMap()
        {
            // Primary Key
            this.HasKey(t => t.ApplAddrID);

            // Properties
            this.Property(t => t.NumStreet)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Suburb)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TownCity)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Status)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("Applicant_Address");
            this.Property(t => t.ApplAddrID).HasColumnName("ApplAddrID");
            this.Property(t => t.ApplicantID).HasColumnName("ApplicantID");
            this.Property(t => t.NumStreet).HasColumnName("NumStreet");
            this.Property(t => t.Suburb).HasColumnName("Suburb");
            this.Property(t => t.TownCity).HasColumnName("TownCity");
            this.Property(t => t.PostalCode).HasColumnName("PostalCode");
            this.Property(t => t.Telephone1).HasColumnName("Telephone1");
            this.Property(t => t.Telephone2).HasColumnName("Telephone2");
            this.Property(t => t.FaxNo).HasColumnName("FaxNo");
            this.Property(t => t.AddrType).HasColumnName("AddrType");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.Applicant)
                .WithMany(t => t.Applicants_Address)
                .HasForeignKey(d => d.ApplicantID);
            this.HasRequired(t => t.Sub_Category)
                .WithMany(t => t.Applicants_Address)
                .HasForeignKey(d => d.AddrType);

        }
    }
}
