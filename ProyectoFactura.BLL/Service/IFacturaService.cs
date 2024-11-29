using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoFactura.Models;
using ProyectoFactura.Models.DTOs;

namespace ProyectoFactura.BLL.Service
{
    public interface IFacturaService
    {
        Task<List<Factura>> GetAll();
        Task<Factura> GetById(object id);
        Task<bool> Add(FacturaDTO entity);

        Task<object> GetProductoMasVendidoEnMes(int anio, int mes);
    }
}
