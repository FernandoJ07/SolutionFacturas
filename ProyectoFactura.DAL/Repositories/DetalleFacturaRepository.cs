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
    public class DetalleFacturaRepository : IGenericRepository<Detallexfactura>
    {
        private readonly ModelContext _dbcontext;

        public DetalleFacturaRepository(ModelContext context)
        {
            _dbcontext = context;
        }

        public async Task<bool> Add(Detallexfactura detalles)
        {
            _dbcontext.Detallexfacturas.Add(detalles);
            try
            {
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error al agregar el detalle de la factura a la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado al agregar el detalle de la factura.", ex);
            }
        }

        public Task<List<Detallexfactura>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Detallexfactura> GetById(object id)
        {
            throw new NotImplementedException();
        }
    }
}
