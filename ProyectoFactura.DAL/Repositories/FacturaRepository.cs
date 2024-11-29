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
    public class FacturaRepository : IGenericRepository<Factura>
    {

        private readonly ModelContext _dbcontext;

        public FacturaRepository(ModelContext context)
        {
            _dbcontext = context;
        }
        public async Task<bool> Add(Factura factura)
        {
            _dbcontext.Facturas.Add(factura);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Factura>> GetAll()
        {
            return _dbcontext.Facturas
                             .Include(f => f.IdclienteNavigation)
                             .Include(f => f.NromesaNavigation)
                             .Include(f => f.IdmeseroNavigation)
                             .Include(f => f.Detallexfacturas)
                                .ThenInclude(d => d.IdsupervisorNavigation)
                             .ToList();
        }

        public async Task<Factura> GetById(object id)
        {
            decimal facturaId = Convert.ToDecimal(id);
            return await _dbcontext.Facturas
                         .Include(f => f.NromesaNavigation)
                         .Include(f => f.Detallexfacturas)
                            .ThenInclude(d => d.IdsupervisorNavigation)
                         .FirstOrDefaultAsync(f => f.Nrofactura == facturaId);
        }
    }
}
