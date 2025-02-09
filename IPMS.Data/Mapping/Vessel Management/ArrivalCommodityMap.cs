using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
	public class ArrivalCommodityMap : EntityTypeConfiguration<ArrivalCommodity>
	{
		public ArrivalCommodityMap()
		{
			// Primary Key
			this.HasKey(t => t.ArrivalCommodityID);

			// Properties
			this.Property(t => t.VCN)
				.IsRequired()
                .HasMaxLength(12);

			this.Property(t => t.PortCode)
				.IsRequired()
                .HasMaxLength(2);

			this.Property(t => t.QuayCode)
				.IsRequired()
                .HasMaxLength(4);

			this.Property(t => t.BerthCode)
				.IsRequired()
                .HasMaxLength(4);

			this.Property(t => t.CargoType)
				.IsRequired()
                .HasMaxLength(4);

			this.Property(t => t.Package)
				.IsRequired()
                .HasMaxLength(4);

			this.Property(t => t.UOM)
				.IsRequired()
                .HasMaxLength(4);

			this.Property(t => t.RecordStatus)
				.IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

			// Table & Column Mappings
			this.ToTable("ArrivalCommodity");
			this.Property(t => t.ArrivalCommodityID).HasColumnName("ArrivalCommodityID");
			this.Property(t => t.VCN).HasColumnName("VCN");
			this.Property(t => t.PortCode).HasColumnName("PortCode");
			this.Property(t => t.QuayCode).HasColumnName("QuayCode");
			this.Property(t => t.BerthCode).HasColumnName("BerthCode");
			this.Property(t => t.CargoType).HasColumnName("CargoType");
			this.Property(t => t.Package).HasColumnName("Package");
			this.Property(t => t.UOM).HasColumnName("UOM");
			this.Property(t => t.Quantity).HasColumnName("Quantity");
			this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
			this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
			this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
			this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
			this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

			// Relationships
			this.HasRequired(t => t.SubCategory)
				.WithMany(t => t.ArrivalCommodities)
				.HasForeignKey(d => d.CargoType);
			this.HasRequired(t => t.User)
				.WithMany(t => t.ArrivalCommodities)
				.HasForeignKey(d => d.CreatedBy);
			this.HasRequired(t => t.User1)
				.WithMany(t => t.ArrivalCommodities1)
				.HasForeignKey(d => d.ModifiedBy);
			this.HasRequired(t => t.SubCategory1)
				.WithMany(t => t.ArrivalCommodities1)
				.HasForeignKey(d => d.Package);
			this.HasRequired(t => t.Berth)
				.WithMany(t => t.ArrivalCommodities)
				.HasForeignKey(d => new { d.PortCode, d.QuayCode, d.BerthCode });
			this.HasRequired(t => t.SubCategory2)
				.WithMany(t => t.ArrivalCommodities2)
				.HasForeignKey(d => d.UOM);
			this.HasRequired(t => t.ArrivalNotification)
				.WithMany(t => t.ArrivalCommodities)
				.HasForeignKey(d => d.VCN);

		}
	}
}
