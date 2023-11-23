using System;
using System.Collections.Generic;

namespace Domain.entities;

public partial class Pedido:BaseEntity
{
    public int CodigoPedido { get; set; }

    public DateTime FechaPedido { get; set; }

    public DateTime FechaEsperada { get; set; }

    public DateTime? FechaEntrega { get; set; }

    public string Estado { get; set; } = null!;

    public string? Comentarios { get; set; }

    public int CodigoCliente { get; set; }

    public virtual Cliente CodigoClienteNavigation { get; set; } = null!;

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();
}
