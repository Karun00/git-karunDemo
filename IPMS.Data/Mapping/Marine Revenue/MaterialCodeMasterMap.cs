using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class MaterialCodeMasterMap : EntityTypeConfiguration<MaterialCodeMaster>
    {
        public MaterialCodeMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.MaterialCodeMasterid);

            // Properties
            this.Property(t => t.GroupCode)
                .HasMaxLength(10);

            this.Property(t => t.MaterialCode)
                .HasMaxLength(10);

            this.Property(t => t.MovementType)
                .HasMaxLength(4);

            this.Property(t => t.ServiceType)
                .HasMaxLength(4);

            this.Property(t => t.MaterialDescription)
                .HasMaxLength(100);

            this.Property(t => t.UOM)
                .HasMaxLength(10);

            this.Property(t => t.IsCalculated)
              .HasMaxLength(1);

            this.Property(t => t.Chargedas)
              .HasMaxLength(50);

            this.Property(t => t.Remarks)
                .HasMaxLength(200);

            this.Property(t => t.RecordStatus)
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("MaterialCodeMaster");
            this.Property(t => t.MaterialCodeMasterid).HasColumnName("MaterialCodeMasterid");
            this.Property(t => t.GroupCode).HasColumnName("GroupCode");
            this.Property(t => t.MaterialCode).HasColumnName("MaterialCode");
            this.Property(t => t.MovementType).HasColumnName("MovementType");
            this.Property(t => t.ServiceType).HasColumnName("ServiceType");
            this.Property(t => t.MaterialDescription).HasColumnName("MaterialDescription");
            this.Property(t => t.UOM).HasColumnName("UOM");
            this.Property(t => t.IsCalculated).HasColumnName("IsCalculated");
            this.Property(t => t.Chargedas).HasColumnName("Chargedas");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
        }
    }
}
