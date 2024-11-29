using System;
using System.Collections.Generic;
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

        //// PUT: api/Supervisors/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutSupervisor(decimal id, Supervisor supervisor)
        //{
        //    if (id != supervisor.Idsupervisor)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(supervisor).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!SupervisorExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Supervisors
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Supervisor>> PostSupervisor(Supervisor supervisor)
        //{
        //    _context.Supervisors.Add(supervisor);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (SupervisorExists(supervisor.Idsupervisor))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetSupervisor", new { id = supervisor.Idsupervisor }, supervisor);
        //}

        //// DELETE: api/Supervisors/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteSupervisor(decimal id)
        //{
        //    var supervisor = await _context.Supervisors.FindAsync(id);
        //    if (supervisor == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Supervisors.Remove(supervisor);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool SupervisorExists(decimal id)
        //{
        //    return _context.Supervisors.Any(e => e.Idsupervisor == id);
        //}
    }
}
