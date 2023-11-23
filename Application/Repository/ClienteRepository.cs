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
    public class ClienteRepository : GenericRepository<Cliente>, ICliente
    {
        private readonly JardineriaContext _context;

        public ClienteRepository(JardineriaContext context) : base(context)
        {
            _context = context;
        }


        public async Task<IEnumerable<string>> GetNameSpainCoustumers()
        {
            var result = await _context.Clientes
                .Where(cliente => cliente.Pais == "España")
                .Select(cliente => cliente.NombreCliente)
                .ToListAsync();

            return result;
        }

    }
}