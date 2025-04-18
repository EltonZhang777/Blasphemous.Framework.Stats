using Blasphemous.Framework.Stats.Components;
using Blasphemous.Framework.Stats.Extensions;
using Gameplay.GameControllers.Penitent;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blasphemous.Framework.Stats.PenitentInfo;

/// <summary>
/// Stats of TPO
/// </summary>
public class PenitentData : IAccessible<Penitent>
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
}
