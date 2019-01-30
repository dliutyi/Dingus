using System;
using System.Collections.Generic;
using System.Windows.Input;
using Dingus.Helpers;
using Dingus.Models;
using Dingus.Services;
using Xamarin.Forms;

namespace Dingus.ViewModels
{
    public delegate void ExceptionHandler(Exception ex);

    class UserViewModel : ViewModelBase
    {
        private bool _isUserNotFound;

        private User _user;
        private List<User> _users;

        private UserServices UserService { get; set; }

        public ICommand SignInCommand { get; protected set; }
        public ICommand SignUpCommand { get; protected set; }

        public event EventHandler Validated;
        public event ExceptionHandler Exception;

        public UserViewModel()
        {
            InitializeCommands();

            ActiveUser = new User();
            UserService = new UserServices();
        }

        public void InitializeCommands()
        {
            SignInCommand = new Command(SignInCommandHandler);
            SignUpCommand = new Command(SignUpCommandHandler);
        }

        public async void SignInCommandHandler()
        {
            try
            {
                AppSettings.CurrentUser = await UserService.Auth(ActiveUser);
                Validated?.Invoke(this, new EventArgs());
            }
            catch(Exception ex)
            {
                Exception?.Invoke(ex);
            }
        }

        public async void SignUpCommandHandler()
        {
            try
            {
                AppSettings.CurrentUser = await UserService.Register(ActiveUser);
                Validated?.Invoke(this, new EventArgs());
            }
            catch(Exception ex)
            {
                Exception?.Invoke(ex);
            }
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
