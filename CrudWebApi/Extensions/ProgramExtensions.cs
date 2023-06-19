using Core.Boundaries.Infrastructure.Interfaces;
using Core.Entities.Validators;
using Core.Services;
using Core.Services.Interfaces;
using FluentValidation;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Employee.Validators;

namespace CrudWebApi.Extensions
{
    public static class ProgramExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                    });
            });
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IFileService, FileService>();
        }

        public static void ConfigureDbContext(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDbContext<SqlContext>(options => options.UseSqlServer(configuration.GetConnectionString("ConnectionString")));
        }

        public static void ConfigureValidators(this IServiceCollection services) 
        { 
            services.AddValidatorsFromAssemblyContaining<EmployeeEntityValidator>();
            services.AddValidatorsFromAssemblyContaining<EmployeeDTOValidator>();  
        }
    }
}
