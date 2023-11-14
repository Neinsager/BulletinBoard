using AutoMapper;
using BulletinBoard.Application.AppServices.Authentication;
using BulletinBoard.Application.AppServices.Authentication.ConstantsUserRoles;
using BulletinBoard.Application.AppServices.Authentication.EntitiesAuth;
using BulletinBoard.Application.AppServices.Authentication.Handlers;
using BulletinBoard.Application.AppServices.Authentication.Passwords;
using BulletinBoard.Application.AppServices.Authentication.Services;
using BulletinBoard.Application.AppServices.Contexts.Attachments.Repositories;
using BulletinBoard.Application.AppServices.Contexts.Attachments.Services;
using BulletinBoard.Application.AppServices.Contexts.Categories.Repositories;
using BulletinBoard.Application.AppServices.Contexts.Categories.Services;
using BulletinBoard.Application.AppServices.Contexts.Comments.Repositories;
using BulletinBoard.Application.AppServices.Contexts.Comments.Services;
using BulletinBoard.Application.AppServices.Contexts.Posts.Repositories;
using BulletinBoard.Application.AppServices.Contexts.Posts.Services;
using BulletinBoard.Application.AppServices.Contexts.Users.Repositories;
using BulletinBoard.Application.AppServices.Contexts.Users.Services;
using BulletinBoard.Infrastructure.DataAccess;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Attachments.Repositories;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Categories.Repositories;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Comments.Repositories;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Posts.Repositories;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Users.Repositories;
using BulletinBoard.Infrastructure.DataAccess.Db;
using BulletinBoard.Infrastructure.MappingProfiles;
using BulletinBoard.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BulletinBoard.Hosts.Api
{
    /// <summary>
    /// Методы расширения для добавления сервисов.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавлtние сервисов.
        /// </summary>
        /// <param name="services">Сервисы приложения.</param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAttachmentService, AttachmentService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IEntityAuthorizationService, EntityAuthorizationService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IPasswordService, PasswordService>();

            return services;
        }

        /// <summary>
        /// Добавляет репозиториев.
        /// </summary>
        /// <param name="services">Сервисы приложения.</param>
        /// <returns></returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAttachmentRepository, AttachmentRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();

            return services;
        }

        /// <summary>
        /// Добавляет контекст конфигурации БД.
        /// </summary>
        /// <param name = "services" ></param >
        /// <returns ></returns >
        public static IServiceCollection AddDbContextConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IDbInitializer, EfDbInitializer>();
            services.AddSingleton<IDbContextOptionsConfigurator<BaseDbContext>, BaseDbContextConfiguration>();
            services.AddDbContext<BaseDbContext>((serviceProvider, options) =>
            {
                var configurator = serviceProvider.GetRequiredService<IDbContextOptionsConfigurator<BaseDbContext>>();
                configurator.Configure((DbContextOptionsBuilder<BaseDbContext>)options);
            });
            services.AddScoped<DbContext, BaseDbContext>();

            return services;
        }

        /// <summary>
        /// Добавляет Автомаппер в DI.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PostMapProfile>();
                cfg.AddProfile<AttachmentMapProfile>();
                cfg.AddProfile<UserMapProfile>();
                cfg.AddProfile<CategoryMapProfile>();
                cfg.AddProfile<CommentMapProfile>();
            });

            config.AssertConfigurationIsValid();

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }

        /// <summary>
        /// Добавляет аутентификацию и авторизацию.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IServiceCollection AddAuth(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
                    };})
                    .AddScheme<JwtSchemeOptions, JwtSchemeHandler>(AuthSchemas.Custom, options => { });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("CustomPolicy", policy =>
                {
                    policy.RequireRole("Administrator");
                    policy.RequireClaim("User", "User");
                });
            });
            services.AddAuthorization();
            return services;
        }

        /// <summary>
        /// Добавляет кэш хранящийся в памяти веб-сервера.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMemoryCaching(this IServiceCollection services)
        {
            services.AddMemoryCache(options =>
            {
                options.CompactionPercentage = 0.8;
                options.ExpirationScanFrequency = TimeSpan.FromMinutes(60);
            });

            return services;
        }
    }
}