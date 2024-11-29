using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoFactura.Models;

namespace ProyectoFactura.BLL.Service
{
    public interface IMeseroService
    {
        Task<List<Mesero>> GetAll();
        Task<Mesero> GetById(object id);
        Task<bool> Add(Mesero entity);
        Task<bool> Update(Mesero entity);
        Task<bool> Delete(object id);

        Task<List<object>> GetTotalVendidoPorMesero(DateTime fechaInicio, DateTime fechaFin);
    }
}
