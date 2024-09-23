
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(IPMS.Web.Controllers.ChatHubs.Startup))]
namespace IPMS.Web.Controllers.ChatHubs
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var idProvider = new CustomUserIdProvider();

            GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => idProvider);     

            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }
    }
}