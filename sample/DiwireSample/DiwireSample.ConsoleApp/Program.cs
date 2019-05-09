using Diwire.Container;
using DiwireSample.MyServices;
using System;

namespace DiwireSample.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new DiwireContainer();

            var myServicesModule = new MyServicesModule();
            myServicesModule.RegisterTypes(container);

            var fooService = container.Resolve<IFooService>();

            Console.WriteLine(fooService.HelloWorldString);
            Console.ReadLine();
        }
    }
}
