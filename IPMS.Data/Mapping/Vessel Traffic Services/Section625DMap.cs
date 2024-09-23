using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class Section625DMap : EntityTypeConfiguration<Section625D>
    {
        public Section625DMap()
        {
            // Primary Key
            this.HasKey(t => t.Section625DID);

            // Properties
            this.Property(t => t.SpecifyLocationOfFire)
                .HasMaxLength(200);

            this.Property(t => t.FireDepartmentAttend)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.OthersSpecify)
                .HasMaxLength(200);

            this.Property(t => t.FICommercial)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.FIStorage)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.FIIndustry)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.FITransport)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.FIOthers)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.FIMiscillaniousSpecify)
                .HasMaxLength(200);

            this.Property(t => t.ICOthersSpecify)
                .HasMaxLength(200);

            this.Property(t => t.DEROthersSpecify)
                .HasMaxLength(200);

            this.Property(t => t.APDDamage)
                .HasMaxLength(500);

            this.Property(t => t.MEByWhom)
                .HasMaxLength(200);

            this.Property(t => t.MEWithWhatBeforeFire)
                .HasMaxLength(200);

            this.Property(t => t.MEWithWhatAfterFire)
                .HasMaxLength(200);

            this.Property(t => t.FurtherInformation)
                .HasMaxLength(200);

            this.Property(t => t.WCWeather)
                .HasMaxLength(200);

            this.Property(t => t.WCTemperature)
                .HasMaxLength(200);

            this.Property(t => t.WCWindSpeed)
                .HasMaxLength(200);

            this.Property(t => t.WCWindDirection)
                .HasMaxLength(200);

            this.Property(t => t.Remarks)
                .HasMaxLength(500);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("Section625D");
            this.Property(t => t.Section625DID).HasColumnName("Section625DID");
            this.Property(t => t.Section625ABCDID).HasColumnName("Section625ABCDID");
            this.Property(t => t.IncidentDateTime).HasColumnName("IncidentDateTime");
            this.Property(t => t.TimeReported).HasColumnName("TimeReported");
            this.Property(t => t.SpecifyLocationOfFire).HasColumnName("SpecifyLocationOfFire");
            this.Property(t => t.FireDepartmentAttend).HasColumnName("FireDepartmentAttend");
            this.Property(t => t.OthersSpecify).HasColumnName("OthersSpecify");
            this.Property(t => t.FICommercial).HasColumnName("FICommercial");
            this.Property(t => t.FIStorage).HasColumnName("FIStorage");
            this.Property(t => t.FIIndustry).HasColumnName("FIIndustry");
            this.Property(t => t.FITransport).HasColumnName("FITransport");
            this.Property(t => t.FIOthers).HasColumnName("FIOthers");
            this.Property(t => t.FIMiscillaniousSpecify).HasColumnName("FIMiscillaniousSpecify");
            this.Property(t => t.ICOthersSpecify).HasColumnName("ICOthersSpecify");
            this.Property(t => t.DEROthersSpecify).HasColumnName("DEROthersSpecify");
            this.Property(t => t.APDDamage).HasColumnName("APDDamage");
            this.Property(t => t.APDMaximumEstimatedFinancialLoss).HasColumnName("APDMaximumEstimatedFinancialLoss");
            this.Property(t => t.APDActualLoss).HasColumnName("APDActualLoss");
            this.Property(t => t.MEByWhom).HasColumnName("MEByWhom");
            this.Property(t => t.MEWithWhatBeforeFire).HasColumnName("MEWithWhatBeforeFire");
            this.Property(t => t.MEWithWhatAfterFire).HasColumnName("MEWithWhatAfterFire");
            this.Property(t => t.FurtherInformation).HasColumnName("FurtherInformation");
            this.Property(t => t.WCWeather).HasColumnName("WCWeather");
            this.Property(t => t.WCTemperature).HasColumnName("WCTemperature");
            this.Property(t => t.WCWindSpeed).HasColumnName("WCWindSpeed");
            this.Property(t => t.WCWindDirection).HasColumnName("WCWindDirection");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.Hour24Report625ID).HasColumnName("Hour24Report625ID");

            // Relationships
            this.HasRequired(t => t.Hour24Report625)
                .WithMany(t => t.Section625D)
                .HasForeignKey(d => d.Hour24Report625ID);
            this.HasRequired(t => t.Section625ABCD)
                .WithMany(t => t.Section625D)
                .HasForeignKey(d => d.Section625ABCDID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.Section625D)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.Section625D1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
