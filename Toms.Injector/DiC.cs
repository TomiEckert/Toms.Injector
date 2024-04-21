using System.Reflection;
using Toms.Injector.Attributes;

namespace Toms.Injector;

/// <summary>
/// Represents a Dependency Injection Container of DiServices
/// for very simple dependency injection.
/// </summary>
public class DependencyInjectionContainer
{
    private readonly Dictionary<Type, object> singletonContainer = [];
    private readonly List<Type> transientContainer = [];
    private readonly Dictionary<Type, Type> typeCache = [];

    public DependencyInjectionContainer()
    {

    }

    /// <summary>
    /// Register a DiService for injection.
    /// The service must have a construction with the DepService attribute.
    /// </summary>
    /// <typeparam name="T">The type of the service to register.</typeparam>
    /// <exception cref="ArgumentException"></exception>
    public void RegisterSingleton<T>()
    {
        if (singletonContainer.ContainsKey(typeof(T)))
            throw new ArgumentException("Type is already registered.");
        singletonContainer.Add(typeof(T), CreateInstance(typeof(T)));
        typeCache.Add(typeof(T), typeof(T));
    }

    /// <summary>
    /// Register a DiService for injection.
    /// The service must have a construction with the DepService attribute.
    /// </summary>
    /// <typeparam name="T">The type of the service to register.</typeparam>
    /// <exception cref="ArgumentException"></exception>
    public void RegisterTransient<T>()
    {
        if (transientContainer.Contains(typeof(T)))
            throw new ArgumentException("Type is already registered.");
        transientContainer.Add(typeof(T));
    }

    /// <summary>
    /// Resolves a DiService for a client.
    /// </summary>
    /// <typeparam name="T">The type of the instance to resolve.</typeparam>
    /// <returns>An instance of the specified type.</returns>
    public T Resolve<T>()
    {
        return (T)Resolve(typeof(T));
    }

    /// <summary>
    /// Creates an instance of the specified type.
    /// Tries to resolves all construction parameters
    /// automatically.
    /// </summary>
    /// <typeparam name="T">The type of the instance to create.</typeparam>
    /// <returns>An instance of the specified type.</returns>
    public T CreateInstanceOf<T>()
    {
        return (T)CreateInstance(typeof(T));
    }

    private object Resolve(Type type)
    {
        if (transientContainer.Contains(type))
            return CreateInstance(type);
        if (!TryResolveFromCache(type, out object? instance))
            throw new KeyNotFoundException($"The type {type.Name} is not registered in the dependency injection container.");
        return instance;
    }

    private bool TryResolveFromCache(Type type, out object instance)
    {
        instance = new object();

        if (CanResolve(type))
        {
            instance = singletonContainer[typeCache[type]];
            return true;
        }
        return false;
    }

    private bool CanResolve(Type type)
    {
        if (!typeCache.ContainsKey(type))
        {
            foreach (var singleton in singletonContainer.Keys)
            {
                if (type.IsAssignableFrom(singleton))
                {
                    typeCache.Add(type, singleton);
                    return true;
                }
            }
            return false;
        }
        return true;
    }

    private object CreateInstance(Type type)
    {
        var constructor = type.GetConstructors().FirstOrDefault(x =>
        x.GetCustomAttribute<DiServiceCtorAttribute>() != null)         // Check for attribute
            ?? type.GetConstructors().FirstOrDefault(y =>
            y.GetParameters().Length == 0)                              // Check for 0 parameter ctor
            ?? type.GetConstructors().FirstOrDefault(z =>
            z.GetParameters().All(p => CanResolve(p.ParameterType)))    // Check for resolvable ctor
                ?? throw new ArgumentException("Failed to find a constructor with the DepService attribute or resolvable parameters.");

        var parameters = constructor.GetParameters();
        var parameterInstances = new List<object>();

        foreach (var parameter in parameters)
        {
            parameterInstances.Add(Resolve(parameter.ParameterType));
        }

        return Activator.CreateInstance(type, [.. parameterInstances])
            ?? throw new ArgumentException("Failed to create instance of type " + type.Name);
    }
}