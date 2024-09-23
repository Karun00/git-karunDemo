using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class PersonalPermitMap : EntityTypeConfiguration<PersonalPermit>
    {
        public PersonalPermitMap()
        {
            // Primary Key
            this.HasKey(t => t.PersonalPermitID);

            // Properties
            this.Property(t => t.PermitCategoryCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.AllNPASites)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SpecificNPASites)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SpecifyArea)
                .HasMaxLength(200);

            this.Property(t => t.LeaseholdSite)
                .HasMaxLength(200);

            this.Property(t => t.PhysicalAddress)
                .HasMaxLength(500);

            this.Property(t => t.AdhocPermits)
                .HasMaxLength(4);

            this.Property(t => t.TemporaryPermits)
                .HasMaxLength(4);

            this.Property(t => t.AllPorts)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ConstructionArea)
                .HasMaxLength(4);

            this.Property(t => t.PermanentPermits)
                .IsFixedLength()
                .HasMaxLength(1);

            //this.Property(t => t.Reason)
            //    .HasMaxLength(200);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);
            this.Property(t => t.permittype)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("PersonalPermit");
            this.Property(t => t.PersonalPermitID).HasColumnName("PersonalPermitID");
            this.Property(t => t.PermitRequestID).HasColumnName("PermitRequestID");
            this.Property(t => t.PermitCategoryCode).HasColumnName("PermitCategoryCode");
            this.Property(t => t.AllNPASites).HasColumnName("AllNPASites");
            this.Property(t => t.SpecificNPASites).HasColumnName("SpecificNPASites");
            this.Property(t => t.SpecifyArea).HasColumnName("SpecifyArea");
            this.Property(t => t.LeaseholdSite).HasColumnName("LeaseholdSite");
            this.Property(t => t.PhysicalAddress).HasColumnName("PhysicalAddress");
            this.Property(t => t.AdhocPermits).HasColumnName("AdhocPermits");
            this.Property(t => t.TemporaryPermits).HasColumnName("TemporaryPermits");
            this.Property(t => t.AllPorts).HasColumnName("AllPorts");
            this.Property(t => t.ConstructionArea).HasColumnName("ConstructionArea");
            this.Property(t => t.PermanentPermits).HasColumnName("PermanentPermits");
            this.Property(t => t.Reason).HasColumnName("Reason");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.permittype).HasColumnName("permittype");
           

           




            // Relationships
            this.HasRequired(t => t.PermitRequest)
                .WithMany(t => t.PersonalPermits)
                .HasForeignKey(d => d.PermitRequestID);
            this.HasOptional(t => t.SubCategory)
                .WithMany(t => t.PersonalPermits)
                .HasForeignKey(d => d.AdhocPermits);

            this.HasOptional(t => t.SubCategory1)
                .WithMany(t => t.PersonalPermits1)
                .HasForeignKey(d => d.ConstructionArea);

            this.HasRequired(t => t.User)
                .WithMany(t => t.PersonalPermits)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.PersonalPermits1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.SubCategory2)
                .WithMany(t => t.PersonalPermits2)
                .HasForeignKey(d => d.PermitCategoryCode);
            this.HasOptional(t => t.SubCategory3)
                .WithMany(t => t.PersonalPermits3)
                .HasForeignKey(d => d.TemporaryPermits);

        }
    }
}
