using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudentComment.Startup))]
namespace StudentComment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
