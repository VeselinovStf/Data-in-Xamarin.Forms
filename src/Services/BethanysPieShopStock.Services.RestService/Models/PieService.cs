using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BethanysPieShopStock.Services.RestService.Models
{
    public class PieService : IPieService
    {
        private readonly IPieRepository _pieRepository;

        public PieService(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
        }

        public async Task<Pie> GetPieById(Guid id)
        {
            return await _pieRepository.GetPieById(id);
        }

        public async Task<List<Pie>> GetPies()
        {
            return await _pieRepository.GetAllPies();
        }

        public async Task<Pie> AddPie(Pie pie)
        {
            return await _pieRepository.AddPie(pie);
        }

        public async Task UpdatePie(Pie pie)
        {
            await _pieRepository.UpdatePie(pie);
        }

        public async Task DeletePie(Guid id)
        {
            await _pieRepository.DeletePie(id);
        }
    }
}