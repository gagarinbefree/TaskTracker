using MediatR;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
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

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ServiceDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("Db")));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));
            builder.Services.AddTransient<IRequestHandler<GetTskAllQuery, IEnumerable<Tsk>>, GetTskAllQueryHandler>();
            builder.Services.AddTransient<IRequestHandler<CreateTskCommand, int>, CreateTskCommandHandler>();

            var app = builder.Build();
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ServiceDbContext>();
            context?.Database.Migrate();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
