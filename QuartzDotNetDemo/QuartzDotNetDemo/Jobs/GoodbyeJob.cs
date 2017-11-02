using Quartz;
using QuartzDotNetDemo.Service;

namespace QuartzDotNetDemo.Jobs
{
    public class GoodbyeJob : BaseJob
    {
        private readonly GoodByeService _goodByeService;

        public GoodbyeJob(GoodByeService goodByeService, CommonService commonService) : base(commonService)
        {
            _goodByeService = goodByeService;
        }

        public override void ExecuteJob(IJobExecutionContext context)
        {
            _goodByeService.SayGoodBye();
        }
    }
}
