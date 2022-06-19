using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ICYL.Web.Startup))]
namespace ICYL.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
