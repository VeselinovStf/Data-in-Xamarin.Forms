using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Services.Protocols;
using BethanysPieShopStockApp.Models;

namespace BethanysPieShopStockApp.Droid
{
    public class PieRepository : IPieRepository
    {
        BethanysPieShopRemoteServices.PieAsmxService _pieAsmxService;
        TaskCompletionSource<bool> getAllPiesRequestComplete  = null;
        TaskCompletionSource<bool> getPieByIdRequestComplete  = null;
        TaskCompletionSource<bool> addPieRequestComplete  = null;
        TaskCompletionSource<bool> deletePieRequestComplete  = null;
        TaskCompletionSource<bool> updatePieRequestComplete  = null;

        public List<Pie> Pies { get; private set; } = new List<Pie>();
        public Pie Pie { get; set; }

        public PieRepository()
        {
            _pieAsmxService = new BethanysPieShopRemoteServices.PieAsmxService
            {
                //Url = "https://bethanyspieshopstockappasmxservice.azurewebsites.net/PieAsmxService.asmx"
                Url = "https://localhost:44304/PieAsmxService.asmx" 
            };

            _pieAsmxService.GetAllPiesCompleted += PieAsmxService_GetAllPiesCompleted;
            _pieAsmxService.GetPieByIdCompleted += PieAsmxService_GetPieByIdCompleted;
            _pieAsmxService.AddPieCompleted += PieAsmxService_AddPieCompleted;
            _pieAsmxService.UpdatePieCompleted += PieAsmxService_UpdatePieCompleted;
            _pieAsmxService.DeletePieCompleted += PieAsmxService_DeletePieCompleted;
        }

        public async Task<List<Pie>> GetAllPiesAsync()
        {
            getAllPiesRequestComplete = new TaskCompletionSource<bool>();
            _pieAsmxService.GetAllPiesAsync();
            await getAllPiesRequestComplete.Task;
            return Pies;
        }

        private void PieAsmxService_GetAllPiesCompleted(object sender, BethanysPieShopRemoteServices.GetAllPiesCompletedEventArgs e)
        {
            try
            {
                //if (getAllPiesRequestComplete != null)
                //{
                    getAllPiesRequestComplete = new TaskCompletionSource<bool>();
                //}
               

                Pies = new List<Pie>();

                foreach (var pie in e.Result)
                {
                    Pies.Add(ConvertToLocalPie(pie));
                }

                getAllPiesRequestComplete?.TrySetResult(true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public async Task<Pie> GetPieByIdAsync(Guid id)
        {
            getPieByIdRequestComplete = new TaskCompletionSource<bool>();
            _pieAsmxService.GetPieByIdAsync(id);
            await getPieByIdRequestComplete.Task;
            return Pie;
        }

        private void PieAsmxService_GetPieByIdCompleted(object sender, BethanysPieShopRemoteServices.GetPieByIdCompletedEventArgs e)
        {
            try
            {
                //if (getPieByIdRequestComplete != null)
                //{
                    getPieByIdRequestComplete = new TaskCompletionSource<bool>();
                //}
                

                Pie = ConvertToLocalPie(e.Result);

                getPieByIdRequestComplete?.TrySetResult(true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public async Task AddPieAsync(Pie pie)
        {
            try
            {
                var remotePie = ConvertToRemotePie(pie);
                addPieRequestComplete = new TaskCompletionSource<bool>();
                _pieAsmxService.AddPieAsync(remotePie);
                await addPieRequestComplete.Task;
            }
            catch (SoapException se)
            {
                System.Diagnostics.Debug.WriteLine(se.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void PieAsmxService_AddPieCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            addPieRequestComplete?.TrySetResult(true);
        }

        public async Task UpdatePieAsync(Pie pie)
        {
            try
            {
                var remotePie = ConvertToRemotePie(pie);
                updatePieRequestComplete = new TaskCompletionSource<bool>();
                _pieAsmxService.UpdatePieAsync(remotePie);
                await updatePieRequestComplete.Task;
            }
            catch (SoapException se)
            {
                System.Diagnostics.Debug.WriteLine(se.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void PieAsmxService_UpdatePieCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            updatePieRequestComplete?.TrySetResult(true);
        }

        public async Task DeletePieAsync(Guid id)
        {
            try
            {
                deletePieRequestComplete = new TaskCompletionSource<bool>();
                _pieAsmxService.DeletePieAsync(id);
                await deletePieRequestComplete.Task;
            }
            catch (SoapException se)
            {
                System.Diagnostics.Debug.WriteLine(se.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void PieAsmxService_DeletePieCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            deletePieRequestComplete?.TrySetResult(true);
        }

        private Pie ConvertToLocalPie(BethanysPieShopRemoteServices.Pie remotePie)
        {
            return new Pie
            {
                Id = remotePie.Id,
                Description = remotePie.Description,
                ImageUrl = remotePie.ImageUrl,
                InStock = remotePie.InStock,
                PieName = remotePie.PieName,
                Price = remotePie.Price
            };
        }

        private BethanysPieShopRemoteServices.Pie ConvertToRemotePie(BethanysPieShopStockApp.Models.Pie localPie)
        {
            return new BethanysPieShopRemoteServices.Pie
            {
                Id = localPie.Id,
                Description = localPie.Description,
                ImageUrl = localPie.ImageUrl,
                InStock = localPie.InStock,
                PieName = localPie.PieName,
                Price = localPie.Price
            };
        }
    }
}