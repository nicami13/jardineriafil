using System;
using System.Collections.Generic;

namespace Domain.entities;

public partial class Empleado:BaseEntity
{
    public int CodigoEmpleado { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido1 { get; set; } = null!;

    public string? Apellido2 { get; set; }

    public string Extension { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string CodigoOficina { get; set; } = null!;

    public int? CodigoJefe { get; set; }

    public string? Puesto { get; set; }

    public virtual Empleado? CodigoJefeNavigation { get; set; }

    public virtual ICollection<Empleado> InverseCodigoJefeNavigation { get; set; } = new List<Empleado>();
}
