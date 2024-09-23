using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class Section625GMap : EntityTypeConfiguration<Section625G>
    {
        public Section625GMap()
        {
            // Primary Key
            this.HasKey(t => t.Section625GID);

            // Properties
            this.Property(t => t.WIWitnessName1)
                .HasMaxLength(200);

            this.Property(t => t.WIContactNo1)
                .HasMaxLength(15);

            this.Property(t => t.WIWitnessName2)
                .HasMaxLength(200);

            this.Property(t => t.WIContactNo2)
                .HasMaxLength(15);

            this.Property(t => t.IncidentDescription)
                .HasMaxLength(200);

            this.Property(t => t.IncidentExtent)
                .HasMaxLength(200);

            this.Property(t => t.QuantityVolumeMaterial)
                .HasMaxLength(200);

            this.Property(t => t.EstimateDistanceNearestWaterway)
                .HasMaxLength(200);

            this.Property(t => t.ActivityTypeIncident)
                .HasMaxLength(200);

            this.Property(t => t.IncidentIdentified)
                .HasMaxLength(200);

            this.Property(t => t.NameOfComplainant)
                .HasMaxLength(200);

            this.Property(t => t.ContactNoOfComplainant)
                .HasMaxLength(15);

            this.Property(t => t.LIMinorEnvironmentalIncident)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.LISignificantEnvironmentalIncident)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.LIMajorEnvironmentalIncident)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ImmediateReleventActionsTaken)
                .HasMaxLength(200);

            this.Property(t => t.EnvironmentalImpactDescription)
                .HasMaxLength(200);

            this.Property(t => t.ContributingFactorsCourses)
                .HasMaxLength(200);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.LikelyUnderlyingCauses)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Section625G");
            this.Property(t => t.Section625GID).HasColumnName("Section625GID");
            this.Property(t => t.Section625ABCDID).HasColumnName("Section625ABCDID");
            this.Property(t => t.IncidentDateTime).HasColumnName("IncidentDateTime");
            this.Property(t => t.TimeReported).HasColumnName("TimeReported");
            this.Property(t => t.WIWitnessName1).HasColumnName("WIWitnessName1");
            this.Property(t => t.WIContactNo1).HasColumnName("WIContactNo1");
            this.Property(t => t.WIWitnessName2).HasColumnName("WIWitnessName2");
            this.Property(t => t.WIContactNo2).HasColumnName("WIContactNo2");
            this.Property(t => t.IncidentDescription).HasColumnName("IncidentDescription");
            this.Property(t => t.IncidentExtent).HasColumnName("IncidentExtent");
            this.Property(t => t.QuantityVolumeMaterial).HasColumnName("QuantityVolumeMaterial");
            this.Property(t => t.EstimateDistanceNearestWaterway).HasColumnName("EstimateDistanceNearestWaterway");
            this.Property(t => t.ActivityTypeIncident).HasColumnName("ActivityTypeIncident");
            this.Property(t => t.IncidentIdentified).HasColumnName("IncidentIdentified");
            this.Property(t => t.NameOfComplainant).HasColumnName("NameOfComplainant");
            this.Property(t => t.ContactNoOfComplainant).HasColumnName("ContactNoOfComplainant");
            this.Property(t => t.LIMinorEnvironmentalIncident).HasColumnName("LIMinorEnvironmentalIncident");
            this.Property(t => t.LISignificantEnvironmentalIncident).HasColumnName("LISignificantEnvironmentalIncident");
            this.Property(t => t.LIMajorEnvironmentalIncident).HasColumnName("LIMajorEnvironmentalIncident");
            this.Property(t => t.ImmediateReleventActionsTaken).HasColumnName("ImmediateReleventActionsTaken");
            this.Property(t => t.EnvironmentalImpactDescription).HasColumnName("EnvironmentalImpactDescription");
            this.Property(t => t.ContributingFactorsCourses).HasColumnName("ContributingFactorsCourses");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.LikelyUnderlyingCauses).HasColumnName("LikelyUnderlyingCauses");
            this.Property(t => t.Hour24Report625ID).HasColumnName("Hour24Report625ID");

            // Relationships
            this.HasRequired(t => t.Hour24Report625)
                .WithMany(t => t.Section625G)
                .HasForeignKey(d => d.Hour24Report625ID);
            this.HasRequired(t => t.Section625ABCD)
                .WithMany(t => t.Section625G)
                .HasForeignKey(d => d.Section625ABCDID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.Section625G)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.Section625G1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
