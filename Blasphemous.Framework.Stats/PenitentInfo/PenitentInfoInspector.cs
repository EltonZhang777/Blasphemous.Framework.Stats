using Blasphemous.Framework.Stats.Extensions;
using Framework.Managers;
using Gameplay.GameControllers.Penitent;
using Newtonsoft.Json;

namespace Blasphemous.Framework.Stats.PenitentInfo;

internal class PenitentInfoInspector
{
    public static PenitentStats ReadPenitentInfo()
    {
        PenitentStats result = new();
        Penitent penitent = Core.Logic.Penitent;
        result.entityStats = penitent.Stats.ToEntityStatsValues_Penitent();
        result.platformCharacterController = penitent.PlatformCharacterController.ToPlatformCharacterControllerValues_Penitent();
        return result;
    }
}


public class PenitentStats
{
    [JsonProperty] public EntityStatsValues_Penitent entityStats;
    [JsonProperty] public PlatformCharacterControllerValues_Penitent platformCharacterController;
}