# Tom's Injector

A very simple dependency injection container.

## Getting Started

To use Tom's Injector in your project, you'll need to include the `Toms.Injector.dll` in your project's build. 
Or you can run the following command to install the nuget package:
```
PM> NuGet\Install-Package Toms.Injector
```

## Usage
Register your service with the container using the `RegisterSingleton` or `RegisterTransient` method.

```
dic.RegisterSingleton<MyService>();
```

Resolve your service from the container using the `Resolve` method.
```
dic.Resolve<IMyInterface>().HelloWorld();
```

Create an instance of a class that has a constructor parameter of the registered service type.
```
dic.CreateInstanceOf<MyClassWithProperty>().MyMethod();
```

## Contributing

If you'd like to contribute to the project, please refer to the repository at `https://github.com/TomiEckert/Toms.Injector`. All contributions are welcome!

## License

Please refer to the repository for licensing information.