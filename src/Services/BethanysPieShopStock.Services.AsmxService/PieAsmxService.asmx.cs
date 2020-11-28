using BethanysPieShopStock.Services.AsmxService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace BethanysPieShopStock.Services.AsmxService
{
    /// <summary>
    /// Summary description for PieAsmxService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class PieAsmxService : WebService
    {

        private static IPieService _pieService = new PieService(new PieRepository());

        [WebMethod]
        public List<Pie> GetAllPies()
        {
            return _pieService.GetPies().ToList();
        }

        [WebMethod]
        public Pie GetPieById(Guid id)
        {
            return _pieService.GetPieById(id);
        }

        [WebMethod]
        public void AddPie(Pie pie)
        {
            try
            {
                if (pie == null || string.IsNullOrWhiteSpace(pie.PieName))
                {
                    throw new SoapException("Pie name is required", SoapException.ClientFaultCode);
                }

                _pieService.AddPie(pie);
            }
            catch (Exception ex)
            {
                throw new SoapException("Something went wrong", SoapException.ServerFaultCode, ex);
            }
        }

        [WebMethod]
        public void UpdatePie(Pie pie)
        {
            try
            {
                if (pie == null || string.IsNullOrWhiteSpace(pie.PieName))
                    throw new SoapException("Pie name is required", SoapException.ClientFaultCode);

                Pie pieToUpdate = _pieService.GetPieById(pie.Id);
                if (pieToUpdate != null)
                    _pieService.UpdatePie(pie);
                else
                    throw new SoapException("Pie doesn't exist", SoapException.ClientFaultCode);
            }
            catch (Exception ex)
            {
                throw new SoapException("Something went wrong", SoapException.ServerFaultCode, ex);
            }
        }

        [WebMethod]
        public void DeletePie(Guid id)
        {
            try
            {
                Pie pieToDelete = _pieService.GetPieById(id);
                if (pieToDelete != null)
                    _pieService.DeletePie(id);
                else
                    throw new SoapException("Pie doesn't exist", SoapException.ClientFaultCode);
            }
            catch (Exception ex)
            {
                throw new SoapException("Something went wrong", SoapException.ServerFaultCode, ex);
            }
        }
    }
}
