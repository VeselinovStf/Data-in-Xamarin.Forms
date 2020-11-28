using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BethanysPieShopStock.Services.RestService.Models
{
    public interface IPieService
    {
        Task<List<Pie>> GetPies();
        Task<Pie> GetPieById(Guid id);
        Task<Pie> AddPie(Pie pie);
        Task UpdatePie(Pie pie);
        Task DeletePie(Guid id);
    }
}