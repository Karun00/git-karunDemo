using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
    public class NewsPortMap : EntityTypeConfiguration<NewsPort>
    {
        public NewsPortMap()
        {
            // Primary Key
            this.HasKey(t => t.NewsPortID);

            // Properties
            this.Property(t => t.RecordStatus)
                .IsFixedLength()
                .HasMaxLength(1);          
            // Table & Column Mappings
            this.ToTable("NewsPort");
            this.Property(t => t.NewsPortID).HasColumnName("NewsPortID");
            this.Property(t => t.NewsID).HasColumnName("NewsID");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.PortCode).HasColumnName("PortCode");



            // Relationships
            this.HasRequired(t => t.News)
                .WithMany(t => t.NewsPorts)
                .HasForeignKey(d => d.NewsID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.NewsPort)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.User1)
                .WithMany(t => t.NewsPort1)
                .HasForeignKey(d => d.ModifiedBy);          
            this.HasOptional(t => t.Port)
                .WithMany(t => t.NewsPorts)
                .HasForeignKey(d => d.PortCode);
      

        }
    }
}