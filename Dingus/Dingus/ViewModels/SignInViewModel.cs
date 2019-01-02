using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Dingus.Models;
using Dingus.Pages;
using Dingus.Services;
using Xamarin.Forms;

namespace Dingus.ViewModels
{
    class SignInViewModel : ViewModelBase
    {
        private bool _isUserNotFound;

        private User _user;
        private List<User> _users;

        public ICommand SignInCommand { protected set; get; }
        public ICommand SignUpCommand { protected set; get; }

        public SignInViewModel()
        {
            InitializeCommands();

            UserServices userService = new UserServices();
            _users = userService.GetUsers();

            _user = new User();
        }

        public void InitializeCommands()
        {
            SignInCommand = new Command(SignInCommandHandler);
            SignUpCommand = new Command(SignUpCommandHandler);
        }

        public async void SignInCommandHandler()
        {
            IsUserNotFound = !_users.Contains(SignInUser);
            if (!IsUserNotFound)
            {
                App.Current.MainPage.Navigation.InsertPageBefore(new DashboardPage(), ((NavigationPage)App.Current.MainPage).RootPage);
                await App.Current.MainPage.Navigation.PopToRootAsync();
            }
        }

        public async void SignUpCommandHandler()
        {
            await App.Current.MainPage.Navigation.PushAsync(new SignUpPage());
        }

        public bool IsUserNotFound
        {
            get { return _isUserNotFound; }
            set { SetProperty(ref _isUserNotFound, value); }
        }

        public User SignInUser
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
