using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class AddressMap : EntityTypeConfiguration<Address>
    {
        public AddressMap()
        {
            // Primary Key
            this.HasKey(t => t.AddressID);

            // Properties
            this.Property(t => t.AddressType)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.NumberStreet)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Suburb)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TownCity)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.PostalCode)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.RecordStatus)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.CountryCode)
                .HasMaxLength(4);

            // Table & Column Mappings
            this.ToTable("Address");
            this.Property(t => t.AddressID).HasColumnName("AddressID");
            this.Property(t => t.AddressType).HasColumnName("AddressType");
            this.Property(t => t.NumberStreet).HasColumnName("NumberStreet");
            this.Property(t => t.Suburb).HasColumnName("Suburb");
            this.Property(t => t.TownCity).HasColumnName("TownCity");
            this.Property(t => t.PostalCode).HasColumnName("PostalCode");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.CountryCode).HasColumnName("CountryCode");

            // Relationships
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.Addresses)
                .HasForeignKey(d => d.AddressType);
            this.HasOptional(t => t.SubCategory1)
                .WithMany(t => t.Addresses1)
                .HasForeignKey(d => d.CountryCode);
            this.HasRequired(t => t.User)
                .WithMany(t => t.Addresses)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.User1)
                .WithMany(t => t.Addresses1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
