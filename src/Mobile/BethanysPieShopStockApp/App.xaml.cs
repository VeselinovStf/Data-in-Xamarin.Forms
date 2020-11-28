using BethanysPieShopStockApp.Models;
using BethanysPieShopStockApp.Services;
using BethanysPieShopStockApp.Utility;
using BethanysPieShopStockApp.Views;
using Xamarin.Forms;

namespace BethanysPieShopStockApp
{
    public partial class App : Application
    {
        public static NavigationService NavigationService { get; }  = new NavigationService();

        public static IPieDataService PieDataServie { get; set; } = new AdvancedPieDataService(new AdvancedPieRepository());

        public App()
        {
            InitializeComponent();

            NavigationService.Configure(ViewNames.PieOverviewView, typeof(PieOverviewView));
            NavigationService.Configure(ViewNames.PieDetailView, typeof(PieDetailView));

            MainPage = new NavigationPage(new PieOverviewView());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
