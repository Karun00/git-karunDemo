using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;
namespace IPMS.Data.Mapping
{
	public class NewsMap : EntityTypeConfiguration<News>
	{
		public NewsMap()
		{
			// Primary Key
			this.HasKey(t => t.NewsID);

			// Properties
			this.Property(t => t.Title)
				.IsRequired()
				.HasMaxLength(200);

			this.Property(t => t.NewsContent)
				.IsRequired()
				.HasMaxLength(200);

			this.Property(t => t.RecordStatus)
				.IsFixedLength()
				.HasMaxLength(1);

            this.Property(t => t.NewsUrl)
                .IsRequired()
                .HasMaxLength(80);

			// Table & Column Mappings
			this.ToTable("News");
			this.Property(t => t.NewsID).HasColumnName("NewsID");
			this.Property(t => t.Title).HasColumnName("Title");
			this.Property(t => t.NewsContent).HasColumnName("NewsContent");
            this.Property(t => t.NewsUrl).HasColumnName("NewsUrl");
			this.Property(t => t.StartDate).HasColumnName("StartDate");
			this.Property(t => t.EndDate).HasColumnName("EndDate");
			this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
			this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
			this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
			this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
			this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
			// Relationships
			this.HasRequired(t => t.User)
				.WithMany(t => t.News)
				.HasForeignKey(d => d.CreatedBy);
			this.HasOptional(t => t.User1)
				.WithMany(t => t.News1)
				.HasForeignKey(d => d.ModifiedBy);    
		}
	}
}
