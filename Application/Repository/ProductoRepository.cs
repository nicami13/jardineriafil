using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.entities;
using Domain.Interfaces;
using persistense.Data;

namespace Application.Repository
{
    public class ProductoRepository : GenericRepository<Producto>, IProducto
    {
        public ProductoRepository(JardineriaContext context) : base(context)
        {
        }
    }
}