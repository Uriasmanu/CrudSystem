using CrudSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Collaborator> Collaborators { get; set; }
        public DbSet<TimeTracker> TimeTrackers { get; set; }
        public DbSet<Tarefas> Tarefas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:manarger-serve.database.windows.net,1433;Initial Catalog=api-Db;Persist Security Info=False;User ID=manoela;Password=sistema1#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.UUIDUserName)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasOne(u => u.Collaborator)
                .WithOne(c => c.User)
                .HasForeignKey<Collaborator>(c => c.UserId);

            modelBuilder.Entity<Tarefas>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Tarefas>()
                .HasOne(t => t.Project)
                .WithMany()
                .HasForeignKey(t => t.ProjectId);

            modelBuilder.Entity<TimeTracker>()
                .HasOne(tt => tt.Tarefas)
                .WithMany()
                .HasForeignKey(tt => tt.TarefasId);

            modelBuilder.Entity<TimeTracker>()
                .HasOne(tt => tt.Collaborator)
                .WithMany()
                .HasForeignKey(tt => tt.CollaboratorId);
        }
    }
}
