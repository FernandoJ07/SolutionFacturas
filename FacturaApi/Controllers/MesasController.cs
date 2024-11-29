using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFactura.BLL.Service;
using ProyectoFactura.DAL.DataContext;
using ProyectoFactura.Models;

namespace FacturaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesasController : ControllerBase
    {
        private readonly IMesaService _mesaService;

        public MesasController(IMesaService mesaService)
        {
            _mesaService = mesaService;
        }

        // GET: api/Mesas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mesa>>> GetMesas()
        {
            try
            {
                var mesas = await _mesaService.GetAll();
                if (mesas == null || !mesas.Any())
                {
                    return NotFound("No se encontraron las mesas.");
                }
                return Ok(mesas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/Mesas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mesa>> GetMesa(decimal id)
        {
            var mesa = await _mesaService.GetById(id);

            if (mesa == null)
            {
                return NotFound();
            }

            return Ok(mesa);
        }

        //// PUT: api/Mesas/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutMesa(decimal id, Mesa mesa)
        //{
        //    if (id != mesa.Nromesa)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(mesa).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MesaExists(id))
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

        //// POST: api/Mesas
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Mesa>> PostMesa(Mesa mesa)
        //{
        //    _context.Mesas.Add(mesa);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (MesaExists(mesa.Nromesa))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetMesa", new { id = mesa.Nromesa }, mesa);
        //}

        //// DELETE: api/Mesas/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteMesa(decimal id)
        //{
        //    var mesa = await _context.Mesas.FindAsync(id);
        //    if (mesa == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Mesas.Remove(mesa);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool MesaExists(decimal id)
        //{
        //    return _context.Mesas.Any(e => e.Nromesa == id);
        //}
    }
}
