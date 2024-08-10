using CrudSystem.Data;
using CrudSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudSystem.Services
{
    public class CollaboratorServices
    {
        private readonly ApplicationDbContext _dbContext;

        public CollaboratorServices(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Collaborator>> BuscarTodosCollaborators()
        {
            return await _dbContext.Collaborators.ToListAsync();
        }

    }
}
