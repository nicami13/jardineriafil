using System;
using System.Collections.Generic;

namespace Domain.entities;

public partial class Rol:BaseEntity
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<User> Usuarios { get; set; } = new List<User>();
}
