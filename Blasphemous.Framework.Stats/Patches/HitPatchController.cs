using Blasphemous.Framework.Stats.StatsPatching;
using Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents;
using Blasphemous.Framework.Stats.StatsPatching.ItemStats.SubComponents.Prayers;
using Framework.Managers;
using System.Collections.Generic;
using System.Linq;

namespace Blasphemous.Framework.Stats.Patches;

/// <summary>
/// Containing patch data and methods related to hit values.
/// </summary>
internal class HitPatchController
{
    internal HitPatchData PR12 = new()
    {
        baseDamage = 0f,
        attackDamageMultiplier = 0f,
        prayerBonusEfficiency = 0.5f,
        IsActive = AnyActiveModificationOf<PenitentAreaAttackValues>
    };

    internal HitPatchData PR14 = new()
    {
        baseDamage = 0f,
        attackDamageMultiplier = 0f,
        prayerBonusEfficiency = 1f,
        IsActive = AnyActiveModificationOf<PenitentCrawlerOrbsEffectValues>
    };

    /// <summary>
    /// Calculate the final damage of a hit.
    /// </summary>
    /// <param name="baseDamage">base damage before bonuses and multipliers</param>
    /// <param name="finalMultiplier">final damage multiplier after individual multipliers are calculated</param>
    /// <param name="damageBonuses">individual stat bonus and their multiplier (e.g. 20 attack power with 50% of physical damage bonus will be input as {20, 0.5})</param>
    /// <returns>Final calculated damage</returns>
    internal static float CalculateFinalDamageWithBonuses(float baseDamage, float finalMultiplier, params KeyValuePair<float, float>[] damageBonuses)
    {
        float result = baseDamage;
        foreach (KeyValuePair<float, float> kvp in damageBonuses)
        {
            result += kvp.Key * kvp.Value;
        }
        result *= finalMultiplier;
        return result;
    }

    /// <summary>
    /// Calculate the final damage of a hit.
    /// </summary>
    /// <param name="baseDamage">base damage before bonuses and multipliers</param>
    /// <param name="finalMultiplier">final damage multiplier after individual multipliers are calculated</param>
    /// <param name="damageBonuses">individual stat bonus and their multiplier (e.g. 20 attack power with 50% of physical damage bonus will be input as {20, 0.5})</param>
    /// <returns>Final calculated damage</returns>
    internal static float? CalculateFinalDamageWithBonuses(float? baseDamage, float? finalMultiplier, params KeyValuePair<float?, float?>[] damageBonuses)
    {
        float? result = baseDamage;
        foreach (KeyValuePair<float?, float?> kvp in damageBonuses)
        {
            result += kvp.Key * kvp.Value;
        }
        result *= finalMultiplier;
        return result;
    }

    internal static float CalculatePrayerDamageMultiplier(float prayerBonusEfficiency)
    {
        return 1f + (Core.Logic.Penitent.Stats.PrayerStrengthMultiplier.Final - 1f) * prayerBonusEfficiency;
    }

    internal static float? CalculatePrayerDamageMultiplier(float? prayerBonusEfficiency)
    {
        return 1f + (Core.Logic.Penitent.Stats.PrayerStrengthMultiplier.Final - 1f) * prayerBonusEfficiency;
    }

    /// <summary>
    /// Determines if a hit patch should be active. 
    /// It should activate if any active patch contains a modification to the specified child type of <see cref="ObjectEffectValues"/>
    /// </summary>
    public static bool AnyActiveModificationOf<T>()
    {
        return StatsPatchRegister.ItemPatches.Any(
            invItemStatsPatch =>
                invItemStatsPatch.isActive
                && invItemStatsPatch.statsPatches.Any(
                    invItemData =>
                        invItemData.effectAdditions.Any(
                            objEffVal => objEffVal is T)));
    }
}
