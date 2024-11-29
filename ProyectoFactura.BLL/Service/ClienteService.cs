using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoFactura.DAL.Repositories;
using ProyectoFactura.Models;

namespace ProyectoFactura.BLL.Service
{
    public class ClienteService : IClienteService
    {
        private readonly IGenericRepository<Cliente> _clientRepo;

        public ClienteService(IGenericRepository<Cliente> clientRepo)
        {
            _clientRepo = clientRepo;
        }

        public async Task<bool> Add(Cliente cliente)
        {
            if (string.IsNullOrEmpty(cliente.Identificacion))
            {
                throw new ArgumentException("La identificación del cliente no puede ser nula o vacía.");
            }

            return await _clientRepo.Add(cliente);
        }

        public async Task<List<Cliente>> GetAll()
        {
            try
            {
                var clientes = await _clientRepo.GetAll();
                return clientes;
            }
            catch (Exception ex)
            {
                
                throw new Exception("Error al obtener los clientes", ex);
            }
        }

        public async Task<Cliente> GetById(object id)
        {
            var cliente = await _clientRepo.GetById(id);
            return cliente == null ? throw new Exception("Cliente no encontrado") : cliente;
        }


        public async Task<List<Object>> GetClientesPorConsumo(decimal valorMinimo, DateTime fechaInicio, DateTime fechaFin)
        {
            var clientes = await _clientRepo.GetAll();
            var resultado = clientes
                .Select(c => new
                {
                    Cliente = $"{c.Nombres} {c.Apellidos}",
                    TotalConsumo = c.Facturas
                        .Where(f => f.Fecha.Date >= fechaInicio.Date && f.Fecha.Date <= fechaFin.Date)
                        .SelectMany(f => f.Detallexfacturas)
                        .Sum(df => (decimal?)df.Valor ?? 0) 
                })
                .Where(c => c.TotalConsumo >= valorMinimo)
                .ToList();

            return resultado.Cast<Object>().ToList();
        }
    }
}
