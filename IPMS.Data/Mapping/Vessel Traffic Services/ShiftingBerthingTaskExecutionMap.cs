using IPMS.Domain.Models;
using Core.Repository;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;


namespace IPMS.Data.Mapping
{
    public class ShiftingBerthingTaskExecutionMap : EntityTypeConfiguration<ShiftingBerthingTaskExecution>
    {
        public ShiftingBerthingTaskExecutionMap()
        {
            // Primary Key
            this.HasKey(t => t.BerthingTaskExecutionID);

            // Properties
            this.Property(t => t.FromBerthPortCode)
                .HasMaxLength(2);

            this.Property(t => t.FromBerthQuayCode)
                .HasMaxLength(4);

            this.Property(t => t.FromBerthCode)
                .HasMaxLength(4);

            this.Property(t => t.ToBerthPortCode)
                .HasMaxLength(2);

            this.Property(t => t.ToBerthQuayCode)
                .HasMaxLength(4);

            this.Property(t => t.ToBerthCode)
                .HasMaxLength(4);

            this.Property(t => t.BerthingSide)
                .HasMaxLength(4);

            this.Property(t => t.FromBollardPortCode)
                .HasMaxLength(2);

            this.Property(t => t.FromBollardQuayCode)
                .HasMaxLength(4);

            this.Property(t => t.FromBollardBerthCode)
                .HasMaxLength(4);

            this.Property(t => t.FromBollardCode)
                .HasMaxLength(4);

            this.Property(t => t.ToBollardPortCode)
                .HasMaxLength(2);

            this.Property(t => t.ToBollardQuayCode)
                .HasMaxLength(4);

            this.Property(t => t.ToBollardBerthCode)
                .HasMaxLength(4);

            this.Property(t => t.ToBollardCode)
                .HasMaxLength(4);

            this.Property(t => t.MooringBollardBowPortcode)
                .HasMaxLength(2);

            this.Property(t => t.MooringBollardBowQuayCode)
                .HasMaxLength(4);

            this.Property(t => t.MooringBollardBowBerthCode)
                .HasMaxLength(4);

            this.Property(t => t.MooringBollardBowBollardCode)
                .HasMaxLength(4);

            this.Property(t => t.MooringBollardStemPortcode)
                .HasMaxLength(2);

            this.Property(t => t.MooringBollardStemQuayCode)
                .HasMaxLength(4);

            this.Property(t => t.MooringBollardStemBerthCode)
                .HasMaxLength(4);

            this.Property(t => t.MooringBollardStemBollardCode)
                .HasMaxLength(4);

            this.Property(t => t.ForwardDraft)
                .HasMaxLength(5);

            this.Property(t => t.AftDraft)
                .HasMaxLength(5);

            this.Property(t => t.Remarks)
                .HasMaxLength(500);

            this.Property(t => t.Deficiencies)
                .HasMaxLength(500);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.DelayReason)
              .HasMaxLength(500);


