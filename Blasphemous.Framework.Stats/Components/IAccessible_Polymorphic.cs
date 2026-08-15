namespace Blasphemous.Framework.Stats.Components;

public interface IAccessible_Polymorphic
{
    /// <summary>
    /// Read the value of given struct instance to this IAccessible instance's fields
    /// </summary>
    public abstract void GetValueFrom(object obj);

    /// <summary>
    /// Write the value from this IAccessible instance's fields to the given strcut instance
    /// </summary>
    public abstract void SetValueTo(object obj);
}
