namespace BulletinBoard.Application.AppServices.Exceptions
{
    public class PasswordMatchException : Exception
    {
        /// <summary>
        /// Ошибка несовпадение паролей.
        /// </summary>
        public PasswordMatchException() 
            : base("Пароли не совпадают.")
        {
        }

        /// <summary>
        /// Стандартное сообщение.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        public PasswordMatchException(string message)
            : base(message)
        {
        }
    }
}