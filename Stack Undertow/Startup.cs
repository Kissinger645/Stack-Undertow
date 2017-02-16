using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Stack_Undertow.Startup))]
namespace Stack_Undertow
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
