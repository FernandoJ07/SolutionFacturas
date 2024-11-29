﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using ProyectoFactura.DAL.DataContext;
using ProyectoFactura.BLL.Service;
using ProyectoFactura.Models;
using ProyectoFactura.Models.DTOs;

namespace FacturaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private readonly IFacturaService _facturaService;

        public FacturasController(IFacturaService facturaService)
        {
            _facturaService = facturaService;
        }

        //GET: api/Facturas
           [HttpGet]
            public async Task<ActionResult<List<Factura>>> GetFacturas()
        {
            try
            {
                var facturas = await _facturaService.GetAll();
                if (facturas == null || !facturas.Any())
                {
                    return NotFound("No se encontraron facturas.");
                }
                return Ok(facturas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/Facturas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Factura>> GetFactura(decimal id)
        {
            var factura = await _facturaService.GetById(id);

            if (factura == null)
            {
                return NotFound();
            }

            return Ok(factura);
        }

        //    // PUT: api/Facturas/5
        //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //    [HttpPut("{id}")]
        //    public async Task<IActionResult> PutFactura(decimal id, Factura factura)
        //    {
        //        if (id != factura.Nrofactura)
        //        {
        //            return BadRequest();
        //        }

        //        _context.Entry(factura).State = EntityState.Modified;

        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!FacturaExists(id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }

        //        return NoContent();
        //    }

        // POST: api/Facturas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FacturaDTO>> PostFactura(FacturaDTO facturaDto)
        {

            if (facturaDto == null)
            {
                return BadRequest("La factura no puede ser nula.");
            }

            try
            {
                var resultado = await _facturaService.Add(facturaDto);
                if (!resultado)
                {
                    return Conflict("La factura ya existe.");
                }

                return CreatedAtAction(nameof(GetFactura), new { id = facturaDto.Nrofactura}, facturaDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al registrar la factura: {ex.Message}");
            }
        }


        //    //DELETE: api/Facturas/5
        //    [HttpDelete("{id}")]
        //    public async Task<IActionResult> DeleteFactura(decimal id)
        //    {
        //        var factura = await _context.Facturas.FindAsync(id);
        //        if (factura == null)
        //        {
        //            return NotFound();
        //        }

        //        _context.Facturas.Remove(factura);
        //        await _context.SaveChangesAsync();

        //        return NoContent();
        //    }

        //    private bool FacturaExists(decimal id)
        //    {
        //        return _context.Facturas.Any(e => e.Nrofactura == id);
        //    }


        // GET: api/Facturas/producto-mas-vendido
        [HttpGet("producto-mas-vendido")]
        public async Task<ActionResult<object>> GetProductoMasVendidoEnMes([FromQuery] int year, [FromQuery] int month)
        {
            try
            {
                var productoMasVendido = await _facturaService.GetProductoMasVendidoEnMes(year, month);
                return Ok(productoMasVendido);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener el producto más vendido: {ex.Message}");
            }
        }
    }
}