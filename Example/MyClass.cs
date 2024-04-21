using TomsInjector.Attributes;

namespace TomsInjector
{
    class MyClass : IMyInterface
    {
        [DiServiceCtor]
        public MyClass() {}
        public void HelloWorld()
        {
            Console.WriteLine("Hello, World!");
        }
    }
}

