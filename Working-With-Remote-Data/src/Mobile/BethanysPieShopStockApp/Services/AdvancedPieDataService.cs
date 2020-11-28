using Akavache;
using BethanysPieShopStockApp.Models;
using BethanysPieShopStockApp.Utility;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopStockApp.Services
{
    public class AdvancedPieDataService : IPieDataService
    {
        private readonly IBlobCache _cache;
        private readonly IPieRepository _pieRepository;

        public AdvancedPieDataService(IPieRepository pieRepository)
        {
            _cache = BlobCache.LocalMachine;
            _pieRepository = pieRepository;
        }

        public async Task<T> GetFromCache<T>(string cacheName)
        {
            try
            {
                T t = await _cache.GetObject<T>(cacheName);
                return t;
            }
            catch (KeyNotFoundException)
            {
                return default;
            }
        }

        public async Task AddPieAsync(Pie pie)
        {
            await _pieRepository.AddPieAsync(pie);
        }

        public async Task DeletePieAsync(Guid id)
        {
            await _pieRepository.DeletePieAsync(id);
        }

        public async Task<List<Pie>> GetAllPiesAsync(bool force)
        {
            if (force)
            {
                return await GetAndStorePiesAsync();
            }

            var piesFromCache = await GetFromCache<List<Pie>>(CacheNameConstants.AllPies);

            if (piesFromCache != null)//loaded from cache
            {
                return piesFromCache;
            }

            return await GetAndStorePiesAsync();
        }


        private async Task<List<Pie>> GetAndStorePiesAsync()
        {
            var pies = await _pieRepository.GetAllPiesAsync();

            await _cache.InsertObject(CacheNameConstants.AllPies, pies, DateTimeOffset.Now.AddSeconds(10));

            return pies;
        }

        public async Task<Pie> GetPieByIdAsync(Guid id)
        {
            return await _pieRepository.GetPieByIdAsync(id);
        }

        public async Task UpdatePieAsync(Pie pie)
        {
            await _pieRepository.UpdatePieAsync(pie);
        }

        
    }
}
