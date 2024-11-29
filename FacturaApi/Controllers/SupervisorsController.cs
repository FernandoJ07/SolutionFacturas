using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFactura.BLL.Service;
using ProyectoFactura.DAL.DataContext;
using ProyectoFactura.DAL.Repositories;
using ProyectoFactura.Models;

namespace FacturaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupervisorsController : ControllerBase
    {
        private readonly ISupervisorService _supervisorService;

        public SupervisorsController(ISupervisorService supervisorService)
        {
            _supervisorService = supervisorService;
        }

        // GET: api/Supervisors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supervisor>>> GetSupervisors()
        {
            try
            {
                var supervisores = await _supervisorService.GetAll();
                if (supervisores == null || !supervisores.Any())
                {
                    return NotFound("No se encontraron supervisores.");
                }
                return Ok(supervisores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/Supervisors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Supervisor>> GetSupervisor(decimal id)
        {
            var supervisor = await _supervisorService.GetById(id);

            if (supervisor == null)
            {
                return NotFound();
            }

            return Ok(supervisor);
        }


        // POST: api/Supervisors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Supervisor>> PostSupervisor(Supervisor supervisor)
        {
            if (supervisor == null)
            {
                return BadRequest("El supervisor no puede ser vacio.");
            }

            try
            {
                var resultado = await _supervisorService.Add(supervisor);
                if (!resultado)
                {
                    return Conflict("El supervisor ya existe.");
                }

                return CreatedAtAction(nameof(GetSupervisor), new { id = supervisor.Idsupervisor}, supervisor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar el cliente: {ex.Message}");
            }

            return CreatedAtAction("GetSupervisor", new { id = supervisor.Idsupervisor }, supervisor);
        }


    }
}
