namespace Blasphemous.Framework.Stats.Components;

/// <summary>
/// Interface of classes that can read/write values of specific class instances
/// </summary>
public interface IAccessible<in T> where T : class
{
    /// <summary>
    /// Read the value of given class instance to this IAccessible instance's fields
    /// </summary>
    public abstract void GetValueFrom(T obj);

    /// <summary>
    /// Write the value from this IAccessible instance's fields to the given class instance
    /// </summary>
    public abstract void SetValueTo(T obj);
}