using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using SalesApp.LocalData;
using SalesApp.ViewModels;
using SalesApp.ViewModels.Base;
using SalesApp.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SalesApp.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly IUserService _userService;

        private List<Page> _backStack
        {
            get
            {
                var mainPage = Application.Current.MainPage as CustomNavigationView;

                if (mainPage != null)
                {
                    return mainPage.Navigation.NavigationStack.ToList();
                }
                else
                {
                    return new List<Page>();
                }

            }
        }



        public ViewModelBase PreviousPageViewModel
        {
            get
            {
                var mainPage = Application.Current.MainPage as CustomNavigationView;
                var viewModel = mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2].BindingContext;
                return viewModel as ViewModelBase;
            }
        }

        public NavigationService(IUserService userService)
        {
            _userService = userService;
        }

        public Task InitializeAsync()
        {
            if (Globals.LoggedInUser == null)
            {
                //Read User token from local storage - LocalApplicationData
                //var result = CheckLocalStoredUser();
                //return result;

                //Read User token from local preferences - LocalApplicationData
                var result = CheckLocalPrederencesStoredUser();
                return result;
            }
            else
            {
                App.Current.MainPage = new MainView();
                return NavigateToAsync<DashboardViewModel>();
            }
        }

        private Task CheckLocalPrederencesStoredUser()
        {
            var token = Preferences.Get(PreferenceKeys.USER_TOKEN, string.Empty);

            if (token.Equals(string.Empty))
            {
                return NavigateToAsync<LoginViewModel>();
            }
            else
            {
                var user = _userService.GetUser(token).Result;
                if (user != null)
                {
                    Globals.LoggedInUser = user;
                    App.Current.MainPage = new MainView();
                    return NavigateToAsync<DashboardViewModel>();
                }
                else
                {
                    return NavigateToAsync<LoginViewModel>();
                }
            }
        }

        private Task CheckLocalStoredUser()
        {
            var storeFilePath = FileNames.TOKEN_FILE_PATH;

            if (File.Exists(storeFilePath))
            {
                string token = File.ReadAllText(storeFilePath);
                var user = _userService.GetUser(token).Result;
                if (user != null)
                {
                    Globals.LoggedInUser = user;
                    App.Current.MainPage = new MainView();
                    return NavigateToAsync<DashboardViewModel>();
                }
                else
                {
                    return NavigateToAsync<LoginViewModel>();
                }

            }
            else
            {
                return NavigateToAsync<LoginViewModel>();
            }
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task RemoveLastFromBackStackAsync()
        {
            var mainPage = Application.Current.MainPage as CustomNavigationView;

            if (mainPage != null)
            {
                mainPage.Navigation.RemovePage(
                    mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2]);
            }

            return Task.FromResult(true);
        }

        public Task RemoveBackStackAsync()
        {
            var mainPage = Application.Current.MainPage as CustomNavigationView;

            if (mainPage != null)
            {
                for (int i = 0; i < mainPage.Navigation.NavigationStack.Count - 1; i++)
                {
                    var page = mainPage.Navigation.NavigationStack[i];
                    mainPage.Navigation.RemovePage(page);
                }
            }

            return Task.FromResult(true);
        }

        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreatePage(viewModelType, parameter);

            if (page is MainView)
            {
                Application.Current.MainPage = page;
            }
            else if (page is LoginView)
            {
                Application.Current.MainPage = new CustomNavigationView(page);
            }
            else if (Application.Current.MainPage is MainView)
            {
                var mainPage = Application.Current.MainPage as MainView;
                var navigationPage = mainPage.Detail as CustomNavigationView;

                if (viewModelType == typeof(SalesManagementViewModel) || viewModelType == typeof(DashboardViewModel) || 
                    viewModelType == typeof(AddressDetailViewModel) || navigationPage == null)
                {
                    navigationPage = new CustomNavigationView(page);
                    mainPage.Detail = navigationPage;
                }
                else
                {
                    await navigationPage.PushAsync(page);
                }

                mainPage.IsPresented = false;
            }
            else
            {

                var navigationPage = Application.Current.MainPage as CustomNavigationView;
                if (navigationPage != null)
                {
                    await navigationPage.PushAsync(page);
                }
                else
                {
                    Application.Current.MainPage = new CustomNavigationView(page);
                }
            }

            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace("Model", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }

        private Page CreatePage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            return page;
        }
    }
}
