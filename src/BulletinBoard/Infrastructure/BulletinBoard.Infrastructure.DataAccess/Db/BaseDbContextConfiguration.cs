using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BulletinBoard.Infrastructure.DataAccess.Db
{
    /// <summary>
    /// Конфигурация контекста БД.
    /// </summary>
    public class BaseDbContextConfiguration : IDbContextOptionsConfigurator<BaseDbContext>
    {
        private const string PostgesConnectionStringName = "PostgresDb";
        
        private readonly IConfiguration _configuration;
        private readonly ILoggerFactory _loggerFactory;

        /// <summary>
        /// Инициализация экземпляра <see cref="BaseDbContextConfiguration" />
        /// </summary>
        /// <param name="configuration">Конфигурация.</param>
        /// <param name="loggerFactory">Фабрика логирования.</param>
        public BaseDbContextConfiguration(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
            _loggerFactory = loggerFactory;
        }

        /// <inheritdoc />
        public void Configure(DbContextOptionsBuilder<BaseDbContext> options)
        {
            var connectionString = _configuration.GetConnectionString(PostgesConnectionStringName);
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    $"Не найдена строка подключения с именем '{PostgesConnectionStringName}'");
            }
            options.UseNpgsql(connectionString);
            options.UseLoggerFactory(_loggerFactory);
            options.UseLazyLoadingProxies();
        }
    }
}