using BethanysPieShopStockApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BethanysPieShopStockApp.Services
{
    public interface IPieDataService
    {
        Task<List<Pie>> GetAllPiesAsync();
        Task AddPieAsync(Pie pie);
        Task UpdatePieAsync(Pie pie);
        Task DeletePieAsync(Guid id);
        Task<Pie> GetPieByIdAsync(Guid id);
    }
}
