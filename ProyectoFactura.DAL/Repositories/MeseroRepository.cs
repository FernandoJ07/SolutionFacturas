using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoFactura.DAL.DataContext;
using ProyectoFactura.Models;

namespace ProyectoFactura.DAL.Repositories
{
    public class MeseroRepository : IGenericRepository<Mesero>
    {

        private readonly ModelContext _dbcontext;

        public MeseroRepository(ModelContext context)
        {
            _dbcontext = context;
        }

        public async Task<bool> Add(Mesero mesero)
        {
            _dbcontext.Meseros.Add(mesero);
            try
            {
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                if (MeseroExists(mesero.Idmesero))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }
        private bool MeseroExists(decimal id)
        {
            return _dbcontext.Meseros.Any(e => e.Idmesero == id);
        }

        public async Task<List<Mesero>> GetAll()
        {
            return _dbcontext.Meseros
                             .Include(m => m.Facturas)
                             .ThenInclude(f => f.Detallexfacturas)
                             .ToList();
        }

        public async Task<Mesero> GetById(object id)
        {
            decimal meseroId = Convert.ToDecimal(id);
            return await _dbcontext.Meseros
                         .Include(m => m.Facturas)
                             .ThenInclude(f => f.Detallexfacturas)
                         .FirstOrDefaultAsync(c => c.Idmesero == meseroId);

        }

    }
}
