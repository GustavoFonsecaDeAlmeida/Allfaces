using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RedeSocial.WEB.Startup))]
namespace RedeSocial.WEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
