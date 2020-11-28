using System;
using System.Collections.Generic;
using System.Linq;

namespace BethanysPieShopStock.Services.AsmxService.Models
{
    public class PieRepository: IPieRepository
    {
        public PieRepository()
        {
            InitializePies();
        }

        private void InitializePies()
        {
            if (_pies == null)
            {
                _pies = new List<Pie>
                {
                    new Pie
                    {
                        Id = Guid.Parse("{70822596-265D-49E3-8CCC-CD996093E601}"),
                        InStock = true,
                        ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/applepiesmall.jpg",
                        PieName = "Apple Pie",
                        Price = 15.95,
                        Description = "Our famous apple pies!"
                    },
                    new Pie
                    {
                        Id = Guid.Parse("{11DB10F5-C461-490F-A7A3-5BA5AF3A58AF}"),
                        InStock = true,
                        ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/blueberrycheesecakesmall.jpg",
                        PieName = "Blueberry cheese cake",
                        Price = 18.95,
                        Description = "You'll love it!"
                    },
                    new Pie
                    {
                        Id = Guid.Parse("{0AC44F1C-84FB-4BCC-8E1F-49FCC8F2A17C}"),
                        InStock = true,
                        ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cheesecakesmall.jpg",
                        PieName = "Cheese cake",
                        Price = 12.95,
                        Description = "Plain cheese cake. Plain pleasure."
                    },

                    new Pie
                    {
                        Id = Guid.Parse("{71A9453F-7A6B-40AC-9D66-E659296EF128}"),
                        InStock = true,
                        ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cherrypiesmall.jpg",
                        PieName = "Cherry pie",
                        Price = 15.95,
                        Description = "A summer classic!"
                    },
                    new Pie
                    {
                        Id = Guid.Parse("{27629374-72AA-40CC-9819-993440356585}"),
                        InStock = true,
                        ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/christmasapplepiesmall.jpg",
                        PieName = "Christmas apple pie",
                        Price = 12.95,
                        Description = "Happy holidays with this pie!"
                    },
                    new Pie
                    {
                        Id = Guid.Parse("{27629374-72AA-40CC-9819-993440356585}"),
                        InStock = true,
                        ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/peachpiesmall.jpg",
                        PieName = "Peach pie",
                        Price = 12.95,
                        Description = "Sweet as peach!"
                    },
                    new Pie
                    {
                        Id = Guid.Parse("{FAA549E2-F940-4A2D-B5FE-93D3A9EDBCE7}"),
                        InStock = true,
                        ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/pumpkinpiesmall.jpg",
                        PieName = "Pumpkin Pie",
                        Price = 12.95,
                        Description = "Our Halloween favorite"
                    },

                    new Pie
                    {
                        Id = Guid.Parse("{CAA2A5D8-99D4-423C-B1F3-1B0E48C483C3}"),
                        InStock = true,
                        ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cranberrypiesmall.jpg",
                        PieName = "Cranberry pie",
                        Price = 12.95,
                        Description = "A Christmas favorite"
                    },
                    new Pie
                    {
                        Id = Guid.Parse("{27629374-72AA-40CC-9819-993440356585}"),
                        InStock = true,
                        ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/rhubarbpiesmall.jpg",
                        PieName = "Rhubarb pie",
                        Price = 12.95,
                        Description = "My God, so sweet!"
                    },
                    new Pie
                    {
                        Id = Guid.Parse("{C956B142-3FC7-4176-9FED-A726A9EB47FA}"),
                        InStock = true,
                        ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrycheesecakesmall.jpg",
                        PieName = "Strawberry cheese cake",
                        Price = 12.95,
                        Description = "You'll love it!"
                    },
                    new Pie
                    {
                        Id = Guid.Parse("{8C9AC766-BC2F-40F0-ACB7-B0AD6157C07B}"),
                        InStock = true,
                        ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrypiesmall.jpg",
                        PieName = "Strawberry pie",
                        Price = 12.95,
                        Description = "Our delicious strawberry pie!"
                    }
                };
            }
        }

        private List<Pie> _pies;

        public IEnumerable<Pie> GetAllPies()
        {
            return _pies;
        }

        public Pie GetPieById(Guid id)
        {
            return _pies.Where(p => p.Id == id).FirstOrDefault(); 
        }

        public void AddPie(Pie pie)
        {
            pie.Id = Guid.NewGuid();
            pie.ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cherrypiesmall.jpg";
            _pies.Add(pie);
        }

        public void UpdatePie(Pie pie)
        {
            var pieToUpdate = _pies.Where(p => p.Id == pie.Id).FirstOrDefault();
            pieToUpdate.PieName = pie.PieName;
            pieToUpdate.Price = pie.Price;
            pieToUpdate.Description = pie.Description;
            pieToUpdate.InStock = pie.InStock;
        }

        public void DeletePie(Guid id)
        {
            var pieToDelete = _pies.Where(p => p.Id == id).FirstOrDefault();
            _pies.Remove(pieToDelete);
        }
    }
}