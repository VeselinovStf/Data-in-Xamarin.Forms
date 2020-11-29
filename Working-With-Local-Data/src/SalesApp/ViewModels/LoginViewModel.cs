using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SalesApp.Database;
using SalesApp.LocalData;
using SalesApp.Models;
using SalesApp.Services.Authentication;
using SalesApp.ViewModels.Base;
using SalesApp.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SalesApp.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly ILocalDbService _dbService;
        private IAuthenticationService _authenticationService;
        private string _emailAddress;
        public string EmailAddress
        {
            get => _emailAddress;
            set
            {
                _emailAddress = value;
                RaisePropertyChanged(() => EmailAddress);
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }


        private string _statusMessage;
        public string StatusMessage
        {
            get { return _statusMessage; }
            set
            {
                _statusMessage = value;
                RaisePropertyChanged(() => StatusMessage);
            }
        }

        public ICommand SignInCommand => new Command(async () => await SignInAsync());


        public LoginViewModel(
            ILocalDbService dbService,
            IAuthenticationService authenticationService)
        {
            _dbService = dbService;
            _authenticationService = authenticationService;
        }

        public override async Task InitializeAsync(object navigationData)
        {

            if (navigationData != null && navigationData is string)
            {
                this.StatusMessage = (string)navigationData;
            }

            await base.InitializeAsync(navigationData);
        }

        public async Task SignInAsync()
        {
            this.StatusMessage = null;

            if (String.IsNullOrEmpty(EmailAddress) || String.IsNullOrEmpty(Password))
            {
                this.StatusMessage = "Please enter your username and password";
                return;
            }

            this.IsBusy = true;

            User user = await _authenticationService.AuthenticateAsync(this.EmailAddress, this.Password);

            if (user == null)
            {
                this.IsBusy = false;
                this.StatusMessage = "Invalid username or password";
            }
            else
            {
                Globals.LoggedInUser = user;

                //StoreUserLocal(user.Token);
                //StoreUserLocalPreferencess(user.Token);
                _dbService.CreateUser(user);

                App.Current.MainPage = new MainView();

                await NavigationService.NavigateToAsync<DashboardViewModel>();

                await NavigationService.RemoveLastFromBackStackAsync();
            }
        }

        private void StoreUserLocalPreferencess(string token)
        {
            Preferences.Set(PreferenceKeys.USER_TOKEN, token);
        }

        /// <summary>
        /// Write user token to Local storage - LocalApplicationData
        /// </summary>
        /// <param name="token"></param>
        private void StoreUserLocal(string token)
        {
            var storeFilePath = FileNames.TOKEN_FILE_PATH;

            File.WriteAllText(storeFilePath, token);
        }
    }
}
