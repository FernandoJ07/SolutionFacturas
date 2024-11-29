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

        
        //// POST: api/Mesas
        [HttpPost]
        public async Task<ActionResult<Mesa>> PostMesa(Mesa mesa)
        {
            if (mesa == null)
            {
                return BadRequest("La mesa no puede ser vacio.");
            }

            try
            {
                var resultado = await _mesaService.Add(mesa);
                if (!resultado)
                {
                    return Conflict("La mesa ya existe.");
                }

                return CreatedAtAction(nameof(GetMesa), new { id = mesa.Nromesa }, mesa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar el cliente: {ex.Message}");
            }
        }

        
    }
}
