using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class Section625EMap : EntityTypeConfiguration<Section625E>
    {
        public Section625EMap()
        {
            // Primary Key
            this.HasKey(t => t.Section625EID);

            // Properties
            this.Property(t => t.OwnerNameofStolenItem)
                .HasMaxLength(200);

            this.Property(t => t.OwnerAddress)
                .HasMaxLength(500);

            this.Property(t => t.TelephoneNo)
                .HasMaxLength(15);

            this.Property(t => t.MobileNo)
                .HasMaxLength(15);

            this.Property(t => t.EmailID)
                .HasMaxLength(20);

            this.Property(t => t.IDWhenandWhereStolenLocation)
                .HasMaxLength(200);

            this.Property(t => t.IDWhenWasDiscoveredLocation)
                .HasMaxLength(200);

            this.Property(t => t.TheftOccur)
                .HasMaxLength(200);

            this.Property(t => t.StolenFromBuilding)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ISPSBreach)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ProtectTheft)
                .HasMaxLength(200);

            this.Property(t => t.Circumstances)
                .HasMaxLength(200);

            this.Property(t => t.TheftAvoided)
                .HasMaxLength(200);

            this.Property(t => t.PoliceAdviced)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SAPSOBNumber)
                .HasMaxLength(15);

            this.Property(t => t.PoliceStationReportedTo)
                .HasMaxLength(200);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("Section625E");
            this.Property(t => t.Section625EID).HasColumnName("Section625EID");
            this.Property(t => t.Section625ABCDID).HasColumnName("Section625ABCDID");
            this.Property(t => t.IncidentDateTime).HasColumnName("IncidentDateTime");
            this.Property(t => t.TimeReported).HasColumnName("TimeReported");
            this.Property(t => t.OwnerNameofStolenItem).HasColumnName("OwnerNameofStolenItem");
            this.Property(t => t.OwnerAddress).HasColumnName("OwnerAddress");
            this.Property(t => t.TelephoneNo).HasColumnName("TelephoneNo");
            this.Property(t => t.MobileNo).HasColumnName("MobileNo");
            this.Property(t => t.EmailID).HasColumnName("EmailID");
            this.Property(t => t.IDWhenandWhereStolenDateTime).HasColumnName("IDWhenandWhereStolenDateTime");
            this.Property(t => t.IDWhenandWhereStolenLocation).HasColumnName("IDWhenandWhereStolenLocation");
            this.Property(t => t.IDWhenWasDiscoveredDateTime).HasColumnName("IDWhenWasDiscoveredDateTime");
            this.Property(t => t.IDWhenWasDiscoveredLocation).HasColumnName("IDWhenWasDiscoveredLocation");
            this.Property(t => t.TheftOccur).HasColumnName("TheftOccur");
            this.Property(t => t.StolenFromBuilding).HasColumnName("StolenFromBuilding");
            this.Property(t => t.ISPSBreach).HasColumnName("ISPSBreach");
            this.Property(t => t.ProtectTheft).HasColumnName("ProtectTheft");
            this.Property(t => t.Circumstances).HasColumnName("Circumstances");
            this.Property(t => t.TheftAvoided).HasColumnName("TheftAvoided");
            this.Property(t => t.PoliceAdviced).HasColumnName("PoliceAdviced");
            this.Property(t => t.SAPSOBNumber).HasColumnName("SAPSOBNumber");
            this.Property(t => t.PoliceStationReportedTo).HasColumnName("PoliceStationReportedTo");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.Hour24Report625ID).HasColumnName("Hour24Report625ID");

            // Relationships
            this.HasRequired(t => t.Hour24Report625)
                .WithMany(t => t.Section625E)
                .HasForeignKey(d => d.Hour24Report625ID);
            this.HasRequired(t => t.Section625ABCD)
                .WithMany(t => t.Section625E)
                .HasForeignKey(d => d.Section625ABCDID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.Section625E)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.Section625E1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
