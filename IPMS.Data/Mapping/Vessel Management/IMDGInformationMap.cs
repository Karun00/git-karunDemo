using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{

    public class IMDGInformationMap : EntityTypeConfiguration<IMDGInformation>
    {
        public IMDGInformationMap()
        {
            // Primary Key
            this.HasKey(t => t.IMDGInformationID);

            // Properties
            this.Property(t => t.VCN)
                .IsRequired()
                .HasMaxLength(12);

            this.Property(t => t.ClassCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.Purpose)
        .IsRequired()
        .HasMaxLength(4);

            this.Property(t => t.CargoCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.UNNo)
                .HasMaxLength(15);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);


            this.Property(t => t.Others)
               .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("IMDGInformation");
            this.Property(t => t.IMDGInformationID).HasColumnName("IMDGInformationID");
            this.Property(t => t.VCN).HasColumnName("VCN");
            this.Property(t => t.ClassCode).HasColumnName("ClassCode");
            this.Property(t => t.CargoCode).HasColumnName("CargoCode");
            this.Property(t => t.UNNo).HasColumnName("UNNo");
            this.Property(t => t.Purpose).HasColumnName("Purpose");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.NoofContainer).HasColumnName("NoofContainer");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.Others).HasColumnName("Others");
            // Relationships
            this.HasRequired(t => t.ArrivalNotification)
                .WithMany(t => t.IMDGInformations)
                .HasForeignKey(d => d.VCN);
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.IMDGInformations)
                .HasForeignKey(d => d.CargoCode);
            this.HasRequired(t => t.SubCategory1)
                .WithMany(t => t.IMDGInformations1)
                .HasForeignKey(d => d.ClassCode);
            this.HasRequired(t => t.User)
                .WithMany(t => t.IMDGInformations)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.IMDGInformations1)
                .HasForeignKey(d => d.ModifiedBy);

            this.HasRequired(t => t.SubCategoryPurpus)
            .WithMany(t => t.ArrivalIMDGInformatin)
            .HasForeignKey(d => d.Purpose);

        }
    }
}
