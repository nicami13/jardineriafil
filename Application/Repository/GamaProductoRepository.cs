using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.entities;
using Domain.Interfaces;
using persistense.Data;

namespace Application.Repository
{
    public class GamaProductoRepository : GenericRepository<GamaProducto>, IGamaProducto
    {
        public GamaProductoRepository(JardineriaContext context) : base(context)
        {
        }
    }
}