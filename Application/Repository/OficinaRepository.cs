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

    public class OficinaRepository : GenericRepository<Oficina>, IOficina
    {
        private readonly JardineriaContext _context;

        public OficinaRepository(JardineriaContext context) : base(context)
        {
            _context = context;
        }
    public async Task<IEnumerable<Oficina>> GetPOFCodeAndCity()
    {
        var listado = await _context.Oficinas
            .Select(oficina => new Oficina { CodigoOficina = oficina.CodigoOficina, Ciudad = oficina.Ciudad })
            .ToListAsync();

        return listado;
    }
        public async Task<IEnumerable<Oficina>> GetCityAndPhoneOfficeSpain()
    {
        var listado = await _context.Oficinas
            .Where(oficina => oficina.Pais == "España")
            .Select(oficina => new Oficina { Ciudad = oficina.Ciudad, Telefono = oficina.Telefono })
            .ToListAsync();

        return listado;
    }
    }
}