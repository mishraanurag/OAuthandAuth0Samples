using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using TestOAuth2NugetPkg;

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

            //AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(new System.Web.Optimization.BundleCollection());
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //RouteCollection routes = new RouteCollection();
            //RouteConfig.RegisterRoutes(routes);

            app.Use(
                    (context, next) =>
                         
                    {                             
                            if (context.Request.Path.HasValue && context.Request.Path.Value.ToUpper().Contains("/HELP"))
                                {                                    
                                     return context.Response.WriteAsync("<Html><Body><H1>This feature is disabled for Authorization Server</H1></Body></Html>");                                                                        
                                }
                        return next.Invoke();
                    });

            /*app.Use((context, next) =>
            {
                PrintCurrentIntegratedPipelineStage(context, "Middleware 1");
                return next.Invoke();
            });
            app.Use((context, next) =>
            {
                PrintCurrentIntegratedPipelineStage(context, "2nd MW");
                return next.Invoke();
            });*/

          /*  app.Run(context =>
            {
                //PrintCurrentIntegratedPipelineStage(context, "3rd MW");
                return context.Response.WriteAsync("Hello world");
            });*/
        }

        //private void PrintCurrentIntegratedPipelineStage(IOwinContext context, string msg)
        //{
        //    var currentIntegratedpipelineStage = HttpContext.Current.CurrentNotification;
        //    context.Get<TextWriter>("host.TraceOutput").WriteLine(
        //        "Current IIS event: " + currentIntegratedpipelineStage
        //        + " Msg: " + msg);
        //}
    }
}
