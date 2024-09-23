using IPMS.Domain.Models;
using System.Data.Entity.ModelConfiguration;

namespace IPMS.Data.Mapping
{
    public class ConversationMap : EntityTypeConfiguration<Conversation>
    {

        public ConversationMap()
        {
            // Primary Key
            this.HasKey(t => t.ConversationID);

            // Properties
            this.Property(t => t.IPAddress)
                .HasMaxLength(30);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("Conversation");
            this.Property(t => t.ConversationID).HasColumnName("ConversationID");
            this.Property(t => t.UserID1).HasColumnName("UserID1");
            this.Property(t => t.UserID2).HasColumnName("UserID2");
            this.Property(t => t.IPAddress).HasColumnName("IPAddress");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.Conversations)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.Conversations1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.User2)
                .WithMany(t => t.Conversations2)
                .HasForeignKey(d => d.UserID1);
            this.HasRequired(t => t.User3)
                .WithMany(t => t.Conversations3)
                .HasForeignKey(d => d.UserID2);

        }
    }
}
