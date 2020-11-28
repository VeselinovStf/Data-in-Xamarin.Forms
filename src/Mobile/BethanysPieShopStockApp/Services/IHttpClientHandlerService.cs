using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace BethanysPieShopStockApp.Services
{
    public interface IHttpClientHandlerService
    {
        HttpClientHandler GetInsecureHandler();
    }
}
