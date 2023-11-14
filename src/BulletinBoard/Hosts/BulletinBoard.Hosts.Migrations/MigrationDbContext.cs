using BulletinBoard.Infrastructure.DataAccess.Db;
using Microsoft.EntityFrameworkCore;

namespace BulletinBoard.Hosts.DbMigrator
{
    /// <summary>
    /// Контекст базы данных для мигратора.
    /// </summary>
    public class MigrationDbContext : BaseDbContext
    {
        public MigrationDbContext(DbContextOptions options) : base(options) { }
    }
}