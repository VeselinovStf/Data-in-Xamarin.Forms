using System;
using System.Collections.Generic;


namespace BethanysPieShopStock.Services.AsmxService.Models
{
    public interface IPieRepository
    {
        IEnumerable<Pie> GetAllPies();
        Pie GetPieById(Guid id);
        void AddPie(Pie pie);
        void UpdatePie(Pie pie);
        void DeletePie(Guid id);
    }
}