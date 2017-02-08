using empHelper.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace empHelper.Pages
{
    public partial class Login : ContentPage
    {
        IAuthenticator iAuth = DependencyService.Get<IAuthenticator>();

        public Login()
        {
            InitializeComponent();
        }

        async void loginClicked(object sender, EventArgs args)
        {

            try
            {
                var authResult = await iAuth.Authenticate("","","","");

                App.AuthenticationResult = authResult;
                var loginInfo = authResult.UserInfo.GivenName + " " + authResult.UserInfo.FamilyName;

                var userEmail = authResult.UserInfo.DisplayableId;
                var userAccount = userEmail.Split('@');

                await DisplayAlert("Notification", authResult.UserInfo.DisplayableId, "OK");


            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Oops! Something went wrong: {ex.Message}");

                //TODO: 目前有user cancel登入後，會產生錯誤，用catch先欄住
                //if (ex.Message == "User canceled authentication")
                //{
                //}
            }
            finally { }
        }
    }
}
