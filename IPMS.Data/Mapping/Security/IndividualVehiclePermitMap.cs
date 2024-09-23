using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class IndividualVehiclePermitMap: EntityTypeConfiguration<IndividualVehiclePermit>
    {
        public IndividualVehiclePermitMap()
        {
            // Primary Key
            this.HasKey(t => t.IndividualVehiclePermitID);

            // Properties
            this.Property(t => t.VehicleMake)
                .HasMaxLength(50);

            this.Property(t => t.VehicleRegnNo)
                .HasMaxLength(50);

           
         

            // Table & Column Mappings
            this.ToTable("IndividualVehiclePermit");
            this.Property(t => t.IndividualVehiclePermitID).HasColumnName("IndividualVehiclePermitID");
            this.Property(t => t.PermitRequestID).HasColumnName("PermitRequestID");
            this.Property(t => t.VehicleMake).HasColumnName("VehicleMake");
            this.Property(t => t.VehicleRegnNo).HasColumnName("VehicleRegnNo");
          
          

            // Relationships
            this.HasRequired(t => t.PermitRequest)
                .WithMany(t => t.IndividualVehiclePermits)
                .HasForeignKey(d => d.PermitRequestID);
          

        }
    }
}
