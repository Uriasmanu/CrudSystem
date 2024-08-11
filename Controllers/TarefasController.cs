using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CrudSystem.Models;
using CrudSystem.Services;

namespace CrudSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaService _tarefaService;

        public TarefaController(ITarefaService tarefaService)
        {
            _tarefaService = tarefaService;
        }

        // GET: api/Tarefa
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarefas>>> GetAllTarefas()
        {
            var tarefas = await _tarefaService.GetAllTarefasAsync();
            return Ok(tarefas);
        }

        // GET: api/Tarefa/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarefas>> GetTarefaById(Guid id)
        {
            var tarefa = await _tarefaService.GetTarefaByIdAsync(id);

            if (tarefa == null)
            {
                return NotFound();
            }

            return Ok(tarefa);
        }

        // POST: api/Tarefa
        [HttpPost]
        public async Task<ActionResult<Tarefas>> CreateTarefa(Tarefas tarefa)
        {
            var createdTarefa = await _tarefaService.CreateTarefaAsync(tarefa);
            return CreatedAtAction(nameof(GetTarefaById), new { id = createdTarefa.Id }, createdTarefa);
        }

        // PUT: api/Tarefa/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTarefa(Guid id, Tarefas tarefa)
        {
            if (id != tarefa.Id)
            {
                return BadRequest();
            }

            try
            {
                await _tarefaService.UpdateTarefaAsync(tarefa);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Tarefa/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarefa(Guid id)
        {
            try
            {
                await _tarefaService.DeleteTarefaAsync(id);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
