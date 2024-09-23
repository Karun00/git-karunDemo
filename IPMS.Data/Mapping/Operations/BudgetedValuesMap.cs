using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class BudgetedValuesMap : EntityTypeConfiguration<BudgetedValues>
    {
        public BudgetedValuesMap()
        {
            // Primary Key
            this.HasKey(t => t.BudgetedValuesID);
         
            // Properties
            this.Property(t => t.PortCode)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("BudgetedValues");
            this.Property(t => t.BudgetedValuesID).HasColumnName("BudgetedValuesID");
            this.Property(t => t.FinancialYearID).HasColumnName("FinancialYearID");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.VolumesContainers).HasColumnName("VolumesContainers");
            this.Property(t => t.VolumesRBCT).HasColumnName("VolumesRBCT");
            this.Property(t => t.VolumesDryBulk).HasColumnName("VolumesDryBulk");
            this.Property(t => t.VolumesBreakBulk).HasColumnName("VolumesBreakBulk");
            this.Property(t => t.MovementsContainers).HasColumnName("MovementsContainers");
            this.Property(t => t.MovementsRBCT).HasColumnName("MovementsRBCT");
            this.Property(t => t.MovementsDryBulk).HasColumnName("MovementsDryBulk");
            this.Property(t => t.MovementsBreakBulk).HasColumnName("MovementsBreakBulk");
            this.Property(t => t.STATContainers).HasColumnName("STATContainers");
            this.Property(t => t.STATRBCT).HasColumnName("STATRBCT");
            this.Property(t => t.STATDryBulk).HasColumnName("STATDryBulk");
            this.Property(t => t.STATBreakBulk).HasColumnName("STATBreakBulk");
            
            //Newly Added as Per report format
            this.Property(t => t.TotalArrivals).HasColumnName("TotalArrivals");
            this.Property(t => t.TotalGT).HasColumnName("TotalGT");
            this.Property(t => t.TotalPilotDelays).HasColumnName("TotalPilotDelays");
            this.Property(t => t.TotalTugDelays).HasColumnName("TotalTugDelays");
            this.Property(t => t.TotalBerthingDelays).HasColumnName("TotalBerthingDelays");
            this.Property(t => t.TotalTugAvailability).HasColumnName("TotalTugAvailability");
            this.Property(t => t.TotalTugUtilization).HasColumnName("TotalTugUtilization");

            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.BudgetedValues)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.FinancialYear)
                .WithMany(t => t.BudgetedValues)
                .HasForeignKey(d => d.FinancialYearID);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.BudgetedValues1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.Port)
                .WithMany(t => t.BudgetedValues)
                .HasForeignKey(d => d.PortCode);
        }
    }
}
