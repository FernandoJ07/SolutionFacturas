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
    public class SupervisorRepository : IGenericRepository<Supervisor>
    {

        private readonly ModelContext _dbcontext;

        public SupervisorRepository(ModelContext context)
        {
            _dbcontext = context;
        }

        public async Task<bool> Add(Supervisor supervisor)
        {
            _dbcontext.Supervisors.Add(supervisor);
            await _dbcontext.SaveChangesAsync();
            return true;
        }


        public async Task<List<Supervisor>> GetAll()
        {
            return _dbcontext.Supervisors
                             .Include(f => f.Detallexfacturas)
                             .ToList();
        }

        public async Task<Supervisor> GetById(object id)
        {
            decimal supervisorId = Convert.ToDecimal(id);
            return await _dbcontext.Supervisors
                        .Include(s => s.Detallexfacturas)
                        .FirstOrDefaultAsync(s => s.Idsupervisor == supervisorId);
        }

        public async Task<bool> Update(Supervisor supervisor)
        {
            throw new NotImplementedException();
        }
    }
}
