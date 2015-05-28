using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eProduct.Startup))]
namespace eProduct
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
