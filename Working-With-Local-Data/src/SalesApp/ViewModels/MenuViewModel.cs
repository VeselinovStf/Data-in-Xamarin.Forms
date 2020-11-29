using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows.Input;
using SalesApp.LocalData;
using SalesApp.Models;
using SalesApp.Services.Enums;
using SalesApp.ViewModels.Base;
using Xamarin.Essentials;
using Xamarin.Forms;
using MenuItem = SalesApp.Models.MenuItem;

namespace SalesApp.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        public ICommand ItemSelectedCommand => new Command<MenuItem>(SelectMenuItemAsync);
        public ICommand LogoutCommand => new Command(LogoutAsync);


        private ObservableCollection<MenuItem> _menuItems = new ObservableCollection<MenuItem>();
        public ObservableCollection<MenuItem> MenuItems
        {
            get => _menuItems;
            set
            {
                _menuItems = value;
                RaisePropertyChanged(() => MenuItems);
            }
        }


        User _currentUser;
        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                RaisePropertyChanged(() => CurrentUser);
            }
        }
        

        public MenuViewModel()
        {
            CurrentUser = Globals.LoggedInUser;

            InitMenuItems();
        }


        private void InitMenuItems()
        {
            MenuItems.Add(new MenuItem
            {
                Title = "Dashboard",
                MenuItemType = MenuItemType.Dashboard,
                ViewModelType = typeof(DashboardViewModel),
                IsEnabled = true
            });

            MenuItems.Add(new MenuItem
            {
                Title = "Address Management",
                MenuItemType = MenuItemType.SalesManagement,
                ViewModelType = typeof(SalesManagementViewModel),
                IsEnabled = true
            });

        }


        private async void SelectMenuItemAsync(MenuItem item)
        {
            if (item.IsEnabled)
            {
                switch (item.MenuItemType)
                {
                    case MenuItemType.Dashboard:
                        await NavigationService.NavigateToAsync<DashboardViewModel>(null);
                        break;
                    case MenuItemType.SalesManagement:
                        await NavigationService.NavigateToAsync<SalesManagementViewModel>(null);
                        break;

                }
            }
        }

        private async void LogoutAsync()
        {
            Globals.LoggedInUser = null;

            //Removing Local Stored Token on LogOut
            //RemoveLocalStoredToken();

            //Removing Preferences Stored Token on LogOut
            RemovePreferencesStoredToken();
           

            await NavigationService.NavigateToAsync<LoginViewModel>();
            await NavigationService.RemoveBackStackAsync();
        }

        private void RemovePreferencesStoredToken()
        {
            Preferences.Remove(PreferenceKeys.USER_TOKEN);
        }

        private void RemoveLocalStoredToken()
        {
            var storeFilePath = FileNames.TOKEN_FILE_PATH;

            if (File.Exists(storeFilePath))
            {
                File.Delete(storeFilePath);
            }
        }
    }
}
