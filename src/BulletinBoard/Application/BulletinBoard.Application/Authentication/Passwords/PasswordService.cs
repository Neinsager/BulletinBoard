using BulletinBoard.Application.AppServices.Exceptions;
using System.Security.Cryptography;
using System.Text;

namespace BulletinBoard.Application.AppServices.Authentication.Passwords
{
    /// <inheritdoc />
    public class PasswordService : IPasswordService
    {
        /// <inheritdoc />
        public string HashPassword(string password)
        {
            // Вычисляется хэш, записывается в массив байтов.
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));

            // Преобразует байты в строку.
            var builder = new StringBuilder();
            foreach (var t in bytes) builder.Append(t.ToString());

            return builder.ToString();
        }

        /// <inheritdoc />
        public void ComparePasswordHashWithPassword(string hashedPassword, string passwordToCheck)
        {
            // Вычисляется хэш, записывается в массив байтов.
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(passwordToCheck));

            // Преобразует байты в строку.
            var builder = new StringBuilder();
            foreach (var t in bytes) builder.Append(t.ToString());

            // Сравнивает два хеша.
            var success = hashedPassword == builder.ToString();

            if (!success) throw new InvalidSignInException();
        }

        /// <inheritdoc />
        public void ComparePasswords(string password1, string password2)
        {
            var success = string.Equals(password1, password2);

            if (!success) throw new PasswordMatchException();
        }
    }
}