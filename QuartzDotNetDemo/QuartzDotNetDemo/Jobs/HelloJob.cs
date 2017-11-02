using Quartz;
using QuartzDotNetDemo.Service;

namespace QuartzDotNetDemo.Jobs
{
    public class HelloJob : BaseJob
    {
        private readonly HelloService _helloService;

        public HelloJob(HelloService helloService, CommonService commonService) : base(commonService)
        {
            _helloService = helloService;
        }

        public override void ExecuteJob(IJobExecutionContext context)
        {
            _helloService.SayHello();
        }
    }
}
