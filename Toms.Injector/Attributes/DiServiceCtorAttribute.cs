namespace Toms.Injector.Attributes;


/// <summary>
/// Marks the constructor for DIC instance creation system.
/// </summary>
[AttributeUsage(AttributeTargets.Constructor, Inherited = false, AllowMultiple = false)]
sealed class DiServiceCtorAttribute : Attribute
{
    /// <summary>
    /// Marks the constructor for DIC instance creation system.
    /// </summary>
    public DiServiceCtorAttribute()
    {
    }
}