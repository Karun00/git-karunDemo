using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class IndividualPermitApplicationDetailsMap : EntityTypeConfiguration<IndividualPermitApplicationDetails>
    {
        public IndividualPermitApplicationDetailsMap()
        {
            // Primary Key
            this.HasKey(t => t.IndividualApplicationID);

            // Properties
            //this.Property(t => t.AccessGates)
            //    .IsRequired()
            //    .HasMaxLength(4);

            // Table & Column Mappings
            this.ToTable("IndividualPermitApplicationDetails");
            this.Property(t => t.Initial).HasColumnName("Initial");
            this.Property(t => t.SACitizen).HasColumnName("SACitizen");
            this.Property(t => t.Gender).HasColumnName("Gender");
            this.Property(t => t.PermitRequestID).HasColumnName("PermitRequestID");
            this.Property(t => t.Suburb).HasColumnName("Suburb");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.PostalCode).HasColumnName("PostalCode");
            this.Property(t => t.CountryOfOrigin).HasColumnName("CountryOfOrigin");
            this.Property(t => t.DepartmentManager).HasColumnName("DepartmentManager");
            this.Property(t => t.JobTitle).HasColumnName("JobTitle");
            this.Property(t => t.Current_Permit_Exists).HasColumnName("Current_Permit_Exists");
            this.Property(t => t.Reason_Reapplication).HasColumnName("Reason_Reapplication");
            this.Property(t => t.Port_Induction_Training).HasColumnName("Port_Induction_Training");
            this.Property(t => t.Training_Date).HasColumnName("Training_Date");
            this.Property(t => t.Criminal_Bckground).HasColumnName("Criminal_Bckground");
            this.Property(t => t.Signature).HasColumnName("Signature");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.EmployeeNo).HasColumnName("EmployeeNo");

            // Relationships
            this.HasRequired(t => t.PermitRequest)
                .WithMany(t => t.IndividualPermitApplicationDetails)
                .HasForeignKey(d => d.PermitRequestID);
          

        }
    }
}
