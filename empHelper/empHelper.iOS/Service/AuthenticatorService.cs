using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UIKit;


[assembly: Dependency(typeof(empHelper.iOS.Service.AuthenticatorService))]
namespace empHelper.iOS.Service
{
    public class AuthenticatorService : IAuthenticator
    {

        public async Task<AuthenticationResult> Authenticate(string authority, string resource, string clientId, string returnUri)
        {
            // Clear the cache.
            TokenCache.DefaultShared.Clear();


            var authContext = new AuthenticationContext(authority);
            if (authContext.TokenCache.ReadItems().Any())
                authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);

            var controller = UIApplication.SharedApplication.KeyWindow.RootViewController;

            // ensures that the currently presented viewcontroller is acquired, even a modally presented one
            while (controller.PresentedViewController != null)
            {
                controller = controller.PresentedViewController;
            }

            var uri = new Uri(returnUri);
            var platformParams = new PlatformParameters(controller);

            try
            {
                var authResult = await authContext.AcquireTokenAsync(resource, clientId, uri, platformParams);
                return authResult;
            }
            catch (AdalException ex)
            {
                if (ex.ErrorCode == AdalError.AuthenticationCanceled)
                {
                    return null;
                }
                throw ex;
            }

        }

        public void Logout(string authority)
        {
            var authContext = new AuthenticationContext(authority);
            authContext.TokenCache.Clear();

        }
    }
}
