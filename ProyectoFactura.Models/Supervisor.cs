using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProyectoFactura.Models;

public partial class Supervisor
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal Idsupervisor { get; set; }
    public string Nombres { get; set; } = null!;
    public string Apellidos { get; set; } = null!;
    public decimal? Edad { get; set; }
    public decimal? Antiguedad { get; set; }

    [JsonIgnore]
    public virtual ICollection<Detallexfactura>? Detallexfacturas { get; set; } = new List<Detallexfactura>();
}
