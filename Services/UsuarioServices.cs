using CrudSystem.Data;
using CrudSystem.DTOs;
using CrudSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudSystem.Services
{
    public class UsuarioServices
    {
        private readonly ApplicationDbContext _dbContext;

        public UsuarioServices(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> BuscarTodosUsuarios()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> BuscarPorId(Guid id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task<User> Adicionar(UserDTO userDTO)
        {
            var existingUser = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.UUIDUserName == userDTO.UUIDUserName);

            if (existingUser != null)
            {
                throw new Exception("Usuário com esse UUIDUserName já existe.");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                UUIDUserName = userDTO.UUIDUserName,
                Password = userDTO.Password,
                CreatedAt = DateTime.UtcNow
            };

            var collaborator = new Collaborator
            {
                Id = Guid.NewGuid(),
                Name = userDTO.UUIDUserName,
                CreatedAt = DateTime.UtcNow,
                User = user
            };

            _dbContext.Users.Add(user);
            _dbContext.Collaborators.Add(collaborator);

            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<bool> Apagar(Guid id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
