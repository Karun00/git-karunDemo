using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
	public class VesselCallMovementMap : EntityTypeConfiguration<VesselCallMovement>
	{
		public VesselCallMovementMap()
		{
			// Primary Key
			this.HasKey(t => t.VesselCallMovementID);

			// Properties
			this.Property(t => t.VCN)
				.IsRequired()
				.HasMaxLength(12);
          
			this.Property(t => t.FromPositionPortCode)				
				.HasMaxLength(2);

			this.Property(t => t.FromPositionQuayCode)			
				.HasMaxLength(4);

			this.Property(t => t.FromPositionBerthCode)				
				.HasMaxLength(4);

			this.Property(t => t.FromPositionBollardCode)				
				.HasMaxLength(4);

			this.Property(t => t.ToPositionPortCode)
				.IsRequired()
				.HasMaxLength(2);

			this.Property(t => t.ToPositionQuayCode)				
				.HasMaxLength(4);

			this.Property(t => t.ToPositionBerthCode)			
				.HasMaxLength(4);

			this.Property(t => t.ToPositionBollardCode)				
				.HasMaxLength(4);

            this.Property(t => t.SlotStatus)            
            .HasMaxLength(4);

            this.Property(t => t.Slot)
                .HasMaxLength(20);

            this.Property(t => t.MovementStatus)           
            .HasMaxLength(4);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.MovementType)
              .HasMaxLength(4);           	

            //-- Added by sandeep on 29-04-2015
            this.Property(t => t.MooringBollardBowPortCode)
                .HasMaxLength(2);

            this.Property(t => t.MooringBollardBowQuayCode)
                .HasMaxLength(4);

            this.Property(t => t.MooringBollardBowBerthCode)
                .HasMaxLength(4);

            this.Property(t => t.MooringBollardBowBollardCode)
                .HasMaxLength(4);

            this.Property(t => t.MooringBollardStemPortCode)
                .HasMaxLength(2);

            this.Property(t => t.MooringBollardStemQuayCode)
                .HasMaxLength(4);

            this.Property(t => t.MooringBollardStemBerthCode)
                .HasMaxLength(4);

            this.Property(t => t.MooringBollardStemBollardCode)
                .HasMaxLength(4);
            //-- end

			// Table & Column Mappings
			this.ToTable("VesselCallMovement");
			this.Property(t => t.VesselCallMovementID).HasColumnName("VesselCallMovementID");
			this.Property(t => t.VCN).HasColumnName("VCN");
            this.Property(t => t.ServiceRequestID).HasColumnName("ServiceRequestID");
            this.Property(t => t.FromPositionPortCode).HasColumnName("FromPositionPortCode");
            this.Property(t => t.FromPositionQuayCode).HasColumnName("FromPositionQuayCode");
            this.Property(t => t.FromPositionBerthCode).HasColumnName("FromPositionBerthCode");
            this.Property(t => t.FromPositionBollardCode).HasColumnName("FromPositionBollardCode");
            this.Property(t => t.ToPositionPortCode).HasColumnName("ToPositionPortCode");
            this.Property(t => t.ToPositionQuayCode).HasColumnName("ToPositionQuayCode");
            this.Property(t => t.ToPositionBerthCode).HasColumnName("ToPositionBerthCode");
            this.Property(t => t.ToPositionBollardCode).HasColumnName("ToPositionBollardCode");
            this.Property(t => t.SlotStatus).HasColumnName("SlotStatus");
            this.Property(t => t.SlotDate).HasColumnName("SlotDate");
            this.Property(t => t.Slot).HasColumnName("Slot");
            this.Property(t => t.MovementStatus).HasColumnName("MovementStatus");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.MovementType).HasColumnName("MovementType");          
            this.Property(t => t.MovementDateTime).HasColumnName("MovementDateTime");			
            this.Property(t => t.ETB).HasColumnName("ETB");
            this.Property(t => t.ETUB).HasColumnName("ETUB");            
            this.Property(t => t.ATB).HasColumnName("ATB");
            this.Property(t => t.ATUB).HasColumnName("ATUB");

            //-- Added by sandeep on 29-04-2015
            this.Property(t => t.MooringBollardBowPortCode).HasColumnName("MooringBollardBowPortCode");
            this.Property(t => t.MooringBollardBowQuayCode).HasColumnName("MooringBollardBowQuayCode");
            this.Property(t => t.MooringBollardBowBerthCode).HasColumnName("MooringBollardBowBerthCode");
            this.Property(t => t.MooringBollardBowBollardCode).HasColumnName("MooringBollardBowBollardCode");
            this.Property(t => t.MooringBollardStemPortCode).HasColumnName("MooringBollardStemPortCode");
            this.Property(t => t.MooringBollardStemQuayCode).HasColumnName("MooringBollardStemQuayCode");
            this.Property(t => t.MooringBollardStemBerthCode).HasColumnName("MooringBollardStemBerthCode");
            this.Property(t => t.MooringBollardStemBollardCode).HasColumnName("MooringBollardStemBollardCode");
            //-- end
			
			// Relationships
			this.HasRequired(t => t.ArrivalNotification)
				.WithMany(t => t.VesselCallMovements)
				.HasForeignKey(d => d.VCN);
            this.HasOptional(t => t.Bollard)
				.WithMany(t => t.VesselCallMovements)
				.HasForeignKey(d => new { d.FromPositionPortCode, d.FromPositionQuayCode, d.FromPositionBerthCode, d.FromPositionBollardCode });
            this.HasOptional(t => t.Bollard1)
				.WithMany(t => t.VesselCallMovements1)
				.HasForeignKey(d => new { d.ToPositionPortCode, d.ToPositionQuayCode, d.ToPositionBerthCode, d.ToPositionBollardCode });

            //-- Added by sandeep on 29-04-2015
            this.HasOptional(t => t.Bollard2)
          .WithMany(t => t.VesselCallMovements2)
          .HasForeignKey(d => new { d.MooringBollardBowPortCode, d.MooringBollardBowQuayCode, d.MooringBollardBowBerthCode, d.MooringBollardBowBollardCode });
            this.HasOptional(t => t.Bollard3)
                .WithMany(t => t.VesselCallMovements3)
                .HasForeignKey(d => new { d.MooringBollardStemPortCode, d.MooringBollardStemQuayCode, d.MooringBollardStemBerthCode, d.MooringBollardStemBollardCode });            
            //-- end

			this.HasOptional(t => t.ServiceRequest)
				.WithMany(t => t.VesselCallMovements)
				.HasForeignKey(d => d.ServiceRequestID);
            this.HasOptional(t => t.MovementStatusName)
				.WithMany(t => t.VesselCallMovements)
				.HasForeignKey(d => d.MovementStatus);
            this.HasOptional(t => t.MovementType_SubCategory)
              .WithMany(t => t.VesselCallMovementsMovementType)
              .HasForeignKey(d => d.MovementType);
            this.HasOptional(t => t.SubCategory1)
				.WithMany(t => t.VesselCallMovements1)
				.HasForeignKey(d => d.SlotStatus);
			this.HasRequired(t => t.User)
				.WithMany(t => t.VesselCallMovements)
				.HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
				.WithMany(t => t.VesselCallMovements1)
				.HasForeignKey(d => d.ModifiedBy);

		}
	}
}
