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
                optionsBuilder.UseSqlServer("Server=tcp:manarger-serve.database.windows.net,1433;Initial Catalog=api-Db;Persist Security Info=False;User ID=manoela;Password=sistem1#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=100;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurando a relação User -> Collaborator
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UUIDUserName)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasOne(u => u.Collaborator)
                .WithOne(c => c.User)
                .HasForeignKey<Collaborator>(c => c.UserId);

            // Configurando a relação Project -> Collaborator
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Collaborators)
                .WithMany(c => c.Projects)
                .UsingEntity<Dictionary<string, object>>(
                    "ProjectCollaborator",
                    j => j.HasOne<Collaborator>().WithMany().HasForeignKey("CollaboratorId"),
                    j => j.HasOne<Project>().WithMany().HasForeignKey("ProjectId"));

            // Configurando a relação Tarefas -> Collaborator
            modelBuilder.Entity<Tarefas>()
                .HasOne(t => t.Collaborator)
                .WithMany(c => c.Tarefas)
                .HasForeignKey(t => t.CollaboratorId);

            // Configurando a relação Tarefas -> Project
            modelBuilder.Entity<Tarefas>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Tarefas)
                .HasForeignKey(t => t.ProjectId);

            // Configurando a relação TimeTracker -> Tarefas
            modelBuilder.Entity<TimeTracker>()
                .HasOne(tt => tt.Tarefas)
                .WithMany(t => t.TimeTrackers)
                .HasForeignKey(tt => tt.TarefasId);

            // Configurando a relação TimeTracker -> Collaborator
            modelBuilder.Entity<TimeTracker>()
                .HasOne(tt => tt.Collaborator)
                .WithMany(c => c.TimeTrackers)
                .HasForeignKey(tt => tt.CollaboratorId);
        }
    }
}
