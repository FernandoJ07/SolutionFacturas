using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFactura.Models.DTOs
{
    public class FacturaDTO
    {
        public decimal Nrofactura { get; set; }
        public string Idcliente { get; set; } = null!;
        public decimal Nromesa { get; set; }
        public decimal Idmesero { get; set; }
        public DateTime Fecha { get; set; }

        public virtual ICollection<Detallexfactura> Detallexfacturas { get; set; } = new List<Detallexfactura>();
    }
}
