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
    public class ClienteRepository : IGenericRepository<Cliente>
    {
        private readonly ModelContext _dbcontext;

        public ClienteRepository(ModelContext context)
        {
            _dbcontext = context;
        }


        public async Task<bool> Add(Cliente cliente)
        {
            _dbcontext.Clientes.Add(cliente);
            try
            {
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                if (await ClienteExists(cliente.Identificacion))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }
        private async Task<bool> ClienteExists(string id)
        {
            return await _dbcontext.Clientes.FindAsync(id) != null;
        }


        public async Task<List<Cliente>> GetAll()
        {
            return _dbcontext.Clientes
                             .Include(c => c.Facturas)
                             .ThenInclude(f => f.Detallexfacturas)
                             .ToList();
        }

        public async Task<Cliente> GetById(object id)
        {
            return await _dbcontext.Clientes
                        .Include(c => c.Facturas)
                            .ThenInclude(f => f.Detallexfacturas)
                        .FirstOrDefaultAsync(c => c.Identificacion == id);
        }

    }
}
