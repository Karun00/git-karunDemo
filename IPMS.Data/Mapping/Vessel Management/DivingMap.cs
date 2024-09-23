using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class DivingMap : EntityTypeConfiguration<Diving>
    {
        public DivingMap()
        {
            // Primary Key
            this.HasKey(t => t.DivingID);

            // Properties
            this.Property(t => t.QualificationsCompetencies)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ProvideDivingPorts)
                .IsFixedLength()
                .HasMaxLength(1);         

            this.Property(t => t.RegisteredDepartmentLabour)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.EquipmentPersProtClothing)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.EquipmentRegisterTestCert)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.EquipmentIncludeTwoRadioSets)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.QualifyPublLiabInsurance)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.BBBEE)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("Diving");
            this.Property(t => t.DivingID).HasColumnName("DivingID");
            this.Property(t => t.LicenseRequestID).HasColumnName("LicenseRequestID");
            this.Property(t => t.QualificationsCompetencies).HasColumnName("QualificationsCompetencies");
            this.Property(t => t.ProvideDivingPorts).HasColumnName("ProvideDivingPorts");
            this.Property(t => t.YearsProvidingDiving).HasColumnName("YearsProvidingDiving");
            this.Property(t => t.RegisteredDepartmentLabour).HasColumnName("RegisteredDepartmentLabour");
            this.Property(t => t.EquipmentPersProtClothing).HasColumnName("EquipmentPersProtClothing");
            this.Property(t => t.EquipmentRegisterTestCert).HasColumnName("EquipmentRegisterTestCert");
            this.Property(t => t.EquipmentIncludeTwoRadioSets).HasColumnName("EquipmentIncludeTwoRadioSets");
            this.Property(t => t.QualifyPublLiabInsurance).HasColumnName("QualifyPublLiabInsurance");
            this.Property(t => t.BBBEE).HasColumnName("BBBEE");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.Divings)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.LicenseRequest)
                .WithMany(t => t.Divings)
                .HasForeignKey(d => d.LicenseRequestID);
            this.HasOptional(t => t.User1)
                .WithMany(t => t.Divings1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
