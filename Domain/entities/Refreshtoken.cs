using System;
using System.Collections.Generic;

namespace Domain.entities;

public partial class Refreshtoken:BaseEntity
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Token { get; set; } = null!;

    public DateTime Expires { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Revoked { get; set; }
    public bool IsExpired => DateTime.UtcNow >= Expires;

    public virtual User User { get; set; } = null!;
    public bool IsActive => Revoked == null && !IsExpired;
}
