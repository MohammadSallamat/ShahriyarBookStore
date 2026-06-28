using Application.CommonApplication.Config;
using Application.CommonApplication.FileUtil.Interfaces;
using Application.CommonApplication.FileUtil.Services;
using Application.UsersApp.Create;
using Facade.Presentation._Utils;
using Facade.Presentation.Config;
using Query.Users.List_of_users;

namespace Facade.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.RegisterShopDependencies(connectionString);

            CommonBootstrapper.Init(builder.Services);
            builder.Services.AddScoped<ILocalFileService, LocalFileService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

            }
            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseMiddleware<ExceptionMiddleware>();
            app.MapControllers();

            app.Run();
        }
    }
}
