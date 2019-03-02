using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Dingus.Helpers;
using Dingus.Models;
using Dingus.Services;

namespace Dingus.ViewModels
{
    class UserViewModel : ViewModelBase
    {
        private bool _isUserNotFound;

        private User _user;
        private List<User> _users;

        private UserServices UserService { get; set; }

        public ICommand NavigateCommand { get; private set; }
        public ICommand SignInCommand { get; private set; }
        public ICommand SignUpCommand { get; private set; }

        public UserViewModel()
        {
            InitializeCommands();

            ActiveUser = new User();
            UserService = new UserServices();

            IsUserNotFound = false;
        }

        private void InitializeCommands()
        {
            NavigateCommand = new Command<string>(NavigateCommandHandler);
            SignInCommand = new Command<string>(SignInCommandHandler);
            SignUpCommand = new Command<string>(SignUpCommandHandler);
        }

        private async void NavigateCommandHandler(string pageName) => await App.MainNavigationService.NavigateTo(pageName, false);

        private async void SignInCommandHandler(string pageName)
        {
            try
            {
                AppSettings.CurrentUser = await UserService.Auth(ActiveUser);
            }
            catch
            {
                IsUserNotFound = true;
                return;
            }

            await App.MainNavigationService.NavigateTo(pageName, false);
        }

        private async void SignUpCommandHandler(string pageName)
        {
            try
            {
                AppSettings.CurrentUser = await UserService.Register(ActiveUser);
            }
            catch
            {
                return;
            }

            await App.MainNavigationService.NavigateTo(pageName, false);
        }

        public bool IsUserNotFound
        {
            get { return _isUserNotFound; }
            set { SetProperty(ref _isUserNotFound, value); }
        }

        public User ActiveUser
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        public List<User> Users
        {
            get { return _users; }
            set { SetProperty(ref _users, value); }
        }
    }
}
