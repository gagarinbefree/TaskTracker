using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TaskTracker.Application.Tasks.Commands;
using TaskTracker.Application.Tasks.Queries;
using TaskTracker.Data;
using TaskTracker.Domain;
using TaskTracker.Domain.Entities;

namespace TaskTracker.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options => {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    Description = "Get JWT security key from GET /api/token"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
            
            builder.Services.AddDbContext<ServiceDbContext>(opt => 
                opt.UseNpgsql(builder.Configuration.GetConnectionString("Db"))                   
                   .LogTo(Console.WriteLine)
                   .EnableSensitiveDataLogging()
            );
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));
            builder.Services.AddTransient<IRequestHandler<GetTskAllQuery, IEnumerable<Tsk>>, GetTskAllQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetTskByIdQuery, Tsk?>, GetTskByIdQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<CreateTskCommand, int>, CreateTskCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<DeleteTskCommand, bool>, DeleteTskCommandHandler>();

            builder.Services
               .AddAuthentication(options => options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options => {
                   options.RequireHttpsMetadata = false;
                   options.SaveToken = true;
                   var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Secret"] ?? throw new Exception("JWT security key not found"));
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(key),
                       ValidateIssuer = true,
                       ValidIssuer = builder.Configuration["Jwt:Issuer"],
                       ValidateAudience = true,
                       ValidAudience = builder.Configuration["Jwt:Audience"]
                   };
               }
           );

            var app = builder.Build();
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ServiceDbContext>();
            context?.Database.Migrate();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options => options.DefaultModelsExpandDepth(-1));
            }
           
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
