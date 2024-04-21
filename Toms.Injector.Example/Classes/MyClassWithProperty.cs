namespace Toms.Injector.Example.Classes;

class MyClassWithProperty
{
    private IMyInterface myService;

    public MyClassWithProperty(IMyInterface myService)
    {
        this.myService = myService;
    }

    public void MyMethod()
    {
        myService.HelloWorld();
    }
}