namespace Toms.Injector.Example.Classes
{
    class MyService : IMyInterface
    {
        public MyService() { }
        public void HelloWorld()
        {
            Console.WriteLine("Hello, World!");
        }
    }
}

