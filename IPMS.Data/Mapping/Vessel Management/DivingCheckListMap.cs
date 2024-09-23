using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class DivingCheckListMap : EntityTypeConfiguration<DivingCheckList>
    {
        public DivingCheckListMap()
        {
            // Primary Key
            this.HasKey(t => t.DivingCheckListID);

            // Properties
            this.Property(t => t.DiveReferenceNo)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.DivingSupervisorName)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.WBPSDDEDiving)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.WBPSheetPileInspection)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.WBPLiftingOperations)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.WBPBouyInspection)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.WBPQuayWallInspection)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.WBPConcretePileInspection)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.WBPObjectRecovery)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.WBPDockyardInspection)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.WBPCordlessComsScuba)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.WBPCraftInspection)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.WBPHotWork)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.WBPOther)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.WBPOtherDescription)
                .HasMaxLength(2000);

            this.Property(t => t.PPEHardHot)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.PPEReflectiveVests)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.PPESafetyGlosses)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.PPEGloves)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.PPEOverall)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.PPESafetyShoes)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.PPELifeJacket)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.PPEOther)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.PPEOtherDescription)
                .HasMaxLength(2000);

            this.Property(t => t.EQPUsedCorrectly)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.EQPCompetentToUseEquipment)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.EQPGoodCondition)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.EQPInDate)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.EQPSecured)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.EQPSafetyDevicesInPlace)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.EQPDailyChecksCompleted)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.EQPOther)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.EQPOtherDescription)
                .HasMaxLength(2000);

            this.Property(t => t.PRADivePermit)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.PRALockOutProc)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.PRAFlogAlpha)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.PRACommunicationNetworkCompleted)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.PRAWorkPlaceTidy)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.PRORequired)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.PROSupplied)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.PRORiskAssessment)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.PROTaskKnownUnderstood)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.PROOnsiteHazardID)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("DivingCheckList");
            this.Property(t => t.DivingCheckListID).HasColumnName("DivingCheckListID");
            this.Property(t => t.DivingRequestID).HasColumnName("DivingRequestID");
            this.Property(t => t.DiveReferenceNo).HasColumnName("DiveReferenceNo");
            this.Property(t => t.DivingSupervisorName).HasColumnName("DivingSupervisorName");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.WBPSDDEDiving).HasColumnName("WBPSDDEDiving");
            this.Property(t => t.WBPSheetPileInspection).HasColumnName("WBPSheetPileInspection");
            this.Property(t => t.WBPLiftingOperations).HasColumnName("WBPLiftingOperations");
            this.Property(t => t.WBPBouyInspection).HasColumnName("WBPBouyInspection");
            this.Property(t => t.WBPQuayWallInspection).HasColumnName("WBPQuayWallInspection");
            this.Property(t => t.WBPConcretePileInspection).HasColumnName("WBPConcretePileInspection");
            this.Property(t => t.WBPObjectRecovery).HasColumnName("WBPObjectRecovery");
            this.Property(t => t.WBPDockyardInspection).HasColumnName("WBPDockyardInspection");
            this.Property(t => t.WBPCordlessComsScuba).HasColumnName("WBPCordlessComsScuba");
            this.Property(t => t.WBPCraftInspection).HasColumnName("WBPCraftInspection");
            this.Property(t => t.WBPHotWork).HasColumnName("WBPHotWork");
            this.Property(t => t.WBPOther).HasColumnName("WBPOther");
            this.Property(t => t.WBPOtherDescription).HasColumnName("WBPOtherDescription");
            this.Property(t => t.PPEHardHot).HasColumnName("PPEHardHot");
            this.Property(t => t.PPEReflectiveVests).HasColumnName("PPEReflectiveVests");
            this.Property(t => t.PPESafetyGlosses).HasColumnName("PPESafetyGlosses");
            this.Property(t => t.PPEGloves).HasColumnName("PPEGloves");
            this.Property(t => t.PPEOverall).HasColumnName("PPEOverall");
            this.Property(t => t.PPESafetyShoes).HasColumnName("PPESafetyShoes");
            this.Property(t => t.PPELifeJacket).HasColumnName("PPELifeJacket");
            this.Property(t => t.PPEOther).HasColumnName("PPEOther");
            this.Property(t => t.PPEOtherDescription).HasColumnName("PPEOtherDescription");
            this.Property(t => t.EQPUsedCorrectly).HasColumnName("EQPUsedCorrectly");
            this.Property(t => t.EQPCompetentToUseEquipment).HasColumnName("EQPCompetentToUseEquipment");
            this.Property(t => t.EQPGoodCondition).HasColumnName("EQPGoodCondition");
            this.Property(t => t.EQPInDate).HasColumnName("EQPInDate");
            this.Property(t => t.EQPSecured).HasColumnName("EQPSecured");
            this.Property(t => t.EQPSafetyDevicesInPlace).HasColumnName("EQPSafetyDevicesInPlace");
            this.Property(t => t.EQPDailyChecksCompleted).HasColumnName("EQPDailyChecksCompleted");
            this.Property(t => t.EQPOther).HasColumnName("EQPOther");
            this.Property(t => t.EQPOtherDescription).HasColumnName("EQPOtherDescription");
            this.Property(t => t.PRADivePermit).HasColumnName("PRADivePermit");
            this.Property(t => t.PRALockOutProc).HasColumnName("PRALockOutProc");
            this.Property(t => t.PRAFlogAlpha).HasColumnName("PRAFlogAlpha");
            this.Property(t => t.PRACommunicationNetworkCompleted).HasColumnName("PRACommunicationNetworkCompleted");
            this.Property(t => t.PRAWorkPlaceTidy).HasColumnName("PRAWorkPlaceTidy");
            this.Property(t => t.PRORequired).HasColumnName("PRORequired");
            this.Property(t => t.PROSupplied).HasColumnName("PROSupplied");
            this.Property(t => t.PRORiskAssessment).HasColumnName("PRORiskAssessment");
            this.Property(t => t.PROTaskKnownUnderstood).HasColumnName("PROTaskKnownUnderstood");
            this.Property(t => t.PROOnsiteHazardID).HasColumnName("PROOnsiteHazardID");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.DivingCheckLists)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.DivingRequest)
                .WithMany(t => t.DivingCheckLists)
                .HasForeignKey(d => d.DivingRequestID);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.DivingCheckLists1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
