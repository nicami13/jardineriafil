using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using persistense.Data;

namespace Application.Repository
{
    public class PedidoRepository : GenericRepository<Pedido>, IPedido
    {
        private readonly JardineriaContext _context;

        public PedidoRepository(JardineriaContext context) : base(context)
        {
            _context = context;
        }


        public async Task<IEnumerable<string>> GetPedidoEstadosAsync()
        {
            try
            {
                var result = await _context.Pedidos
                    .Select(pedido => pedido.Estado)
                    .Distinct()
                    .ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                // Manejar errores según tus necesidades
                throw new ApplicationException($"Error al obtener los estados de los pedidos: {ex.Message}");
            }
        }
        public async Task<IEnumerable<Pedido>> GetPedidosNoEntregadosATiempo()
        {
            var pedidosNoEntregados = await _context.Pedidos
                .Where(pedido => pedido.FechaEntrega.HasValue && pedido.FechaEntrega > pedido.FechaEsperada)
                .Select(pedido => new Pedido
                {
                    CodigoPedido = pedido.CodigoPedido,
                    CodigoCliente = pedido.CodigoCliente,
                    FechaEsperada = pedido.FechaEsperada,
                    FechaEntrega = pedido.FechaEntrega.Value
                })
                .ToListAsync();

            return pedidosNoEntregados;
        }
        public async Task<IEnumerable<Pedido>> GetPedidosFechaEntregaDosDiasAntes()
        {
            var pedidos = await _context.Pedidos
                .Where(pedido => pedido.FechaEsperada - pedido.FechaEntrega.Value >= TimeSpan.FromDays(2))
                .Select(pedido => new Pedido
                {
                    CodigoPedido = pedido.CodigoPedido,
                    CodigoCliente = pedido.CodigoCliente,
                    FechaEsperada = pedido.FechaEsperada,
                    FechaEntrega = pedido.FechaEntrega.Value
                })
                .ToListAsync();

            return pedidos;
        }
        public async Task<IEnumerable<Pago>> GetPagos2008Paypal()
        {
            var pagos = await _context.Pagos
                .Where(p => p.FechaPago.Year == 2008 && p.FormaPago == "Paypal")
                .OrderByDescending(p => p.Total)
                .ToListAsync();

            return pagos;
        }
        public async Task<IEnumerable<Pedido>> GetPedidosRechazados2009()
        {
            var pedidosRechazados = await _context.Pedidos
                .Where(p => p.Comentarios != null && p.Comentarios.Contains("rechazado", StringComparison.OrdinalIgnoreCase) && p.FechaPedido.Year == 2009)
                .ToListAsync();

            return pedidosRechazados;
        }
        public async Task<IEnumerable<Pedido>> GetPedidosEntregados2009()
        {
            var pedidosEntregados = await _context.Pedidos
                .Where(p => p.Estado != null && p.Estado.Equals("entregado", StringComparison.OrdinalIgnoreCase) && p.FechaPedido.Year == 2009)
                .ToListAsync();

            return pedidosEntregados;
        }





    }
}