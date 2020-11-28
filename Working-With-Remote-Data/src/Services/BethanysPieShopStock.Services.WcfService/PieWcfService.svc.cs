using BethanysPieShopStock.Services.WcfService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace BethanysPieShopStock.Services.WcfService
{
    public class PieWcfService : IPieWcfService
    {
        private static IPieService _pieService = new PieService(new PieRepository());

        public List<Pie> GetAllPies()
        {
            return _pieService.GetPies().ToList();
        }

        public Pie GetPieById(Guid id)
        {
            return _pieService.GetPieById(id);
        }

        public void AddPie(Pie pie)
        {
            try
            {
                if (pie == null || string.IsNullOrWhiteSpace(pie.PieName))
                {
                    throw new FaultException("Pie name is required");
                }

                _pieService.AddPie(pie);
            }
            catch (Exception ex)
            {
                throw new FaultException("Something went wrong: " + ex.Message);
            }
        }

        public void UpdatePie(Pie pie)
        {
            try
            {
                if (pie == null || string.IsNullOrWhiteSpace(pie.PieName))
                    throw new FaultException("Pie name is required");

                Pie pieToUpdate = _pieService.GetPieById(pie.Id);
                if (pieToUpdate != null)
                    _pieService.UpdatePie(pie);
                else
                    throw new FaultException("Pie doesn't exist");
            }
            catch (Exception ex)
            {
                throw new FaultException("Something went wrong: " + ex.Message);
            }
        }

        public void DeletePie(Guid id)
        {
            try
            {
                Pie pieToDelete = _pieService.GetPieById(id);
                if (pieToDelete != null)
                    _pieService.DeletePie(id);
                else
                    throw new FaultException("Pie doesn't exist");
            }
            catch (Exception ex)
            {
                throw new FaultException("Something went wrong: " + ex.Message);

            }
        }
    }
}
