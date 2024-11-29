using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProyectoFactura.Models;

public partial class Factura
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal Nrofactura { get; set; }
    public string Idcliente { get; set; } = null!;
    public decimal Nromesa { get; set; }
    public decimal Idmesero { get; set; }
    public DateTime Fecha { get; set; }

    public virtual ICollection<Detallexfactura> Detallexfacturas { get; set; } = new List<Detallexfactura>();
    public virtual Cliente? IdclienteNavigation { get; set; } = null!;
    public virtual Mesero? IdmeseroNavigation { get; set; } = null!;
    public virtual Mesa? NromesaNavigation { get; set; } = null!;
}
