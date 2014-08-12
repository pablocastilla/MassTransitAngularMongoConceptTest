using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UIAngular.Startup))]
namespace UIAngular
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();

            ConfigureAuth(app);
        }
    }
}
