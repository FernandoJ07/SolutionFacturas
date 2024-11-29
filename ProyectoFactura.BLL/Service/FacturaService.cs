using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoFactura.DAL.Repositories;
using ProyectoFactura.Models;
using ProyectoFactura.Models.DTOs;

namespace ProyectoFactura.BLL.Service
{
    public class FacturaService : IFacturaService
    {

        private readonly IGenericRepository<Factura> _facturaRepository;

        public FacturaService(IGenericRepository<Factura> facturaRepo)
        {
            _facturaRepository = facturaRepo;
        }
        public async Task<bool> Add(FacturaDTO facturaDto)
        {
            var factura = new Factura
            {
                Nrofactura = facturaDto.Nrofactura,
                Idcliente = facturaDto.Idcliente,
                Nromesa = facturaDto.Nromesa,
                Idmesero = facturaDto.Idmesero,
                Fecha = facturaDto.Fecha,
                Detallexfacturas = facturaDto.Detallexfacturas.Select(d => new Detallexfactura
                {
                    Nrofactura = d.Nrofactura,
                    Iddetallexfactura = d.Iddetallexfactura,
                    Idsupervisor = d.Idsupervisor,
                    Plato = d.Plato,
                    Valor = d.Valor
                }).ToList()
            };
            return await _facturaRepository.Add(factura);
        }

        public async Task<List<Factura>> GetAll()
        {
            try
            {
                var facturas = await _facturaRepository.GetAll();
                return facturas;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener las facturas", ex);
            }
        }

        public async Task<Factura> GetById(object id)
        {
            var factura = await _facturaRepository.GetById(id);
            return factura == null ? throw new Exception("Factura no encontrada") : factura;
        }

        
        public async Task<object> GetProductoMasVendidoEnMes(int anio, int mes)
        {
            var facturas = await _facturaRepository.GetAll();
            var detalles = facturas
                .Where(f => f.Fecha.Year == anio && f.Fecha.Month == mes)
                .SelectMany(f => f.Detallexfacturas);

            var productoMasVendido = detalles
                .GroupBy(d => d.Plato)
                .Select(g => new
                {
                    Plato = g.Key,
                    CantidadVendida = g.Count(),
                    TotalFacturado = g.Sum(d => d.Valor ?? 0)
                })
                .OrderByDescending(p => p.CantidadVendida)
                .FirstOrDefault();

            return productoMasVendido ?? throw new Exception("No se encontraron ventas en el mes especificado.");
        }


    }
}
