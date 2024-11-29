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
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> GetClientes()
        {
            try
            {
                var clientes = await _clienteService.GetAll();
                if (clientes == null || !clientes.Any())
                {
                    return NotFound("No se encontraron clientes.");
                }
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(string id)
        {
            var cliente = await _clienteService.GetById(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            if (cliente == null)
            {
                return BadRequest("El cliente no puede ser vacio.");
            }

            try
            {
                var resultado = await _clienteService.Add(cliente);
                if (!resultado)
                {
                    return Conflict("El cliente ya existe.");
                }

                return CreatedAtAction(nameof(GetCliente), new { id = cliente.Identificacion }, cliente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar el cliente: {ex.Message}");
            }
        }

        [HttpGet("clientes-por-consumo")]
        public async Task<ActionResult<List<Cliente>>> GetClientesPorConsumo([FromQuery] decimal valorMinimo, [FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin)
        {
            try
            {
                var clientes = await _clienteService.GetClientesPorConsumo(valorMinimo, fechaInicio, fechaFin);
                if (clientes == null || !clientes.Any())
                {
                    return NotFound("No se encontraron clientes con el consumo especificado en el rango de fechas.");
                }
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener los clientes por consumo: {ex.Message}");
            }
        }
    }
}
