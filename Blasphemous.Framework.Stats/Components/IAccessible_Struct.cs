namespace Blasphemous.Framework.Stats.Components;

/// <summary>
/// Interface of structs that can read/write values of specific struct instances
/// </summary>
public interface IAccessible_Struct<T> where T : struct
{
    /// <summary>
    /// Read the value of given struct instance to this IAccessible instance's fields
    /// </summary>
    public abstract void GetValueFrom(T obj);

    /// <summary>
    /// Write the value from this IAccessible instance's fields to the given strcut instance
    /// </summary>
    public abstract void SetValueTo(ref T obj);
}