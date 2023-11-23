using System;
using System.Collections.Generic;

namespace Domain.entities;

public class User:BaseEntity
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Refreshtoken> Refreshtokens { get; set; } = new List<Refreshtoken>();

    public virtual ICollection<Rol> Rols { get; set; } = new List<Rol>();
}
