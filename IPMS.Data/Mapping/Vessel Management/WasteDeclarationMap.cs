using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{

    public class WasteDeclarationMap : EntityTypeConfiguration<WasteDeclaration>
    {
        public WasteDeclarationMap()
        {
            // Primary Key
            this.HasKey(t => t.WasteDeclarationID);

            // Properties
            this.Property(t => t.VCN)
                .IsRequired()
                .HasMaxLength(12);

            this.Property(t => t.MarpolCode)
              .IsRequired()
              .HasMaxLength(4);

            this.Property(t => t.ClassCode)
                .IsRequired()
                .HasMaxLength(4);  

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);


            this.Property(t => t.Others)
               .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("WasteDeclaration");
            this.Property(t => t.WasteDeclarationID).HasColumnName("WasteDeclarationID");
            this.Property(t => t.VCN).HasColumnName("VCN");            
            this.Property(t => t.MarpolCode).HasColumnName("MarpolCode");
            this.Property(t => t.ClassCode).HasColumnName("ClassCode");
            this.Property(t => t.LicenseRequestID).HasColumnName("LicenseRequestID");            
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.DeclarationName).HasColumnName("DeclarationName");
            this.Property(t => t.Others).HasColumnName("Others");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            
            // Relationships
            this.HasRequired(t => t.ArrivalNotification)
                .WithMany(t => t.WasteDeclarations)
                .HasForeignKey(d => d.VCN);

            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.WasteDeclarations)
                .HasForeignKey(d => d.MarpolCode);

            this.HasRequired(t => t.Marpol)
                .WithMany(t => t.WasteDeclarations)
                .HasForeignKey(d => d.ClassCode);

            this.HasRequired(t => t.LicenseRequest)
                .WithMany(t => t.WasteDeclarations)
                .HasForeignKey(d => d.LicenseRequestID);
            
            this.HasRequired(t => t.User)
                .WithMany(t => t.WasteDeclarations)
                .HasForeignKey(d => d.CreatedBy);            
            this.HasRequired(t => t.User1)
                .WithMany(t => t.WasteDeclarations1)
                .HasForeignKey(d => d.ModifiedBy);

           

        }
    }
}
