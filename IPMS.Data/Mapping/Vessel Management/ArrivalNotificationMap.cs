using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class ArrivalNotificationMap : EntityTypeConfiguration<ArrivalNotification>
    {
        public ArrivalNotificationMap()
        {
            // Primary Key
            this.HasKey(t => t.VCN);

            // Properties
            this.Property(t => t.VCN)
                .IsRequired()
                .HasMaxLength(12);

            this.Property(t => t.PortCode)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.VoyageIn)
                .HasMaxLength(50);

            this.Property(t => t.VoyageOut)
                .HasMaxLength(50);

            this.Property(t => t.ArrDraft)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.DepDraft)
                .IsRequired()
                .HasMaxLength(15);       

            this.Property(t => t.ReasonForVisit)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.IsTerminalOperator)
                .IsFixedLength()
                .HasMaxLength(1);


            this.Property(t => t.LastPortOfCall)
                .IsRequired()
                .HasMaxLength(5);


            this.Property(t => t.NextPortOfCall)
                .IsRequired()
                .HasMaxLength(5);


            this.Property(t => t.AppliedForISPS)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.Clearance)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ISPSReferenceNo)
                .HasMaxLength(20);

            this.Property(t => t.PilotExemption)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.PreferredPortCode)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.PreferredQuayCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.PreferredBerthCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.AlternatePortCode)
                .HasMaxLength(2);

            this.Property(t => t.AlternateQuayCode)
                .HasMaxLength(4);

            this.Property(t => t.AlternateBerthCode)
                .HasMaxLength(4);

            // New
            this.Property(t => t.DryDockBerthPortCode)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.DryDockBerthQuayCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.DryDockBerthCode)
                .IsRequired()
                .HasMaxLength(4);




            this.Property(t => t.PreferredSideDock)
                .IsRequired()
                .HasMaxLength(4);



            this.Property(t => t.BunkersRequired)
                  .HasMaxLength(4);

            this.Property(t => t.BunkersMethod)
                  .HasMaxLength(4);


            this.Property(t => t.PreferredSideAlternateBirth)
                .HasMaxLength(4);

            this.Property(t => t.ReasonAlternateBirth)
                .HasMaxLength(100);


            this.Property(t => t.SpecifyReason)
                .HasMaxLength(200);


            this.Property(t => t.Tidal)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.BallastWater)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.WasteDeclaration)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.DaylightRestriction)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.DaylightSpecifyReason)
                .HasMaxLength(200);

            this.Property(t => t.CancelRemarks)
           .HasMaxLength(200);

            this.Property(t => t.ExceedPortLimitations)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ExceedSpecifyReason)
                .HasMaxLength(200);

            this.Property(t => t.AnyAdditionalInfo)
                .HasMaxLength(200);

            this.Property(t => t.AnyDangerousGoodsonBoard)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.AnyDangerousGoodsonBoard)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);


            this.Property(t => t.GeneratedVCN)
                .IsFixedLength()
                .HasMaxLength(12);

            this.Property(t => t.Isdraft)
                .HasMaxLength(1);

            this.Property(t => t.UNNo)
                .HasMaxLength(15);

            this.Property(t => t.CellNo)
                .HasMaxLength(15);

            this.Property(t => t.CargoDescription)
                .HasMaxLength(500);

            this.Property(t => t.ReasonForLayup)
                .HasMaxLength(200);   


            this.Property(t => t.AnyImpInfo)
                .HasMaxLength(200);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.IsANFinal)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed)
            .HasMaxLength(2);
            this.Property(t => t.IsISPSANFinal)
                  .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed)
             .HasMaxLength(2);
            this.Property(t => t.IsPHANFinal)
                  .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed)
             .HasMaxLength(2);
            this.Property(t => t.IsIMDGANFinal)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed)
             .HasMaxLength(2);

            this.Property(t => t.IsSpecialNature)
                         .IsFixedLength()
                         .HasMaxLength(1);

            this.Property(t => t.SpecialNatureReason)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("ArrivalNotification");
            this.Property(t => t.VCN).HasColumnName("VCN");
            this.Property(t => t.PortCode).HasColumnName("PortCode");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.VesselID).HasColumnName("VesselID");
            this.Property(t => t.VoyageIn).HasColumnName("VoyageIn");
            this.Property(t => t.VoyageOut).HasColumnName("VoyageOut");
            this.Property(t => t.ETA).HasColumnName("ETA");
            this.Property(t => t.ETD).HasColumnName("ETD");
            this.Property(t => t.ArrDraft).HasColumnName("ArrDraft");
            this.Property(t => t.DepDraft).HasColumnName("DepDraft");            
            this.Property(t => t.ReasonForVisit).HasColumnName("ReasonForVisit");
            this.Property(t => t.IsTerminalOperator).HasColumnName("IsTerminalOperator");
            this.Property(t => t.TerminalOperatorID).HasColumnName("TerminalOperatorID");
            this.Property(t => t.LastPortOfCall).HasColumnName("LastPortOfCall");
            this.Property(t => t.NextPortOfCall).HasColumnName("NextPortOfCall");
            this.Property(t => t.NominationDate).HasColumnName("NominationDate");
            this.Property(t => t.AppliedForISPS).HasColumnName("AppliedForISPS");
            this.Property(t => t.AppliedDate).HasColumnName("AppliedDate");
            this.Property(t => t.Clearance).HasColumnName("Clearance");
            this.Property(t => t.ISPSReferenceNo).HasColumnName("ISPSReferenceNo");
            this.Property(t => t.PilotExemption).HasColumnName("PilotExemption");
            this.Property(t => t.ExemptionPilotID).HasColumnName("ExemptionPilotID");
            this.Property(t => t.PreferredPortCode).HasColumnName("PreferredPortCode");
            this.Property(t => t.PreferredQuayCode).HasColumnName("PreferredQuayCode");
            this.Property(t => t.PreferredBerthCode).HasColumnName("PreferredBerthCode");
            this.Property(t => t.AlternatePortCode).HasColumnName("AlternatePortCode");
            this.Property(t => t.AlternateQuayCode).HasColumnName("AlternateQuayCode");
            this.Property(t => t.AlternateBerthCode).HasColumnName("AlternateBerthCode");

            // New 
            this.Property(t => t.DryDockBerthPortCode).HasColumnName("DryDockBerthPortCode");
            this.Property(t => t.DryDockBerthQuayCode).HasColumnName("DryDockBerthQuayCode");
            this.Property(t => t.DryDockBerthCode).HasColumnName("DryDockBerthCode");


            this.Property(t => t.PreferredSideDock).HasColumnName("PreferredSideDock");
            this.Property(t => t.PreferredSideAlternateBirth).HasColumnName("PreferredSideAlternateBirth");
            this.Property(t => t.ReasonAlternateBirth).HasColumnName("ReasonAlternateBirth");
            this.Property(t => t.SpecifyReason).HasColumnName("SpecifyReason");


            this.Property(t => t.Tidal).HasColumnName("Tidal");
            this.Property(t => t.BallastWater).HasColumnName("BallastWater");
            this.Property(t => t.WasteDeclaration).HasColumnName("WasteDeclaration");
            this.Property(t => t.DaylightRestriction).HasColumnName("DaylightRestriction");
            this.Property(t => t.DaylightSpecifyReason).HasColumnName("DaylightSpecifyReason");
            this.Property(t => t.CancelRemarks).HasColumnName("CancelRemarks");
            this.Property(t => t.ExceedPortLimitations).HasColumnName("ExceedPortLimitations");
            this.Property(t => t.ExceedSpecifyReason).HasColumnName("ExceedSpecifyReason");
            this.Property(t => t.AnyAdditionalInfo).HasColumnName("AnyAdditionalInfo");
            this.Property(t => t.PlanDateTimeOfBerth).HasColumnName("PlanDateTimeOfBerth");
            this.Property(t => t.PlanDateTimeToVacateBerth).HasColumnName("PlanDateTimeToVacateBerth");
            this.Property(t => t.PlanDateTimeToStartCargo).HasColumnName("PlanDateTimeToStartCargo");
            this.Property(t => t.PlanDateTimeToCompleteCargo).HasColumnName("PlanDateTimeToCompleteCargo");
            this.Property(t => t.AnyDangerousGoodsonBoard).HasColumnName("AnyDangerousGoodsonBoard");
            this.Property(t => t.DangerousGoodsClass).HasColumnName("DangerousGoodsClass");
            this.Property(t => t.UNNo).HasColumnName("UNNo");
            this.Property(t => t.LoadDischargeDate).HasColumnName("LoadDischargeDate");
            this.Property(t => t.DischargeDate).HasColumnName("DischargeDate");
            this.Property(t => t.IMDGNetQty).HasColumnName("IMDGNetQty");
            this.Property(t => t.CellNo).HasColumnName("CellNo");
            this.Property(t => t.Isdraft).HasColumnName("Isdraft");
            this.Property(t => t.GeneratedVCN).HasColumnName("GeneratedVCN");
            this.Property(t => t.CargoDescription).HasColumnName("CargoDescription");
            this.Property(t => t.PlannedDurationDate).HasColumnName("PlannedDurationDate");
            this.Property(t => t.PlannedDurationToDate).HasColumnName("PlannedDurationToDate");
            this.Property(t => t.ReasonForLayup).HasColumnName("ReasonForLayup");
            this.Property(t => t.BunkersRequired).HasColumnName("BunkersRequired");
            this.Property(t => t.BunkersMethod).HasColumnName("BunkersMethod");
            this.Property(t => t.BunkerService).HasColumnName("BunkerService");
            this.Property(t => t.DistanceFromStern).HasColumnName("DistanceFromStern");
            this.Property(t => t.TonsMT).HasColumnName("TonsMT");
            this.Property(t => t.AnyImpInfo).HasColumnName("AnyImpInfo");
            this.Property(t => t.WorkflowInstanceId).HasColumnName("WorkflowInstanceId");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.IsANFinal).HasColumnName("IsANFinal");
            this.Property(t => t.IsISPSANFinal).HasColumnName("IsISPSANFinal");
            this.Property(t => t.IsPHANFinal).HasColumnName("IsPHANFinal");
            this.Property(t => t.IsIMDGANFinal).HasColumnName("IsIMDGANFinal");

            this.Property(t => t.LastPortWasteDelivered).HasColumnName("LastPortWasteDelivered");
            this.Property(t => t.NextPortWasteDelivery).HasColumnName("NextPortWasteDelivery");
            this.Property(t => t.DateLastWasteDelivered).HasColumnName("DateLastWasteDelivered");           

            // Relationships
            this.HasRequired(t => t.Agent)
                .WithMany(t => t.ArrivalNotifications)
                .HasForeignKey(d => d.AgentID);

            this.HasOptional(t => t.LicenseRequest)
               .WithMany(t => t.ArrivalNotifications)
               .HasForeignKey(d => d.BunkerService);


            this.HasOptional(t => t.Berth)
                .WithMany(t => t.ArrivalNotifications)
                .HasForeignKey(d => new { d.AlternatePortCode, d.AlternateQuayCode, d.AlternateBerthCode });

            this.HasRequired(t => t.Berth2)
               .WithMany(t => t.ArrivalNotifications2)
               .HasForeignKey(d => new { d.DryDockBerthPortCode, d.DryDockBerthQuayCode, d.DryDockBerthCode });








            this.HasRequired(t => t.User)
                .WithMany(t => t.ArrivalNotifications)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.Pilot)
                .WithMany(t => t.ArrivalNotifications)
                .HasForeignKey(d => d.ExemptionPilotID);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.ArrivalNotifications1)
                .HasForeignKey(d => d.ModifiedBy);


            this.HasRequired(t => t.Port)
                .WithMany(t => t.ArrivalNotifications)
                .HasForeignKey(d => d.PortCode);

            this.HasRequired(t => t.LastPort)
                 .WithMany(t => t.LastArrivalNotifications)
                 .HasForeignKey(d => d.LastPortOfCall);
            this.HasRequired(t => t.NextPort)
                 .WithMany(t => t.NextArrivalNotifications)
                 .HasForeignKey(d => d.NextPortOfCall);

            this.HasRequired(t => t.Berth1)
                .WithMany(t => t.ArrivalNotifications1)
                .HasForeignKey(d => new { d.PreferredPortCode, d.PreferredQuayCode, d.PreferredBerthCode });
            this.HasRequired(t => t.SubCategory1)
                .WithMany(t => t.ArrivalNotifications1)
                .HasForeignKey(d => d.PreferredSideAlternateBirth);
            this.HasRequired(t => t.SubCategory2)
                .WithMany(t => t.ArrivalNotifications2)
                .HasForeignKey(d => d.PreferredSideDock);

            this.HasRequired(t => t.SubCategory12)
               .WithMany(t => t.ArrivalNotifications12)
               .HasForeignKey(d => d.BunkersRequired);
            this.HasRequired(t => t.SubCategory13)
               .WithMany(t => t.ArrivalNotifications13)
               .HasForeignKey(d => d.BunkersMethod);

            this.HasRequired(t => t.SubCategory3)
                .WithMany(t => t.ArrivalNotifications3)
                .HasForeignKey(d => d.ReasonForVisit);
            this.HasOptional(t => t.TerminalOperator)
                .WithMany(t => t.ArrivalNotifications)
                .HasForeignKey(d => d.TerminalOperatorID);
            this.HasRequired(t => t.Vessel)
                .WithMany(t => t.ArrivalNotifications)
                .HasForeignKey(d => d.VesselID);
            this.HasOptional(t => t.WorkflowInstance)
                .WithMany(t => t.ArrivalNotifications)
                .HasForeignKey(d => d.WorkflowInstanceId);


            this.HasRequired(t => t.LastPortWasteDeliveredPort)
                 .WithMany(t => t.LastWasteDeclarationPorts)
                 .HasForeignKey(d => d.LastPortWasteDelivered);
            this.HasRequired(t => t.NextPortWasteDeliveryPort)
                 .WithMany(t => t.NextWasteDeclarationPorts)
                 .HasForeignKey(d => d.NextPortWasteDelivery);

        }
    }
}
