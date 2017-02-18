using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DartaGram.Startup))]
namespace DartaGram
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
