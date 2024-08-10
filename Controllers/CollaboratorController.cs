using CrudSystem.DTOs;
using CrudSystem.Models;
using CrudSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace CrudSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        private readonly CollaboratorServices _collaboratorServices;

        public CollaboratorController(CollaboratorServices collaboratorServices)
        {
            _collaboratorServices = collaboratorServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<CollaboratorDTO>>> BuscarCollaborators()
        {
            var collaborator = await _collaboratorServices.BuscarTodosCollaborators();
            var collaboratorDTO = collaborator.Select(c => new CollaboratorDTO
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            return Ok(collaboratorDTO);
        }

        
    }
}
