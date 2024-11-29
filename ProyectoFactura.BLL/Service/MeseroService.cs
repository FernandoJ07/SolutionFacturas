using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoFactura.DAL.Repositories;
using ProyectoFactura.Models;

namespace ProyectoFactura.BLL.Service
{
    public class MeseroService : IMeseroService
    {
        private readonly IGenericRepository<Mesero> _meseroRepo;

        public MeseroService(IGenericRepository<Mesero> meseroRepo)
        {
            _meseroRepo = meseroRepo;
        }

        public async Task<bool> Add(Mesero mesero)
        {
            string meseroId = Convert.ToString(mesero.Idmesero);
            if (string.IsNullOrEmpty(meseroId))
            {
                throw new ArgumentException("La identificación del mesero no puede ser nula o vacía.");
            }

            return await _meseroRepo.Add(mesero);
        }

        public async Task<List<Mesero>> GetAll()
        {
            try
            {
                var meseros = await _meseroRepo.GetAll();
                return meseros;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener los meseros", ex);
            }
        }

        public async Task<Mesero> GetById(object id)
        {
            var mesero = await _meseroRepo.GetById(id);
            return mesero == null ? throw new Exception("Mesero no encontrado") : mesero;
        }

        public async Task<List<object>> GetTotalVendidoPorMesero(DateTime fechaInicio, DateTime fechaFin)
        {
            var meseros = await _meseroRepo.GetAll();
            var resultado = meseros
                .Select(m => new
                {
                    Mesero = $"{m.Nombres} {m.Apellidos}",
                    TotalVendido = m.Facturas
                        .Where(f => f.Fecha.Date >= fechaInicio.Date && f.Fecha.Date <= fechaFin.Date)
                        .SelectMany(f => f.Detallexfacturas)
                        .Sum(df => (decimal?)df.Valor ?? 0)
                })
                .ToList();

            return resultado.Cast<object>().ToList();
        }
    }
}
