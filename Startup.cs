using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartupAttribute(typeof(aspnet_mvc_razor_app.Startup))]
namespace aspnet_mvc_razor_app
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //// Enable the application to use a cookie to store information for the signed in user
            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            //    LoginPath = new PathString("/Home/Index")
            //});
        }
    }
}