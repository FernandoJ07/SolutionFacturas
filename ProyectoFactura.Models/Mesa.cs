using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ProyectoFactura.Models;

public partial class Mesa
{
    public decimal Nromesa { get; set; }

    public string? Nombre { get; set; }

    public string? Reservada { get; set; }

    public decimal? Puestos { get; set; }
    [JsonIgnore]
    public virtual ICollection<Factura>? Facturas { get; set; } = new List<Factura>();
}
