using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class PilotMap : EntityTypeConfiguration<Pilot>
    {
        public PilotMap()
        {
            // Primary Key
            this.HasKey(t => t.PilotID);

            // Properties
            this.Property(t => t.PortCode)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.Surname)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.LastName)
                .HasMaxLength(30);

            this.Property(t => t.IDNo)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.NationalityCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.IssuingAuthority)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.InvoiceRecipient)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.LicenseRecipient)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.ContactNo)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.CellNo)
                .HasMaxLength(15);

            this.Property(t => t.EmailID)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.Certificate_of_Competency)
            .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Pilot");
            this.Property(t => t.PilotID).HasColumnName("PilotID");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.Surname).HasColumnName("Surname");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.DateofBirth).HasColumnName("DateofBirth");
            this.Property(t => t.IDNo).HasColumnName("IDNo");
            this.Property(t => t.NationalityCode).HasColumnName("NationalityCode");
            this.Property(t => t.IssuedDate).HasColumnName("IssuedDate");
            this.Property(t => t.RenewalDate).HasColumnName("RenewalDate");
            this.Property(t => t.IssuingAuthority).HasColumnName("IssuingAuthority");
            this.Property(t => t.InvoiceRecipient).HasColumnName("InvoiceRecipient");
            this.Property(t => t.LicenseRecipient).HasColumnName("LicenseRecipient");
            this.Property(t => t.PostalAddressID).HasColumnName("PostalAddressID");
            this.Property(t => t.ResidentialAddressID).HasColumnName("ResidentialAddressID");
            this.Property(t => t.ContactNo).HasColumnName("ContactNo");
            this.Property(t => t.CellNo).HasColumnName("CellNo");
            this.Property(t => t.EmailID).HasColumnName("EmailID");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.WorkflowInstanceId).HasColumnName("WorkflowInstanceId");

            //Added by Santosh on 31-Dec-2014 for BugID: 1935
            this.Property(t => t.IssuedApprovedDate).HasColumnName("IssuedApprovedDate");
            this.Property(t => t.ExpiryDate).HasColumnName("ExpiryDate");
            this.Property(t => t.Certificate_of_Competency).HasColumnName("Certificate_of_Competency");


            // Relationships
            this.HasRequired(t => t.PostalAddress)
                .WithMany(t => t.Pilots)
                .HasForeignKey(d => d.PostalAddressID);
            this.HasOptional(t => t.ResidentialAddress)
                .WithMany(t => t.Pilots1)
                .HasForeignKey(d => d.ResidentialAddressID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.Pilots)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.Pilots1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.Pilots)
                .HasForeignKey(d => d.NationalityCode);
            this.HasRequired(t => t.Port)
                .WithMany(t => t.Pilots)
                .HasForeignKey(d => d.PortCode);
            this.HasOptional(t => t.WorkflowInstance)
                .WithMany(t => t.Pilots)
                .HasForeignKey(d => d.WorkflowInstanceId);
        }
    }
}
