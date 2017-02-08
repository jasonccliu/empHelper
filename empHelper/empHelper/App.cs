using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace empHelper
{
    public class App : Application
    {
        public static AuthenticationResult AuthenticationResult = null;

        public App()
        {
            var loginPage = new Pages.Login();
            MainPage = new NavigationPage(loginPage);
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
