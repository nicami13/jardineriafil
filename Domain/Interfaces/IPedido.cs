using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.entities;

namespace Domain.Interfaces
{
    public interface IPedido : IGenericRepository<Pedido>
    {
        Task<IEnumerable<string>> GetPedidoEstadosAsync();
        Task<IEnumerable<Pedido>> GetPedidosNoEntregadosATiempo();
        Task<IEnumerable<Pedido>> GetPedidosFechaEntregaDosDiasAntes();
        Task<IEnumerable<Pago>> GetPagos2008Paypal();
        Task<IEnumerable<Pedido>> GetPedidosRechazados2009();
        Task<IEnumerable<Pedido>> GetPedidosEntregados2009();
    }
}