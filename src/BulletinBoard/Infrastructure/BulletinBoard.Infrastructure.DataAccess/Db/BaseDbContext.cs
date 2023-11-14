using BulletinBoard.Infrastructure.DataAccess.Contexts.Advertisements.Configurations;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Attachments.Configurations;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Categories.Configurations;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Comments.Configurations;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Users.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BulletinBoard.Infrastructure.DataAccess.Db
{
    /// <summary>
    /// Контекст БД.
    /// </summary>
    public class BaseDbContext : DbContext
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="BaseDbContext"/>.
        /// </summary>
        public BaseDbContext(DbContextOptions options) : base(options) { }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new AttachmentConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}