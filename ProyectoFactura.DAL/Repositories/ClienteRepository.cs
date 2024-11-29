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
                if (ClienteExists(cliente.Identificacion))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

        }
        private bool ClienteExists(string id)
        {
            return _dbcontext.Clientes.Any(e => e.Identificacion == id);
        }

        public async Task<bool> Delete(object id)
        {
            Cliente cliente = await _dbcontext.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return false;
            }
            _dbcontext.Clientes.Remove(cliente);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<IQueryable<Cliente>> GetAll()
        {
            return _dbcontext.Clientes
                             .Include(c => c.Facturas)
                             .ThenInclude(f => f.Detallexfacturas)
                             .AsQueryable();
        }

        public async Task<Cliente> GetById(object id)
        {
            return await _dbcontext.Clientes
                        .Include(c => c.Facturas)
                            .ThenInclude(f => f.Detallexfacturas)
                        .FirstOrDefaultAsync(c => c.Identificacion == id);
        }

        public async Task<bool> Update(Cliente cliente)
        {
            _dbcontext.Clientes.Update(cliente);
            await _dbcontext.SaveChangesAsync();
            return true;
        }
    }
}
