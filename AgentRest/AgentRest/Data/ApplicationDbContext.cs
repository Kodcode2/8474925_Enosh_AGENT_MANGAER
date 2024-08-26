using AgentRest.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AgentRest.Data
{

    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IConfiguration configuration
            ) : base(options)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<AgentModel> AgentSet { get; set; }
        public DbSet<TargetModel> TargetSet { get; set; }
        public DbSet<MissionModel> MissionSet { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<AgentModel>()
                .Property(a => a.Status)
                .HasConversion<string>()
                .IsRequired();

            modelBuilder.Entity<TargetModel>()
            .Property(t => t.Status)
            .HasConversion<string>()
            .IsRequired();

            modelBuilder.Entity<MissionModel>()
            .HasOne(m => m.Agent)
            .WithMany(a => a.Missions)
            .HasForeignKey(m => m.AgentId);

            modelBuilder.Entity<MissionModel>()
            .HasOne(m => m.Target)
            .WithMany()
            .HasForeignKey(m => m.TargetId);

        }

    }
}
