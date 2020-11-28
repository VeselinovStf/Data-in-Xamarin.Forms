using System.Threading.Tasks;

namespace BethanysPieShopStockApp.Services
{
    public interface IDialogService
    {
        Task ShowDialog(string message, string title, string buttonLabel);
    }
}
