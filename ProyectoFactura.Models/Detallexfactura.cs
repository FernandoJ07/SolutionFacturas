using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProyectoFactura.Models;

public partial class Detallexfactura
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal Iddetallexfactura { get; set; }
    public decimal Nrofactura { get; set; }
    public decimal Idsupervisor { get; set; }
    public string Plato { get; set; } = null!;
    public decimal? Valor { get; set; }

    public virtual Supervisor? IdsupervisorNavigation { get; set; } = null!;
    public virtual Factura? NrofacturaNavigation { get; set; } = null!;
}
