using System;

namespace QuartzDotNetDemo.Service
{
    public class HelloService : IService
    {
        public void SayHello()
        {
            Console.WriteLine("Hello");
        }
    }
}
