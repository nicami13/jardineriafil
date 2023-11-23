using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.entities;

namespace Domain.Interfaces
{
    public interface IProducto:IGenericRepository<Producto>
    {
       Task<IEnumerable<Producto>> GetProductosConDetallesVacios(); 
    }
}