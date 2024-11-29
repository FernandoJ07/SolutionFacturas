using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoFactura.Models;

namespace ProyectoFactura.BLL.Service
{
    public interface IClienteService
    {
        Task<List<Cliente>> GetAll();
        Task<Cliente> GetById(object id);
        Task<bool> Add(Cliente entity);
        Task<bool> Update(Cliente entity);
        Task<bool> Delete(object id);

        Task<List<Object>> GetClientesPorConsumo(decimal valorMinimo, DateTime fechaInicio, DateTime fechaFin);
    }
}
