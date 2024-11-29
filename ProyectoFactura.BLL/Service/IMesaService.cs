using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoFactura.Models;

namespace ProyectoFactura.BLL.Service
{
    public interface IMesaService
    {
        Task<List<Mesa>> GetAll();
        Task<Mesa> GetById(object id);
        Task<bool> Add(Mesa entity);
    }
}
