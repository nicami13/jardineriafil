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


        public async Task<IEnumerable<Pago>> GetPagos2008Paypal()
        {
            var pagos = await _context.Pagos
                .Where(p => p.FechaPago.Year == 2008 && p.FormaPago == "Paypal")
                .OrderByDescending(p => p.Total)
                .ToListAsync();

            return pagos;
        }
        
    public async Task<IEnumerable<object>> cuentapedido()
        {
            var result = from r in _context.Pedidos
                         group r by r.Estado into m
                         select new { Estado = m.Key, ConPedido = m.Count() };
            return await result.OrderByDescending(hf => hf.ConPedido).ToListAsync();
        }

    }






    }

