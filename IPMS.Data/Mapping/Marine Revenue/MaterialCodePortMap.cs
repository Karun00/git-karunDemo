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
    public class MaterialCodePortMap : EntityTypeConfiguration<MaterialCodePort>
    {
        public MaterialCodePortMap()
        {
            // Primary Key
            this.HasKey(t => t.MaterialCodePortID);

            // Properties
            this.Property(t => t.PortCode)
                .HasMaxLength(2);

            // Table & Column Mappings
            this.ToTable("MaterialCodePort");
            this.Property(t => t.MaterialCodePortID).HasColumnName("MaterialCodePortID");
            this.Property(t => t.MaterialCodeMasterid).HasColumnName("MaterialCodeMasterid");
            this.Property(t => t.PortCode).HasColumnName("PortCode");

            // Relationships
            this.HasOptional(t => t.MaterialCodeMaster)
                .WithMany(t => t.MaterialCodePorts)
                .HasForeignKey(d => d.MaterialCodeMasterid);
            this.HasOptional(t => t.Port)
                .WithMany(t => t.MaterialCodePorts)
                .HasForeignKey(d => d.PortCode);

        }
    }
}
