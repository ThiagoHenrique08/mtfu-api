using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MoreThanFollowUp.Domain.Entities.Projects;
using MoreThanFollowUp.Domain.Entities.Resources;
using MoreThanFollowUp.Domain.Models;


namespace MoreThanFollowUp.Infrastructure.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly string? _connectionString;

        public ApplicationDbContext()
        {

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            _connectionString = configuration.GetConnectionString("ConnectionString")!;

        }
        public ApplicationDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }


        public DbSet<Project> Projects { get; set; }
        public DbSet<Project_User> ProjectUsers { get; set; }
        public DbSet<Sprint_User> SprintUsers { get; set; }
        public DbSet<ProjectCategory> Categories { get; set; }
        public DbSet<ProjectStatus> ProjectStatus { get; set; }
        public DbSet<ProjectResponsible> Responsible { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Enterprise> Enteprises { get; set; }
        public DbSet<Planning> Plannings { get; set; }
        public DbSet<Sprint> Sprints { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString, sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5, // Número máximo de tentativas
                        maxRetryDelay: TimeSpan.FromSeconds(5), // Tempo máximo entre as tentativas
                        errorNumbersToAdd: null); // Números de erro adicionais que disparam o retry (opcional));
                });
                optionsBuilder.UseLazyLoadingProxies();

            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Global turn off delete behaviour on foreign keys
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Cascade;
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        }
    }
}
