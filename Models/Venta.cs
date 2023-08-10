using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace apiGrupoCoris.Models;

public partial class Venta
{
    public string ?Id { get; set; }

    public string Canal { get; set; } = null!;

    public string Cliente { get; set; } = null!;

    public string Presentacion { get; set; } = null!;

    public string Categoria { get; set; } = null!;

    public string SubCategoria { get; set; } = null!;

    public string Detalle { get; set; } = null!;

    public string Empresa { get; set; } = null!;

    public string Marca { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int Gramaje { get; set; }

    public decimal Precio { get; set; }

    public decimal PrecioxGr { get; set; }

    public DateTime? FechaIngresa { get; set; }
}
