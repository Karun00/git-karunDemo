using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public  class SuppDryDockExtensionMap : EntityTypeConfiguration<SuppDryDockExtension>
    {
        public SuppDryDockExtensionMap() 
        {
            // Primary Key
            this.HasKey(t => t.SuppDryDockExtensionID);

            // Properties
            this.Property(t => t.ScheduleStatus)
               .HasMaxLength(4);

            this.Property(t => t.TermsandConditions)
               .IsRequired()
               .IsFixedLength()
               .HasMaxLength(1);

            this.Property(t => t.Remarks)
                .HasMaxLength(200);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);


            // Table & Column Mappings
            this.ToTable("SuppDryDockExtension");
            this.Property(t => t.SuppDryDockExtensionID).HasColumnName("SuppDryDockExtensionID");
            this.Property(t => t.SuppDryDockID).HasColumnName("SuppDryDockID");
            this.Property(t => t.ScheduleFromDate).HasColumnName("ScheduleFromDate");
            this.Property(t => t.ScheduleToDate).HasColumnName("ScheduleToDate");
            this.Property(t => t.ScheduleStatus).HasColumnName("ScheduleStatus");
            this.Property(t => t.ExtensionDateTime).HasColumnName("ExtensionDateTime");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.TermsandConditions).HasColumnName("TermsandConditions");
            this.Property(t => t.WorkflowInstanceID).HasColumnName("WorkflowInstanceID");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");


           
            // Relationships
            // Relationships
            this.HasRequired(t => t.SuppDryDock)
                .WithMany(t => t.SuppDryDockExtensions)
                .HasForeignKey(d => d.SuppDryDockID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.SuppDryDockExtensions)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.SuppDryDockExtension1)
                .HasForeignKey(d => d.ModifiedBy);
            
        }
    }
}
