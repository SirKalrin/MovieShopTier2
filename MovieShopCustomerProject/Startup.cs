using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MovieShopCustomerProject.Startup))]
namespace MovieShopCustomerProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
