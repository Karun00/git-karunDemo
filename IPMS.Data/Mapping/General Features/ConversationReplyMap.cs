using IPMS.Domain.Models;
using System.Data.Entity.ModelConfiguration;

namespace IPMS.Data.Mapping
{
    public class ConversationReplyMap : EntityTypeConfiguration<ConversationReply>
    {
        public ConversationReplyMap()
        {
            // Primary Key
            this.HasKey(t => t.ConversationReplyID);

            // Properties
            this.Property(t => t.IPAddress)
                .HasMaxLength(30);

            this.Property(t => t.Reply)
                .HasMaxLength(2000);

            this.Property(t => t.IsRead)
               .IsRequired()
               .IsFixedLength()
               .HasMaxLength(1);

            this.Property(t => t.RecordStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("ConversationReply");
            this.Property(t => t.ConversationReplyID).HasColumnName("ConversationReplyID");
            this.Property(t => t.ConversationID).HasColumnName("ConversationID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.IPAddress).HasColumnName("IPAddress");
            this.Property(t => t.Reply).HasColumnName("Reply");
            this.Property(t => t.IsRead).HasColumnName("IsRead");
            this.Property(t => t.RecordStatus).HasColumnName("RecordStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.Conversation)
                .WithMany(t => t.ConversationReplies)
                .HasForeignKey(d => d.ConversationID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.ConversationReplies)
                .HasForeignKey(d => d.CreatedBy);
            this.HasRequired(t => t.User1)
                .WithMany(t => t.ConversationReplies1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.User2)
                .WithMany(t => t.ConversationReplies2)
                .HasForeignKey(d => d.UserID);

        }
    }
}
