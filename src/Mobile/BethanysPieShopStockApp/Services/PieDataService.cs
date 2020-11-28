using BethanysPieShopStockApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BethanysPieShopStockApp.Services
{
    public class PieDataService : IPieDataService
    {
        private IPieRepository _pieRepository;

        public PieDataService(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
        }

        public async Task AddPieAsync(Pie pie)
        {
            await _pieRepository.AddPieAsync(pie);
        }

        public async Task DeletePieAsync(Guid id)
        {
            await _pieRepository.DeletePieAsync(id);
        }

        public async Task<List<Pie>> GetAllPiesAsync()
        {
            return await _pieRepository.GetAllPiesAsync();
        }

        public async Task<Pie> GetPieByIdAsync(Guid id)
        {
            return await _pieRepository.GetPieByIdAsync(id);
        }

        public async Task UpdatePieAsync(Pie pie)
        {
            await _pieRepository.UpdatePieAsync(pie);
        }
    }
}
