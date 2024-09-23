using IPMS.Domain.Models;
using System.Data.Entity.ModelConfiguration;
namespace IPMS.Data.Mapping
{
    public class ApplicantMap : EntityTypeConfiguration<Applicant>
    {
        public ApplicantMap()
        {
            // Primary Key
            this.HasKey(t => t.ApplicantID);

            // Properties
            this.Property(t => t.ApplicantName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.ApplicantTradName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.RegnNo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.VatNo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.IncTaxNo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SkilDevLevyNo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SarsTaxCleaCert)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SAASOA)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.BBBEEQualify)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.BBBEEStatVeri)
                .IsRequired();
                //.IsFixedLength()
               // .HasMaxLength(1);


            this.Property(t => t.Status)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("dbo.Applicant");
            this.Property(t => t.ApplicantID).HasColumnName("ApplicantID");
            this.Property(t => t.ApplicantName).HasColumnName("ApplicantName");
            this.Property(t => t.ApplicantTradName).HasColumnName("ApplicantTradName");
            this.Property(t => t.RegnNo).HasColumnName("RegnNo");
            this.Property(t => t.VatNo).HasColumnName("VatNo");
            this.Property(t => t.IncTaxNo).HasColumnName("IncTaxNo");
            this.Property(t => t.SkilDevLevyNo).HasColumnName("SkilDevLevyNo");
            this.Property(t => t.SarsTaxCleaCert).HasColumnName("SarsTaxCleaCert");
            this.Property(t => t.SAASOA).HasColumnName("SAASOA");
            this.Property(t => t.BBBEEQualify).HasColumnName("BBBEEQualify");
            this.Property(t => t.BBBEEStatus).HasColumnName("BBBEEStatus");
            this.Property(t => t.BBBEEStatVeri).HasColumnName("BBBEEStatVeri");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
        }
    }
}
