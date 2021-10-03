using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Poshbots.Startup))]
namespace Poshbots
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
