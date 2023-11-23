using System;
using System.Collections.Generic;
namespace Domain.entities;

public partial class Jefe:BaseEntity
{
    public int CodigoJefe { get; set; }

    public string Nombre { get; set; } = null!;
}
