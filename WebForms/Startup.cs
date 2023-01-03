using Microsoft.Owin.Extensions;
using Owin;


namespace WebForms
{
    public partial class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.UseStageMarker(PipelineStage.Authenticate);

        }

    }
}