using BulletinBoard.Application.AppServices.Contexts.Post.Repositories;
using BulletinBoard.Application.AppServices.Contexts.Post.Services;
using BulletinBoard.Contracts;
using BulletinBoard.Contracts.Post;
using BulletinBoard.Hosts.Api.Controllers;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Post.Repositories;

namespace BulletinBoard.Hosts.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(s =>
            {
                var includeDocsTypesMarker = new[]
                {
                    typeof(PostDto),
                    typeof(PostController)
                };

                foreach (var marker in includeDocsTypesMarker)
                {
                    var xmlName = $"{marker.Assembly.GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlName);

                    if(File.Exists(xmlPath))
                        s.IncludeXmlComments(xmlPath);
                }
            });

            builder.Services.AddTransient<IPostService, PostService>();
            builder.Services.AddTransient<IPostRepository, PostRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}