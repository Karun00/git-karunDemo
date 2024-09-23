using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class SuppMiscServiceMap : EntityTypeConfiguration<SuppMiscService>
    {
        public SuppMiscServiceMap()
        {
            // Primary Key
            this.HasKey(t => t.SuppMiscServiceID);

            // Properties
            this.Property(t => t.Phase)              
                .HasMaxLength(4);

           

            this.Property(t => t.Remarks)
                .HasMaxLength(200);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ServiceTypeCode)
               .IsRequired()
               .HasMaxLength(4);


            // Table & Column Mappings
            this.ToTable("SuppMiscService");
            this.Property(t => t.SuppMiscServiceID).HasColumnName("SuppMiscServiceID");          
            this.Property(t => t.Phase).HasColumnName("Phase");
            this.Property(t => t.FromDateTime).HasColumnName("FromDateTime");
            this.Property(t => t.ToDateTime).HasColumnName("ToDateTime");
            this.Property(t => t.Quantity).HasColumnName("Quantity");        
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.SuppDryDockID).HasColumnName("SuppDryDockID");
            this.Property(t => t.ServiceTypeCode).HasColumnName("ServiceTypeCode");
            this.Property(t => t.ServiceTypeID).HasColumnName("ServiceTypeID");
            this.Property(t => t.StartMeterReading).HasColumnName("StartMeter");//added by divya on 30Oct
            this.Property(t => t.EndMeterReading).HasColumnName("EndMeter");//added by divya on 30Oct

            // Relationships
            this.HasOptional(t => t.SubCategory)
                   .WithMany(t => t.SuppMiscServices)
                   .HasForeignKey(d => d.Phase);
          
            this.HasRequired(t => t.ServiceType)
               .WithMany(t => t.SuppMiscServices)
               .HasForeignKey(d => d.ServiceTypeID);
            this.HasRequired(t => t.SuppDryDock)
               .WithMany(t => t.SuppMiscServices)
               .HasForeignKey(d => d.SuppDryDockID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.SuppMiscServices)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.SuppMiscServices1)
                .HasForeignKey(d => d.ModifiedBy);



       
         

        }
    }
}
