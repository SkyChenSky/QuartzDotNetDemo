using System;

namespace QuartzDotNetDemo.Service
{
    public class CommonService : IService
    {
        public void Enabled()
        {
            Console.WriteLine("Enabled");
        }
    }
}
