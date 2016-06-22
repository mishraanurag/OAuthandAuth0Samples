//using Microsoft.AspNet.Identity;
using Microsoft.Owin;
//using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;

namespace TestMVCAPI5
{
    public partial class Startup
    {

        public void ConfigureAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),                
                //AccessTokenExpireTimeSpan = TimeSpan.FromSeconds(60),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
                Provider = new SimpleAuthorizationServerProvider(),                
            };
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            app.UseOAuthAuthorizationServer(oAuthServerOptions);            
        }
    }
}