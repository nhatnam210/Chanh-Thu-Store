using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChanhThu_Store.Startup))]
namespace ChanhThu_Store
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
