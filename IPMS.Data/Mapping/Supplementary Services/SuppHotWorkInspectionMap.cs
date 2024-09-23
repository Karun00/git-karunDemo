using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class SuppHotWorkInspectionMap : EntityTypeConfiguration<SuppHotWorkInspection>
    {
        public SuppHotWorkInspectionMap()
        {
            // Primary Key
            this.HasKey(t => t.SuppHotWorkInspectionID);

            // Properties
            this.Property(t => t.EmergencyProcedure)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.FireRiskAssessment)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.FlammableGases)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.GasMonitoring)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.FireDetectors)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.EquipmentCondition)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ConductiveMetals)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.EquipmentStandby)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.HighProtection)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.AdequateVentilation)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.BarricadesRequired)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SymbolicSafetyScience)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.PersonalProtective)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.TrainedFireWatch)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.PostWelding)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.HouseKeepingPractices)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.AdditionalConditions)
                .HasMaxLength(500);

            this.Property(t => t.PermitStatus)
                .HasMaxLength(4);

            this.Property(t => t.Remarks)
                .HasMaxLength(500);

            this.Property(t => t.HWPN)
              .HasMaxLength(20);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("SuppHotWorkInspection");
            this.Property(t => t.SuppHotWorkInspectionID).HasColumnName("SuppHotWorkInspectionID");
            this.Property(t => t.SuppServiceRequestID).HasColumnName("SuppServiceRequestID");
            this.Property(t => t.EmergencyProcedure).HasColumnName("EmergencyProcedure");
            this.Property(t => t.FireRiskAssessment).HasColumnName("FireRiskAssessment");
            this.Property(t => t.FlammableGases).HasColumnName("FlammableGases");
            this.Property(t => t.GasMonitoring).HasColumnName("GasMonitoring");
            this.Property(t => t.FireDetectors).HasColumnName("FireDetectors");
            this.Property(t => t.EquipmentCondition).HasColumnName("EquipmentCondition");
            this.Property(t => t.ConductiveMetals).HasColumnName("ConductiveMetals");
            this.Property(t => t.EquipmentStandby).HasColumnName("EquipmentStandby");
            this.Property(t => t.HighProtection).HasColumnName("HighProtection");
            this.Property(t => t.AdequateVentilation).HasColumnName("AdequateVentilation");
            this.Property(t => t.BarricadesRequired).HasColumnName("BarricadesRequired");
            this.Property(t => t.SymbolicSafetyScience).HasColumnName("SymbolicSafetyScience");
            this.Property(t => t.PersonalProtective).HasColumnName("PersonalProtective");
            this.Property(t => t.TrainedFireWatch).HasColumnName("TrainedFireWatch");
            this.Property(t => t.PostWelding).HasColumnName("PostWelding");
            this.Property(t => t.HouseKeepingPractices).HasColumnName("HouseKeepingPractices");
            this.Property(t => t.AdditionalConditions).HasColumnName("AdditionalConditions");
            this.Property(t => t.PermitStatus).HasColumnName("PermitStatus");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.HWPN).HasColumnName("HWPN");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasOptional(t => t.SubCategory)
                .WithMany(t => t.SuppHotWorkInspections)
                .HasForeignKey(d => d.PermitStatus);
            this.HasRequired(t => t.User)
                .WithMany(t => t.SuppHotWorkInspections)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.SuppHotWorkInspections1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.SuppServiceRequest)
                .WithMany(t => t.SuppHotWorkInspections)
                .HasForeignKey(d => d.SuppServiceRequestID);

        }
    }
}
