using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ProyectoFactura.Models;

public partial class Cliente
{
    public string Identificacion { get; set; } = null!;

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }
    [JsonIgnore]
    public virtual ICollection<Factura>? Facturas { get; set; } = new List<Factura>();
}
