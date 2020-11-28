using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BethanysPieShopStockApp.Models
{
    public interface IPieRepository
    {
        Task<List<Pie>> GetAllPiesAsync();
        Task AddPieAsync(Pie pie);
        Task UpdatePieAsync(Pie pie);
        Task DeletePieAsync(Guid id);
        Task<Pie> GetPieByIdAsync(Guid id);
    }
}
