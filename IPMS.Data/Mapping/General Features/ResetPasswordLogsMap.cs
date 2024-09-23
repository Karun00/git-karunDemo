using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Data.Mapping
{
    public class ResetPasswordLogsMap : EntityTypeConfiguration<ResetPasswordLogs>
    {
        public ResetPasswordLogsMap()
        {
            // Primary Key
            this.HasKey(t => t.ResetPasswordLogID);

            // Table & Column Mappings
            this.ToTable("ResetPasswordLogs");
            this.Property(t => t.ResetPasswordLogID).HasColumnName("ResetPasswordLogID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.AuditFlag).HasColumnName("AuditFlag");


        }
    }
}
