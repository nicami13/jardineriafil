using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JarApi.Dtos
{
    public class PedidoDto
    {
    public int CodigoPedido { get; set; }

    public DateTime FechaPedido { get; set; }

    public DateTime FechaEsperada { get; set; }

    public DateTime? FechaEntrega { get; set; }

    public string Estado { get; set; } = null!;

    public string? Comentarios { get; set; }

    public int CodigoCliente { get; set; }
    }
}