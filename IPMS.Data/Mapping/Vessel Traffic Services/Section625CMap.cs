using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class Section625CMap : EntityTypeConfiguration<Section625C>
    {
        public Section625CMap()
        {
            // Primary Key
            this.HasKey(t => t.Section625CID);

            // Properties
            this.Property(t => t.IDIncidentSpecificLocation)
                .HasMaxLength(200);

            this.Property(t => t.WIWitnessName1)
                .HasMaxLength(200);

            this.Property(t => t.WITelephoneNo1)
                .HasMaxLength(15);

            this.Property(t => t.WIWitnessName2)
                .HasMaxLength(200);

            this.Property(t => t.WITelephoneNo2)
                .HasMaxLength(15);

            this.Property(t => t.PIName)
                .HasMaxLength(200);

            this.Property(t => t.PISurname)
                .HasMaxLength(200);

            this.Property(t => t.PIEmployeeNo)
                .HasMaxLength(20);

            this.Property(t => t.PIGender)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.PIGradePosition)
                .HasMaxLength(200);

            this.Property(t => t.PIPartOfBody)
                .HasMaxLength(200);

            this.Property(t => t.IncidentDescription)
                .HasMaxLength(200);

            this.Property(t => t.IIInvestigatorName)
                .HasMaxLength(200);

            this.Property(t => t.IIDesignation)
                .HasMaxLength(200);

            this.Property(t => t.GAOthersSpecify)
                .HasMaxLength(200);

            this.Property(t => t.GAOHAOthersSpecify)
                .HasMaxLength(200);

            this.Property(t => t.IDCOthersSpecify)
                .HasMaxLength(200);

            this.Property(t => t.CurrentControls)
                .HasMaxLength(500);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("Section625C");
            this.Property(t => t.Section625CID).HasColumnName("Section625CID");
            this.Property(t => t.Section625ABCDID).HasColumnName("Section625ABCDID");
            this.Property(t => t.IDIncidentDateTime).HasColumnName("IDIncidentDateTime");
            this.Property(t => t.IDTimeReported).HasColumnName("IDTimeReported");
            this.Property(t => t.IDIncidentSpecificLocation).HasColumnName("IDIncidentSpecificLocation");
            this.Property(t => t.WIWitnessName1).HasColumnName("WIWitnessName1");
            this.Property(t => t.WITelephoneNo1).HasColumnName("WITelephoneNo1");
            this.Property(t => t.WIWitnessName2).HasColumnName("WIWitnessName2");
            this.Property(t => t.WITelephoneNo2).HasColumnName("WITelephoneNo2");
            this.Property(t => t.PIName).HasColumnName("PIName");
            this.Property(t => t.PISurname).HasColumnName("PISurname");
            this.Property(t => t.PIEmployeeNo).HasColumnName("PIEmployeeNo");
            this.Property(t => t.PINoOfDaysLost).HasColumnName("PINoOfDaysLost");
            this.Property(t => t.PIGender).HasColumnName("PIGender");
            this.Property(t => t.PIAge).HasColumnName("PIAge");
            this.Property(t => t.PIGradePosition).HasColumnName("PIGradePosition");
            this.Property(t => t.PIPartOfBody).HasColumnName("PIPartOfBody");
            this.Property(t => t.IncidentDescription).HasColumnName("IncidentDescription");
            this.Property(t => t.IIInvestigatorName).HasColumnName("IIInvestigatorName");
            this.Property(t => t.IIDesignation).HasColumnName("IIDesignation");
            this.Property(t => t.IIInvestigationDate).HasColumnName("IIInvestigationDate");
            this.Property(t => t.GAOthersSpecify).HasColumnName("GAOthersSpecify");
            this.Property(t => t.GAOHAOthersSpecify).HasColumnName("GAOHAOthersSpecify");
            this.Property(t => t.IDCOthersSpecify).HasColumnName("IDCOthersSpecify");
            this.Property(t => t.CurrentControls).HasColumnName("CurrentControls");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.Hour24Report625ID).HasColumnName("Hour24Report625ID");

            // Relationships
            this.HasRequired(t => t.Hour24Report625)
                .WithMany(t => t.Section625C)
                .HasForeignKey(d => d.Hour24Report625ID);
            this.HasRequired(t => t.Section625ABCD)
                .WithMany(t => t.Section625C)
                .HasForeignKey(d => d.Section625ABCDID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.Section625C)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.Section625C1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
