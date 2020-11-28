using Acr.UserDialogs;
using System.Threading.Tasks;

namespace BethanysPieShopStockApp.Services
{
    public class DialogService : IDialogService
    {
        public Task ShowDialog(string message, string title, string buttonLabel)
        {
            return UserDialogs.Instance.AlertAsync(message, title, buttonLabel);
        }
    }
}
