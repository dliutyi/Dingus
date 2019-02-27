using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Dingus.Services
{ 
    public delegate void MainPageChangedHandler(NavigationPage page);
    public class NavigationService
    {
        private NavigationPage _mainPage;

        public event MainPageChangedHandler MainPageChangedEvent;
        public void PresentAsMainPage(string pageName)
        {
            if (_mainPage != null)
            {
                _mainPage.PopToRootAsync().Wait();
            }

            Page page = GetPageInstance(pageName);
            NavigationPage navigationPage = GetNavigationPage(page);

            _mainPage = navigationPage;
            MainPageChangedEvent?.Invoke(navigationPage);
        }

        public async Task NavigateTo(string pageName, bool isNavigatable = true)
        {
            Page page = GetPageInstance(pageName);
            if (isNavigatable)
            {
                await _mainPage.PushAsync(GetNavigationPage(page));
            }
            else
            {
                await _mainPage.PushAsync(page);
            }
        }

        public void NavigateToDetail(string pageName)
        {
            Page page = GetPageInstance(pageName);
            MasterDetailPage headPage = (_mainPage.CurrentPage as MasterDetailPage);
            headPage.Detail = GetNavigationPage(page);
        }

        public async Task NavigateBack()
        {
            await _mainPage.PopAsync();
        }

        private NavigationPage GetNavigationPage(Page page)
        {
            NavigationPage navigationPage = (page as NavigationPage);
            if (navigationPage == null)
            {
                navigationPage = new NavigationPage(page);
            }

            return navigationPage;
        }

        private Page GetPageInstance(string pageName)
        {
            string className = string.Format("Dingus.Pages.{0}Page", pageName);
            try
            {
                Type pageType = Type.GetType(className);
                return (Activator.CreateInstance(pageType) as Page);
            }
            catch
            {
                throw new Exception(string.Format("Can not create instance of {0}", className));
            }
        }
    }
}
