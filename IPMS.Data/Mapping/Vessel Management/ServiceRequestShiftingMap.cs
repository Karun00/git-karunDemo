using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
	public class ServiceRequestShiftingMap : EntityTypeConfiguration<ServiceRequestShifting>
	{
		public ServiceRequestShiftingMap()
		{
			// Primary Key
			this.HasKey(t => t.ServiceRequestShiftingID);

			// Properties
			this.Property(t => t.ToPortCode)
				.IsRequired()
                .HasMaxLength(2);

			this.Property(t => t.ToQuayCode)
				.IsRequired()
                .HasMaxLength(4);

			this.Property(t => t.ToBerthCode)
				.IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.FromPositionPortCode)             
                .HasMaxLength(2);

            this.Property(t => t.FromPositionQuayCode)               
                .HasMaxLength(4);

            this.Property(t => t.FromPositionBerthCode)               
                .HasMaxLength(4);

            this.Property(t => t.FromPositionBollardCode)               
                .HasMaxLength(4);

            this.Property(t => t.ToPositionPortCode)               
                .HasMaxLength(2);

            this.Property(t => t.ToPositionQuayCode)               
                .HasMaxLength(4);

            this.Property(t => t.ToPositionBerthCode)                
                .HasMaxLength(4);

            this.Property(t => t.ToPositionBollardCode)              
                .HasMaxLength(4);

			this.Property(t => t.RecordStatus)
				.IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

			// Table & Column Mappings
			this.ToTable("ServiceRequestShifting");
			this.Property(t => t.ServiceRequestShiftingID).HasColumnName("ServiceRequestShiftingID");
			this.Property(t => t.ServiceRequestID).HasColumnName("ServiceRequestID");
			this.Property(t => t.ToPortCode).HasColumnName("ToPortCode");
			this.Property(t => t.ToQuayCode).HasColumnName("ToQuayCode");
			this.Property(t => t.ToBerthCode).HasColumnName("ToBerthCode");
			this.Property(t => t.FromPositionPortCode).HasColumnName("FromPositionPortCode");
			this.Property(t => t.FromPositionQuayCode).HasColumnName("FromPositionQuayCode");
			this.Property(t => t.FromPositionBerthCode).HasColumnName("FromPositionBerthCode");
			this.Property(t => t.FromPositionBollardCode).HasColumnName("FromPositionBollardCode");
			this.Property(t => t.ToPositionPortCode).HasColumnName("ToPositionPortCode");
			this.Property(t => t.ToPositionQuayCode).HasColumnName("ToPositionQuayCode");
			this.Property(t => t.ToPositionBerthCode).HasColumnName("ToPositionBerthCode");
			this.Property(t => t.ToPositionBollardCode).HasColumnName("ToPositionBollardCode");
			this.Property(t => t.DraftFWD).HasColumnName("DraftFWD");
			this.Property(t => t.DraftAFT).HasColumnName("DraftAFT");
			this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
			this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
			this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
			this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
			this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

			// Relationships
			this.HasRequired(t => t.Berth)
				.WithMany(t => t.ServiceRequestShiftings)
				.HasForeignKey(d => new { d.ToPortCode, d.ToQuayCode, d.ToBerthCode });
            this.HasOptional(t => t.Bollard)
                .WithMany(t => t.ServiceRequestShiftings)
                .HasForeignKey(d => new { d.FromPositionPortCode, d.FromPositionQuayCode, d.FromPositionBerthCode, d.FromPositionBollardCode });
            this.HasOptional(t => t.Bollard1)
                .WithMany(t => t.ServiceRequestShiftings1)
                .HasForeignKey(d => new { d.ToPositionPortCode, d.ToPositionQuayCode, d.ToPositionBerthCode, d.ToPositionBollardCode });
			this.HasRequired(t => t.ServiceRequest)
				.WithMany(t => t.ServiceRequestShiftings)
				.HasForeignKey(d => d.ServiceRequestID);
			this.HasRequired(t => t.User)
				.WithMany(t => t.ServiceRequestShiftings)
				.HasForeignKey(d => d.CreatedBy);
			this.HasRequired(t => t.User1)
				.WithMany(t => t.ServiceRequestShiftings1)
				.HasForeignKey(d => d.ModifiedBy);

		}
	}
}
