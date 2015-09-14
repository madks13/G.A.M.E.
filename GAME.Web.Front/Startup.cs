using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GAME.Web.Front.Startup))]
namespace GAME.Web.Front
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
