using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using TaskTracker.Application.Tasks.Commands;
using TaskTracker.Application.Tasks.Dto;
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
            TypeAdapterConfig<Tsk, TskDto>.NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.ParentTaskId, src => src.ParentTaskId)
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.Status, src => src.StatusId)
                .Map(dest => dest.StatusName, src => src.StatusId.ToString())
                .Map(dest => dest.Priority, src => src.PriorityId)
                .Map(dest => dest.PriorityName, src => src.PriorityId.ToString())
                .Map(dest => dest.CreatedAt, src => src.CreatedAt)
                .Map(dest => dest.SubTasks, src => src.SubTasks)
                .Map(dest => dest.RelatedTasks, src => src.RelatedTasks)
                .Map(dest => dest.SubTasks, src => src.SubTasks != null ? src.SubTasks.Adapt<List<TskDto>>() : new List<TskDto>())
                .Map(dest => dest.RelatedTasks, src => src.RelatedTasks != null ? src.RelatedTasks.Adapt<List<TskRelationshipDto>>() : new List<TskRelationshipDto>());

            TypeAdapterConfig<TskRelationship, TskRelationshipDto>.NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.SourceTaskId, src => src.SourceTaskId)
                .Map(dest => dest.TargetTaskId, src => src.TargetTaskId)
                .Map(dest => dest.TypeId, src => src.TypeId)
                .Map(dest => dest.TypeName, src => src.TypeId.ToString());

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
            builder.Services.AddTransient<IRequestHandler<GetTskAllQuery, IEnumerable<TskDto>>, GetTskAllQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetTskByIdQuery, TskDto?>, GetTskByIdQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<CreateTskCommand, TskDto>, CreateTskCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<DeleteTskCommand, bool>, DeleteTskCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<UpdateTskCommand, TskDto?>, UpdateTskCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<GetTskRelationshipAllQuery, IEnumerable<TskRelationshipDto>>, GetTskRelationshipAllQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<GetTskRelationshipByIdQuery, TskRelationshipDto?>, GetTskRelationshipByIdQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<CreateTskRelationshipCommand, TskRelationshipDto>, CreateTskRelationshipCommandHandler>();
            builder.Services.AddTransient<IRequestHandler<DeleteTskRelationshipCommand, bool>, DeleteTskRelationshipCommandHandler>();

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

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

            builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => {
                options.SerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });

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
