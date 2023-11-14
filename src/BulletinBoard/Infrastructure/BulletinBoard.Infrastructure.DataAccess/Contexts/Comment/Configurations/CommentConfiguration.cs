using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BulletinBoard.Domain.Comments;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Comments.Configurations
{
    /// <summary>
    /// Конфигурация отношения Comment.
    /// </summary>
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable(nameof(Comment));

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Content).IsRequired().HasMaxLength(1000);

            builder.HasOne(c => c.Post)
                .WithMany(a => a.Comments)
                .HasForeignKey(c => c.PostId);

            builder.HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId);
        }
    }
}
