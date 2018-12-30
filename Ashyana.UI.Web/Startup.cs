using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ashyana.UI.Web.Startup))]
namespace Ashyana.UI.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
