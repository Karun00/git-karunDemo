using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
	public class ArrivalIMDGTankerMap : EntityTypeConfiguration<ArrivalIMDGTanker>
	{
		public ArrivalIMDGTankerMap()
		{
			// Primary Key
			this.HasKey(t => t.ArrivalIMDGTankerID);

			// Properties
			this.Property(t => t.VCN)
				.IsRequired()
                .HasMaxLength(12);

			this.Property(t => t.Purpose)
				.IsRequired()
                .HasMaxLength(4);

			this.Property(t => t.Commodity)
				.IsRequired()
                .HasMaxLength(4);

			this.Property(t => t.FromTank)
				.HasMaxLength(10);

            this.Property(t => t.RecordStatus)
				.IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

			// Table & Column Mappings
			this.ToTable("ArrivalIMDGTanker");
			this.Property(t => t.ArrivalIMDGTankerID).HasColumnName("ArrivalIMDGTankerID");
			this.Property(t => t.VCN).HasColumnName("VCN");
			this.Property(t => t.Purpose).HasColumnName("Purpose");
			this.Property(t => t.Commodity).HasColumnName("Commodity");
			this.Property(t => t.Quantity).HasColumnName("Quantity");
			this.Property(t => t.FromTank).HasColumnName("FromTank");
            
            
			this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
			this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
			this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
			this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
			this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

			// Relationships
			this.HasRequired(t => t.SubCategory)
				.WithMany(t => t.ArrivalIMDGTankers)
				.HasForeignKey(d => d.Commodity);
			this.HasRequired(t => t.User)
				.WithMany(t => t.ArrivalIMDGTankers)
				.HasForeignKey(d => d.CreatedBy);
			this.HasRequired(t => t.User1)
				.WithMany(t => t.ArrivalIMDGTankers1)
				.HasForeignKey(d => d.ModifiedBy);
			this.HasRequired(t => t.SubCategory1)
				.WithMany(t => t.ArrivalIMDGTankers1)
				.HasForeignKey(d => d.Purpose);
			this.HasRequired(t => t.ArrivalNotification)
				.WithMany(t => t.ArrivalIMDGTankers)
				.HasForeignKey(d => d.VCN);

		}
	}
}
