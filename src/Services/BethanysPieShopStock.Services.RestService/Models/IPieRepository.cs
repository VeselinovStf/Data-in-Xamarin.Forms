using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BethanysPieShopStock.Services.RestService.Models
{
    public interface IPieRepository
    {
        Task<List<Pie>> GetAllPies();
        Task<Pie> GetPieById(Guid id);
        Task<Pie> AddPie(Pie pie);
        Task UpdatePie(Pie pie);
        Task DeletePie(Guid id);
    }
}