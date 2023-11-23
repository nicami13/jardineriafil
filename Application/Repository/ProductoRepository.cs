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
    public class ProductoRepository : GenericRepository<Producto>, IProducto
    {
        private readonly JardineriaContext _context;

        public ProductoRepository(JardineriaContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Producto>> GetProductosConDetallesVacios()
        {
            var result = await _context.Productos
                .Where(producto => !producto.DetallePedidos.Any())
                .ToListAsync();

            return result;
        }


    }
}