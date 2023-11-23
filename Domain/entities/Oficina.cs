using System;
using System.Collections.Generic;

namespace Domain.entities;

public partial class Oficina:BaseEntity
{
    public string CodigoOficina { get; set; } = null!;

    public string Ciudad { get; set; } = null!;

    public string Pais { get; set; } = null!;

    public string? Region { get; set; }

    public string CodigoPostal { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string LineaDireccion1 { get; set; } = null!;

    public string LineaDireccion2 { get; set; } = null!;
}
