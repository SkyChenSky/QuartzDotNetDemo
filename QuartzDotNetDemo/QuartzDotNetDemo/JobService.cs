using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extras.Quartz;
using log4net;
using Quartz;
using Quartz.Spi;
using QuartzDotNetDemo.Service;
using Toolkit;
using Topshelf.Quartz;
using Topshelf.ServiceConfigurators;

namespace QuartzDotNetDemo
{
    public class JobService
    {
        #region 初始化
        private static readonly ILog Log = LogManager.GetLogger(typeof(JobService));

        private const string JobFile = "JobsConfig.xml";
        private static readonly string JobNamespceFormat;
        public static readonly string ServiceName;
        private static readonly Jobdetail[] JobList;
        public static IContainer Container;

        static JobService()
        {
            var job = JobFile.XmlToObject<JobsConfig>();

            ServiceName = job.Quartz.ServiceName;
            JobNamespceFormat = job.Quartz.Namespace;
            JobList = job.Quartz.JobList.JobDetail;

            Log.Info("Jobs.xml 初始化完毕");

            InitContainer();
        } 
        #endregion

        /// <summary>
        /// 初始化调度任务
        /// </summary>
        /// <param name="svc"></param>
        public static void InitSchedule(ServiceConfigurator<JobService> svc)
        {
            svc.UsingQuartzJobFactory(Container.Resolve<IJobFactory>);

            foreach (var job in JobList)
            {
                svc.ScheduleQuartzJob(q =>
                {
                    q.WithJob(JobBuilder.Create(Type.GetType(string.Format(JobNamespceFormat, job.JobName)))
                        .WithIdentity(job.JobName, ServiceName)
                        .Build);

                    q.AddTrigger(() => TriggerBuilder.Create()
                        .WithCronSchedule(job.Cron)
                        .Build());

                    Log.InfoFormat("任务 {0} 已完成调度设置", string.Format(JobNamespceFormat, job.JobName));
                });
            }

            Log.Info("调度任务 初始化完毕");
        }

        /// <summary>
        /// 初始化容器
        /// </summary>
        private static void InitContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new QuartzAutofacFactoryModule());
            builder.RegisterModule(new QuartzAutofacJobsModule(typeof(JobService).Assembly));
            builder.RegisterType<JobService>().AsSelf();

            var execDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var files = Directory.GetFiles(execDir, "QuartzDotNetDemo.*.dll", SearchOption.TopDirectoryOnly);
            if (files.Length > 0)
            {
                var assemblies = new Assembly[files.Length];
                for (var i = 0; i < files.Length; i++)
                    assemblies[i] = Assembly.LoadFile(files[i]);

                builder.RegisterAssemblyTypes(assemblies)
                    .Where(t => t.GetInterfaces().ToList().Contains(typeof(IService)))
                    .AsSelf()
                    .InstancePerLifetimeScope();
            }

            Container = builder.Build();
            Log.Info("IOC容器 初始化完毕");
        }

        public bool Start()
        {
            Log.Info("服务已启动");
            return true;
        }

        public bool Stop()
        {
            Container.Dispose();
            Log.Info("服务已关闭");
            return false;
        }
    }
}
