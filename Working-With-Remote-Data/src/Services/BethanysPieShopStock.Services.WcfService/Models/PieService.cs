using System;
using System.Collections.Generic;

namespace BethanysPieShopStock.Services.WcfService.Models
{
    public class PieService : IPieService
    {
        private readonly IPieRepository _pieRepository;

        public PieService(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
        }

        public void AddPie(Pie pie)
        {
            _pieRepository.AddPie(pie);
        }

        public void DeletePie(Guid id)
        {
            _pieRepository.DeletePie(id);
        }

        public Pie GetPieById(Guid id)
        {
            return _pieRepository.GetPieById(id);
        }

        public IEnumerable<Pie> GetPies()
        {
            return _pieRepository.GetAllPies();
        }

        public void UpdatePie(Pie pie)
        {
            _pieRepository.UpdatePie(pie);
        }
    }
}