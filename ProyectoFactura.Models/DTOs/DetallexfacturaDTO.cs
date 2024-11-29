using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFactura.Models.DTOs
{
    public class DetallexfacturaDTO
    {
        public int NroFactura { get; set; }
        public int IdDetallexFactura { get; set; }
        public int IdSupervisor { get; set; }
        public required string Plato { get; set; }
        public decimal Valor { get; set; }
    }
}
