using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BethanysPieShopStock.Services.RestService.Models
{
    public class Pie
    {
        public Guid Id
        {
            get; set;
        }

        public string PieName
        {
            get; set;

        }

        public string Description
        {
            get; set;

        }

        public double Price
        {
            get; set;

        }

        public string ImageUrl
        {
            get; set;

        }

        public bool InStock
        {
            get; set;

        }



    }
}