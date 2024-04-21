namespace TomsInjector;

class MyAutocreateClass
{
    private IMyInterface myInterface = (IMyInterface)new object();

    public MyAutocreateClass() {
        
    }

    public void MyMethod() {
        myInterface.HelloWorld();
    }
}