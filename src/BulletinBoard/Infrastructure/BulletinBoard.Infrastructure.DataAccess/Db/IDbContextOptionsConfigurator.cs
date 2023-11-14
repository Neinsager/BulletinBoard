using Microsoft.EntityFrameworkCore;

namespace BulletinBoard.Infrastructure.DataAccess.Db
{
    /// <summary>
    /// Конфигуратор контекста.
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public interface IDbContextOptionsConfigurator<TContext> where TContext : DbContext
    {
        /// <summary>
        /// Выполняет конфигурацию контекста.
        /// </summary>
        /// <param name="options">Настройки.</param>
        void Configure(DbContextOptionsBuilder<TContext> options);
    }
}