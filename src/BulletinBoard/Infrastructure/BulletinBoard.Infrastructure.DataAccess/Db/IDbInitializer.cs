namespace BulletinBoard.Infrastructure.DataAccess.Db
{
    /// <summary>
    /// Инициализатор БД.
    /// </summary>
    public interface IDbInitializer
    {
        /// <summary>
        /// Инициализировать БД.
        /// </summary>
        void InitializeDb();
    }
}