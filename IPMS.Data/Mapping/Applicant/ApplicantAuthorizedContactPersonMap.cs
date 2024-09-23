using IPMS.Domain.Models;
using System.Data.Entity.ModelConfiguration;
namespace IPMS.Data.Mapping
{
    public class ApplicantAuthorizedContactPersonMap : EntityTypeConfiguration<ApplicantAuthorizedContactPerson>
    {
        public ApplicantAuthorizedContactPersonMap()
        {
            // Primary Key
            this.HasKey(t => t.ApplAuthContPersID);

            // Properties
            this.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.MiddleName)
                .HasMaxLength(100);

            this.Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.IdentityNo)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.Designation)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.EmailAddress)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Status)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("Appl_Auth_Cont_Pers_Det");
            this.Property(t => t.ApplAuthContPersID).HasColumnName("ApplAuthContPersID");
            this.Property(t => t.ApplicantID).HasColumnName("ApplicantID");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.MiddleName).HasColumnName("MiddleName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.IdentityType).HasColumnName("IdentityType");
            this.Property(t => t.IdentityNo).HasColumnName("IdentityNo");
            this.Property(t => t.Designation).HasColumnName("Designation");
            this.Property(t => t.CellularNo).HasColumnName("CellularNo");
            this.Property(t => t.EmailAddress).HasColumnName("EmailAddress");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.Applicant)
                .WithMany(t => t.Appl_Auth_Cont_Pers_Det)
                .HasForeignKey(d => d.ApplicantID);
            this.HasRequired(t => t.Sub_Category)
                .WithMany(t => t.Appl_Auth_Cont_Pers_Det)
                .HasForeignKey(d => d.IdentityType);

        }
    }
}
