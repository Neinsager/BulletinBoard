namespace BulletinBoard.Application.AppServices.Exceptions
{
    public class InvalidSignInException : Exception
    {
        /// <summary>
        /// Ошибка ввода логина и/или пароля.
        /// </summary>
        public InvalidSignInException() 
            : base("Логин и/или пароль неверные.")
        {
        }

        /// <summary>
        /// Стандартное сообщение.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        public InvalidSignInException(string message)
            : base(message)
        {
        }
    }
}