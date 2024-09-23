using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
	public class EmployeeMap : EntityTypeConfiguration<Employee>
	{
		public EmployeeMap()
		{
			// Primary Key
			this.HasKey(t => t.EmployeeID);

			// Properties
			this.Property(t => t.SAPNumber)
				.HasMaxLength(10);

             this.Property(t => t.PortCode)
                 .IsRequired()
                 .HasMaxLength(2);

			this.Property(t => t.FirstName)
				.IsRequired()
				.HasMaxLength(50);

			this.Property(t => t.LastName)
				.IsRequired()
				.HasMaxLength(50);

			this.Property(t => t.Initials)
				.HasMaxLength(10);

			this.Property(t => t.EmailID)
				.IsRequired()
				.HasMaxLength(60);

			this.Property(t => t.Gender)
				.IsRequired()
				.HasMaxLength(4);

			this.Property(t => t.Department)
				.IsRequired()
				.HasMaxLength(4);

			this.Property(t => t.Designation)
				.IsRequired()
				.HasMaxLength(4);

			this.Property(t => t.BusinessUnit)
				//.IsRequired() // Commented by sandeep on 14-09-2015
				.HasMaxLength(4);

			this.Property(t => t.CostCenter)
				.IsRequired()
				.HasMaxLength(4);

			this.Property(t => t.PayrollArea)
				.IsRequired()
				.HasMaxLength(4);

			this.Property(t => t.IDNo)
				.IsRequired()
				.HasMaxLength(20);

			this.Property(t => t.PSGroup)
				.IsRequired()
				.HasMaxLength(4);

			this.Property(t => t.PersonalSubArea)
				.IsRequired()
				.HasMaxLength(4);

			this.Property(t => t.OrganizationalUnit)
				.IsRequired()
				.HasMaxLength(4);

			this.Property(t => t.RecordStatus)
				.IsFixedLength()
				.HasMaxLength(1);

			// Table & Column Mappings
			this.ToTable("Employee");
			this.Property(t => t.EmployeeID).HasColumnName("EmployeeID");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
			this.Property(t => t.SAPNumber).HasColumnName("SAPNumber");
			this.Property(t => t.FirstName).HasColumnName("FirstName");
			this.Property(t => t.LastName).HasColumnName("LastName");
			this.Property(t => t.Initials).HasColumnName("Initials");
			this.Property(t => t.BirthDate).HasColumnName("BirthDate");
			this.Property(t => t.Age).HasColumnName("Age");
			this.Property(t => t.JoiningDate).HasColumnName("JoiningDate");
			this.Property(t => t.YearsofService).HasColumnName("YearsofService");
			this.Property(t => t.OfficialMobileNo).HasColumnName("OfficialMobileNo");
			this.Property(t => t.PersonalMobileNo).HasColumnName("PersonalMobileNo");
			this.Property(t => t.EmailID).HasColumnName("EmailID");
			this.Property(t => t.Gender).HasColumnName("Gender");
			this.Property(t => t.Department).HasColumnName("Department");
			this.Property(t => t.Designation).HasColumnName("Designation");
			this.Property(t => t.BusinessUnit).HasColumnName("BusinessUnit");
			this.Property(t => t.CostCenter).HasColumnName("CostCenter");
			this.Property(t => t.PayrollArea).HasColumnName("PayrollArea");
			this.Property(t => t.IDNo).HasColumnName("IDNo");
			this.Property(t => t.PSGroup).HasColumnName("PSGroup");
			this.Property(t => t.PersonalSubArea).HasColumnName("PersonalSubArea");
			this.Property(t => t.OrganizationalUnit).HasColumnName("OrganizationalUnit");
			this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
			this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
			this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
			this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
			this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // -- Added by sandeep on 06-10-2014
            this.Property(t => t.GrossWeightTonnage).HasColumnName("GrossWeightTonnage");
            this.Property(t => t.DeadWeightTonnage).HasColumnName("DeadWeightTonnage");
            // -- end

			// Relationships

            //--Modified by sandeep on 14-09-2015
            //this.HasRequired(t => t.SubCategory)
            //    .WithMany(t => t.Employees)
            //    .HasForeignKey(d => d.BusinessUnit);

            this.HasOptional(t => t.SubCategory)
                .WithMany(t => t.Employees)
                .HasForeignKey(d => d.BusinessUnit);

            //-- end

			this.HasRequired(t => t.SubCategory1)
				.WithMany(t => t.Employees1)
				.HasForeignKey(d => d.CostCenter);
			this.HasRequired(t => t.User)
				.WithMany(t => t.Employees)
				.HasForeignKey(d => d.CreatedBy);
			this.HasRequired(t => t.SubCategory2)
				.WithMany(t => t.Employees2)
				.HasForeignKey(d => d.Department);
			this.HasRequired(t => t.SubCategory3)
				.WithMany(t => t.Employees3)
				.HasForeignKey(d => d.Designation);
			this.HasRequired(t => t.SubCategory4)
				.WithMany(t => t.Employees4)
				.HasForeignKey(d => d.Gender);
			this.HasOptional(t => t.User1)
				.WithMany(t => t.Employees1)
				.HasForeignKey(d => d.ModifiedBy);
			this.HasRequired(t => t.SubCategory5)
				.WithMany(t => t.Employees5)
				.HasForeignKey(d => d.OrganizationalUnit);
			this.HasRequired(t => t.SubCategory6)
				.WithMany(t => t.Employees6)
				.HasForeignKey(d => d.PayrollArea);
			this.HasRequired(t => t.SubCategory7)
				.WithMany(t => t.Employees7)
				.HasForeignKey(d => d.PersonalSubArea);
			this.HasRequired(t => t.SubCategory8)
				.WithMany(t => t.Employees8)
				.HasForeignKey(d => d.PSGroup);

		}
	}
}
