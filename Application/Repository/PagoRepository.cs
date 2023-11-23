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
    public class PagoRepository : GenericRepository<Pago>, IPago
    {
        private readonly JardineriaContext _context;

        public PagoRepository(JardineriaContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<int>> GetClientesConPagosEn2008()
        {
            var result = await _context.Pagos
                .Where(pago => pago.FechaPago >= new DateTime(2008, 1, 1) && pago.FechaPago < new DateTime(2009, 1, 1))
                .Select(pago => pago.CodigoCliente)
                .Distinct()
                .ToListAsync();

            return result;
        }
        public async Task<IEnumerable<string>> GetFormasPagoUnicas()
        {
            var formasPago = await _context.Pagos
                .Select(p => p.FormaPago)
                .Distinct()
                .ToListAsync();

            return formasPago;
        }
    }
}