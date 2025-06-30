namespace Blasphemous.Framework.Stats.Components;

public interface IStatsPatchable
{
    /// <summary>
    /// Find targets and store them in the derived class's internal storage. 
    /// Returns false if no targets are found.
    /// </summary>
    public abstract bool TryGetTargets();

    /// <summary>
    /// Get value from first target
    /// </summary>
    public abstract void GetValueFromFirstTarget();

    /// <summary>
    /// Set value to all matching targets
    /// </summary>
    public abstract void SetValueToAllTargets();
}
