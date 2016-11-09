using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MovieShopWepApp.Startup))]
namespace MovieShopWepApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
