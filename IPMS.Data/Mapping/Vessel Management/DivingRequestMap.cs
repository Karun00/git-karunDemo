using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Domain.Models
{
    public class DivingRequestMap : EntityTypeConfiguration<DivingRequest>
    {
        public DivingRequestMap()
        {
            // Primary Key
            this.HasKey(t => t.DivingRequestID);

            this.Property(t => t.FromPortCode)
                .HasMaxLength(2);

            this.Property(t => t.FromQuayCode)
                .HasMaxLength(4);

            this.Property(t => t.FromBerthCode)
                .HasMaxLength(4);

            this.Property(t => t.FromBollardCode)
                .HasMaxLength(4);

            this.Property(t => t.ToPortCode)
                .HasMaxLength(2);

            this.Property(t => t.ToQuayCode)
                .HasMaxLength(4);

            this.Property(t => t.ToBerthCode)
                .HasMaxLength(4);

            this.Property(t => t.ToBollardCode)
                .HasMaxLength(4);

            this.Property(t => t.Remarks)
                .HasMaxLength(2000);

            this.Property(t => t.DRN)
               .HasMaxLength(20);

            this.Property(t => t.ClearanceNo)
               .HasMaxLength(20);

            this.Property(t => t.OccupationReason)
               //.HasMaxLength(200);
               .HasMaxLength(4);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("DivingRequest");
            this.Property(t => t.DivingRequestID).HasColumnName("DivingRequestID");

            this.Property(t => t.FromPortCode).HasColumnName("FromPortCode");
            this.Property(t => t.FromQuayCode).HasColumnName("FromQuayCode");
            this.Property(t => t.FromBerthCode).HasColumnName("FromBerthCode");
            this.Property(t => t.FromBollardCode).HasColumnName("FromBollardCode");
            this.Property(t => t.ToPortCode).HasColumnName("ToPortCode");
            this.Property(t => t.ToQuayCode).HasColumnName("ToQuayCode");
            this.Property(t => t.ToBerthCode).HasColumnName("ToBerthCode");
            this.Property(t => t.ToBollardCode).HasColumnName("ToBollardCode");
            this.Property(t => t.RequiredByDate).HasColumnName("RequiredByDate");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            this.Property(t => t.OtherLocation).HasColumnName("OtherLocation");
            this.Property(t => t.OcupationToDate).HasColumnName("OcupationToDate");
            this.Property(t => t.HoursOfOccupation1).HasColumnName("HoursOfOccupation1");
            this.Property(t => t.StartTime).HasColumnName("StartTime");
            this.Property(t => t.StopTime).HasColumnName("StopTime");
            this.Property(t => t.HoursOfOccupation2).HasColumnName("HoursOfOccupation2");
            this.Property(t => t.DivingReferenceNo).HasColumnName("DivingReferenceNo");
            this.Property(t => t.QuayLocation).HasColumnName("QuayLocation");
            this.Property(t => t.SupervisorName).HasColumnName("SupervisorName");
            this.Property(t => t.DiveTenders).HasColumnName("DiveTenders");
            this.Property(t => t.LoggedDiveTimeFrom).HasColumnName("LoggedDiveTimeFrom");
            this.Property(t => t.LoggedDiveTimeTo).HasColumnName("LoggedDiveTimeTo");
            this.Property(t => t.TimeDiveOperationCancelled).HasColumnName("TimeDiveOperationCancelled");
            this.Property(t => t.DiveNature).HasColumnName("DiveNature");
            this.Property(t => t.DiverDepth).HasColumnName("DiverDepth");
            this.Property(t => t.BreathingMixture).HasColumnName("BreathingMixture");
            this.Property(t => t.CompressedAir).HasColumnName("CompressedAir");
            this.Property(t => t.DivingEquipmentUsed1).HasColumnName("DivingEquipmentUsed1");
            this.Property(t => t.DivingEquipmentUsed2).HasColumnName("DivingEquipmentUsed2");
            this.Property(t => t.TimeLeftWorkshop).HasColumnName("TimeLeftWorkshop");
            this.Property(t => t.TimeLeftSite).HasColumnName("TimeLeftSite");
            this.Property(t => t.TimeArrivedWorkshop).HasColumnName("TimeArrivedWorkshop");
            this.Property(t => t.TimeArrivedSite).HasColumnName("TimeArrivedSite");
            this.Property(t => t.DecompressionTables).HasColumnName("DecompressionTables");
            this.Property(t => t.CommsCheck).HasColumnName("CommsCheck");
            this.Property(t => t.BoilOut).HasColumnName("BoilOut");
            this.Property(t => t.Visibility).HasColumnName("Visibility");
            this.Property(t => t.SeaCondition).HasColumnName("SeaCondition");
            this.Property(t => t.UnderWaterCurrents).HasColumnName("UnderWaterCurrents");
            this.Property(t => t.ContaminatedWater).HasColumnName("ContaminatedWater");
            this.Property(t => t.WaterTemperature).HasColumnName("WaterTemperature");
            this.Property(t => t.LostDiveTime).HasColumnName("LostDiveTime");
            this.Property(t => t.RepetiveDiveDesignation).HasColumnName("RepetiveDiveDesignation");
            this.Property(t => t.SkiBoat).HasColumnName("SkiBoat");
            this.Property(t => t.LDV).HasColumnName("LDV");
            this.Property(t => t.Trailer).HasColumnName("Trailer");
            this.Property(t => t.LocationType).HasColumnName("LocationType");
            this.Property(t => t.ChangeLocation).HasColumnName("ChangeLocation");

            this.Property(t => t.MainGas).HasColumnName("MainGas");
            this.Property(t => t.Schedule).HasColumnName("Schedule");

            this.Property(t => t.DRN).HasColumnName("DRN");
            this.Property(t => t.OccupationReason).HasColumnName("OccupationReason");

            // -- Added by sandeep on 15-12-2014
            this.Property(t => t.ClearanceNo).HasColumnName("ClearanceNo");
            this.Property(t => t.OcupationFromDate).HasColumnName("OcupationFromDate");
            //-- end

            this.Property(t => t.DivingReferenceNo)
                .HasMaxLength(200);

            this.Property(t => t.QuayLocation)
                .HasMaxLength(4);

            this.Property(t => t.SupervisorName)
                .HasMaxLength(200);

            this.Property(t => t.DiveTenders)
                .HasMaxLength(200);

            this.Property(t => t.DiveNature)
                .HasMaxLength(200);

            this.Property(t => t.BreathingMixture)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.CompressedAir)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.DivingEquipmentUsed1)
                .HasMaxLength(200);

            this.Property(t => t.DivingEquipmentUsed2)
                .HasMaxLength(200);

            this.Property(t => t.DecompressionTables)
                //.HasMaxLength(50);
                 .HasMaxLength(300);

            this.Property(t => t.CommsCheck)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.BoilOut)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.Visibility)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SeaCondition)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.UnderWaterCurrents)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ContaminatedWater)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.RepetiveDiveDesignation)
                .HasMaxLength(50);

            this.Property(t => t.SkiBoat)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.LDV)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.Trailer)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.LocationType)
              .IsRequired()
              .IsFixedLength()
              .HasMaxLength(1);

            this.Property(t => t.MainGas)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.Schedule)
                .HasMaxLength(300);

            // Relationships
            this.HasOptional(t => t.Bollard)
                .WithMany(t => t.DivingRequests)
                .HasForeignKey(d => new { d.FromPortCode, d.FromQuayCode, d.FromBerthCode, d.FromBollardCode });
            this.HasOptional(t => t.Bollard1)
                .WithMany(t => t.DivingRequests1)
                .HasForeignKey(d => new { d.ToPortCode, d.ToQuayCode, d.ToBerthCode, d.ToBollardCode });
            this.HasRequired(t => t.User)
                .WithMany(t => t.DivingRequests)
                .HasForeignKey(d => d.CreatedBy);

            this.HasRequired(t => t.User1)
                .WithMany(t => t.DivingRequests1)
                .HasForeignKey(d => d.ModifiedBy);

            this.HasOptional(t => t.Quay)
                .WithMany(t => t.DivingRequests)
                .HasForeignKey(d => new { d.FromPortCode, d.QuayLocation });

            this.HasOptional(t => t.Location)
                .WithMany(t => t.DivingRequests)
               .HasForeignKey(d => d.ChangeLocation);

            this.HasOptional(t => t.Location1)
                .WithMany(t => t.DivingRequests1)
                .HasForeignKey(d => d.OtherLocation);

            //-- Added by sandeep on 10-03-2015
            this.HasOptional(t => t.SubCategory)
                .WithMany(t => t.DivingRequests)
                .HasForeignKey(d => d.OccupationReason);
            //-- end
        }
    }
}
