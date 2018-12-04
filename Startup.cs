using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tour.Startup))]
namespace Tour
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
