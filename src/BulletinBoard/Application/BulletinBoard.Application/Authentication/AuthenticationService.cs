using AutoMapper;
using BulletinBoard.Application.AppServices.Authentication.Passwords;
using BulletinBoard.Application.AppServices.Authentication.Exceptions;
using BulletinBoard.Application.AppServices.Authentication.Constants;
using BulletinBoard.Application.AppServices.Contexts.Users.Repositories;
using BulletinBoard.Contracts.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using BulletinBoard.Domain.Users;

namespace BulletinBoard.Application.AppServices.Authentication.Services
{
    /// <inheritdoc cref="IAuthenticationService"/>
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;


        /// <summary>
        /// Инициализирует <see cref="AuthenticationService"/>.
        /// </summary>
        /// <param name="userRepository">Репозиторий пользователей.</param>
        /// <param name="configuration">Конфигурация.</param>
        /// <param name="mapper">Маппер.</param>
        /// <param name="passwordService">Хеширование пароля.</param>
        public AuthenticationService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper, IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        /// <inheritdoc/>
        public async Task<string> Login(AuthUserDto dto, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByPredicate(elem => elem.Login == dto.Login, cancellationToken) ?? throw new LoginNotFoundException();

            var hashedPassword = _passwordService.HashPassword(dto.Password);
            if (hashedPassword != user.PasswordHash) throw new InvalidPasswordException();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken
            (
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn
            );

            var result = new JwtSecurityTokenHandler().WriteToken(token);

            return result;
        }

        /// <inheritdoc/>
        public async Task<Guid> Register(CreateUserDto dto, CancellationToken cancellationToken)
        {
            if ((await _userRepository.GetByPredicate(elem => elem.Login == dto.Login, cancellationToken)) is not null)
            {
                throw new LoginAlreadyExistsException();
            }

            var user = _mapper.Map<User>(dto);

            user.PasswordHash = _passwordService.HashPassword(dto.Password);
            user.Role = AuthRoles.Default;

            var result = await _userRepository.CreateAsync(user, cancellationToken);
            return result;
        }
    }
}