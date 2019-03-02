using Xamarin.Forms;

namespace Dingus.Pages
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage() => InitializeComponent();
        protected override bool OnBackButtonPressed() => true;
    }
}