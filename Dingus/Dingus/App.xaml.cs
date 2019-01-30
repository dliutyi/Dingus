using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Dingus.Pages;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Dingus
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new StartUpPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
