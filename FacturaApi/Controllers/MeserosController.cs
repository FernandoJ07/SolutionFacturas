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
    public class MeserosController : ControllerBase
    {
        private readonly IMeseroService _meseroService;

        public MeserosController(IMeseroService meseroService)
        {
            _meseroService = meseroService;
        }

        // GET: api/Meseros
        [HttpGet]
        public async Task<ActionResult<List<Mesero>>> GetMeseros()
        {
            try
            {
                var meseros = await _meseroService.GetAll();
                if (meseros == null || !meseros.Any())
                {
                    return NotFound("No se encontraron meseros.");
                }
                return Ok(meseros);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/Meseros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mesero>> GetMesero(decimal id)
        {
            var cliente = await _meseroService.GetById(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        // POST: api/Meseros
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Mesero>> PostMesero(Mesero mesero)
        {
            if (mesero == null)
            {
                return BadRequest("El mesero no puede ser vacio.");
            }

            try
            {
                var resultado = await _meseroService.Add(mesero);
                if (!resultado)
                {
                    return Conflict("El mesero ya existe.");
                }

                return CreatedAtAction(nameof(GetMesero), new { id = mesero.Idmesero }, mesero);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar el cliente: {ex.Message}");
            }
        }

        [HttpGet("total-vendido-meseros")]
        public async Task<ActionResult<List<object>>> GetTotalVendidoPorMesero([FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin)
        {
            try
            {
                var resultado = await _meseroService.GetTotalVendidoPorMesero(fechaInicio, fechaFin);
                if (resultado == null || !resultado.Any())
                {
                    return NotFound("No se encontraron ventas en el rango de fechas especificado.");
                }
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener el total vendido por mesero: {ex.Message}");
            }
        }
    }


}
