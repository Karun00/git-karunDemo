using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class CraftMap : EntityTypeConfiguration<Craft>
    {
        public CraftMap()
        {
            // Primary Key
            this.HasKey(t => t.CraftID);

            // Properties
            this.Property(t => t.CraftCode)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.PortCode)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.CraftName)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.IMONo)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.CallSign)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.ExCallSign)
                .HasMaxLength(20);

            this.Property(t => t.CraftType)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.CraftNationality)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.ClassificationSociety)
                .HasMaxLength(4);

            this.Property(t => t.FuelType)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.PortOfRegistry)
                .HasMaxLength(4);

            this.Property(t => t.EngineType)
                .HasMaxLength(4);

            this.Property(t => t.PropulsionType)
                .HasMaxLength(4);

            this.Property(t => t.OwnersName)
                .HasMaxLength(30);

            this.Property(t => t.Address)
                .HasMaxLength(500);

            this.Property(t => t.PhoneNumber)
                .HasMaxLength(15);

            this.Property(t => t.EmailID)
                .HasMaxLength(50);

            this.Property(t => t.CraftCommissionStatus)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // -- Added by santosh on 09-01-2015
            this.Property(t => t.DredgerColorCode)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Craft");
            this.Property(t => t.CraftID).HasColumnName("CraftID");
            this.Property(t => t.CraftCode).HasColumnName("CraftCode");
            this.Property(t => t.CraftName).HasColumnName("CraftName");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.IMONo).HasColumnName("IMONo");
            this.Property(t => t.CallSign).HasColumnName("CallSign");
            this.Property(t => t.ExCallSign).HasColumnName("ExCallSign");
            this.Property(t => t.CraftType).HasColumnName("CraftType");
            this.Property(t => t.CraftBuildDate).HasColumnName("CraftBuildDate");
            this.Property(t => t.DateOfDelivery).HasColumnName("DateOfDelivery");
            this.Property(t => t.CraftNationality).HasColumnName("CraftNationality");
            this.Property(t => t.ClassificationSociety).HasColumnName("ClassificationSociety");
            this.Property(t => t.CommissionDate).HasColumnName("CommissionDate");
            this.Property(t => t.AFCInMetricTon).HasColumnName("AFCInMetricTon");
            this.Property(t => t.FuelType).HasColumnName("FuelType");
            this.Property(t => t.PortOfRegistry).HasColumnName("PortOfRegistry");
            this.Property(t => t.EnginePower).HasColumnName("EnginePower");
            this.Property(t => t.EngineType).HasColumnName("EngineType");
            this.Property(t => t.PropulsionType).HasColumnName("PropulsionType");
            this.Property(t => t.NoOfPropellers).HasColumnName("NoOfPropellers");
            this.Property(t => t.MaxManeuveringSpeed).HasColumnName("MaxManeuveringSpeed");
            this.Property(t => t.BeamM).HasColumnName("BeamM");
            this.Property(t => t.RegisteredLengthM).HasColumnName("RegisteredLengthM");
            this.Property(t => t.ForwardDraftM).HasColumnName("ForwardDraftM");
            this.Property(t => t.AftDraftM).HasColumnName("AftDraftM");
            this.Property(t => t.GrossRegisteredTonnageMT).HasColumnName("GrossRegisteredTonnageMT");
            this.Property(t => t.NetRegisteredTonnageMT).HasColumnName("NetRegisteredTonnageMT");
            this.Property(t => t.DeadWeightTonnageMT).HasColumnName("DeadWeightTonnageMT");
            this.Property(t => t.BollardPullMT).HasColumnName("BollardPullMT");
            this.Property(t => t.OwnersName).HasColumnName("OwnersName");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.PhoneNumber).HasColumnName("PhoneNumber");
            this.Property(t => t.EmailID).HasColumnName("EmailID");
            this.Property(t => t.InitialFuelQuantityMT).HasColumnName("InitialFuelQuantityMT");
            this.Property(t => t.LOROBMT).HasColumnName("LOROBMT");
            this.Property(t => t.FreshwaterROBMT).HasColumnName("FreshwaterROBMT");
            this.Property(t => t.CraftCommissionStatus).HasColumnName("CraftCommissionStatus");
            this.Property(t => t.HYDROBMT).HasColumnName("HYDROBMT");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasOptional(t => t.SubCategory)
                .WithMany(t => t.Crafts8)
                .HasForeignKey(d => d.ClassificationSociety);
            this.HasRequired(t => t.Port1)
                .WithMany(t => t.CraftsPort)
                .HasForeignKey(d => d.PortCode);
            this.HasRequired(t => t.SubCategory1)
                .WithMany(t => t.Crafts1)
                .HasForeignKey(d => d.CraftCommissionStatus);
            this.HasRequired(t => t.SubCategory2)
                .WithMany(t => t.Crafts2)
                .HasForeignKey(d => d.CraftNationality);
            this.HasRequired(t => t.SubCategory3)
                .WithMany(t => t.Crafts3)
                .HasForeignKey(d => d.CraftType);
            this.HasRequired(t => t.User)
                .WithMany(t => t.Crafts)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.SubCategory4)
                .WithMany(t => t.Crafts4)
                .HasForeignKey(d => d.EngineType);
            this.HasRequired(t => t.SubCategory5)
                .WithMany(t => t.Crafts5)
                .HasForeignKey(d => d.FuelType);
            this.HasOptional(t => t.User1)
                .WithMany(t => t.Crafts1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasOptional(t => t.SubCategory6)
                .WithMany(t => t.Crafts6)
                .HasForeignKey(d => d.PortOfRegistry);
            this.HasOptional(t => t.SubCategory7)
                .WithMany(t => t.Crafts7)
                .HasForeignKey(d => d.PropulsionType);
        }
    }
}
