using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SNB.Web.Startup))]
namespace SNB.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
