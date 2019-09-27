using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PayTabs.Web.Startup))]
namespace PayTabs.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
