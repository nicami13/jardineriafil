using Domain.entities;

namespace Domain.Interfaces
{
    public interface IPedido : IGenericRepository<Pedido>
    {
        Task<IEnumerable<string>> GetPedidoEstadosAsync();

        Task<IEnumerable<Pago>> GetPagos2008Paypal();
    
    }
}