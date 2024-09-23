using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
	public class UserMap : EntityTypeConfiguration<User>
	{
		public UserMap()
		{
			// Primary Key
			this.HasKey(t => t.UserID);

			// Properties
			this.Property(t => t.UserName)
				.IsRequired()
				.HasMaxLength(20);

			this.Property(t => t.UserType)
				.IsRequired()
				.HasMaxLength(4);

			this.Property(t => t.FirstName)
				.IsRequired()
				.HasMaxLength(50);

			this.Property(t => t.LastName)
				.HasMaxLength(50);

            this.Property(t => t.PWD)
                .HasMaxLength(128);

			this.Property(t => t.EmailID)
				.IsRequired()
				.HasMaxLength(50);

			this.Property(t => t.RecordStatus)
				.IsFixedLength()
				.HasMaxLength(1);

            this.Property(t => t.IsFirstTimeLogin)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.WorkflowInstanceId);

            this.Property(t => t.AnonymousUserYn)
                           .IsFixedLength()
                           .HasMaxLength(1);

            this.Property(t => t.DormantStatus)
                          .IsFixedLength()
                          .HasMaxLength(1);

            this.Property(t => t.IncorrectLogins)
                .IsRequired();

            this.Property(t => t.ReasonForAccess).HasMaxLength(100);

            this.Property(t => t.ValidFromDate);
            
            this.Property(t => t.ValidToDate);

			// Table & Column Mappings
			this.ToTable("Users");
			this.Property(t => t.UserID).HasColumnName("UserID");
			this.Property(t => t.UserName).HasColumnName("UserName");
			this.Property(t => t.UserType).HasColumnName("UserType");
			this.Property(t => t.UserTypeID).HasColumnName("UserTypeID");
			this.Property(t => t.FirstName).HasColumnName("FirstName");
			this.Property(t => t.LastName).HasColumnName("LastName");
			this.Property(t => t.ContactNo).HasColumnName("ContactNo");
			this.Property(t => t.EmailID).HasColumnName("EmailID");
			this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
			this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
			this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
			this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
			this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AnonymousUserYn).HasColumnName("AnonymousUserYn");
            this.Property(t => t.PWD).HasColumnName("PWD");
            this.Property(t => t.IsFirstTimeLogin).HasColumnName("IsFirstTimeLogin");
            this.Property(t => t.PwdExpirtyDate).HasColumnName("PwdExpirtyDate");
            this.Property(t => t.WorkflowInstanceId).HasColumnName("WorkflowInstanceId");
            this.Property(t => t.IncorrectLogins).HasColumnName("IncorrectLogins");
            this.Property(t => t.LoginTime).HasColumnName("LoginTime");
            this.Property(t => t.DormantStatus).HasColumnName("DormantStatus");
            this.Property(t => t.ReasonForAccess).HasColumnName("ReasonForAccess");
            this.Property(t => t.ValidFromDate).HasColumnName("ValidFromDate");
            this.Property(t => t.ValidToDate).HasColumnName("ValidToDate");

            
			// Relationships
			this.HasRequired(t => t.SubCategory)
				.WithMany(t => t.Users)
				.HasForeignKey(d => d.UserType);
			this.HasRequired(t => t.User1)
				.WithMany(t => t.Users1)
				.HasForeignKey(d => d.CreatedBy);
			this.HasOptional(t => t.User2)
				.WithMany(t => t.Users11)
				.HasForeignKey(d => d.ModifiedBy);

		}
	}
}
