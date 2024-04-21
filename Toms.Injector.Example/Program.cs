using Toms.Injector.Example.Classes;
using Toms.Injector;

namespace Toms.Injector.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var dic = new DependencyInjectionContainer();
            
            // Register a service
            dic.RegisterSingleton<MyService>();

            // Resolve a service
            dic.Resolve<IMyInterface>().HelloWorld();
            
            // Resolve a service in the constructor
            dic.CreateInstanceOf<MyClassWithProperty>().MyMethod();
        }
    }
}