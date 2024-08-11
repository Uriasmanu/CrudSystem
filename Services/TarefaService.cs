using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CrudSystem.Data;
using CrudSystem.Models;
using CrudSystem.DTOs;

namespace CrudSystem.Services
{
    public interface ITarefaService
    {
        Task<Tarefas> GetTarefaByIdAsync(Guid id);
        Task<IEnumerable<Tarefas>> GetAllTarefasAsync();
        Task<Tarefas> CreateTarefaAsync(Tarefas tarefa);
        Task UpdateTarefaAsync(Tarefas tarefa);
        Task DeleteTarefaAsync(Guid id);
    }

    public class TarefaService : ITarefaService
    {
        private readonly ApplicationDbContext _context;

        public TarefaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Tarefas> GetTarefaByIdAsync(Guid id)
        {
            return await _context.Tarefas
                .Include(t => t.Project)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Tarefas>> GetAllTarefasAsync()
        {
            return await _context.Tarefas
                .Include(t => t.Project)
                .ToListAsync();
        }

        public async Task<Tarefas> CreateTarefaAsync(Tarefas tarefa)
        {
            var projetoExiste = await _context.Tarefas
                .FirstOrDefaultAsync(u => u.Name == tarefa.Name);

            if (projetoExiste != null)
            {
                throw new ArgumentException("Já existe uma tarefa com esse nome.");

            }

            tarefa.Id = Guid.NewGuid(); 
            tarefa.CreatedAt = DateTime.UtcNow;
            tarefa.UpdatedAt = DateTime.UtcNow;

            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();

            return tarefa;
        }

        public async Task UpdateTarefaAsync(Tarefas tarefa)
        {
            var existingTarefa = await _context.Tarefas.FindAsync(tarefa.Id);
            if (existingTarefa == null) throw new ArgumentException("Tarefa não encontrada");

            existingTarefa.Name = tarefa.Name;
            existingTarefa.Descritiva = tarefa.Descritiva;
            existingTarefa.ProjectId = tarefa.ProjectId;
            existingTarefa.UpdatedAt = DateTime.UtcNow;

            _context.Tarefas.Update(existingTarefa);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTarefaAsync(Guid id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null) throw new ArgumentException("Tarefa não encontrada");

            tarefa.DeletedAt = DateTime.UtcNow;
            _context.Tarefas.Update(tarefa);
            await _context.SaveChangesAsync();
        }
    }
}
