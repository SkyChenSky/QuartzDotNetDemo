using Quartz;
using QuartzDotNetDemo.Service;

namespace QuartzDotNetDemo
{
    public abstract class BaseJob : IJob
    {
        protected readonly CommonService CommonService;

        protected BaseJob(CommonService commonService)
        {
            CommonService = commonService;
        }

        public void Execute(IJobExecutionContext context)
        {
            //公共逻辑
            CommonService.Enabled();

            //job逻辑
            ExecuteJob(context);
        }

        public abstract void ExecuteJob(IJobExecutionContext context);
    }
}
