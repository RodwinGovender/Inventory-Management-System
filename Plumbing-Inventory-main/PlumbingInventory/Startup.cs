using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PlumbingInventory.Startup))]
namespace PlumbingInventory
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
