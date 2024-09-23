using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Domain.Models
{
    public class FuelConsumptionDailyLogMap : EntityTypeConfiguration<FuelConsumptionDailyLog>
    {
        public FuelConsumptionDailyLogMap()
        {
            // Primary Key
            this.HasKey(t => t.FuelConsumptionDailyLogID);

            // Properties
            this.Property(t => t.PortCode)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.Remarks)
                .HasMaxLength(200);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);


            this.Property(t => t.PreviousROB).HasPrecision(10, 2);

            this.Property(t => t.PresentROB).HasPrecision(10, 2);

            this.Property(t => t.AvgFuelConsumed).HasPrecision(10, 2);

            this.Property(t => t.FuelReceived).HasPrecision(10, 2);

            this.Property(t => t.StartRunningHrs).HasPrecision(10, 2);

            this.Property(t => t.EndRunningHrs).HasPrecision(10, 2);

            this.Property(t => t.RunningHours).HasPrecision(10, 2);

            // Table & Column Mappings
            this.ToTable("FuelConsumptionDailyLog");
            this.Property(t => t.FuelConsumptionDailyLogID).HasColumnName("FuelConsumptionDailyLogID");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.CraftID).HasColumnName("CraftID");
            this.Property(t => t.PreviousROB).HasColumnName("PreviousROB");
            this.Property(t => t.PresentROB).HasColumnName("PresentROB");
            this.Property(t => t.StartDateTime).HasColumnName("StartDateTime");
            this.Property(t => t.EndDateTime).HasColumnName("EndDateTime");
            this.Property(t => t.RunningHours).HasColumnName("RunningHours");
            this.Property(t => t.AvgFuelConsumed).HasColumnName("AvgFuelConsumed");
            this.Property(t => t.FuelReceived).HasColumnName("FuelReceived");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.StartRunningHrs).HasColumnName("StartRunningHrs");
            this.Property(t => t.EndRunningHrs).HasColumnName("EndRunningHrs");

            // Relationships
            this.HasRequired(t => t.Craft)
                .WithMany(t => t.FuelConsumptionDailyLogs)
                .HasForeignKey(d => d.CraftID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.FuelConsumptionDailyLogs)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.FuelConsumptionDailyLogs1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.Port)
                .WithMany(t => t.FuelConsumptionDailyLogs)
                .HasForeignKey(d => d.PortCode);

        }
    }
}
