using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BethanysPieShopStock.Services.WcfService.Models
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