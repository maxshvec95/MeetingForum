using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MeetingForum.Startup))]
namespace MeetingForum
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
