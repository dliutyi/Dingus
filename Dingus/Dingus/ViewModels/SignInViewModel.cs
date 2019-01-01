using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Dingus.Models;
using Dingus.Services;
using Xamarin.Forms;

namespace Dingus.ViewModels
{
    class SignInViewModel : ViewModelBase
    {
        private List<User> _users;

        public ICommand SignInCommand { protected set; get; }
        public ICommand SignUpCommand { protected set; get; }

        public SignInViewModel()
        {
            InitializeCommands();

            UserServices userService = new UserServices();
            _users = userService.GetUsers();
        }

        public void InitializeCommands()
        {
            SignInCommand = new Command(() => { App.Current.MainPage.DisplayAlert("Notification", "SignIn command", "OK"); });
            SignUpCommand = new Command(() => { App.Current.MainPage.DisplayAlert("Notification", "SignUp command", "OK"); });
        }

        public List<User> Users
        {
            get { return _users; }
            set { SetProperty(ref _users, value); }
        }
    }

}
