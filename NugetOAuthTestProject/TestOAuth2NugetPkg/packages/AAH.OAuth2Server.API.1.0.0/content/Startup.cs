using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;
using System.Web.Routing;

[assembly: OwinStartupAttribute(typeof(TestMVCAPI5.Startup))]
namespace TestMVCAPI5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);           
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            
            app.UseWebApi(config);
        }
    }
}
