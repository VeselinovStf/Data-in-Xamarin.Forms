using BethanysPieShopStock.Services.WcfService.Models;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace BethanysPieShopStock.Services.WcfService
{
    [ServiceContract]
    public interface IPieWcfService
    {
        [OperationContract]
        List<Pie> GetAllPies();
        [OperationContract] 
        Pie GetPieById(Guid id);
        [OperationContract] 
        void AddPie(Pie pie);
        [OperationContract] 
        void UpdatePie(Pie pie);
        [OperationContract] 
        void DeletePie(Guid id);
    }
}
