using Core.Entities;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class SqlContext : DbContext
    {
        public DbSet<Employee> Employee { get; set; }
        public DbSet<PersonalInformation> PersonalInformation { get; set; }

        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new PersonalInformationConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
