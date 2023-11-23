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
    public class EmpleadoRepository : GenericRepository<Empleado>, IEmpleado
    {
        private readonly JardineriaContext _context;

        public EmpleadoRepository(JardineriaContext context) : base(context)
        {
            _context = context;
        }
public async Task<IEnumerable<Empleado>> GetEmployedTO7()
{
    var result = await _context.Empleados
        .Where(empleado => empleado.CodigoJefe == 7)
        .Select(empleado => new Empleado
        {
            Nombre = empleado.Nombre,
            Apellido1 = empleado.Apellido1,
            Apellido2 = empleado.Apellido2,
            Email = empleado.Email
        })
        .ToListAsync();

    return result;
}

    }
}