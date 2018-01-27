using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(btfb.Startup))]
namespace btfb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
