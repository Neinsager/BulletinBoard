using BulletinBoard.Domain.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BulletinBoard.Infrastructure.DataAccess.Contexts.Advertisements.Configurations;

/// <summary>
/// Конфигурация таблицы Post.
/// </summary>
public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable(nameof(Post));
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.Title).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Description).IsRequired().HasMaxLength(500);
        builder.Property(p => p.Price).IsRequired().HasPrecision(12);

        builder.HasOne(a => a.User)
            .WithMany(u => u.Posts)
            .HasForeignKey(a => a.UserId);

        builder.HasOne(a => a.Category)
            .WithMany(c => c.Posts)
            .HasForeignKey(a => a.CategoryId);

        builder.HasMany(a => a.Attachments)
            .WithOne(att => att.Post)
            .HasForeignKey(att => att.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(a => a.Comments)
            .WithOne(c => c.Post)
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}