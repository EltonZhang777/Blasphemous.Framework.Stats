using Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents;

namespace Blasphemous.Framework.Stats.Patches;

/// <summary>
/// Containing hit data of a specific patch towards a specific hit.
/// </summary>
internal class HitPatchData
{
    public static float? baseDamage;
    public static float? prayerBonusEfficiency;
    public static HitValues hitValues;
}
