using BethanysPieShopStockApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BethanysPieShopStockApp.Models
{
    public class PieRepository : IPieRepository
    {
        private readonly HttpClient _httpClient;

        //local
        private string baseUrl = Device.RuntimePlatform == Device.Android ? "https://10.0.2.2:5001/api/piestock" : "https://localhost:5001/api/piestock";
        
        //remote
        //private const string baseUrl = 
        //    "https://bethanyspieshopstockapprestservice.azurewebsites.net/api/piestock";

        public PieRepository()
        {
            //_httpClient = new HttpClient();
            _httpClient = new HttpClient(DependencyService.Get<IHttpClientHandlerService>().GetInsecureHandler());
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<List<Pie>> GetAllPiesAsync()
        {
            var url = new Uri(baseUrl);

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Pie>>(content);
            }
            return null;
        }

        public async Task<Pie> GetPieByIdAsync(Guid id)
        {
            var url = new Uri($"{baseUrl}/{id}");

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Pie>(content);
            }
            return null;
        }

        public async Task AddPieAsync(Pie pie)
        {
            var url = new Uri(baseUrl);

            var json = JsonConvert.SerializeObject(pie);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await _httpClient.PostAsync(url, content);
        }

        public async Task UpdatePieAsync(Pie pie)
        {
            var url = new Uri(baseUrl);

            var json = JsonConvert.SerializeObject(pie);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await _httpClient.PutAsync(url, content);
        }

        public async Task DeletePieAsync(Guid id)
        {
            var url = new Uri($"{baseUrl}/{id}");
            await _httpClient.DeleteAsync(url);
        }
    }
}
