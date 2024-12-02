using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoFactura.Models;

namespace ProyectoFactura.BLL.Service
{
    public interface IDetallesFactura
    {
        Task<List<Detallexfactura>> GetAll();
        Task<Detallexfactura> GetById(object id);
        Task<bool> Add(Detallexfactura entity);

    }
}
