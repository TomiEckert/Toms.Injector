using Toms.Injector;

namespace TomsInjector
{
    class Program
    {
        static void Main(string[] args)
        {
            var dic = new DependencyInjectionContainer();
            dic.RegisterSingleton<MyClass>();
            dic.Resolve<IMyInterface>().HelloWorld();
            dic.CreateInstanceOf<Asd>().Test.HelloWorld();
        }
    }

    class Asd(IMyInterface test)
    {
        public IMyInterface Test { get; set; } = test;
    }
}

