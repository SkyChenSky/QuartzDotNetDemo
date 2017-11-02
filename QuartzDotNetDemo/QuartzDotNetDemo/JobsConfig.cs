using Newtonsoft.Json;

namespace QuartzDotNetDemo
{
    public class JobsConfig
    {
        public Quartz Quartz { get; set; }
    }

    public class Quartz
    {
        public string ServiceName { get; set; }
        public string Namespace { get; set; }
        public Joblist JobList { get; set; }
    }

    public class Joblist
    {
        public Jobdetail[] JobDetail { get; set; }
    }

    public class Jobdetail
    {
        [JsonProperty("@name")]
        public string JobName { get; set; }

        [JsonProperty("#text")]
        public string Cron { get; set; }
    }
}