            // Table & Column Mappings
            this.ToTable("ShiftingBerthingTaskExecution");
            this.Property(t => t.BerthingTaskExecutionID).HasColumnName("BerthingTaskExecutionID");
            this.Property(t => t.ResourceAllocationID).HasColumnName("ResourceAllocationID");
            this.Property(t => t.StartTime).HasColumnName("StartTime");
            this.Property(t => t.EndTime).HasColumnName("EndTime");
            this.Property(t => t.FromBerthPortCode).HasColumnName("FromBerthPortCode");
            this.Property(t => t.FromBerthQuayCode).HasColumnName("FromBerthQuayCode");
            this.Property(t => t.FromBerthCode).HasColumnName("FromBerthCode");
            this.Property(t => t.ToBerthPortCode).HasColumnName("ToBerthPortCode");
            this.Property(t => t.ToBerthQuayCode).HasColumnName("ToBerthQuayCode");
            this.Property(t => t.ToBerthCode).HasColumnName("ToBerthCode");
            this.Property(t => t.BerthingSide).HasColumnName("BerthingSide");
            this.Property(t => t.FromBollardPortCode).HasColumnName("FromBollardPortCode");
            this.Property(t => t.FromBollardQuayCode).HasColumnName("FromBollardQuayCode");
            this.Property(t => t.FromBollardBerthCode).HasColumnName("FromBollardBerthCode");
            this.Property(t => t.FromBollardCode).HasColumnName("FromBollardCode");
            this.Property(t => t.ToBollardPortCode).HasColumnName("ToBollardPortCode");
            this.Property(t => t.ToBollardQuayCode).HasColumnName("ToBollardQuayCode");
            this.Property(t => t.ToBollardBerthCode).HasColumnName("ToBollardBerthCode");
            this.Property(t => t.ToBollardCode).HasColumnName("ToBollardCode");
            this.Property(t => t.MooringBollardBowPortcode).HasColumnName("MooringBollardBowPortcode");
            this.Property(t => t.MooringBollardBowQuayCode).HasColumnName("MooringBollardBowQuayCode");
            this.Property(t => t.MooringBollardBowBerthCode).HasColumnName("MooringBollardBowBerthCode");
            this.Property(t => t.MooringBollardBowBollardCode).HasColumnName("MooringBollardBowBollardCode");
            this.Property(t => t.MooringBollardStemPortcode).HasColumnName("MooringBollardStemPortcode");
            this.Property(t => t.MooringBollardStemQuayCode).HasColumnName("MooringBollardStemQuayCode");
            this.Property(t => t.MooringBollardStemBerthCode).HasColumnName("MooringBollardStemBerthCode");
            this.Property(t => t.MooringBollardStemBollardCode).HasColumnName("MooringBollardStemBollardCode");
            this.Property(t => t.FirstLineIn).HasColumnName("FirstLineIn");
            this.Property(t => t.LastLineIn).HasColumnName("LastLineIn");
            this.Property(t => t.FirstLineOut).HasColumnName("FirstLineOut");
            this.Property(t => t.LastLineOut).HasColumnName("LastLineOut");
            this.Property(t => t.ForwardDraft).HasColumnName("ForwardDraft");
            this.Property(t => t.AftDraft).HasColumnName("AftDraft");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.Deficiencies).HasColumnName("Deficiencies");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.WaitingStartTime).HasColumnName("WaitingStartTime");
            this.Property(t => t.WaitingEndTime).HasColumnName("WaitingEndTime");
            this.Property(t => t.DelayReason).HasColumnName("DelayReason");

            // Relationships
            this.HasOptional(t => t.Berth)
                .WithMany(t => t.ShiftingBerthingTaskExecutions)
                .HasForeignKey(d => new { d.FromBerthPortCode, d.FromBerthQuayCode, d.FromBerthCode });
            this.HasOptional(t => t.Berth1)
                .WithMany(t => t.ShiftingBerthingTaskExecutions1)
                .HasForeignKey(d => new { d.ToBerthPortCode, d.ToBerthQuayCode, d.ToBerthCode });
            this.HasOptional(t => t.Bollard)
                .WithMany(t => t.ShiftingBerthingTaskExecutions)
                .HasForeignKey(d => new { d.FromBollardPortCode, d.FromBollardQuayCode, d.FromBollardBerthCode, d.FromBollardCode });
            this.HasOptional(t => t.Bollard1)
                .WithMany(t => t.ShiftingBerthingTaskExecutions1)
                .HasForeignKey(d => new { d.MooringBollardBowPortcode, d.MooringBollardBowQuayCode, d.MooringBollardBowBerthCode, d.MooringBollardBowBollardCode });
            this.HasOptional(t => t.Bollard2)
                .WithMany(t => t.ShiftingBerthingTaskExecutions2)
                .HasForeignKey(d => new { d.MooringBollardStemPortcode, d.MooringBollardStemQuayCode, d.MooringBollardStemBerthCode, d.MooringBollardStemBollardCode });
            this.HasOptional(t => t.Bollard3)
                .WithMany(t => t.ShiftingBerthingTaskExecutions3)
                .HasForeignKey(d => new { d.ToBollardPortCode, d.ToBollardQuayCode, d.ToBollardBerthCode, d.ToBollardCode });
            this.HasRequired(t => t.ResourceAllocation)
                .WithMany(t => t.ShiftingBerthingTaskExecutions)
                .HasForeignKey(d => d.ResourceAllocationID);
            this.HasOptional(t => t.SubCategory)
                .WithMany(t => t.ShiftingBerthingTaskExecutions)
                .HasForeignKey(d => d.BerthingSide);
            this.HasRequired(t => t.User)
                .WithMany(t => t.ShiftingBerthingTaskExecutions)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.ShiftingBerthingTaskExecutions1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
