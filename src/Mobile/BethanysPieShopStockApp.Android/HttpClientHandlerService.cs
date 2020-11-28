using System.Net.Http;
using BethanysPieShopStockApp.Droid;
using BethanysPieShopStockApp.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(HttpClientHandlerService))]
namespace BethanysPieShopStockApp.Droid
{
    public class HttpClientHandlerService : IHttpClientHandlerService
    {
        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }
    }
}