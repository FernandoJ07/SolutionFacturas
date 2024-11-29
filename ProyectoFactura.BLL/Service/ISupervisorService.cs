using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoFactura.Models;

namespace ProyectoFactura.BLL.Service
{
    public interface ISupervisorService
    {
        Task<List<Supervisor>> GetAll();
        Task<Supervisor> GetById(object id);
        Task<bool> Add(Supervisor entity);
    }
}
