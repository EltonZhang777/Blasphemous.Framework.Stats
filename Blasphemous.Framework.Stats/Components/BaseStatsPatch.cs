using Blasphemous.ModdingAPI;
using Framework.Managers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Blasphemous.Framework.Stats.Components;

/// <summary>
/// Base class of a stats patch that can be patched to modify vanilla stats
/// </summary>
public abstract class BaseStatsPatch
{
    /// <summary>
    /// Name of the patch
    /// </summary>
    public string name;

    /// <summary>
    /// ID of the mod that registered the patch. Handled by <see cref="ModServiceProvider"/>
    /// </summary>
    [JsonIgnore]
    public string parentModId;

    /// <summary>
    /// The <see cref="ActiveType"/> of the patch
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public ActiveType activeType;

    /// <summary>
    /// Patch activates if the flag is true when <see cref="activeType"/> is <see cref="ActiveType.OnFlag"/>
    /// </summary>
    public string activeFlag;

    [JsonIgnore]
    internal bool isActive = false;

    /// <summary>
    /// Conditions of when the patch should be active
    /// </summary>
    public enum ActiveType
    {
        /// <summary>
        /// Active at all times
        /// </summary>
        Unconditional,

        /// <summary>
        /// Active only when a certain flag is true
        /// </summary>
        OnFlag,

        /// <summary>
        /// Manually manipulated by code or debug console
        /// </summary>
        Manually
    }

    /// <summary>
    /// Update whether the patch should be active based on its activate conditions
    /// </summary>
    public virtual void UpdateActive()
    {
        switch (activeType)
        {
            case ActiveType.Unconditional:
                isActive = true;
                break;
            case ActiveType.OnFlag:
                isActive = Core.Events.GetFlag(activeFlag);
                break;
            case ActiveType.Manually:
                break;
            default:
                break;
        }
    }
}
