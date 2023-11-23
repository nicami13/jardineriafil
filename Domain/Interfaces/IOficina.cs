using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.entities;

namespace Domain.Interfaces
{
    public interface IOficina:IGenericRepository<Oficina>
    {
        Task<IEnumerable<Oficina>> GetPOFCodeAndCity();
        Task<IEnumerable<Oficina>>GetCityAndPhoneOfficeSpain();
    }
}