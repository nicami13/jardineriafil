using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.entities;

namespace Domain.Interfaces
{
    public interface IPago : IGenericRepository<Pago>
    {
        Task<IEnumerable<int>> GetClientesConPagosEn2008();
        Task<IEnumerable<string>> GetFormasPagoUnicas();
    }
}