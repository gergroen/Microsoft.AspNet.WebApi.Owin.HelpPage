using Topshelf;

namespace Microsoft.AspNet.WebApi.Owin.OwinSelfHostHelpPage
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<WebApiService>(s =>
                {
                    s.ConstructUsing(name => new WebApiService());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();
                x.SetDescription("OwinSelfHostHelpPage");
                x.SetDisplayName("OwinSelfHostHelpPage");
                x.SetServiceName("OwinSelfHostHelpPage");
            });
        }
    }
}
