using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoFactura.DAL.DataContext;
using ProyectoFactura.Models;

namespace ProyectoFactura.DAL.Repositories
{
    public class MesaRepository : IGenericRepository<Mesa>
    {
        private readonly ModelContext _dbcontext;

        public MesaRepository(ModelContext context)
        {
            _dbcontext = context;
        }

        public async Task<bool> Add(Mesa mesa)
        {
            _dbcontext.Mesas.Add(mesa);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(object id)
        {
            Mesa mesa = await _dbcontext.Mesas.FindAsync(id);
            if (mesa == null)
            {
                return false;
            }
            _dbcontext.Mesas.Remove(mesa);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<IQueryable<Mesa>> GetAll()
        {
            return _dbcontext.Mesas
                              .Include(m => m.Facturas)
                                .ThenInclude(f => f.Detallexfacturas)
                              .AsQueryable();
        }

        public async Task<Mesa> GetById(object id)
        {
            decimal Nromesa = Convert.ToDecimal(id);
            return await _dbcontext.Mesas
                         .Include(m => m.Facturas)
                             .ThenInclude(f => f.Detallexfacturas)
                         .FirstOrDefaultAsync(m => m.Nromesa == Nromesa);
        }

        public async Task<bool> Update(Mesa mesa)
        {
            _dbcontext.Mesas.Update(mesa);
            await _dbcontext.SaveChangesAsync();
            return true;
        }
    }
}
