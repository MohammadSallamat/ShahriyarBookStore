using Application.CommonApplication.Config;
using Application.CommonApplication.FileUtil.Interfaces;
using Application.CommonApplication.FileUtil.Services;
using Application.UsersApp.Create;
using Facade.Presentation._Utils;
using Facade.Presentation.Config;
using Query.Users.GetById;
using Application.UsersApp.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Facade.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            var key = builder.Configuration["Jwt:SecretKey"];
            if (string.IsNullOrWhiteSpace(key) || Encoding.UTF8.GetByteCount(key) < 32)
                throw new InvalidOperationException("Configure Jwt:SecretKey with at least 32 UTF-8 bytes of cryptographically random secret data.");
            var issuer = builder.Configuration["Jwt:Issuer"] ?? "ShahriarBookStore";
            var audience = builder.Configuration["Jwt:Audience"] ?? "ShahriarBookStoreClient";
            builder.Services.AddSingleton(new AuthSettings(key, issuer, audience));
            if (builder.Environment.IsDevelopment())
                builder.Services.AddScoped<IOtpSender, DevelopmentOtpSender>();
            else
                builder.Services.AddScoped<IOtpSender, UnavailableOtpSender>();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    ValidateIssuer = true, ValidIssuer = issuer,
                    ValidateAudience = true, ValidAudience = audience,
                    ValidateLifetime = true, ClockSkew = TimeSpan.Zero
                };
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        var authorization = context.Request.Headers.Authorization.ToString();
                        var raw = authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase) ? authorization[7..].Trim() : "";
                        var service = context.HttpContext.RequestServices.GetRequiredService<IAuthenticationService>();
                        if (string.IsNullOrEmpty(raw) || !await service.IsAccessTokenActiveAsync(raw, context.HttpContext.RequestAborted))
                            context.Fail("Token revoked.");
                    }
                };
            });
            builder.Services.AddAuthorization();
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
            app.UseCors("AllowAll");

            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
