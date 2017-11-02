using System;

namespace QuartzDotNetDemo.Service
{
    public class GoodByeService : IService
    {
        public void SayGoodBye()
        {
            Console.WriteLine("GoodBye");
        }
    }
}
