using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCWEF.Startup))]
namespace MVCWEF
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
