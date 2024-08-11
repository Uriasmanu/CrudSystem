using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CrudSystem.Data;
using CrudSystem.Models;

namespace CrudSystem.Services
{
    public class ProjectService
    {
        private readonly ApplicationDbContext _context;

        public ProjectService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Project> GetProjectByIdAsync(Guid id)
        {
            return await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<Project> CreateProjectAsync(Project project)
        {
            project.Id = Guid.NewGuid(); // Gera um novo Id
            project.CreatedAt = DateTime.UtcNow;
            project.UpdatedAt = DateTime.UtcNow;

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return project;
        }

        public async Task UpdateProjectAsync(Project project)
        {
            var existingProject = await _context.Projects.FindAsync(project.Id);
            if (existingProject == null) throw new ArgumentException("Projeto não encontrado");

            existingProject.Name = project.Name;
            existingProject.UpdatedAt = DateTime.UtcNow;

            _context.Projects.Update(existingProject);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProjectAsync(Guid id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) throw new ArgumentException("Projeto não encontrado");

            project.DeletedAt = DateTime.UtcNow;
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }
    }
}
