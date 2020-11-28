using BethanysPieShopStockApp.Models;
using BethanysPieShopStockApp.Services;
using BethanysPieShopStockApp.Utility;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace BethanysPieShopStockApp.ViewModels
{
    public class PieDetailViewModel : BaseViewModel
    {
        private Pie _selectedPie;

        private IPieDataService _pieDataService;
        private INavigationService _navigationService;

        public PieDetailViewModel(IPieDataService pieDataService, INavigationService navigationService)
        {
            _pieDataService = pieDataService;
            _navigationService = navigationService;

            SelectedPie = new Pie();
            SaveCommand = new Command(OnSaveCommand);
            DeleteCommand = new Command(OnDeleteCommand);
        }

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

        private async void OnSaveCommand()
        {
            if (SelectedPie.Id == Guid.Empty)
                await _pieDataService.AddPieAsync(SelectedPie);
            else
               await _pieDataService.UpdatePieAsync(SelectedPie);

            MessagingCenter.Send(this, MessageNames.PieChangedMessage, SelectedPie);
            _navigationService.GoBack();
        }

        private async void OnDeleteCommand()
        {
            if (SelectedPie.Id != Guid.Empty)//can't delete if we're adding a new one
                await _pieDataService.DeletePieAsync(SelectedPie.Id);
        }

        public Pie SelectedPie
        {
            get { return _selectedPie; }
            set
            {
                _selectedPie = value;
                OnPropertyChanged(nameof(SelectedPie));
            }
        }

        public override void Initialize(object parameter)
        {
            if (parameter == null)
                SelectedPie = new Pie();
            else
                SelectedPie = parameter as Pie;
        }
    }
}
