using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoFactura.DAL.Repositories;
using ProyectoFactura.Models;

namespace ProyectoFactura.BLL.Service
{
    public class MesaService : IMesaService
    {

        private readonly IGenericRepository<Mesa> _mesaRepository;

        public MesaService(IGenericRepository<Mesa> mesaRepo)
        {
            _mesaRepository = mesaRepo;
        }
        public async Task<bool> Add(Mesa mesa)
        {
            string mesaId = Convert.ToString(mesa.Nromesa);
            if (string.IsNullOrEmpty(mesaId))
            {
                throw new ArgumentException("La identificacion de la mesa no puede ser nula o vacía.");
            }

            return await _mesaRepository.Add(mesa);
        }

        public async Task<List<Mesa>> GetAll()
        {
            try
            {
                var mesas = await _mesaRepository.GetAll();
                return mesas;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener las mesas", ex);
            }
        }

        public async Task<Mesa> GetById(object id)
        {
            var mesa = await _mesaRepository.GetById(id);
            return mesa == null ? throw new Exception("Mesa no encontrada") : mesa;
        }

    }
}
