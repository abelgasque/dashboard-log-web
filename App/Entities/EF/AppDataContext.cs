using Microsoft.EntityFrameworkCore;

namespace App.Entities.EF
{
    public class AppDataContext : DbContext
    {
        public DbSet<LogIntegrationTypeEntity> LogIntegrationType { get; set; }

        public DbSet<LogIntegrationEntity> LogIntegration { get; set; }

        public DbSet<UserEntity> User { get; set; }

        public AppDataContext(DbContextOptions options) : base(options)
        {
        }
    }
}
