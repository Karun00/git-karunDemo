using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class StatementFactMap : EntityTypeConfiguration<StatementFact>
    {
        public StatementFactMap()
        {
            // Primary Key
            this.HasKey(t => t.StatementFactID);

            // Properties
            this.Property(t => t.VCN)
                .IsRequired()
                .HasMaxLength(12);

            this.Property(t => t.OperationCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.MasterName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("StatementFact");
            this.Property(t => t.StatementFactID).HasColumnName("StatementFactID");
            this.Property(t => t.VCN).HasColumnName("VCN");
            this.Property(t => t.OperationCode).HasColumnName("OperationCode");
            this.Property(t => t.MasterName).HasColumnName("MasterName");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.ArrivalFuel).HasColumnName("ArrivalFuel");
            this.Property(t => t.ArrivalDiesel).HasColumnName("ArrivalDiesel");
            this.Property(t => t.SailingFuel).HasColumnName("SailingFuel");
            this.Property(t => t.SailingDiesel).HasColumnName("SailingDiesel");
            this.Property(t => t.EOSPDateTime).HasColumnName("EOSPDateTime");
            this.Property(t => t.GangwayDown).HasColumnName("GangwayDown");
            this.Property(t => t.NORTendered).HasColumnName("NORTendered");
            this.Property(t => t.NORAccepted).HasColumnName("NORAccepted");
            this.Property(t => t.StevedoreOnBoard).HasColumnName("StevedoreOnBoard");
            this.Property(t => t.StevedoreStart).HasColumnName("StevedoreStart");
            this.Property(t => t.StevedoreEnd).HasColumnName("StevedoreEnd");
            this.Property(t => t.StevedoreOff).HasColumnName("StevedoreOff");
            this.Property(t => t.CranesDeployed).HasColumnName("CranesDeployed");
            this.Property(t => t.StartCargo).HasColumnName("StartCargo");
            this.Property(t => t.EndCargo).HasColumnName("EndCargo");

            // Relationships
            this.HasRequired(t => t.ArrivalNotification)
                 .WithMany(t => t.StatementFacts)
                 .HasForeignKey(d => d.VCN);
            this.HasRequired(t => t.User)
                .WithMany(t => t.StatementFacts)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.StatementFacts1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.SubCategory)
                .WithMany(t => t.StatementFacts)
                .HasForeignKey(d => d.OperationCode);

        }
    }
}
