using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class VesselMap : EntityTypeConfiguration<Vessel>
    {
        public VesselMap()
        {
            // Primary Key
            this.HasKey(t => t.VesselID);

            // Properties
            this.Property(t => t.IMONo)
                .IsRequired()
                .HasMaxLength(15);

            //this.Property(t => t.ExCallSign)
            //    .IsRequired()
            //    .HasMaxLength(200);

            this.Property(t => t.ExCallSign)
               .HasMaxLength(200);

            this.Property(t => t.ClassificationSociety)

                .HasMaxLength(4);

            this.Property(t => t.VesselName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.VesselType)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.CallSign)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.OfficialNumber)
                .HasMaxLength(15);

            //this.Property(t => t.PortOfRegistry)
            //    .IsRequired()
            //    .HasMaxLength(4);

            //this.Property(t => t.PortOfRegistry)

            //   .HasMaxLength(2);
            this.Property(t => t.PortOfRegistry)
                 .IsRequired()
                 .HasMaxLength(5);

            this.Property(t => t.ExVesselName)
                .HasMaxLength(200);

            this.Property(t => t.VesselNationality)
                .HasMaxLength(4);

            this.Property(t => t.IsGovtVessel)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.BowThruster)
                .IsFixedLength()
                .HasMaxLength(1);

            //this.Property(t => t.WFStatus)
            //    .IsRequired()
            //    .HasMaxLength(4);

            //this.Property(t => t.RejectComments)
            //    .HasMaxLength(200);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);


            this.Property(t => t.IsFinal)
                 .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.SternThruster)
                .IsFixedLength()
                .HasMaxLength(1);


            this.Property(t => t.WorkflowInstanceId);

            // Table & Column Mappings
            this.ToTable("Vessel");
            this.Property(t => t.VesselID).HasColumnName("VesselID");
            this.Property(t => t.IMONo).HasColumnName("IMONo");
            this.Property(t => t.ExCallSign).HasColumnName("ExCallSign");
            this.Property(t => t.ClassificationSociety).HasColumnName("ClassificationSociety");
            this.Property(t => t.VesselName).HasColumnName("VesselName");
            this.Property(t => t.VesselType).HasColumnName("VesselType");
            this.Property(t => t.NoOfBays).HasColumnName("NoOfBays");
            this.Property(t => t.CallSign).HasColumnName("CallSign");
            this.Property(t => t.OfficialNumber).HasColumnName("OfficialNumber");
            this.Property(t => t.VesselBuildYear).HasColumnName("VesselBuildYear");
            this.Property(t => t.PortOfRegistry).HasColumnName("PortOfRegistry");
            this.Property(t => t.ExVesselName).HasColumnName("ExVesselName");
            this.Property(t => t.NoOfRowsOnDesk).HasColumnName("NoOfRowsOnDesk");
            this.Property(t => t.VesselNationality).HasColumnName("VesselNationality");
            this.Property(t => t.IsGovtVessel).HasColumnName("IsGovtVessel");
            this.Property(t => t.MMSINumber).HasColumnName("MMSINumber");
            this.Property(t => t.BeamInM).HasColumnName("BeamInM");
            this.Property(t => t.GrossRegisteredTonnageInMT).HasColumnName("GrossRegisteredTonnageInMT");
            this.Property(t => t.LengthOverallInM).HasColumnName("LengthOverallInM");
            this.Property(t => t.NetRegisteredTonnageInMT).HasColumnName("NetRegisteredTonnageInMT");
            this.Property(t => t.ParallelBodyLengthInM).HasColumnName("ParallelBodyLengthInM");
            this.Property(t => t.DeadWeightTonnageInMT).HasColumnName("DeadWeightTonnageInMT");
            this.Property(t => t.BowToManifoldDistanceInM).HasColumnName("BowToManifoldDistanceInM");
            this.Property(t => t.SummerDeadWeightInMT).HasColumnName("SummerDeadWeightInMT");
            this.Property(t => t.SummerDraftFWDInM).HasColumnName("SummerDraftFWDInM");
            this.Property(t => t.SummerDraftAFTInM).HasColumnName("SummerDraftAFTInM");
            this.Property(t => t.SummerDisplacementInMT).HasColumnName("SummerDisplacementInMT");
            this.Property(t => t.TEUCapacity).HasColumnName("TEUCapacity");
            this.Property(t => t.ReducedGRT).HasColumnName("ReducedGRT");
            this.Property(t => t.BowThruster).HasColumnName("BowThruster");
            this.Property(t => t.BowToForwardHatchDistanceM).HasColumnName("BowToForwardHatchDistanceM");
            this.Property(t => t.BowThrusterPowerKW).HasColumnName("BowThrusterPowerKW");
            this.Property(t => t.BowToBridgeFrontDistanceM).HasColumnName("BowToBridgeFrontDistanceM");
            this.Property(t => t.SternThrusterPowerKW).HasColumnName("SternThrusterPowerKW");
            //this.Property(t => t.WFStatus).HasColumnName("WFStatus");
            //this.Property(t => t.VerifiedBy).HasColumnName("VerifiedBy");
            //this.Property(t => t.VerifiedDate).HasColumnName("VerifiedDate");
            //this.Property(t => t.ApprovedBy).HasColumnName("ApprovedBy");
            //this.Property(t => t.ApprovedDate).HasColumnName("ApprovedDate");
            //this.Property(t => t.RejectComments).HasColumnName("RejectComments");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.WorkflowInstanceId).HasColumnName("WorkflowInstanceId");
            this.Property(t => t.IsFinal).HasColumnName("IsFinal");
            this.Property(t => t.SternThruster).HasColumnName("SternThruster");

            // Relationships
            this.HasOptional(t => t.SubCategory)
                .WithMany(t => t.Vessels)
                .HasForeignKey(d => d.ClassificationSociety);
            //this.HasRequired(t => t.SubCategory1)
            //    .WithMany(t => t.Vessels1)
            //    .HasForeignKey(d => d.PortOfRegistry);
            this.HasRequired(t => t.PortRegistry)
              .WithMany(t => t.Vessels)
              .HasForeignKey(d => d.PortOfRegistry);
            //this.HasOptional(t => t.Port)
            // .WithMany(t => t.Vessels)
            // .HasForeignKey(d => d.PortOfRegistry);
            this.HasOptional(t => t.SubCategory2)
                .WithMany(t => t.Vessels2)
                .HasForeignKey(d => d.VesselNationality);
            this.HasRequired(t => t.SubCategory3)
                .WithMany(t => t.Vessels3)
                .HasForeignKey(d => d.VesselType);
            this.HasRequired(t => t.User)
                .WithMany(t => t.Vessels)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.User1)
                .WithMany(t => t.Vessels1)
                .HasForeignKey(d => d.ModifiedBy);

            this.HasOptional(t => t.WorkflowInstances)
             .WithMany(t => t.Vessels)
             .HasForeignKey(d => d.WorkflowInstanceId);

        }
    }
}
