using Blasphemous.Framework.Stats.Components;
using Blasphemous.Framework.Stats.StatsPatching.EntitiesStats.SubComponents;
using Framework.Managers;
using Gameplay.GameControllers.Penitent;
using Newtonsoft.Json;

namespace Blasphemous.Framework.Stats.StatsPatching.EntitiesStats.PenitentStats;

/// <summary>
/// Stats of TPO
/// </summary>
public class PenitentData : IAccessible_Class<Penitent>, IStatsPatchable
{
    [JsonProperty] public EntityStatsValues_Penitent entityStats = new();
    [JsonProperty] public PlatformCharacterControllerValues_Penitent platformCharacterController = new();

    /// <inheritdoc/>
    public void GetValueFrom(Penitent obj)
    {
        entityStats.GetValueFrom(obj.Stats);
        platformCharacterController.GetValueFrom(obj.PlatformCharacterController);
    }

    /// <inheritdoc/>
    public void SetValueTo(Penitent obj)
    {
        entityStats.SetValueTo(obj.Stats);
        platformCharacterController.SetValueTo(obj.PlatformCharacterController);
    }

    /// <summary>
    /// Checks if Penitent exists
    /// </summary>
    public bool TryGetTargets() => Core.Logic.Penitent != null;


    /// <summary>
    /// Get value from Penitent instance
    /// </summary>
    public void GetValueFromFirstTarget()
    {
        if (TryGetTargets())
            GetValueFrom(Core.Logic.Penitent);
    }

    /// <summary>
    /// Set value to Penitent instance
    /// </summary>
    public void SetValueToAllTargets()
    {
        if (TryGetTargets())
            SetValueTo(Core.Logic.Penitent);
    }
}
