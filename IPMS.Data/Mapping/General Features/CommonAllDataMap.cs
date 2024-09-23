using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Data.Mapping 
{
   public class CommonAllDataMap : EntityTypeConfiguration<CommonAllData>
    {
        public CommonAllDataMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

          

            // Table & Column Mappings
            this.ToTable("CommonAllData");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ModelName).HasColumnName("ModelName");
            this.Property(t => t.ModelData).HasColumnName("ModelData");
            this.Property(t => t.Status).HasColumnName("Status"); 
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            
        }
    }
}
