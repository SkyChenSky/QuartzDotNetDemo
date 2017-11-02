using Autofac;
using Topshelf;
using Topshelf.Autofac;

namespace QuartzDotNetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(config =>
            {
                config.SetServiceName(JobService.ServiceName);
                config.SetDescription("Quartz.NET的demo");
                config.UseLog4Net();
                config.UseAutofacContainer(JobService.Container);

                config.Service<JobService>(setting =>
                {
                    JobService.InitSchedule(setting);
                    setting.ConstructUsingAutofacContainer();
                    setting.WhenStarted(o => o.Start());
                    setting.WhenStopped(o => o.Stop());
                });
            });
        }
    }
}
