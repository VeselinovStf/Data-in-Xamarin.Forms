using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using BethanysPieShopStockApp.Models;
using BethanysPieShopStockApp.Services;
using BethanysPieShopStockApp.Utility;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BethanysPieShopStockApp.ViewModels
{
    public class PieOverviewViewModel : BaseViewModel
    {
        private ObservableCollection<Pie> _pies;

        private IPieDataService _pieDataService;
        private INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        public ObservableCollection<Pie> Pies
        {
            get => _pies;
            set
            {
                _pies = value;
                OnPropertyChanged("Pies");
            }
        }


        public PieOverviewViewModel(IPieDataService pieDataService, INavigationService navigationService, IDialogService dialogService)
        {
            _pieDataService = pieDataService;
            _navigationService = navigationService;
            _dialogService = dialogService;
            LoadCommand = new Command(OnLoadCommand);
            AddCommand = new Command(OnAddCommand);
            PieSelectedCommand = new Command<Pie>(OnPieSelectedCommand);

            Pies = new ObservableCollection<Pie>();

            MessagingCenter.Subscribe<PieDetailViewModel, Pie>
                (this, MessageNames.PieChangedMessage, OnPieChanged);
        }

        public ICommand LoadCommand { get; }
        public ICommand PieSelectedCommand { get; }
        public ICommand AddCommand { get; }

        public async void OnLoadCommand()
        {
            var networkConnection = Connectivity.NetworkAccess;

            if (networkConnection != NetworkAccess.None)
            {
                try
                {
                    Pies = new ObservableCollection<Pie>(await _pieDataService.GetAllPiesAsync(false));
                }
                catch (System.Exception ex)
                {

                    await _dialogService.ShowDialog("Error accues!", "Error", "Ok");
                }

            }
            else
            {
                await _dialogService.ShowDialog("Connection Error", "No active internet connection", "Ok");
            }


        }

        private void OnPieSelectedCommand(Pie pie)
        {
            _navigationService.NavigateTo("PieDetailView", pie);
        }

        private void OnAddCommand()
        {
            _navigationService.NavigateTo("PieDetailView");
        }

        private async void OnPieChanged(PieDetailViewModel sender, Pie pie)
        {
            Pies = new ObservableCollection<Pie>(await _pieDataService.GetAllPiesAsync(true));
        }
    }
}
